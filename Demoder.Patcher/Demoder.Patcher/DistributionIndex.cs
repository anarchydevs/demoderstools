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
using System.Xml.Serialization;
using Demoder.Common;
using Demoder.Patcher.DataClasses;
namespace Demoder.Patcher
{
	public class DistributionIndex
	{
		public class Dir
		{
			#region members
			[XmlAttribute("name")]
			public string name;
			[XmlElement("file")]
			public List<File_entry> files = new List<File_entry>();
			[XmlElement("directory")]
			public List<Dir> directories = new List<Dir>();
			[XmlIgnore]
			public List<BinFile> BinFiles = new List<BinFile>();
			#endregion

			#region constructors
			public Dir()
			{
			}

			public Dir(string path) : this(path, false) { }
			public Dir(string path, bool KeepFilesInMemory)
			{
				DirectoryInfo di = new DirectoryInfo(path);
				this.name = di.Name;
				directories = new List<Dir>();
                if (Directory.Exists(path))
                {
                    foreach (string file in Directory.GetFiles(path))
                    {
                        FileInfo fi = new FileInfo(file);
                        if (new List<string>(new string[] { ".uvga", ".bin" }).Contains(fi.Extension.ToLower()))
                        {
                            this.files.Add(new File_entry(file, KeepFilesInMemory, File_entry.FileType.bin));
                        }
                        else
                        {
                            this.files.Add(new File_entry(file, KeepFilesInMemory, File_entry.FileType.file));
                        }
                    }
                    foreach (string directory in Directory.GetDirectories(path))
                        this.directories.Add(new Dir(directory, KeepFilesInMemory));
                }
			}
			#endregion

			#region Operator overrides
			public static bool operator ==(Dir dir1, Dir dir2)
			{
				if (dir1.name != dir2.name) return false;
				//Compare files.
				foreach (File_entry file in dir1.files)
					if (!dir2.files.Contains(file)) return false;
				foreach (File_entry file in dir2.files)
					if (!dir1.files.Contains(file)) return false;

				//Compare directories
				if (dir1.directories.Count != dir2.directories.Count) return false;
				if (dir1.directories.Count != 0)
				{
					bool found = false;
					foreach (Dir d1 in dir1.directories)
					{
						foreach (Dir d2 in dir2.directories)
						{
							if (d1 == d2)
							{
								found = true;
								break;
							}
						}
						if (found) break;
					}
					if (!found) return false;
					found = false;
					foreach (Dir d2 in dir2.directories)
					{
						foreach (Dir d1 in dir1.directories)
						{
							if (d1 == d2)
							{
								found = true;
								break;
							}
						}
						if (found) break;
					}
					if (!found) return false;
				}
				return true;
			}

			public static bool operator !=(Dir dir1, Dir dir2)
			{
				return dir1 == dir2 ? false : true;
			}
			/// <summary>
			/// To compare two distributions.
			/// </summary>
			/// <param name="dir1">Distribution we want</param>
			/// <param name="dir2">Distribution we have</param>
			/// <returns></returns>
			public static Dir operator -(Dir dir1, Dir dir2)
			{
				//Remove directories.
				foreach (Dir d2 in dir2.directories)
				{
					dir1.directories.Remove(d2);
				}
				//Remove files.
				foreach (File_entry f2 in dir2.files)
				{
					dir1.files.Remove(f2);
				}
				return dir1;
			}

			#endregion

			#region default overrides
			public override bool Equals(object obj)
			{
				try
				{
					Dir compare = (Dir)obj;
					if (compare == this) return true;
					else return false;
				}
				catch
				{
					return false;
				}
			}

			public override string ToString()
			{
				return this.name;
			}
			#endregion

			#region methods
			/// <summary>
			/// Decompile all binfiles within this directory tree
			/// </summary>
			public void DecompileBinFiles()
			{
				//decompile first-level binfiles
				foreach (File_entry fe in this.files)
				{
					if (fe.Filetype == File_entry.FileType.bin)
					{
						BinDecompiler bd = new BinDecompiler(fe.Bytes);
						fe.Bytes = null;
						BinFile bf = bd.Decompile();
						bf.Name = fe.Name;
						this.BinFiles.Add(bf);
					}
				}
				//Then decompile binfiles further into the tree
				foreach (Dir dir in this.directories)
					dir.DecompileBinFiles();
			}

			/// <summary>
			/// Get a list of all binfiles within this directory tree
			/// </summary>
			/// <param name="RemoveEntry">If set to true, binfile will be removed from this instance</param>
			/// <returns></returns>
			public List<BinFile> GetBinFiles(bool RemoveEntry)
			{
				List<BinFile> lbf = new List<BinFile>();
				//This level
				foreach (BinFile bf in this.BinFiles)
					lbf.Add(bf);
				if (RemoveEntry)
					this.BinFiles = null;

				//Deeper levels
				foreach (Dir dir in this.directories)
				{
					List<BinFile> tmplbf = dir.GetBinFiles(RemoveEntry);
					foreach (BinFile bf in tmplbf)
					{
						if (!lbf.Contains(bf))
							lbf.Add(bf);
					}
				}
				return lbf;
			}

