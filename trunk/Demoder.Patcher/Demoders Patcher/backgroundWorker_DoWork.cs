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
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using Demoder.Common;
using Demoder.Patcher;
using Demoders_Patcher.DataClasses;
using Demoder.Patcher.Events;

namespace Demoders_Patcher
{
	public class backgroundWorker_DoWork
	{
		#region members
		BackgroundWorker bw = null;
		#endregion
		public backgroundWorker_DoWork(BackgroundWorker bw)
		{
			this.bw = bw;
		}

		#region RunUpdate
		public bool RunUpdate(List<Uri> uris)
		{
			try
			{
				Demoder.Patcher.DataClasses.PatchServer patchserver = null;
				for (int i = 0; i < uris.Count; i++)
					patchserver = Xml.Deserialize<Demoder.Patcher.DataClasses.PatchServer>(uris[i]);
				PatchStatus patchStatus = Program.PatcherConfig.GetPatchStatus(patchserver.GUID);

				if (patchStatus != null && patchserver.Version == patchStatus.Version)
				{
					this.bw.ReportProgress(100, "Version match.");
					return true;
				}
				DoPatch dp = new DoPatch(patchserver);
				dp.eventDownloadStatusReport += new DownloadStatusReportEventHandler(dp_eventDownloadStatusReport);
				this.bw.ReportProgress(0, "Comparing distributions");
				if (dp.PatchDistributions(Program.PatcherConfig.AnarchyOnlinePath))
				{
					this.bw.ReportProgress(0, "Installing updated map");
					dp.InstallPatchedDistributions(Program.PatcherConfig.AnarchyOnlinePath);
					Program.PatcherConfig.SetPatchStatus(patchserver.GUID, patchserver.Version);
					return true;
				}
			}
			catch(Exception ex)
			{
				this.bw.ReportProgress(0, ex.Message);
			}
			return false;
		}
		private void dp_eventDownloadStatusReport(DownloadStatusReportEventArgs e)
		{
			string totalbytes = "";
			if (e.TotalBytes > 0)
				totalbytes = string.Format("of {0}MiB ",
					Math.Round((double)e.TotalBytes / (double)1024 / (double)1024, 2));


			this.bw.ReportProgress(e.PercentDone,
				String.Format("Downloading: {0}% ({1}MiB {3} @ {2}KiB/s)",
				e.PercentDone, //0
				Math.Round((double)e.DownloadedBytes / (double)1024 / (double)1024), //1
				Math.Round((double)e.BytesPerSecond / (double)1024), //2
				totalbytes //3
				));
		}

		#endregion
		#region UpdateRemoteDefinitions
		public void UpdateRemoteDefinitions()
		{
			this.UpdateRemoteDefinitions(Program.PatcherConfig.CentralDefinitions_CacheExpirityTime);
		}

