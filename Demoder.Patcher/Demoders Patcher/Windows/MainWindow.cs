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
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Demoder.Common;
using Demoder.Patcher;
using Demoders_Patcher.DataClasses;
namespace Demoders_Patcher.Windows
{
	public partial class MainWindow : Form
	{
		#region members
		private DownloadManager _downloadManager = new DownloadManager(3, 10);
		
        //Worker members
        private Queue<KeyValuePair<bgw_tasktype, object>> bw_queue = new Queue<KeyValuePair<bgw_tasktype, object>>();
        private ManualResetEvent bw_taskadded_mre = new ManualResetEvent(false);
		private ManualResetEvent bw_taskdone_mre = new ManualResetEvent(false);
		private backgroundWorker_DoWork bw_dowork = null;
		#endregion

		#region backgroundWorker1
		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.bw_dowork == null)
				this.bw_dowork = new backgroundWorker_DoWork(this.backgroundWorker1);

			while (this.bw_queue.Count == 0)
				this.bw_taskadded_mre.WaitOne();
			this.bw_taskadded_mre.Reset();
			KeyValuePair<bgw_tasktype, object> kvp = new KeyValuePair<bgw_tasktype, object>(bgw_tasktype.Invalid, null);
			lock (this.bw_queue)
				kvp = this.bw_queue.Dequeue();
			if (kvp.Key != bgw_tasktype.Invalid)
			{
				bool result = true;
				switch (kvp.Key)
				{
					case bgw_tasktype.FetchCentralUpdateDefinitions:
						bool force_update = (bool)kvp.Value;
						if (force_update)
							this.bw_dowork.UpdateRemoteDefinitions(0);
						else
							this.bw_dowork.UpdateRemoteDefinitions();
						e.Result = new KeyValuePair<bgw_tasktype, object>(kvp.Key, result);
						break;
					case bgw_tasktype.LoadLocalUpdateDefinitions:
						this.bw_dowork.LoadLocalUpdateDefinitions();
						e.Result = new KeyValuePair<bgw_tasktype, object>(kvp.Key, result);;
						break;
					case bgw_tasktype.CheckIfUpdateDefinitionsExistLocally:
						this.bw_dowork.CheckStateOfUpdateDefinitions();
						e.Result = new KeyValuePair<bgw_tasktype, object>(kvp.Key, result);
						break;
					case bgw_tasktype.RunUpdate:
						result = this.bw_dowork.RunUpdate((List<Uri>)kvp.Value);
						e.Result = new KeyValuePair<bgw_tasktype, object>(kvp.Key, result);
						break;
				}
			}
		}
		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
            KeyValuePair<bgw_tasktype, object> kvp = (KeyValuePair<bgw_tasktype, object>)e.Result;
            switch (kvp.Key)
            {
                case bgw_tasktype.FetchCentralUpdateDefinitions:
				case bgw_tasktype.LoadLocalUpdateDefinitions:
                    this.displayAvailablePatches();
                    break;
				case bgw_tasktype.CheckIfUpdateDefinitionsExistLocally:
					//TODO: Update current listviews content.
					break;
			}
			//Run the worker again.
			this.backgroundWorker1.RunWorkerAsync();
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			string message = string.Empty;
			try
			{
				message = (string)e.UserState;
			}
			catch { }
			lock (Program.StatusbarUpdates)
				Program.StatusbarUpdates.Add(new StatusbarUpdate(e.ProgressPercentage, message));
			if (e.ProgressPercentage < 0 || e.ProgressPercentage > 100)
			{
				this.toolStripProgressBar1.Visible = false;
				this.toolStripStatusLabel1.Visible = false;
				this.timer_statusBarReset.Stop();
			}
			else
			{
				this.toolStripProgressBar1.Value = e.ProgressPercentage;
				this.toolStripProgressBar1.Visible = true;
				this.toolStripStatusLabel1.Text = message;
				this.toolStripStatusLabel1.Visible = true;
				this.timer_statusBarReset.Stop();
                if (e.ProgressPercentage == 0 || e.ProgressPercentage == 100)
				    this.timer_statusBarReset.Start();
			}
		}
		#endregion


		public MainWindow()
		{
			InitializeComponent();
			
        }

		private void MainWindow_Load(object sender, EventArgs e)
		{
			this.createTreeView();
			bw_loadCentralUpdateDefinitions(false);
			bw_loadLocalUpdateDefinitions();
			bw_checkPatchStatus();
			this.backgroundWorker1.RunWorkerAsync();
		}

        #region bgworker enqueement methods
		private void bw_loadCentralUpdateDefinitions(bool force_update)
		{
			lock (this.bw_queue)
				this.bw_queue.Enqueue(new KeyValuePair<bgw_tasktype, object>(bgw_tasktype.FetchCentralUpdateDefinitions, force_update));
			this.bw_taskadded_mre.Set();
		}

		private void bw_loadLocalUpdateDefinitions()
		{
			lock (this.bw_queue)
				this.bw_queue.Enqueue(new KeyValuePair<bgw_tasktype, object>(bgw_tasktype.LoadLocalUpdateDefinitions, null));
			this.bw_taskadded_mre.Set();
		}

		private void bw_runUpdate(List<Uri> uris)
		{
			lock (this.bw_queue)
				this.bw_queue.Enqueue(new KeyValuePair<bgw_tasktype, object>(bgw_tasktype.RunUpdate, uris));
			this.bw_taskadded_mre.Set();

		}

		private void bw_checkPatchStatus()
		{
			lock (this.bw_queue)
				this.bw_queue.Enqueue(new KeyValuePair<bgw_tasktype, object>(bgw_tasktype.CheckIfUpdateDefinitionsExistLocally, true));
			this.bw_taskadded_mre.Set();
		}
        #endregion

		private void createTreeView()
		{
			/* something is causing the app to crash when we add nodes here... but not when adding them via background worker */
			this.treeView_MainWindow.Nodes.Clear();
			//Tree root: Internal
			TreeNode intnode = new TreeNode("Internal");
			intnode.Tag = mw_treeview_Tags.Internal;
			TreeNode lognode = new TreeNode("Event Log");
			lognode.Tag=mw_treeview_Tags.EventLog;
			intnode.Nodes.Add(lognode);
			this.treeView_MainWindow.Nodes.Add(intnode);

			this.treeView_MainWindow.Nodes.Add("repository", "Repository");
			this.treeView_MainWindow.Nodes[this.treeView_MainWindow.Nodes.IndexOfKey("repository")].Tag = mw_treeview_Tags.Repository;
			this.treeView_MainWindow.ExpandAll();

			//Tree root: Create
			TreeNode createNode = new TreeNode("Create");
			createNode.Tag = mw_treeview_Tags.Create;
		}

        private void displayAvailablePatches()
        {
			int index = this.treeView_MainWindow.Nodes.IndexOfKey("repository");
			TreeNode tn = this.treeView_MainWindow.Nodes[index];
			int numnodes = tn.Nodes.Count;
			tn.Nodes.Clear();
            List<string> nodenames = new List<string>(Enum.GetNames(typeof(DefinitionType)));
            nodenames.Sort();
            foreach (string nodename in nodenames)
            {
				TreeNode tn2 = new TreeNode(nodename);
				tn2.Tag = mw_treeview_Tags.Repository;
               tn.Nodes.Add(tn2);
            }
			if (numnodes == 0)
				tn.Expand();
        }

		/// <summary>
		/// Method to update listviews content based on treeView selection.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void treeView_AfterSelect_updateListViewContent(object sender, TreeViewEventArgs e)
        {
			if (e.Node.Parent == null) return;
			if (e.Node.Parent.Tag == null) return;
			this.listView_MainWindow.BeginUpdate();
			this.listView_MainWindow.GridLines = true;
			this.listView_MainWindow.Items.Clear();
			this.listView_MainWindow.Groups.Clear();
			this.listView_MainWindow.Columns.Clear();
			if ((mw_treeview_Tags)e.Node.Parent.Tag == mw_treeview_Tags.Repository)
			{
				this.listView_MainWindow.Columns.Add("Name", "Name");
				this.listView_MainWindow.Columns.Add("Status", "Status");
				this.listView_MainWindow.Columns.Add("Source", "Source");
				this.listView_MainWindow.Columns.Add("Description", "Description");
				ListViewGroup centralitems = new ListViewGroup("Central Database");
				this.listView_MainWindow.ShowGroups = false;
				if (Program.UpdateDefinitions_Central != null)
				{
					foreach (UpdateDefinition ud in Program.UpdateDefinitions_Central.Definitions)
					{
						if (ud.DefinitionType.ToString() == e.Node.Text)
						{
							this._addUpdateItem(ud, true);
						}
					}
				}
				ListViewGroup customitems = new ListViewGroup("Custom Definitions");
				if (Program.UpdateDefinitions_Local != null)
				{
					foreach (UpdateDefinition ud in Program.UpdateDefinitions_Local.Definitions)
					{
						if (ud.DefinitionType.ToString() == e.Node.Text)
						{
							this._addUpdateItem(ud, false);
						}
					}
				}
			}

			else if ((mw_treeview_Tags)e.Node.Parent.Tag == mw_treeview_Tags.Internal)
			{
				if ((mw_treeview_Tags)e.Node.Tag == mw_treeview_Tags.EventLog)
				{
					
					this.listView_MainWindow.Columns.Add("Time", "Time");
					this.listView_MainWindow.Columns.Add("%", "%");
					this.listView_MainWindow.Columns.Add("Message", "Message");
					foreach (StatusbarUpdate su in Program.StatusbarUpdates)
					{
						DateTime dt = Misc.Unixtime(su.Timestamp);
						ListViewItem lvi = new ListViewItem(dt.ToShortTimeString());
						lvi.SubItems.Add(su.Percent.ToString());
						lvi.SubItems.Add(su.Message);
						this.listView_MainWindow.Items.Add(lvi);
					}
				}
			}

			//Autoresize collumn headers.
			Forms.AutoResizeHeaders(this.listView_MainWindow, ColumnHeaderAutoResizeStyle.HeaderSize);
			this.listView_MainWindow.EndUpdate();
        }


		private void _addUpdateItem(UpdateDefinition ud, bool IsFromCentral)
		{
			ListViewItem lvi = new ListViewItem(ud.Name);
			lvi.Tag = ud;
			PatchStatus ps = Program.PatcherConfig.GetPatchStatus(ud.GUID);
			StatusFlag status = StatusFlag.Unknown;
			if (ps == null)
				status = StatusFlag.Unknown;
			else
			{
				switch (ps.Present)
				{
					case PatchStatus.Presence.Unknown:
						status = StatusFlag.Unknown;
						break;
					case PatchStatus.Presence.Present:
						if (ud.Version == "")
							status = StatusFlag.Present;
						else if (ps.Version == "")
							status = StatusFlag.Present;
						else if (ps.Version == ud.Version)
							status = StatusFlag.OK;
						else
							status = StatusFlag.UpdateAvailable;
						break;
					case PatchStatus.Presence.NotPresent:
						status = StatusFlag.Installable;
						break;
				}
			}
			
		
			lvi.SubItems.Add(status.ToString());
			if (IsFromCentral)
				lvi.SubItems.Add("Central");
			else
				lvi.SubItems.Add("Custom");
			lvi.SubItems.Add(ud.Description);
			this.listView_MainWindow.Items.Add(lvi);
		}

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView lv = this.listView_MainWindow;
            if (lv.SelectedItems.Count == 0) return;
            ListViewItem lvi = lv.SelectedItems[0];
			UpdateDefinition ud = (UpdateDefinition)lvi.Tag;
			List<Uri> uris = new List<Uri>();
			foreach (string s in ud.UpdateServers)
				uris.Add(new Uri(s));
			this.bw_runUpdate(uris);
		}

		#region Menu: Tools

		private void syncToCentralRepisoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.bw_loadCentralUpdateDefinitions(true);
		}
		#endregion

		private void timer_statusBarReset_Tick(object sender, EventArgs e)
		{
			this.toolStripProgressBar1.Visible = false;
			this.toolStripStatusLabel1.Visible = false;
		}

		private void createDistributionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Manually created configuration for easier testing of the interface as it's being made.
			CreatePatchServerConfig cpsc = new CreatePatchServerConfig();
			cpsc.Name = "Demoders' GUI";
			cpsc.Version = "1.0";
			/*
			CreateDistributionConfig cdc1 = new CreateDistributionConfig();
			cdc1.DistributionType = Demoder.Patcher.DataClasses.Distribution.DistributionType.AO_Map;
			cdc1.Name = "AoRK";
			cdc1.Directory = @"C:\Games\AO\Anarchy Online\cd_image\textures\PlanetMap\AoRK";

			CreateDistributionConfig cdc2 = new CreateDistributionConfig();
			cdc2.DistributionType = Demoder.Patcher.DataClasses.Distribution.DistributionType.AO_Map;
			cdc2.Name = "AoSL";
			cdc2.Directory = @"C:\Games\AO\Anarchy Online\cd_image\textures\PlanetMap\AoSL";
			*/
			CreateDistributionConfig cdc1 = new CreateDistributionConfig();
			cdc1.DistributionType = Demoder.Patcher.DataClasses.Distribution.DistributionType.AO_GUI;
			cdc1.Name = "Demoders GUI";
			cdc1.Directory = @"C:\Games\AO\Anarchy Online\cd_image\gui\Demoder";

			CreateDistributionConfig cdc2 = new CreateDistributionConfig();
			cdc2.DistributionType = Demoder.Patcher.DataClasses.Distribution.DistributionType.AO_GUI_Textures;
			cdc2.Name = "Demoders GUI - Textures";
			cdc2.Directory = @"C:\Games\AO\Anarchy Online\cd_image\textures\archives\Demoder";

			cpsc.Distributions.Add(cdc1);
			cpsc.Distributions.Add(cdc2);

			CreatePatchServer cd = new CreatePatchServer(cpsc);
			DialogResult dr = cd.ShowDialog();
			switch (dr)
			{
				case DialogResult.OK:
					break;
				case DialogResult.Cancel:
					break;
			}

		}
	}
}
