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
namespace Demoders_Patcher.Windows
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeView_MainWindow = new System.Windows.Forms.TreeView();
			this.listView_MainWindow = new System.Windows.Forms.ListView();
			this.contextMenuStrip_ListView = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.syncToCentralRepisoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.viewEventLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createDistributionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.timer_statusBarReset = new System.Windows.Forms.Timer(this.components);
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.contextMenuStrip_ListView.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(648, 274);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(648, 320);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(648, 22);
			this.statusStrip1.TabIndex = 0;
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
			this.toolStripProgressBar1.Visible = false;
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Visible = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView_MainWindow);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.listView_MainWindow);
			this.splitContainer1.Size = new System.Drawing.Size(648, 274);
			this.splitContainer1.SplitterDistance = 137;
			this.splitContainer1.TabIndex = 0;
			// 
			// treeView_MainWindow
			// 
			this.treeView_MainWindow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView_MainWindow.HideSelection = false;
			this.treeView_MainWindow.Location = new System.Drawing.Point(0, 0);
			this.treeView_MainWindow.Name = "treeView_MainWindow";
			this.treeView_MainWindow.Size = new System.Drawing.Size(137, 274);
			this.treeView_MainWindow.TabIndex = 0;
			this.treeView_MainWindow.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect_updateListViewContent);
			// 
			// listView_MainWindow
			// 
			this.listView_MainWindow.AllowColumnReorder = true;
			this.listView_MainWindow.ContextMenuStrip = this.contextMenuStrip_ListView;
			this.listView_MainWindow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView_MainWindow.FullRowSelect = true;
			this.listView_MainWindow.GridLines = true;
			this.listView_MainWindow.Location = new System.Drawing.Point(0, 0);
			this.listView_MainWindow.Name = "listView_MainWindow";
			this.listView_MainWindow.Size = new System.Drawing.Size(507, 274);
			this.listView_MainWindow.TabIndex = 0;
			this.listView_MainWindow.UseCompatibleStateImageBehavior = false;
			this.listView_MainWindow.View = System.Windows.Forms.View.Details;
			// 
			// contextMenuStrip_ListView
			// 
			this.contextMenuStrip_ListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem});
			this.contextMenuStrip_ListView.Name = "contextMenuStrip_ListView";
			this.contextMenuStrip_ListView.Size = new System.Drawing.Size(113, 26);
			// 
			// updateToolStripMenuItem
			// 
			this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
			this.updateToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.updateToolStripMenuItem.Text = "Update";
			this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.ShowItemToolTips = true;
			this.menuStrip1.Size = new System.Drawing.Size(648, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.syncToCentralRepisoryToolStripMenuItem,
            this.optToolStripMenuItem,
            this.toolStripSeparator1,
            this.viewEventLogToolStripMenuItem,
            this.createDistributionToolStripMenuItem1});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// syncToCentralRepisoryToolStripMenuItem
			// 
			this.syncToCentralRepisoryToolStripMenuItem.Name = "syncToCentralRepisoryToolStripMenuItem";
			this.syncToCentralRepisoryToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.syncToCentralRepisoryToolStripMenuItem.Text = "&Sync to Central Repisory";
			this.syncToCentralRepisoryToolStripMenuItem.ToolTipText = "Fetch the latest list of update definitions from the central repisory";
			this.syncToCentralRepisoryToolStripMenuItem.Click += new System.EventHandler(this.syncToCentralRepisoryToolStripMenuItem_Click);
			// 
			// optToolStripMenuItem
			// 
			this.optToolStripMenuItem.Name = "optToolStripMenuItem";
			this.optToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.optToolStripMenuItem.Text = "&Options...";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
			// 
			// viewEventLogToolStripMenuItem
			// 
			this.viewEventLogToolStripMenuItem.Name = "viewEventLogToolStripMenuItem";
			this.viewEventLogToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.viewEventLogToolStripMenuItem.Text = "View Event &Log";
			this.viewEventLogToolStripMenuItem.ToolTipText = "View the patchers event log";
			// 
			// createDistributionToolStripMenuItem1
			// 
			this.createDistributionToolStripMenuItem1.Name = "createDistributionToolStripMenuItem1";
			this.createDistributionToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
			this.createDistributionToolStripMenuItem1.Text = "Create Distribution";
			this.createDistributionToolStripMenuItem1.Click += new System.EventHandler(this.createDistributionToolStripMenuItem_Click);
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			// 
			// timer_statusBarReset
			// 
			this.timer_statusBarReset.Interval = 5000;
			this.timer_statusBarReset.Tick += new System.EventHandler(this.timer_statusBarReset_Tick);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(648, 320);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainWindow";
			this.Text = "Demoders Patcher";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.contextMenuStrip_ListView.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView treeView_MainWindow;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListView listView_MainWindow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_ListView;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem syncToCentralRepisoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem viewEventLogToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optToolStripMenuItem;
		private System.Windows.Forms.Timer timer_statusBarReset;
		private System.Windows.Forms.ToolStripMenuItem createDistributionToolStripMenuItem1;
	}
}

