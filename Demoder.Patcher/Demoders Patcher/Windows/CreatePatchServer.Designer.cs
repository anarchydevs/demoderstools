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
	partial class CreatePatchServer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePatchServer));
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_GUID = new System.Windows.Forms.TextBox();
			this.listView_distributions = new System.Windows.Forms.ListView();
			this.contextMenuStrip_Distributions = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_version = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_Name = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_PatchServerURLs = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.oFD_Configuration = new System.Windows.Forms.OpenFileDialog();
			this.sFD_Configuration = new System.Windows.Forms.SaveFileDialog();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox_outputDirectory = new System.Windows.Forms.TextBox();
			this.button_browseOutDir = new System.Windows.Forms.Button();
			this.folderBD_OutputDirectory = new System.Windows.Forms.FolderBrowserDialog();
			this.bgw_CreatePS = new System.ComponentModel.BackgroundWorker();
			this.button_CreatePatchServer = new System.Windows.Forms.Button();
			this.contextMenuStrip_Distributions.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(166, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "GUID";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 85);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Distributions";
			// 
			// textBox_GUID
			// 
			this.textBox_GUID.Location = new System.Drawing.Point(206, 57);
			this.textBox_GUID.Multiline = true;
			this.textBox_GUID.Name = "textBox_GUID";
			this.textBox_GUID.Size = new System.Drawing.Size(218, 20);
			this.textBox_GUID.TabIndex = 5;
			// 
			// listView_distributions
			// 
			this.listView_distributions.ContextMenuStrip = this.contextMenuStrip_Distributions;
			this.listView_distributions.GridLines = true;
			this.listView_distributions.Location = new System.Drawing.Point(9, 101);
			this.listView_distributions.Name = "listView_distributions";
			this.listView_distributions.Size = new System.Drawing.Size(415, 133);
			this.listView_distributions.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listView_distributions.TabIndex = 10;
			this.listView_distributions.UseCompatibleStateImageBehavior = false;
			this.listView_distributions.View = System.Windows.Forms.View.Details;
			// 
			// contextMenuStrip_Distributions
			// 
			this.contextMenuStrip_Distributions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteToolStripMenuItem});
			this.contextMenuStrip_Distributions.Name = "contextMenuStrip_Distributions";
			this.contextMenuStrip_Distributions.Size = new System.Drawing.Size(108, 76);
			this.contextMenuStrip_Distributions.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Distributions_Opening);
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.addToolStripMenuItem.Text = "Add...";
			this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.editToolStripMenuItem.Text = "Edit...";
			this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Version";
			// 
			// textBox_version
			// 
			this.textBox_version.Location = new System.Drawing.Point(62, 57);
			this.textBox_version.Name = "textBox_version";
			this.textBox_version.Size = new System.Drawing.Size(102, 20);
			this.textBox_version.TabIndex = 12;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name";
			// 
			// textBox_Name
			// 
			this.textBox_Name.Location = new System.Drawing.Point(62, 28);
			this.textBox_Name.Name = "textBox_Name";
			this.textBox_Name.Size = new System.Drawing.Size(362, 20);
			this.textBox_Name.TabIndex = 4;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 237);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(158, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Patchserver URLs (one per line)";
			// 
			// textBox_PatchServerURLs
			// 
			this.textBox_PatchServerURLs.AcceptsReturn = true;
			this.textBox_PatchServerURLs.Location = new System.Drawing.Point(9, 253);
			this.textBox_PatchServerURLs.Multiline = true;
			this.textBox_PatchServerURLs.Name = "textBox_PatchServerURLs";
			this.textBox_PatchServerURLs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_PatchServerURLs.Size = new System.Drawing.Size(415, 70);
			this.textBox_PatchServerURLs.TabIndex = 15;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(431, 24);
			this.menuStrip1.TabIndex = 16;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(149, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			// 
			// oFD_Configuration
			// 
			this.oFD_Configuration.DefaultExt = "ps.xml";
			this.oFD_Configuration.FileName = ".ps.xml";
			this.oFD_Configuration.Filter = "PatchServer Config|*.ps.xml";
			this.oFD_Configuration.SupportMultiDottedExtensions = true;
			// 
			// sFD_Configuration
			// 
			this.sFD_Configuration.DefaultExt = "ps.xml";
			this.sFD_Configuration.FileName = ".ps.xml";
			this.sFD_Configuration.Filter = "PatchServer Config|*.ps.xml";
			this.sFD_Configuration.SupportMultiDottedExtensions = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 332);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(84, 13);
			this.label6.TabIndex = 17;
			this.label6.Text = "Output Directory";
			// 
			// textBox_outputDirectory
			// 
			this.textBox_outputDirectory.Location = new System.Drawing.Point(96, 329);
			this.textBox_outputDirectory.Name = "textBox_outputDirectory";
			this.textBox_outputDirectory.Size = new System.Drawing.Size(279, 20);
			this.textBox_outputDirectory.TabIndex = 18;
			// 
			// button_browseOutDir
			// 
			this.button_browseOutDir.Location = new System.Drawing.Point(381, 327);
			this.button_browseOutDir.Name = "button_browseOutDir";
			this.button_browseOutDir.Size = new System.Drawing.Size(29, 23);
			this.button_browseOutDir.TabIndex = 19;
			this.button_browseOutDir.Text = "...";
			this.button_browseOutDir.UseVisualStyleBackColor = true;
			this.button_browseOutDir.Click += new System.EventHandler(this.button_browseOutDir_Click);
			// 
			// folderBD_OutputDirectory
			// 
			this.folderBD_OutputDirectory.Description = "Where to export the patchserver?";
			// 
			// bgw_CreatePS
			// 
			this.bgw_CreatePS.WorkerReportsProgress = true;
			this.bgw_CreatePS.WorkerSupportsCancellation = true;
			this.bgw_CreatePS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_CreatePS_DoWork);
			this.bgw_CreatePS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_CreatePS_RunWorkerCompleted);
			// 
			// button_CreatePatchServer
			// 
			this.button_CreatePatchServer.Location = new System.Drawing.Point(123, 376);
			this.button_CreatePatchServer.Name = "button_CreatePatchServer";
			this.button_CreatePatchServer.Size = new System.Drawing.Size(138, 23);
			this.button_CreatePatchServer.TabIndex = 20;
			this.button_CreatePatchServer.Text = "Create Patch Server";
			this.button_CreatePatchServer.UseVisualStyleBackColor = true;
			this.button_CreatePatchServer.Click += new System.EventHandler(this.button_CreatePatchServer_Click);
			// 
			// CreatePatchServer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(431, 411);
			this.Controls.Add(this.button_CreatePatchServer);
			this.Controls.Add(this.button_browseOutDir);
			this.Controls.Add(this.textBox_outputDirectory);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.textBox_PatchServerURLs);
			this.Controls.Add(this.textBox_version);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listView_distributions);
			this.Controls.Add(this.textBox_GUID);
			this.Controls.Add(this.textBox_Name);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "CreatePatchServer";
			this.Text = "Create PatchServer";
			this.Load += new System.EventHandler(this.CreateDistribution_Load);
			this.contextMenuStrip_Distributions.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_GUID;
		private System.Windows.Forms.ListView listView_distributions;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_version;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_Name;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_PatchServerURLs;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Distributions;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog oFD_Configuration;
		private System.Windows.Forms.SaveFileDialog sFD_Configuration;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox_outputDirectory;
		private System.Windows.Forms.Button button_browseOutDir;
		private System.Windows.Forms.FolderBrowserDialog folderBD_OutputDirectory;
		private System.ComponentModel.BackgroundWorker bgw_CreatePS;
		private System.Windows.Forms.Button button_CreatePatchServer;

	}
}