		/// <summary>
		/// Check if our cache of central definitions needs an update
		/// </summary>
		/// <param name="AgeBeforeUpdate">Cache needs to be this many seconds old for it to be updated</param>
		public void UpdateRemoteDefinitions(Int64 AgeBeforeUpdate)
		{
			this.bw.ReportProgress(0, "Loading remote definitions...");
			DirectoryInfo remDefinitions = new DirectoryInfo(Program.ConfigDir.FullName + Path.DirectorySeparatorChar + "RemoteDefinitions");
			if (!remDefinitions.Exists)
				remDefinitions.Create();
			//Check each configured URI
			int numChecked = 0;
			foreach (string s in Program.PatcherConfig.CentralUpdateServer)
			{
				Uri uri = new Uri(s);
				string md5 = GenerateHash.md5(s);
				FileInfo cacheFile = new FileInfo(remDefinitions.FullName + Path.DirectorySeparatorChar + md5 + ".xml");
				bool needsUpdate = false;

				if (!cacheFile.Exists)
				{
					needsUpdate = true;
				}
				else
				{
					UpdateDefinitions uds = Xml.Deserialize<UpdateDefinitions>(cacheFile, false);
					if (uds.TimeStamp <= (Misc.Unixtime() - AgeBeforeUpdate))
						needsUpdate = true;
					else
						lock (Program.UpdateDefinitions_Central)
							Program.UpdateDefinitions_Central += uds; //Add to the list of definitions.
				}

				if (needsUpdate)
				{
					UpdateDefinitions uds = Xml.Deserialize<UpdateDefinitions>(uri);
					uds.TimeStamp = Misc.Unixtime();
					if (uds != null)
					{
						Xml.Serialize<UpdateDefinitions>(cacheFile, uds, false);
						lock (Program.UpdateDefinitions_Central)
							Program.UpdateDefinitions_Central += uds; //Add to the list of definitions.
					}
					this.bw.ReportProgress(math.Percent(Program.PatcherConfig.CentralUpdateServer.Count, numChecked), "Remote definitions: Fetched " + uri.ToString());
				}
				else
				{
					this.bw.ReportProgress(math.Percent(Program.PatcherConfig.CentralUpdateServer.Count, numChecked), "Remote definitions: Using cached version of " + uri.ToString());
				}
				numChecked++;
			}
			this.bw.ReportProgress(100, "Remote definitions: Loaded.");
		}
		#endregion

		public void LoadLocalUpdateDefinitions()
		{
			this.bw.ReportProgress(0, "Local definitions: Loading...");
			FileInfo cacheFile = new FileInfo(Program.ConfigDir.FullName + Path.DirectorySeparatorChar + "localUpdateDefinitions.xml");
			UpdateDefinitions uds = Xml.Deserialize<UpdateDefinitions>(cacheFile, false);
			lock (Program.UpdateDefinitions_Local) {
				if (uds != null)
					Program.UpdateDefinitions_Local = uds;
				else
					Program.UpdateDefinitions_Local = new UpdateDefinitions();
			}
			this.bw.ReportProgress(100, "Local definitions: Loaded.");
		}

		#region Find the 'state' (installed, updated, unknown) of each distribution
		public void CheckStateOfUpdateDefinitions()
		{
			/*	Check if an UpdateDefinition is 'installed' at all.
			 *		If it is, check if it's up-to-date.
			 *		If it isn't, label as installable.
			 */
			lock (Program.UpdateDefinitions_Local)
			{
				lock (Program.UpdateDefinitions_Central)
				{
					List<UpdateDefinition> uds = new List<UpdateDefinition>();

					foreach (UpdateDefinition ud in Program.UpdateDefinitions_Local.Definitions)
						uds.Add(ud);

					foreach (UpdateDefinition ud in Program.UpdateDefinitions_Central.Definitions)
						uds.Add(ud);

					foreach (UpdateDefinition ud in uds)
					{
						PatchStatus ps = null;
						lock (Program.PatcherConfig.PatchStatus)
							foreach (PatchStatus tmpps in Program.PatcherConfig.PatchStatus)
								if (tmpps.GUID == ud.GUID)
									ps = tmpps;
						if (ps == null)
						{
							ps = new PatchStatus(ud.GUID, "");
							lock (Program.PatcherConfig.PatchStatus)
								Program.PatcherConfig.PatchStatus.Add(ps);
						}
						try
						{
							lock (Program.PatcherConfig.AnarchyOnlinePath)
							{
								if (ud.Exists(Program.PatcherConfig.AnarchyOnlinePath, true))
									ps.Present = PatchStatus.Presence.Present;
								else
									ps.Present = PatchStatus.Presence.NotPresent;
								
							}
						}
						catch (Exception ex)
						{
							if (ex.Message == "Unable to fetch patch info")
								ps.Present = PatchStatus.Presence.Unknown;
						}
					}
				}
			}

		}
		#endregion find the 'state'
	}
}
