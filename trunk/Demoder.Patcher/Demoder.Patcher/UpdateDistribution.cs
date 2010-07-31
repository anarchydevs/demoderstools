/*
MIT Licence
Copyright (c) 2010 Demoder <demoder@flw.nu> (project: https://sourceforge.net/projects/demoderstools/)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Threading;
using Demoder.Common;
using Demoder.Patcher.DataClasses;
using Demoder.Patcher.Events;

namespace Demoder.Patcher
{
	public class UpdateDistribution
	{
		#region events
		public event DownloadStatusReportEventHandler eventDownloadStatusReport;
		#endregion


		private Distribution _localDistribution;
		private Dictionary<string, DistributionIndex.File_entry> _localFiles = new Dictionary<string, DistributionIndex.File_entry>();
		Dictionary<string, DataClasses.BinFileSlice> localBinFileSlices = new Dictionary<string, Demoder.Patcher.DataClasses.BinFileSlice>();
		private Distribution _remoteDistribution;
		private List<string> _downloadMirrors;

		private DirectoryInfo _TemporaryDirectory = null;

		/// <summary>
		/// Download manager
		/// </summary>
		private DownloadManager _downloadManager = null;
        private FileInfo _tmpbinfile = null;

		private bool _success = true;
		public bool Success { 
			get { 
				return this._success; 
			} 
		}

		/// <summary>
		/// List of files which needs to be downloaded
		/// </summary>
		private Dictionary<string,DistributionIndex.File_entry> _missingFiles = new Dictionary<string,DistributionIndex.File_entry>();
		/// <summary>
		/// List of slices which need to be downloaded
		/// </summary>
		private Dictionary<string, DataClasses.BinFileSlice> _missingSlices = new Dictionary<string,Demoder.Patcher.DataClasses.BinFileSlice>();
		/// <summary>
		/// Binfiles needing to be updated
		/// </summary>
		private Dictionary<string, DataClasses.BinFile> _missingBinFiles = new Dictionary<string, DataClasses.BinFile>();
		private long _sizeOf_MissingFiles = 0;

		private DirectoryInfo _localBasePath;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="LocalBasePath"></param>
		/// <param name="RemoteDistribution"></param>
		public UpdateDistribution(DirectoryInfo LocalBasePath, Distribution RemoteDistribution, List<string> DownloadMirrors)
		{
			this._downloadMirrors = DownloadMirrors;
			this._localBasePath = LocalBasePath;
			//"Index" remote distribution
            this._remoteDistribution = RemoteDistribution;
		}

		public bool Start (DirectoryInfo TemporaryDirectory) {
			this._TemporaryDirectory = TemporaryDirectory;
			if (!this._TemporaryDirectory.Exists)
				this._TemporaryDirectory.Create();
            this._tmpbinfile = new FileInfo(this._TemporaryDirectory.FullName + Path.DirectorySeparatorChar + "update.bin");
			//Index local distribution
			this.indexLocalDirectories(this._localBasePath, this._remoteDistribution.DistType);

			/*	Compare local and remote distributions
			 *	Go through each directory in the remote distribution, trying to find it in the local distribution and compare.
			 *		Generate a list of files which needs to be updated.
			 *	If a file needs to be updated:
			 *		check if we have another file with the same MD5 & SHA1 hashes in the local distribution before downloading.
			 *			Downloaded files should be stored in a temporary archive.
			 *		if it's a binfile: decompile the local binfile(s) before downloading missing slices.
			 *			Downloaded slices should be periodically flushed into update.bin, so that the already downloaded slices are not lost if the application stops before completion.
			 */
			this.compareDistributions();

			#region Download missing files & slices
			int missingFiles = (this._missingFiles.Count + this._missingSlices.Count);
			if (missingFiles > 0)
			{
				int numfetches = 0;
				do
				{
					this.prepareDownloadQueue();
					this.downloadMissingFiles();
					numfetches++;
					missingFiles = (this._missingFiles.Count + this._missingSlices.Count);
				} while ((missingFiles > 0) && numfetches < 5);
				if (missingFiles > 0) throw new Exception("Failed to fetch the nessecary files.");
			}
			#endregion
			#region Create a temporary directory containing the complete target distribution
			
			//Write the distribution to the temporary directory.
			foreach (DistributionIndex.Dir dir in this._remoteDistribution.Directories)
			{
				DirectoryInfo dstdir = new DirectoryInfo(this._TemporaryDirectory.FullName +
					Path.DirectorySeparatorChar +
					this._remoteDistribution.DistType.ToString() +
					Path.DirectorySeparatorChar +
					this._remoteDistribution.DistributionName +
					Path.DirectorySeparatorChar +
					dir.name);
				this.exportdirectory(dstdir , dir);
			}
			#endregion
			Console.WriteLine("done");
			return this._success;
		}

		private void indexLocalDirectories(DirectoryInfo LocalBasePath, Distribution.DistributionType DistType)
		{
			#region Index the local distribution
			List<string> localdirectories = new List<string>();
			foreach (DistributionIndex.Dir dir in this._remoteDistribution.Directories)
			{
				if (Distribution.IsBannedDirectory(this._remoteDistribution.DistType, dir.name)) continue;
				localdirectories.Add(string.Format("{1}{0}{2}",
				Path.DirectorySeparatorChar,
				Distribution.GetRelativeBaseDir(DistType, LocalBasePath.FullName),
				dir));
			}
			this._localDistribution = new Distribution(localdirectories, DistType);
			foreach (DistributionIndex.Dir dir in this._localDistribution.Directories)
			{
				List<DistributionIndex.File_entry> files = dir.GetFiles();
				foreach (DistributionIndex.File_entry fe in files)
					if (!this._localFiles.ContainsKey(fe.MD5))
						this._localFiles.Add(fe.MD5, fe);
			}
			#endregion
		}

		private void compareDistributions()
		{
			#region Compare distributions
			foreach (DistributionIndex.Dir dir in this._remoteDistribution.Directories)
			{
				if (Distribution.IsBannedDirectory(this._remoteDistribution.DistType, dir.name)) continue;
				List<DistributionIndex.File_entry> files = dir.GetFiles(); //Get ALL files
				foreach (DistributionIndex.File_entry fe in files)
				{
					if (!this._localFiles.ContainsKey(fe.MD5))
					{
						if (fe.Filetype == DistributionIndex.File_entry.FileType.file)
						{
							if (!this._missingFiles.ContainsKey(fe.MD5))
							{
								this._missingFiles.Add(fe.MD5, fe);
								this._sizeOf_MissingFiles += fe.Size;
							}
						}
						else if (fe.Filetype == DistributionIndex.File_entry.FileType.bin)
						{
							if (!this._missingBinFiles.ContainsKey(fe.MD5))
							{
								string[] m = new string[this._downloadMirrors.Count];
								this._downloadMirrors.CopyTo(m);
								List<string> mirrors = new List<string>(m);
								Random rnd = new Random();
								DataClasses.BinFile bf = null;
								//Make sure we try every mirror before saying "no can do"
								do
								{
									int mirrornum = rnd.Next(0, mirrors.Count - 1);
									string mirror = mirrors[mirrornum];
									mirrors.RemoveAt(mirrornum);

									bf = Xml.Deserialize<DataClasses.BinFile>(
									new Uri(mirror + "/" + "bin_" + fe.MD5 + ".xml"));
								} while (bf == null && mirrors.Count > 0);

								if (bf == null) throw new Exception("Could not retrieve BinStructure for " + fe.Name + " (md5: " + fe.MD5 + ")");
								this._missingBinFiles.Add(fe.MD5, bf);
							}
						}
					}
				}
			}
			//Have list of missing files & bin files.

			//Generate list of missing slices
			if (this._missingBinFiles.Count > 0)
			{
				this._localDistribution.DecompileBinFiles();
                //Start of decompiling the temporary download file
                if (this._tmpbinfile.Exists)
                {
                    BinDecompiler bd = new BinDecompiler(this._tmpbinfile.FullName);
                    DataClasses.BinFile bf = bd.Decompile();
                    foreach (DataClasses.BinFileSlice binSlice in bf.BinFileSlices)
                        if (!this.localBinFileSlices.ContainsKey(binSlice.MD5))
                        {
                            this.localBinFileSlices.Add(binSlice.MD5, binSlice);
                        }
                }
                //End of decompiling the temporary download files
				if (true)
				{
					List<DataClasses.BinFile> lbf = this._localDistribution.GetBinFiles(true);
					foreach (DataClasses.BinFile bf in lbf)
					{
						foreach (DataClasses.BinFileSlice binSlice in bf.BinFileSlices)
							if (!this.localBinFileSlices.ContainsKey(binSlice.MD5))
							{
								this.localBinFileSlices.Add(binSlice.MD5, binSlice);
							}
					}
				}

				//Now that we have a dictionary containing all the binfile slices... lets check which ones are missing.
                if (this._missingBinFiles.Count > 0)
                {
                    foreach (KeyValuePair<string, DataClasses.BinFile> kvp in this._missingBinFiles)
                    {
                        foreach (DataClasses.BinFileSlice bfs in kvp.Value.BinFileSlices)
                        {
                            if (!this.localBinFileSlices.ContainsKey(bfs.MD5))
                                if (!this._missingSlices.ContainsKey(bfs.MD5))
                                {
                                    this._missingSlices.Add(bfs.MD5, bfs);
                                    this._sizeOf_MissingFiles += bfs.Size;
                                }
                        }
                    }
                }
			}
			//Have list of missing slices.
			#endregion
		}

		private void prepareDownloadQueue()
		{
			this._downloadManager = new DownloadManager(15, 30);
			int mirrornum = 0;
			//Add files to the queue
			foreach (KeyValuePair<string,DistributionIndex.File_entry> kvp in this._missingFiles)
			{
				string mirror = this._downloadMirrors[mirrornum];
				string relativepath = string.Format("{0}/{1}.file",
					this._remoteDistribution.DistType.ToString(),
					kvp.Value.MD5);

				DownloadManager_Tag tag = new DownloadManager_Tag(mirror, relativepath, DownloadType.File);
				this._downloadManager.Enqueue(tag , new Uri(mirror+"/"+relativepath));
				mirrornum++;
				if (mirrornum >= this._downloadMirrors.Count) mirrornum = 0;
			}
			//Add slices to the queue
			foreach (KeyValuePair<string, DataClasses.BinFileSlice> kvp in this._missingSlices)
			{
				string mirror = this._downloadMirrors[mirrornum];
				string relativepath = string.Format("binslices/{0}/{1}/{2}.slice",
					kvp.Value.MD5.Substring(0,1),
					kvp.Value.MD5.Substring(0,2),
					kvp.Value.MD5);

				DownloadManager_Tag tag = new DownloadManager_Tag(mirror, relativepath, DownloadType.Slice);
				this._downloadManager.Enqueue(tag, new Uri(mirror + "/" + relativepath));
				mirrornum++;
				if (mirrornum >= this._downloadMirrors.Count) mirrornum = 0;
			}
		}
		private void downloadMissingFiles()
		{
			this._downloadManager.Start();
			List<KeyValuePair<object, byte[]>> downloadedfiles = new List<KeyValuePair<object, byte[]>>();
			DateTime start = DateTime.Now;
			Int64 totalbytes = 0;

            FileStream fs = null;
            try
            {
                fs = this._tmpbinfile.OpenWrite();
            }
            catch (Exception ex) { }

            if (!this._tmpbinfile.Exists)
                this._tmpbinfile.Create();
			Dictionary<string,List<string>> faultymirrors = new Dictionary<string,List<string>>();
			while (this._downloadManager.Active)
			{
				Thread.Sleep(1000);
				if (this._downloadManager.NumDownloadedFiles >= 50)
				{
					//Console.WriteLine("Download info:");
					//Console.WriteLine("Queue: {0}", this._downloadManager.QueueLength);
					Dictionary<object, byte[]> dict = this._downloadManager.RetrieveDownloadedFiles;

                    foreach (KeyValuePair<object, byte[]> kvp in dict)
                    {
                        DownloadManager_Tag tag = (DownloadManager_Tag)kvp.Key;
                        if (tag.DownloadType == DownloadType.Slice)
                            if (fs != null)
                                fs.Write(kvp.Value, 0, kvp.Value.Length);
                        totalbytes += kvp.Value.Length;
                        downloadedfiles.Add(new KeyValuePair<object, byte[]>(kvp.Key, kvp.Value));
                    }
					//Report status
					if (this.eventDownloadStatusReport != null)
					{
						lock (this.eventDownloadStatusReport)
						{
							DownloadStatusReportEventArgs arg = new DownloadStatusReportEventArgs((uint)downloadedfiles.Count, (uint)this._downloadManager.QueueLength, (uint)totalbytes, (uint)(this._sizeOf_MissingFiles - totalbytes), (TimeSpan)(DateTime.Now - start));
							this.eventDownloadStatusReport(arg);
						}
					}
				}
                
				if (this._downloadManager.NumProtocolErrors > 0)
				{
					KeyValuePair<object, Uri>[] errors = this._downloadManager.ProtocolErrors;
					foreach (KeyValuePair<object, Uri> kvp in errors)
					{
						DownloadManager_Tag tag = (DownloadManager_Tag)kvp.Key;
						//Register/update faulty mirrors for this uri
						if (!faultymirrors.ContainsKey(tag.RelativePath))
							faultymirrors.Add(tag.RelativePath, new List<string>());
						faultymirrors[tag.RelativePath].Add(tag.Mirror);
						//Add to a non-faulty mirror
						foreach (string mirror in this._downloadMirrors)
						{
							if (!faultymirrors[tag.RelativePath].Contains(mirror))
							{
								tag.Mirror = mirror;
								this._downloadManager.Enqueue(tag, new Uri(tag.Mirror + "/" + tag.RelativePath));
								break;
							}
							throw new Exception("One or more files are not available on any the mirrors. Aborting.", new Exception("triggering event: "+tag.Mirror+"/"+tag.RelativePath));
						}

					}
				}
			}

            if (fs != null)
                fs.Close();

			if (true)
			{
				/*
				FileStream fs = null;
				try
				{
					fs = tmpbinfile.OpenWrite();
				}
				catch { }*/
				foreach (KeyValuePair<object, byte[]> kvp in this._downloadManager.RetrieveDownloadedFiles)
				{
					/*
					if (fs != null)
						fs.Write(kvp.Value, (int)fs.Length - 1, kvp.Value.Length);*/
					downloadedfiles.Add(new KeyValuePair<object, byte[]>(kvp.Key, kvp.Value));
				}
			}

			foreach (KeyValuePair<object, byte[]> kvp in downloadedfiles)
			{
				DownloadManager_Tag tag = (DownloadManager_Tag)kvp.Key;
				if (tag.DownloadType == DownloadType.Slice)
				{
					DataClasses.BinFileSlice bfs = new DataClasses.BinFileSlice(kvp.Value);
					this.localBinFileSlices.Add(bfs.MD5, bfs);
					this._missingSlices.Remove(bfs.MD5);
				}
				else if (tag.DownloadType == DownloadType.File)
				{
					DistributionIndex.File_entry fe = new DistributionIndex.File_entry(kvp.Value);
					this._localFiles.Add(fe.MD5, fe);
					this._missingFiles.Remove(fe.MD5);
				}
			}
		}

		private void exportdirectory(DirectoryInfo ExportTarget, DistributionIndex.Dir Directory)
		{
			if (!ExportTarget.Exists)
				ExportTarget.Create();
			foreach (DistributionIndex.File_entry file in Directory.files)
			{
				if (file.Filetype == DistributionIndex.File_entry.FileType.bin)
				{
					if (this._missingBinFiles.ContainsKey(file.MD5))
					{
						BinAssembler ba = new BinAssembler(this._missingBinFiles[file.MD5], this.localBinFileSlices);
						file.Bytes = ba.Assemble().ToArray();
					}
				}
				else if (file.Filetype == DistributionIndex.File_entry.FileType.file)
				{
					if (this._localFiles.ContainsKey(file.MD5))
						file.Bytes = this._localFiles[file.MD5].Bytes;
				}
				if (file.Bytes != null)
					File.WriteAllBytes(ExportTarget.FullName + Path.DirectorySeparatorChar + file.Name,
						file.Bytes);
				else if (this._missingBinFiles.ContainsKey(file.MD5) && file.Bytes == null)
					this._success = false;
			}
			foreach (DistributionIndex.Dir dir in Directory.directories)
			{
				this.exportdirectory(new DirectoryInfo(ExportTarget.FullName + Path.DirectorySeparatorChar + dir.name), dir);
			}
		}







		public class DownloadManager_Tag
		{
			public DownloadManager_Tag(string Mirror, string RelativePath, DownloadType DownloadType)
			{
				this.RelativePath = RelativePath;
				this.Mirror = Mirror;
				this.DownloadType = DownloadType;
			}
			public string RelativePath;
			public string Mirror;
			public DownloadType DownloadType;
		}
		public enum DownloadType
		{
			File,
			Slice
		}

	}
}