			public List<File_entry> GetFiles()
			{
				List<File_entry> fes = this.files;
				foreach (Dir dir in this.directories)
					fes.AddRange(dir.GetFiles());
				return fes;
			}

			public string[] ToArray()
			{
				List<string> filenames = new List<string>();
				foreach (Dir dir in this.directories)
					foreach (string str in dir.ToArray())
						filenames.Add(dir.name + str);
				foreach (File_entry file in this.files)
					filenames.Add(file.Name);
				return filenames.ToArray();
			}

			public void Export(string BaseDir, string dstfolder)
			{
				if (!System.IO.Directory.Exists(dstfolder)) System.IO.Directory.CreateDirectory(dstfolder);
				//All the files
				foreach (File_entry file in this.files)
				{
					if (file.Bytes != null)
					{
						string filename = string.Format("{1}{0}{2}.file",
							Path.DirectorySeparatorChar,
							dstfolder,
							file.MD5);
						if (!File.Exists(filename))
						{
							System.IO.File.WriteAllBytes(filename, file.Bytes);
						}
					}
				}
				//Binfiles
				foreach (BinFile bf in this.BinFiles)
				{
					//Make sure directories exist.
					string BinSliceExportDir = BaseDir + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "binslices";
					if (!Directory.Exists(BinSliceExportDir)) Directory.CreateDirectory(BinSliceExportDir);
					//Export the xml
					Xml.Serialize<BinFile>(new FileInfo(String.Format("{0}{1}bin_{2}.xml",
						BaseDir,
						Path.DirectorySeparatorChar,
						bf.MD5)), bf, false);
					//Export the slices
					foreach (DataClasses.BinFileSlice bfs in bf.BinFileSlices)
					{
						string sliceDir = BinSliceExportDir + Path.DirectorySeparatorChar + bfs.MD5.Substring(0, 1) + Path.DirectorySeparatorChar + bfs.MD5.Substring(0, 2);
						if (!Directory.Exists(sliceDir)) Directory.CreateDirectory(sliceDir);
						File.WriteAllBytes(String.Format("{0}{1}{2}.slice",
							sliceDir,
							Path.DirectorySeparatorChar,
							bfs.MD5), bfs.Bytes);
					}
				}

				//All the directories
				foreach (Dir dir in this.directories)
					dir.Export(BaseDir, dstfolder);
			}

			//Ensure no harmfull paths are provided.
			public void Sanitize()
			{
				this.name = this.Sanitize(this.name);

				foreach (Dir d in this.directories)
					d.Sanitize();
				foreach (File_entry f in this.files)
					f.Name = this.Sanitize(f.Name);
			}
			private string Sanitize(string str)
			{
				str = str.Replace("..", "");
				str = str.Replace("/", "");
				str = str.Replace("\\", "");
				str = str.Replace("\0", "");
				return str;
			}
			#endregion
		}


		public class File_entry
		{
			#region members
			[XmlAttribute("name")]
			/// <summary>
			/// Filename
			/// </summary>
			public string Name;

			[XmlAttribute("md5")]
			/// <summary>
			/// Files MD5 checksum
			/// </summary>
			public string MD5;

			[XmlAttribute("size")]
			/// <summary>
			/// Filesize in bytes
			/// </summary>
			public long Size;

			[XmlAttribute("type")]
			public FileType Filetype;

			[XmlIgnore]
			public byte[] Bytes
			{
				set
				{
					this._bytes = value;
					this.MD5 = GenerateHash.md5(this._bytes);
					this.Size = this._bytes.Length;
				}
				get
				{
					return this._bytes;
				}
			}
			[XmlIgnore]
			private byte[] _bytes = null;
			#endregion

			#region constructors
			public File_entry() { }

			public File_entry(string path, bool LoadFileToMemory, FileType filetype)
			{
				FileInfo fi = new FileInfo(path);
				this.Name = fi.Name;
				this.Size = fi.Length;
				this.Filetype = filetype;
				byte[] bytes = System.IO.File.ReadAllBytes(path);
				if (LoadFileToMemory) this.Bytes = bytes;
				
				
			}

			/// <summary>
			/// Add a file based on byte array
			/// </summary>
			/// <param name="bytes"></param>
			public File_entry(byte[] bytes)
			{

				this.MD5 = GenerateHash.md5(bytes);
				this.Bytes = bytes;
				this.Size = bytes.Length;
				this.Name = "noname";
				this.Filetype = FileType.file;
			}

			#endregion

			#region operator overrides
			public static bool operator ==(File_entry file1, File_entry file2)
			{
				if (file1.MD5 != file2.MD5) return false;
				if (file1.Name != file2.Name) return false;
				if (file1.Size != file2.Size) return false;
				return true;
			}
			public static bool operator !=(File_entry file1, File_entry file2)
			{
				return file1 == file2 ? false : true;
			}

			#endregion

			#region default overides
			public override bool Equals(object obj)
			{
				try
				{
					File_entry compare = (File_entry)obj;
					if (compare == this) return true;
					else return false;
				}
				catch
				{
					return false;
				}
			}

			public override int GetHashCode()
			{
				//TODO: Test that this works.
				return Convert.ToInt32(this.MD5);
			}

			public override string ToString()
			{
				return this.Name;
			}

			public enum FileType
			{
				file,
				bin
			}
			#endregion
		}
	}
}
