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
using System.Threading;
using Demoder.Common;
using Demoder.Patcher;
using Demoders_Patcher.DataClasses;
using Demoder.Patcher.Events;

namespace Demoders_Patcher
{
	public class backgroundWorker_DoWork
	{
		#region members
		internal BackgroundWorker BackgroundWorker = new BackgroundWorker();
		internal Queue<KeyValuePair<bgw_tasktype, object>> bw_queue = new Queue<KeyValuePair<bgw_tasktype, object>>();
		internal ManualResetEvent bw_taskadded_mre = new ManualResetEvent(false);
		internal ManualResetEvent bw_taskdone_mre = new ManualResetEvent(false);
		#endregion
		#region Events
		public event RunWorkerCompletedEventHandler WorkComplete;
		public event EventHandler QueueEmpty;
		#endregion

		#region constructors

		public backgroundWorker_DoWork()
		{
			this.BackgroundWorker.WorkerReportsProgress = true;
			
			this.BackgroundWorker.DoWork += new DoWorkEventHandler(this.bgw_DoWork);
			this.BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
		}

		#endregion

		#region the background worker.
		private void bgw_DoWork(object sender, DoWorkEventArgs e)
		{
			while (this.bw_queue.Count == 0)
			{
				//Signal that the queue is empty
				if (this.QueueEmpty != null)
					lock (this.QueueEmpty)
						this.QueueEmpty(this, new EventArgs());
				this.bw_taskadded_mre.WaitOne();
			}
			this.bw_taskadded_mre.Reset();
			KeyValuePair<bgw_tasktype, object> kvp = new KeyValuePair<bgw_tasktype, object>(bgw_tasktype.Invalid, null);
			lock (this.bw_queue)
				kvp = this.bw_queue.Dequeue();
			if (kvp.Key != bgw_tasktype.Invalid)
			{
				bool result = true;
				bool force = false;
				switch (kvp.Key)
				{
					case bgw_tasktype.FetchCentralUpdateDefinitions:
						bool force_update = (bool)kvp.Value;
						if (force_update)
							this.UpdateRemoteDefinitions(0);
						else
							this.UpdateRemoteDefinitions();
						e.Result = new KeyValuePair<bgw_tasktype, object>(kvp.Key, result);
						break;
					case bgw_tasktype.LoadLocalUpdateDefinitions:
						this.LoadLocalUpdateDefinitions();
						e.Result = new KeyValuePair<bgw_tasktype, object>(kvp.Key, result); ;
						break;
					case bgw_tasktype.CheckIfUpdateDefinitionsExistLocally:
						this.CheckStateOfUpdateDefinitions();
						e.Result = new KeyValuePair<bgw_tasktype, object>(kvp.Key, result);
						break;
					case bgw_tasktype.RunUpdate:
						force = true;
						goto case bgw_tasktype.RunAutoUpdate;
					case bgw_tasktype.RunAutoUpdate:
						result = this.RunUpdate((List<Uri>)kvp.Value, force);
						e.Result = new KeyValuePair<bgw_tasktype, object>(kvp.Key, result);
						break;
				}
			}
		}
		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//Signal stuff which subscribes to this event
			if (this.WorkComplete!=null)
				lock (this.WorkComplete)
					this.WorkComplete(sender, e);
			//Run the worker again.
			this.BackgroundWorker.RunWorkerAsync();
		}
		#endregion


		#region BGW Enqueuement methods

		private void taskAdded()
		{
			if (!this.BackgroundWorker.IsBusy)
				this.BackgroundWorker.RunWorkerAsync();
			this.bw_taskadded_mre.Set();
		}
		#region bgworker enqueement methods
		internal void Enq_LoadCentralUpdateDefinitions(bool force_update)
		{
			lock (this.bw_queue)
				this.bw_queue.Enqueue(new KeyValuePair<bgw_tasktype, object>(bgw_tasktype.FetchCentralUpdateDefinitions, force_update));
			this.taskAdded();
		}

		internal void Enq_LoadLocalUpdateDefinitions()
		{
			lock (this.bw_queue)
				this.bw_queue.Enqueue(new KeyValuePair<bgw_tasktype, object>(bgw_tasktype.LoadLocalUpdateDefinitions, null));
			this.taskAdded();
		}

		internal void Enq_RunUpdate(List<Uri> uris)
		{
			lock (this.bw_queue)
				this.bw_queue.Enqueue(new KeyValuePair<bgw_tasktype, object>(bgw_tasktype.RunUpdate, uris));
			this.taskAdded();
		}

		internal void Enq_CheckPatchStatus()
		{
			lock (this.bw_queue)
				this.bw_queue.Enqueue(new KeyValuePair<bgw_tasktype, object>(bgw_tasktype.CheckIfUpdateDefinitionsExistLocally, true));
			this.taskAdded();
		}
		#endregion
		#endregion

