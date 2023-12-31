﻿/*
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
		private BWDoWork backgroundWorker = new BWDoWork();
		#endregion

		#region Backgroundworker stuff
		private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//Add events for this stuff.
			string message = string.Empty;
			try
			{
				message = (string)e.UserState;
			}
			catch { }
						
			lock (Program.EventLog)
				Program.EventLog.Log(new EventLogEntry(EventLogLevel.Notice, this, e));
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

		private void bgw_complete(object sender, RunWorkerCompletedEventArgs e)
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
		}
		#endregion

		public MainWindow()
		{
			this.backgroundWorker.BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.bgw_ProgressChanged);
			this.backgroundWorker.WorkComplete += new RunWorkerCompletedEventHandler(this.bgw_complete);
			InitializeComponent();
			
        }

		private void MainWindow_Load(object sender, EventArgs e)
		{
			this.createTreeView();
			this.displayAvailablePatches();
		}

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
				this.listView_MainWindow.Tag = e.Node.Tag;

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
					this.listView_MainWindow.Tag = e.Node.Tag;
					this.listView_MainWindow.Columns.Add("Time", "Time");
					this.listView_MainWindow.Columns.Add("Level", "Level");
					this.listView_MainWindow.Columns.Add("Message", "Message");
					foreach (EventLogEntry el in Program.EventLog.ReadLog(0, EventLogRead.Last))
					{
						ListViewItem lvi = new ListViewItem(el.Time.ToShortTimeString());
						lvi.SubItems.Add(el.LogLevel.ToString());
						lvi.SubItems.Add(el.Message);
						lvi.Tag = el;
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
			ps.StatusFlag = status;
			ud.Tag = ps;
	
			lvi.SubItems.Add(status.ToString());
			if (IsFromCentral)
				lvi.SubItems.Add("Central");
			else
				lvi.SubItems.Add("Custom");
			lvi.SubItems.Add(ud.Description);
			this.listView_MainWindow.Items.Add(lvi);
		}


#region listViewItem context menu
		private void contextMenuStrip_ListView_Opening(object sender, CancelEventArgs e)
		{
			contextMenuStrip_ListView.Items.Clear();
			switch ((mw_treeview_Tags)this.listView_MainWindow.Tag)
			{
				case mw_treeview_Tags.EventLog:
					ToolStripMenuItem tsmi_eventlog_copy = new ToolStripMenuItem("Copy");
					tsmi_eventlog_copy.Click += new EventHandler(listView_MainWindow_ContextMenu_EventLog_Copy_Click);
					this.contextMenuStrip_ListView.Items.Add(tsmi_eventlog_copy);
					break;
				case mw_treeview_Tags.Repository:
					if (listView_MainWindow.SelectedItems.Count > 0)
					{
						ListViewItem lvi = listView_MainWindow.SelectedItems[0];
						UpdateDefinition ud = (UpdateDefinition)lvi.Tag;
#warning Should make this use a label instead of an index.
						StatusFlag sf = (StatusFlag)Enum.Parse(typeof(StatusFlag), lvi.SubItems[1].Text);

						string UpdateButtonText = "Update";
						if (sf == StatusFlag.Installable)
							UpdateButtonText = "Install";
						if (sf == StatusFlag.OK)
							UpdateButtonText = "Force Update";
						
						ToolStripMenuItem tsmi_repository_update = new ToolStripMenuItem(UpdateButtonText);
						tsmi_repository_update.Click += new EventHandler(listView_MainWindow_ContextMenu_Repository_Update_Click);
						this.contextMenuStrip_ListView.Items.Add(tsmi_repository_update);
					}
					break;
			}
		}

		/// <summary>
		/// Event triggered when contextmenubutton Repository:Update is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listView_MainWindow_ContextMenu_EventLog_Copy_Click(object sender, EventArgs e)
		{
			this.listView_MainWindow_CopyItemsToClipBoard();
		}

		/// <summary>
		/// Event triggered when contextmenubutton Repository:Update is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void listView_MainWindow_ContextMenu_Repository_Update_Click(object sender, EventArgs e)
        {
            ListView lv = this.listView_MainWindow;
            if (lv.SelectedItems.Count == 0) return;
            ListViewItem lvi = lv.SelectedItems[0];
			UpdateDefinition ud = (UpdateDefinition)lvi.Tag;
			List<Uri> uris = new List<Uri>();
			foreach (string s in ud.UpdateServers)
				uris.Add(new Uri(s));
			this.backgroundWorker.Enq_RunUpdate(uris);
		}
#endregion

		#region Menu: Tools

		private void syncToCentralRepisoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.backgroundWorker.Enq_LoadCentralUpdateDefinitions(true);
		}
		#endregion

		private void timer_statusBarReset_Tick(object sender, EventArgs e)
		{
			this.toolStripProgressBar1.Visible = false;
			this.toolStripStatusLabel1.Visible = false;
		}

		private void createDistributionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			(new CreatePatchServer()).Show();
		}

		private void listView_MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				//Control key is pressed.
				switch (e.KeyCode)
				{
					case Keys.C:
						this.listView_MainWindow_CopyItemsToClipBoard();
					break;
				}
			}
		}

		private void listView_MainWindow_CopyItemsToClipBoard()
		{
			if (this.listView_MainWindow.SelectedItems.Count > 0)
			{
				List<string> cliptext = new List<string>();
				foreach (ListViewItem lvi in this.listView_MainWindow.SelectedItems)
				{
					cliptext.Add(lvi.Tag.ToString());
				}
				Clipboard.SetText(String.Join("\r\n", cliptext.ToArray()));
			}
		}
		#region Events for listView_MainWindow
		
		#endregion
	}
}