		#region RunUpdate
		public bool RunUpdate(List<Uri> uris, bool ForceUpdate)
		{
			try
			{
				Demoder.Patcher.DataClasses.PatchServer patchserver = null;
				for (int i = 0; i < uris.Count; i++)
					patchserver = Xml.Deserialize<Demoder.Patcher.DataClasses.PatchServer>(uris[i]);
				PatchStatus patchStatus = Program.PatcherConfig.GetPatchStatus(patchserver.GUID);
				
				if (!ForceUpdate && patchStatus != null && patchserver.Version == patchStatus.Version)
				{
					this.BackgroundWorker.ReportProgress(100, "Version match.");
					return true;
				}
				
				DoPatch dp = new DoPatch(patchserver);
				dp.eventDownloadStatusReport += new DownloadStatusReportEventHandler(dp_eventDownloadStatusReport);
				this.BackgroundWorker.ReportProgress(0, "Comparing distributions");
				if (dp.PatchDistributions(Program.PatcherConfig.AnarchyOnlinePath))
				{
					this.BackgroundWorker.ReportProgress(0, "Installing updated map");
					dp.InstallPatchedDistributions(Program.PatcherConfig.AnarchyOnlinePath);
					Program.PatcherConfig.SetPatchStatus(patchserver.GUID, patchserver.Version);
					return true;
				}
			}
			catch(Exception ex)
			{
				this.BackgroundWorker.ReportProgress(0, ex.Message);
			}
			return false;
		}
		private void dp_eventDownloadStatusReport(DownloadStatusReportEventArgs e)
		{
			string totalbytes = "";
			if (e.TotalBytes > 0)
				totalbytes = string.Format("of {0}MiB ",
					Math.Round((double)e.TotalBytes / (double)1024 / (double)1024, 2));


			this.BackgroundWorker.ReportProgress(e.PercentDone,
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
			this.BackgroundWorker.ReportProgress(0, "Remote definitions: Loading...");
			//Check each configured URI
			int numChecked = 0;
			Program.UpdateDefinitions_Central = new UpdateDefinitions();
			foreach (string uri in Program.PatcherConfig.CentralUpdateServer)
			{
				string md5 = GenerateHash.md5(uri);
				bool needsUpdate = false;
				this.BackgroundWorker.ReportProgress(math.Percent(Program.PatcherConfig.CentralUpdateServer.Count, numChecked), "Remote definitions: Loading " + uri);
				
				string[] cacheArgs = new string[] { md5 };
				UpdateDefinitions uds = Program.XmlCache.Get<UpdateDefinitions>().Request(XMLCacheFlags.Default, uri, cacheArgs);

				if (uds.TimeStamp <= (Misc.Unixtime() - AgeBeforeUpdate))
					needsUpdate = true;
				else
					lock (Program.UpdateDefinitions_Central)
						Program.UpdateDefinitions_Central += uds; //Add to the list of definitions.

				if (needsUpdate)
				{
					uds = (Program.XmlCache.Get<UpdateDefinitions>()).Request(XMLCacheFlags.ReadLive | XMLCacheFlags.WriteCache, uri, cacheArgs);
					uds.TimeStamp = Misc.Unixtime();
					if (uds != null)
					{
						lock (Program.UpdateDefinitions_Central)
							Program.UpdateDefinitions_Central += uds; //Add to the list of definitions.
					}
					this.BackgroundWorker.ReportProgress(math.Percent(Program.PatcherConfig.CentralUpdateServer.Count, numChecked), "Remote definitions: Fetched " + uri);
				}
				else
				{
					this.BackgroundWorker.ReportProgress(math.Percent(Program.PatcherConfig.CentralUpdateServer.Count, numChecked), "Remote definitions: Using cached version of " + uri);
				}
				numChecked++;
			}
			this.BackgroundWorker.ReportProgress(100, "Remote definitions: Loaded.");
		}
		#endregion

		public void LoadLocalUpdateDefinitions()
		{
			this.BackgroundWorker.ReportProgress(0, "Local definitions: Loading...");
			FileInfo cacheFile = new FileInfo(Program.ConfigDir.FullName + Path.DirectorySeparatorChar + "localUpdateDefinitions.xml");
			UpdateDefinitions uds = Xml.Deserialize<UpdateDefinitions>(cacheFile, false);
			lock (Program.UpdateDefinitions_Local) {
				if (uds != null)
					Program.UpdateDefinitions_Local = uds;
				else
					Program.UpdateDefinitions_Local = new UpdateDefinitions();
			}
			this.BackgroundWorker.ReportProgress(100, "Local definitions: Loaded.");
		}

		#region Find the 'state' (installed, updated, unknown) of each distribution
		/// <summary>
		/// Check local status of update definitions. This is SLOW!
		/// </summary>
		public void CheckStateOfUpdateDefinitions()
		{
			/*	Check if an UpdateDefinition is 'installed' at all.
			 *		If it is, check if it's up-to-date.
			 *		If it isn't, label as installable.
			 */

			this.BackgroundWorker.ReportProgress(25);
			lock (Program.UpdateDefinitions_Local)
			{
				this.CheckStateOfUpdateDefinitions_dowork(Program.UpdateDefinitions_Local);
			}
			this.BackgroundWorker.ReportProgress(50, "Local definitions checked");

			lock (Program.UpdateDefinitions_Central)
			{
				this.CheckStateOfUpdateDefinitions_dowork(Program.UpdateDefinitions_Central);
			}
			this.BackgroundWorker.ReportProgress(100, "Remote definitions checked");
		}
		private void CheckStateOfUpdateDefinitions_dowork(UpdateDefinitions uds)
		{
			foreach (UpdateDefinition ud in uds.Definitions)
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
		#endregion find the 'state'
	}
}
