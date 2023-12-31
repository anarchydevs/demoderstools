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
namespace AOMC
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this._bw_Compiler = new System.ComponentModel.BackgroundWorker();
			this.label1 = new System.Windows.Forms.Label();
			this._map_name = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this._map_version_major = new System.Windows.Forms.NumericUpDown();
			this._map_version_minor = new System.Windows.Forms.NumericUpDown();
			this._map_version_build = new System.Windows.Forms.NumericUpDown();
			this._map_subdirectory = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this._map_assemblymethod = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this._map_texturesize = new System.Windows.Forms.NumericUpDown();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage_General = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this._map_OutFolder = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._map_binname = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tabPage_Images = new System.Windows.Forms.TabPage();
			this._imagelist = new System.Windows.Forms.ListView();
			this.columnHeader_Name = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_Path = new System.Windows.Forms.ColumnHeader();
			this.images_Contextmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.imagesContextMenu_Add = new System.Windows.Forms.ToolStripMenuItem();
			this.imagesContextMenu_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.imagesContextMenu_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPage_WorkerTasks = new System.Windows.Forms.TabPage();
			this._WorkerTasks = new System.Windows.Forms.ListView();
			this.workerColumn_Name = new System.Windows.Forms.ColumnHeader();
			this.workerColumn_Maprect = new System.Windows.Forms.ColumnHeader();
			this.workerColumn_Imageformat = new System.Windows.Forms.ColumnHeader();
			this.workerColumn_Images = new System.Windows.Forms.ColumnHeader();
			this.workerTasks_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.workerTasks_ContextMenu_Add = new System.Windows.Forms.ToolStripMenuItem();
			this.workerTasks_ContextMenu_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.workerTasks_ContextMenu_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPage_MapVersions = new System.Windows.Forms.TabPage();
			this._MapVersions = new System.Windows.Forms.ListView();
			this._MapVersions_File = new System.Windows.Forms.ColumnHeader();
			this._MapVersions_Type = new System.Windows.Forms.ColumnHeader();
			this._MapVersions_Name = new System.Windows.Forms.ColumnHeader();
			this._MapVersions_CoordsFile = new System.Windows.Forms.ColumnHeader();
			this._MapVersions_Layers = new System.Windows.Forms.ColumnHeader();
			this.mapVersions_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mapVersions_ContextMenu_Add = new System.Windows.Forms.ToolStripMenuItem();
			this.mapVersions_ContextMenu_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.mapVersions_ContextMenu_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPage_Compile = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._label_compilestatusmsg = new System.Windows.Forms.Label();
			this.progressBar_assembler = new System.Windows.Forms.ProgressBar();
			this.label9 = new System.Windows.Forms.Label();
			this.progressBar_worker = new System.Windows.Forms.ProgressBar();
			this.progressBar_imageloader = new System.Windows.Forms.ProgressBar();
			this.progressBar_imageslicer = new System.Windows.Forms.ProgressBar();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.statuslabel_imageloader = new System.Windows.Forms.Label();
			this.statuslabel_imageslicer = new System.Windows.Forms.Label();
			this.statuslabel_worker = new System.Windows.Forms.Label();
			this.statuslabel_assembler = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button_docompile = new System.Windows.Forms.Button();
			this._label_DebugMessages = new System.Windows.Forms.Label();
			this._compiler_debugmessages = new System.Windows.Forms.TextBox();
			this._OpenMapConfig = new System.Windows.Forms.OpenFileDialog();
			this._SaveMapConfig = new System.Windows.Forms.SaveFileDialog();
			this.splitContainer_Secondary = new System.Windows.Forms.SplitContainer();
			this._HelperBox = new System.Windows.Forms.TextBox();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.contentsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.indexToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer_Main = new System.Windows.Forms.SplitContainer();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.numericUpDown_SlicePadding = new System.Windows.Forms.NumericUpDown();
			this.label14 = new System.Windows.Forms.Label();
			this.checkBox_SlicePaddingEnabled = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this._map_version_major)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._map_version_minor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._map_version_build)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._map_texturesize)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage_General.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage_Images.SuspendLayout();
			this.images_Contextmenu.SuspendLayout();
			this.tabPage_WorkerTasks.SuspendLayout();
			this.workerTasks_ContextMenu.SuspendLayout();
			this.tabPage_MapVersions.SuspendLayout();
			this.mapVersions_ContextMenu.SuspendLayout();
			this.tabPage_Compile.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.splitContainer_Secondary.Panel1.SuspendLayout();
			this.splitContainer_Secondary.Panel2.SuspendLayout();
			this.splitContainer_Secondary.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.splitContainer_Main.Panel1.SuspendLayout();
			this.splitContainer_Main.Panel2.SuspendLayout();
			this.splitContainer_Main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SlicePadding)).BeginInit();
			this.SuspendLayout();
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
			this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem2});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.aboutToolStripMenuItem.Text = "&About...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem2
			// 
			this.helpToolStripMenuItem2.Name = "helpToolStripMenuItem2";
			this.helpToolStripMenuItem2.Size = new System.Drawing.Size(131, 22);
			this.helpToolStripMenuItem2.Text = "User Guide";
			this.helpToolStripMenuItem2.Click += new System.EventHandler(this.helpToolStripMenuItem2_Click);
			// 
			// _bw_Compiler
			// 
			this._bw_Compiler.WorkerReportsProgress = true;
			this._bw_Compiler.DoWork += new System.ComponentModel.DoWorkEventHandler(this._bw_Compiler_DoWork);
			this._bw_Compiler.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._bw_Compiler_RunWorkerCompleted);
			this._bw_Compiler.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this._bw_Compiler_ProgressChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name";
			// 
			// _map_name
			// 
			this._map_name.Location = new System.Drawing.Point(86, 12);
			this._map_name.Name = "_map_name";
			this._map_name.Size = new System.Drawing.Size(138, 20);
			this._map_name.TabIndex = 1;
			this._map_name.TextChanged += new System.EventHandler(this._MapInfoChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(0, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Version";
			// 
			// _map_version_major
			// 
			this._map_version_major.Location = new System.Drawing.Point(86, 68);
			this._map_version_major.Name = "_map_version_major";
			this._map_version_major.Size = new System.Drawing.Size(41, 20);
			this._map_version_major.TabIndex = 20;
			this._map_version_major.ValueChanged += new System.EventHandler(this._MapInfoChanged);
			// 
			// _map_version_minor
			// 
			this._map_version_minor.Location = new System.Drawing.Point(133, 68);
			this._map_version_minor.Name = "_map_version_minor";
			this._map_version_minor.Size = new System.Drawing.Size(40, 20);
			this._map_version_minor.TabIndex = 30;
			this._map_version_minor.ValueChanged += new System.EventHandler(this._MapInfoChanged);
			// 
			// _map_version_build
			// 
			this._map_version_build.Location = new System.Drawing.Point(179, 68);
			this._map_version_build.Name = "_map_version_build";
			this._map_version_build.Size = new System.Drawing.Size(40, 20);
			this._map_version_build.TabIndex = 40;
			this._map_version_build.ValueChanged += new System.EventHandler(this._MapInfoChanged);
			// 
			// _map_subdirectory
			// 
			this._map_subdirectory.Location = new System.Drawing.Point(86, 99);
			this._map_subdirectory.Name = "_map_subdirectory";
			this._map_subdirectory.Size = new System.Drawing.Size(138, 20);
			this._map_subdirectory.TabIndex = 50;
			this._map_subdirectory.TextChanged += new System.EventHandler(this._MapInfoChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(0, 103);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Subfolder";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(68, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Bin Structure";
			// 
			// _map_assemblymethod
			// 
			this._map_assemblymethod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this._map_assemblymethod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this._map_assemblymethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._map_assemblymethod.FormattingEnabled = true;
			this._map_assemblymethod.Location = new System.Drawing.Point(86, 12);
			this._map_assemblymethod.Name = "_map_assemblymethod";
			this._map_assemblymethod.Size = new System.Drawing.Size(138, 21);
			this._map_assemblymethod.Sorted = true;
			this._map_assemblymethod.TabIndex = 60;
			this._map_assemblymethod.SelectedIndexChanged += new System.EventHandler(this._MapInfoChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 45);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 13);
			this.label7.TabIndex = 12;
			this.label7.Text = "Texture size";
			// 
			// _map_texturesize
			// 
			this._map_texturesize.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this._map_texturesize.Location = new System.Drawing.Point(86, 41);
			this._map_texturesize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this._map_texturesize.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
			this._map_texturesize.Name = "_map_texturesize";
			this._map_texturesize.Size = new System.Drawing.Size(53, 20);
			this._map_texturesize.TabIndex = 70;
			this._map_texturesize.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this._map_texturesize.ValueChanged += new System.EventHandler(this._MapInfoChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage_General);
			this.tabControl1.Controls.Add(this.tabPage_Images);
			this.tabControl1.Controls.Add(this.tabPage_WorkerTasks);
			this.tabControl1.Controls.Add(this.tabPage_MapVersions);
			this.tabControl1.Controls.Add(this.tabPage_Compile);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(613, 225);
			this.tabControl1.TabIndex = 2;
			this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
			// 
			// tabPage_General
			// 
			this.tabPage_General.Controls.Add(this.groupBox3);
			this.tabPage_General.Controls.Add(this.groupBox2);
			this.tabPage_General.Controls.Add(this.groupBox1);
			this.tabPage_General.Location = new System.Drawing.Point(4, 22);
			this.tabPage_General.Name = "tabPage_General";
			this.tabPage_General.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_General.Size = new System.Drawing.Size(605, 199);
			this.tabPage_General.TabIndex = 0;
			this.tabPage_General.Text = "General";
			this.tabPage_General.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.checkBox_SlicePaddingEnabled);
			this.groupBox3.Controls.Add(this.label14);
			this.groupBox3.Controls.Add(this.numericUpDown_SlicePadding);
			this.groupBox3.Controls.Add(this._map_OutFolder);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Location = new System.Drawing.Point(250, 80);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(234, 68);
			this.groupBox3.TabIndex = 16;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Other";
			// 
			// _map_OutFolder
			// 
			this._map_OutFolder.Location = new System.Drawing.Point(85, 14);
			this._map_OutFolder.Name = "_map_OutFolder";
			this._map_OutFolder.Size = new System.Drawing.Size(138, 20);
			this._map_OutFolder.TabIndex = 80;
			this._map_OutFolder.DoubleClick += new System.EventHandler(this._map_OutFolder_DoubleClick);
			this._map_OutFolder.TextChanged += new System.EventHandler(this._MapInfoChanged);
			this._map_OutFolder.Click += new System.EventHandler(this._map_OutFolder_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(8, 17);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(68, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Output folder";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this._map_texturesize);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this._map_assemblymethod);
			this.groupBox2.Location = new System.Drawing.Point(249, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(235, 67);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Map Layout";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._map_binname);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this._map_subdirectory);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this._map_name);
			this.groupBox1.Controls.Add(this._map_version_build);
			this.groupBox1.Controls.Add(this._map_version_minor);
			this.groupBox1.Controls.Add(this._map_version_major);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(8, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(235, 142);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Map information";
			// 
			// _map_binname
			// 
			this._map_binname.Location = new System.Drawing.Point(86, 40);
			this._map_binname.Name = "_map_binname";
			this._map_binname.Size = new System.Drawing.Size(138, 20);
			this._map_binname.TabIndex = 10;
			this._map_binname.TextChanged += new System.EventHandler(this._MapInfoChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(0, 44);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "BinName";
			// 
			// tabPage_Images
			// 
			this.tabPage_Images.Controls.Add(this._imagelist);
			this.tabPage_Images.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Images.Name = "tabPage_Images";
			this.tabPage_Images.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_Images.Size = new System.Drawing.Size(605, 199);
			this.tabPage_Images.TabIndex = 1;
			this.tabPage_Images.Text = "Images";
			this.tabPage_Images.UseVisualStyleBackColor = true;
			// 
			// _imagelist
			// 
			this._imagelist.AllowColumnReorder = true;
			this._imagelist.AllowDrop = true;
			this._imagelist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Name,
            this.columnHeader_Path});
			this._imagelist.ContextMenuStrip = this.images_Contextmenu;
			this._imagelist.Dock = System.Windows.Forms.DockStyle.Fill;
			this._imagelist.FullRowSelect = true;
			this._imagelist.GridLines = true;
			this._imagelist.Location = new System.Drawing.Point(3, 3);
			this._imagelist.Name = "_imagelist";
			this._imagelist.Size = new System.Drawing.Size(599, 193);
			this._imagelist.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this._imagelist.TabIndex = 0;
			this._imagelist.UseCompatibleStateImageBehavior = false;
			this._imagelist.View = System.Windows.Forms.View.Details;
			this._imagelist.DoubleClick += new System.EventHandler(this._imagelist_DoubleClick);
			this._imagelist.DragDrop += new System.Windows.Forms.DragEventHandler(this._imagelist_DragDrop);
			this._imagelist.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this._lv_ColumnClick);
			this._imagelist.DragEnter += new System.Windows.Forms.DragEventHandler(this._imagelist_DragEnter);
			// 
			// columnHeader_Name
			// 
			this.columnHeader_Name.Text = "Name";
			this.columnHeader_Name.Width = 192;
			// 
			// columnHeader_Path
			// 
			this.columnHeader_Path.Text = "Path";
			this.columnHeader_Path.Width = 375;
			// 
			// images_Contextmenu
			// 
			this.images_Contextmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imagesContextMenu_Add,
            this.imagesContextMenu_Edit,
            this.toolStripSeparator6,
            this.imagesContextMenu_Remove});
			this.images_Contextmenu.Name = "images_Contextmenu";
			this.images_Contextmenu.Size = new System.Drawing.Size(118, 76);
			this.images_Contextmenu.Opening += new System.ComponentModel.CancelEventHandler(this.images_Contextmenu_Opening);
			// 
			// imagesContextMenu_Add
			// 
			this.imagesContextMenu_Add.Name = "imagesContextMenu_Add";
			this.imagesContextMenu_Add.Size = new System.Drawing.Size(117, 22);
			this.imagesContextMenu_Add.Text = "Add...";
			this.imagesContextMenu_Add.Click += new System.EventHandler(this.imagesContextMenu_Add_Click);
			// 
			// imagesContextMenu_Edit
			// 
			this.imagesContextMenu_Edit.Name = "imagesContextMenu_Edit";
			this.imagesContextMenu_Edit.Size = new System.Drawing.Size(117, 22);
			this.imagesContextMenu_Edit.Text = "Edit...";
			this.imagesContextMenu_Edit.Click += new System.EventHandler(this.imagesContextMenu_Edit_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(114, 6);
			// 
			// imagesContextMenu_Remove
			// 
			this.imagesContextMenu_Remove.Name = "imagesContextMenu_Remove";
			this.imagesContextMenu_Remove.Size = new System.Drawing.Size(117, 22);
			this.imagesContextMenu_Remove.Text = "Remove";
			this.imagesContextMenu_Remove.Click += new System.EventHandler(this.imagesContextMenu_Remove_Click);
			// 
			// tabPage_WorkerTasks
			// 
			this.tabPage_WorkerTasks.Controls.Add(this._WorkerTasks);
			this.tabPage_WorkerTasks.Location = new System.Drawing.Point(4, 22);
			this.tabPage_WorkerTasks.Name = "tabPage_WorkerTasks";
			this.tabPage_WorkerTasks.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_WorkerTasks.Size = new System.Drawing.Size(605, 199);
			this.tabPage_WorkerTasks.TabIndex = 2;
			this.tabPage_WorkerTasks.Text = "Worker tasks";
			this.tabPage_WorkerTasks.UseVisualStyleBackColor = true;
			// 
			// _WorkerTasks
			// 
			this._WorkerTasks.AllowColumnReorder = true;
			this._WorkerTasks.AllowDrop = true;
			this._WorkerTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.workerColumn_Name,
            this.workerColumn_Maprect,
            this.workerColumn_Imageformat,
            this.workerColumn_Images});
			this._WorkerTasks.ContextMenuStrip = this.workerTasks_ContextMenu;
			this._WorkerTasks.Dock = System.Windows.Forms.DockStyle.Fill;
			this._WorkerTasks.FullRowSelect = true;
			this._WorkerTasks.GridLines = true;
			this._WorkerTasks.Location = new System.Drawing.Point(3, 3);
			this._WorkerTasks.Name = "_WorkerTasks";
			this._WorkerTasks.Size = new System.Drawing.Size(599, 193);
			this._WorkerTasks.TabIndex = 0;
			this._WorkerTasks.UseCompatibleStateImageBehavior = false;
			this._WorkerTasks.View = System.Windows.Forms.View.Details;
			this._WorkerTasks.DoubleClick += new System.EventHandler(this._WorkerTasks_DoubleClick);
			this._WorkerTasks.DragDrop += new System.Windows.Forms.DragEventHandler(this._WorkerTasks_DragDrop);
			this._WorkerTasks.DragEnter += new System.Windows.Forms.DragEventHandler(this._WorkerTasks_DragEnter);
			this._WorkerTasks.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this._WorkerTasks_ItemDrag);
			// 
			// workerColumn_Name
			// 
			this.workerColumn_Name.Text = "Name";
			this.workerColumn_Name.Width = 83;
			// 
			// workerColumn_Maprect
			// 
			this.workerColumn_Maprect.Text = "Maprect";
			this.workerColumn_Maprect.Width = 103;
			// 
			// workerColumn_Imageformat
			// 
			this.workerColumn_Imageformat.Text = "Format";
			this.workerColumn_Imageformat.Width = 51;
			// 
			// workerColumn_Images
			// 
			this.workerColumn_Images.Text = "Images";
			this.workerColumn_Images.Width = 331;
			// 
			// workerTasks_ContextMenu
			// 
			this.workerTasks_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workerTasks_ContextMenu_Add,
            this.workerTasks_ContextMenu_Edit,
            this.toolStripSeparator4,
            this.workerTasks_ContextMenu_Remove});
			this.workerTasks_ContextMenu.Name = "workerTasks_ContextMenu";
			this.workerTasks_ContextMenu.Size = new System.Drawing.Size(118, 76);
			this.workerTasks_ContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.workerTasks_ContextMenu_Opening);
			// 
			// workerTasks_ContextMenu_Add
			// 
			this.workerTasks_ContextMenu_Add.Name = "workerTasks_ContextMenu_Add";
			this.workerTasks_ContextMenu_Add.Size = new System.Drawing.Size(117, 22);
			this.workerTasks_ContextMenu_Add.Text = "Add";
			this.workerTasks_ContextMenu_Add.Click += new System.EventHandler(this.workerTasks_ContextMenu_Add_Click);
			// 
			// workerTasks_ContextMenu_Edit
			// 
			this.workerTasks_ContextMenu_Edit.Name = "workerTasks_ContextMenu_Edit";
			this.workerTasks_ContextMenu_Edit.Size = new System.Drawing.Size(117, 22);
			this.workerTasks_ContextMenu_Edit.Text = "Edit";
			this.workerTasks_ContextMenu_Edit.Click += new System.EventHandler(this.workerTasks_ContextMenu_Edit_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(114, 6);
			// 
			// workerTasks_ContextMenu_Remove
			// 
			this.workerTasks_ContextMenu_Remove.Name = "workerTasks_ContextMenu_Remove";
			this.workerTasks_ContextMenu_Remove.Size = new System.Drawing.Size(117, 22);
			this.workerTasks_ContextMenu_Remove.Text = "Remove";
			this.workerTasks_ContextMenu_Remove.Click += new System.EventHandler(this.workerTasks_ContextMenu_Remove_Click);
			// 
			// tabPage_MapVersions
			// 
			this.tabPage_MapVersions.Controls.Add(this._MapVersions);
			this.tabPage_MapVersions.Location = new System.Drawing.Point(4, 22);
			this.tabPage_MapVersions.Name = "tabPage_MapVersions";
			this.tabPage_MapVersions.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_MapVersions.Size = new System.Drawing.Size(605, 199);
			this.tabPage_MapVersions.TabIndex = 3;
			this.tabPage_MapVersions.Text = "Map Versions";
			this.tabPage_MapVersions.UseVisualStyleBackColor = true;
			// 
			// _MapVersions
			// 
			this._MapVersions.AllowColumnReorder = true;
			this._MapVersions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._MapVersions_File,
            this._MapVersions_Type,
            this._MapVersions_Name,
            this._MapVersions_CoordsFile,
            this._MapVersions_Layers});
			this._MapVersions.ContextMenuStrip = this.mapVersions_ContextMenu;
			this._MapVersions.Dock = System.Windows.Forms.DockStyle.Fill;
			this._MapVersions.FullRowSelect = true;
			this._MapVersions.GridLines = true;
			this._MapVersions.Location = new System.Drawing.Point(3, 3);
			this._MapVersions.Name = "_MapVersions";
			this._MapVersions.ShowGroups = false;
			this._MapVersions.Size = new System.Drawing.Size(599, 193);
			this._MapVersions.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this._MapVersions.TabIndex = 0;
			this._MapVersions.UseCompatibleStateImageBehavior = false;
			this._MapVersions.View = System.Windows.Forms.View.Details;
			this._MapVersions.DoubleClick += new System.EventHandler(this._MapVersions_DoubleClick);
			this._MapVersions.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this._lv_ColumnClick);
			// 
			// _MapVersions_File
			// 
			this._MapVersions_File.Text = "File";
			this._MapVersions_File.Width = 91;
			// 
			// _MapVersions_Type
			// 
			this._MapVersions_Type.Text = "Type";
			this._MapVersions_Type.Width = 80;
			// 
			// _MapVersions_Name
			// 
			this._MapVersions_Name.Text = "Name";
			this._MapVersions_Name.Width = 134;
			// 
			// _MapVersions_CoordsFile
			// 
			this._MapVersions_CoordsFile.Text = "Coords File";
			this._MapVersions_CoordsFile.Width = 125;
			// 
			// _MapVersions_Layers
			// 
			this._MapVersions_Layers.Text = "Layers";
			this._MapVersions_Layers.Width = 163;
			// 
			// mapVersions_ContextMenu
			// 
			this.mapVersions_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapVersions_ContextMenu_Add,
            this.mapVersions_ContextMenu_Edit,
            this.toolStripSeparator3,
            this.mapVersions_ContextMenu_Remove});
			this.mapVersions_ContextMenu.Name = "mapVersions_ContextMenu";
			this.mapVersions_ContextMenu.Size = new System.Drawing.Size(118, 76);
			this.mapVersions_ContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.mapVersions_ContextMenu_Opening);
			// 
			// mapVersions_ContextMenu_Add
			// 
			this.mapVersions_ContextMenu_Add.Name = "mapVersions_ContextMenu_Add";
			this.mapVersions_ContextMenu_Add.Size = new System.Drawing.Size(117, 22);
			this.mapVersions_ContextMenu_Add.Text = "Add";
			this.mapVersions_ContextMenu_Add.Click += new System.EventHandler(this.mapVersions_ContextMenu_Add_Click);
			// 
			// mapVersions_ContextMenu_Edit
			// 
			this.mapVersions_ContextMenu_Edit.Name = "mapVersions_ContextMenu_Edit";
			this.mapVersions_ContextMenu_Edit.Size = new System.Drawing.Size(117, 22);
			this.mapVersions_ContextMenu_Edit.Text = "Edit";
			this.mapVersions_ContextMenu_Edit.Click += new System.EventHandler(this.mapVersions_ContextMenu_Edit_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(114, 6);
			// 
			// mapVersions_ContextMenu_Remove
			// 
			this.mapVersions_ContextMenu_Remove.Name = "mapVersions_ContextMenu_Remove";
			this.mapVersions_ContextMenu_Remove.Size = new System.Drawing.Size(117, 22);
			this.mapVersions_ContextMenu_Remove.Text = "Remove";
			this.mapVersions_ContextMenu_Remove.Click += new System.EventHandler(this.mapVersions_ContextMenu_Remove_Click);
			// 
			// tabPage_Compile
			// 
			this.tabPage_Compile.Controls.Add(this.splitContainer1);
			this.tabPage_Compile.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Compile.Name = "tabPage_Compile";
			this.tabPage_Compile.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_Compile.Size = new System.Drawing.Size(605, 199);
			this.tabPage_Compile.TabIndex = 4;
			this.tabPage_Compile.Tag = "Compile";
			this.tabPage_Compile.Text = "Compile";
			this.tabPage_Compile.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Panel1.Controls.Add(this.button_docompile);
			this.splitContainer1.Panel1.Controls.Add(this._label_DebugMessages);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this._compiler_debugmessages);
			this.splitContainer1.Size = new System.Drawing.Size(599, 193);
			this.splitContainer1.SplitterDistance = 117;
			this.splitContainer1.TabIndex = 6;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.96916F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.03084F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 252F));
			this.tableLayoutPanel1.Controls.Add(this._label_compilestatusmsg, 2, 4);
			this.tableLayoutPanel1.Controls.Add(this.progressBar_assembler, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.progressBar_worker, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.progressBar_imageloader, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.progressBar_imageslicer, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label12, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.statuslabel_imageloader, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.statuslabel_imageslicer, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.statuslabel_worker, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.statuslabel_assembler, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(455, 87);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// _label_compilestatusmsg
			// 
			this._label_compilestatusmsg.AutoSize = true;
			this._label_compilestatusmsg.Location = new System.Drawing.Point(204, 69);
			this._label_compilestatusmsg.Name = "_label_compilestatusmsg";
			this._label_compilestatusmsg.Size = new System.Drawing.Size(0, 13);
			this._label_compilestatusmsg.TabIndex = 6;
			// 
			// progressBar_assembler
			// 
			this.progressBar_assembler.Location = new System.Drawing.Point(86, 55);
			this.progressBar_assembler.Name = "progressBar_assembler";
			this.progressBar_assembler.Size = new System.Drawing.Size(111, 10);
			this.progressBar_assembler.TabIndex = 8;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(4, 18);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(65, 13);
			this.label9.TabIndex = 0;
			this.label9.Text = "Image Slicer";
			// 
			// progressBar_worker
			// 
			this.progressBar_worker.Location = new System.Drawing.Point(86, 38);
			this.progressBar_worker.Name = "progressBar_worker";
			this.progressBar_worker.Size = new System.Drawing.Size(111, 10);
			this.progressBar_worker.TabIndex = 7;
			// 
			// progressBar_imageloader
			// 
			this.progressBar_imageloader.Location = new System.Drawing.Point(86, 4);
			this.progressBar_imageloader.Name = "progressBar_imageloader";
			this.progressBar_imageloader.Size = new System.Drawing.Size(111, 10);
			this.progressBar_imageloader.TabIndex = 4;
			// 
			// progressBar_imageslicer
			// 
			this.progressBar_imageslicer.Location = new System.Drawing.Point(86, 21);
			this.progressBar_imageslicer.Name = "progressBar_imageslicer";
			this.progressBar_imageslicer.Size = new System.Drawing.Size(111, 10);
			this.progressBar_imageslicer.TabIndex = 6;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(4, 1);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 13);
			this.label10.TabIndex = 1;
			this.label10.Text = "Image Loader";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(4, 35);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(42, 13);
			this.label11.TabIndex = 2;
			this.label11.Text = "Worker";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(4, 52);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(55, 13);
			this.label12.TabIndex = 3;
			this.label12.Text = "Assembler";
			// 
			// statuslabel_imageloader
			// 
			this.statuslabel_imageloader.AutoSize = true;
			this.statuslabel_imageloader.Location = new System.Drawing.Point(204, 1);
			this.statuslabel_imageloader.Name = "statuslabel_imageloader";
			this.statuslabel_imageloader.Size = new System.Drawing.Size(0, 13);
			this.statuslabel_imageloader.TabIndex = 9;
			// 
			// statuslabel_imageslicer
			// 
			this.statuslabel_imageslicer.AutoSize = true;
			this.statuslabel_imageslicer.Location = new System.Drawing.Point(204, 18);
			this.statuslabel_imageslicer.Name = "statuslabel_imageslicer";
			this.statuslabel_imageslicer.Size = new System.Drawing.Size(0, 13);
			this.statuslabel_imageslicer.TabIndex = 10;
			// 
			// statuslabel_worker
			// 
			this.statuslabel_worker.AutoSize = true;
			this.statuslabel_worker.Location = new System.Drawing.Point(204, 35);
			this.statuslabel_worker.Name = "statuslabel_worker";
			this.statuslabel_worker.Size = new System.Drawing.Size(0, 13);
			this.statuslabel_worker.TabIndex = 11;
			// 
			// statuslabel_assembler
			// 
			this.statuslabel_assembler.AutoSize = true;
			this.statuslabel_assembler.Location = new System.Drawing.Point(204, 52);
			this.statuslabel_assembler.Name = "statuslabel_assembler";
			this.statuslabel_assembler.Size = new System.Drawing.Size(0, 13);
			this.statuslabel_assembler.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Status";
			// 
			// button_docompile
			// 
			this.button_docompile.Location = new System.Drawing.Point(194, 94);
			this.button_docompile.Name = "button_docompile";
			this.button_docompile.Size = new System.Drawing.Size(75, 23);
			this.button_docompile.TabIndex = 0;
			this.button_docompile.Text = "Compile";
			this.button_docompile.UseVisualStyleBackColor = true;
			this.button_docompile.Click += new System.EventHandler(this.button_docompile_Click);
			// 
			// _label_DebugMessages
			// 
			this._label_DebugMessages.AutoSize = true;
			this._label_DebugMessages.Location = new System.Drawing.Point(8, 104);
			this._label_DebugMessages.Name = "_label_DebugMessages";
			this._label_DebugMessages.Size = new System.Drawing.Size(89, 13);
			this._label_DebugMessages.TabIndex = 2;
			this._label_DebugMessages.Text = "Debug messages";
			// 
			// _compiler_debugmessages
			// 
			this._compiler_debugmessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this._compiler_debugmessages.Location = new System.Drawing.Point(0, 0);
			this._compiler_debugmessages.Multiline = true;
			this._compiler_debugmessages.Name = "_compiler_debugmessages";
			this._compiler_debugmessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._compiler_debugmessages.Size = new System.Drawing.Size(599, 72);
			this._compiler_debugmessages.TabIndex = 3;
			// 
			// _OpenMapConfig
			// 
			this._OpenMapConfig.DefaultExt = "xml";
			this._OpenMapConfig.FileName = "*.xml";
			this._OpenMapConfig.Filter = "Map Config Files|*.xml|All Files|*.*";
			// 
			// _SaveMapConfig
			// 
			this._SaveMapConfig.DefaultExt = "xml";
			this._SaveMapConfig.Filter = "Map Config Files|*.xml|All files|*.*";
			// 
			// splitContainer_Secondary
			// 
			this.splitContainer_Secondary.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer_Secondary.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer_Secondary.IsSplitterFixed = true;
			this.splitContainer_Secondary.Location = new System.Drawing.Point(0, 0);
			this.splitContainer_Secondary.Name = "splitContainer_Secondary";
			this.splitContainer_Secondary.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer_Secondary.Panel1
			// 
			this.splitContainer_Secondary.Panel1.Controls.Add(this.tabControl1);
			this.splitContainer_Secondary.Panel1MinSize = 150;
			// 
			// splitContainer_Secondary.Panel2
			// 
			this.splitContainer_Secondary.Panel2.Controls.Add(this._HelperBox);
			this.splitContainer_Secondary.Panel2MinSize = 75;
			this.splitContainer_Secondary.Size = new System.Drawing.Size(613, 301);
			this.splitContainer_Secondary.SplitterDistance = 225;
			this.splitContainer_Secondary.SplitterWidth = 1;
			this.splitContainer_Secondary.TabIndex = 3;
			// 
			// _HelperBox
			// 
			this._HelperBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._HelperBox.Location = new System.Drawing.Point(0, 0);
			this._HelperBox.Multiline = true;
			this._HelperBox.Name = "_HelperBox";
			this._HelperBox.ReadOnly = true;
			this._HelperBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._HelperBox.Size = new System.Drawing.Size(613, 75);
			this._HelperBox.TabIndex = 0;
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(613, 24);
			this.menuStrip.TabIndex = 4;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem1
			// 
			this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem1,
            this.openToolStripMenuItem1,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem1,
            this.toolStripSeparator7,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator8,
            this.exitToolStripMenuItem1});
			this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
			this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem1.Text = "&File";
			// 
			// newToolStripMenuItem1
			// 
			this.newToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem1.Image")));
			this.newToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
			this.newToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
			this.newToolStripMenuItem1.Text = "&New";
			// 
			// openToolStripMenuItem1
			// 
			this.openToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem1.Image")));
			this.openToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
			this.openToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
			this.openToolStripMenuItem1.Text = "&Open";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
			// 
			// saveToolStripMenuItem1
			// 
			this.saveToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem1.Image")));
			this.saveToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
			this.saveToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
			this.saveToolStripMenuItem1.Text = "&Save";
			// 
			// saveAsToolStripMenuItem1
			// 
			this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
			this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
			this.saveAsToolStripMenuItem1.Text = "Save &As";
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(140, 6);
			// 
			// printToolStripMenuItem
			// 
			this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
			this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.printToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.printToolStripMenuItem.Text = "&Print";
			// 
			// printPreviewToolStripMenuItem
			// 
			this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
			this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(140, 6);
			// 
			// exitToolStripMenuItem1
			// 
			this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
			this.exitToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
			this.exitToolStripMenuItem1.Text = "E&xit";
			// 
			// toolsToolStripMenuItem1
			// 
			this.toolsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem1});
			this.toolsToolStripMenuItem1.Name = "toolsToolStripMenuItem1";
			this.toolsToolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem1.Text = "&Tools";
			// 
			// optionsToolStripMenuItem1
			// 
			this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
			this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
			this.optionsToolStripMenuItem1.Text = "&Options";
			// 
			// helpToolStripMenuItem1
			// 
			this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem1,
            this.indexToolStripMenuItem1,
            this.searchToolStripMenuItem1,
            this.toolStripSeparator11,
            this.aboutToolStripMenuItem1});
			this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem1.Text = "&Help";
			// 
			// contentsToolStripMenuItem1
			// 
			this.contentsToolStripMenuItem1.Name = "contentsToolStripMenuItem1";
			this.contentsToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
			this.contentsToolStripMenuItem1.Text = "&Contents";
			// 
			// indexToolStripMenuItem1
			// 
			this.indexToolStripMenuItem1.Name = "indexToolStripMenuItem1";
			this.indexToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
			this.indexToolStripMenuItem1.Text = "&Index";
			// 
			// searchToolStripMenuItem1
			// 
			this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
			this.searchToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
			this.searchToolStripMenuItem1.Text = "&Search";
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(119, 6);
			// 
			// aboutToolStripMenuItem1
			// 
			this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
			this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
			this.aboutToolStripMenuItem1.Text = "&About...";
			// 
			// splitContainer_Main
			// 
			this.splitContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer_Main.IsSplitterFixed = true;
			this.splitContainer_Main.Location = new System.Drawing.Point(0, 0);
			this.splitContainer_Main.Name = "splitContainer_Main";
			this.splitContainer_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer_Main.Panel1
			// 
			this.splitContainer_Main.Panel1.Controls.Add(this.menuStrip);
			// 
			// splitContainer_Main.Panel2
			// 
			this.splitContainer_Main.Panel2.Controls.Add(this.splitContainer_Secondary);
			this.splitContainer_Main.Size = new System.Drawing.Size(613, 327);
			this.splitContainer_Main.SplitterDistance = 25;
			this.splitContainer_Main.SplitterWidth = 1;
			this.splitContainer_Main.TabIndex = 5;
			// 
			// numericUpDown_SlicePadding
			// 
			this.numericUpDown_SlicePadding.Location = new System.Drawing.Point(104, 41);
			this.numericUpDown_SlicePadding.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDown_SlicePadding.Name = "numericUpDown_SlicePadding";
			this.numericUpDown_SlicePadding.Size = new System.Drawing.Size(64, 20);
			this.numericUpDown_SlicePadding.TabIndex = 90;
			this.numericUpDown_SlicePadding.ValueChanged += new System.EventHandler(this.numericUpDown_SlicePadding_ValueChanged);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(171, 43);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(23, 13);
			this.label14.TabIndex = 92;
			this.label14.Text = "KiB";
			// 
			// checkBox_SlicePaddingEnabled
			// 
			this.checkBox_SlicePaddingEnabled.AutoSize = true;
			this.checkBox_SlicePaddingEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox_SlicePaddingEnabled.Location = new System.Drawing.Point(6, 44);
			this.checkBox_SlicePaddingEnabled.Name = "checkBox_SlicePaddingEnabled";
			this.checkBox_SlicePaddingEnabled.Size = new System.Drawing.Size(91, 17);
			this.checkBox_SlicePaddingEnabled.TabIndex = 93;
			this.checkBox_SlicePaddingEnabled.Text = "Slice Padding";
			this.checkBox_SlicePaddingEnabled.UseVisualStyleBackColor = true;
			this.checkBox_SlicePaddingEnabled.CheckedChanged += new System.EventHandler(this.checkBox_SlicePaddingEnabled_CheckedChanged);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(613, 327);
			this.Controls.Add(this.splitContainer_Main);
			this.MinimumSize = new System.Drawing.Size(629, 365);
			this.Name = "MainWindow";
			this.Text = "Anarchy Online Map Compiler (C#)";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this._map_version_major)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._map_version_minor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._map_version_build)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._map_texturesize)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage_General.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage_Images.ResumeLayout(false);
			this.images_Contextmenu.ResumeLayout(false);
			this.tabPage_WorkerTasks.ResumeLayout(false);
			this.workerTasks_ContextMenu.ResumeLayout(false);
			this.tabPage_MapVersions.ResumeLayout(false);
			this.mapVersions_ContextMenu.ResumeLayout(false);
			this.tabPage_Compile.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.splitContainer_Secondary.Panel1.ResumeLayout(false);
			this.splitContainer_Secondary.Panel2.ResumeLayout(false);
			this.splitContainer_Secondary.Panel2.PerformLayout();
			this.splitContainer_Secondary.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.splitContainer_Main.Panel1.ResumeLayout(false);
			this.splitContainer_Main.Panel1.PerformLayout();
			this.splitContainer_Main.Panel2.ResumeLayout(false);
			this.splitContainer_Main.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SlicePadding)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker _bw_Compiler;
		private System.Windows.Forms.NumericUpDown _map_version_build;
		private System.Windows.Forms.NumericUpDown _map_version_minor;
		private System.Windows.Forms.NumericUpDown _map_version_major;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _map_name;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _map_subdirectory;
		private System.Windows.Forms.ComboBox _map_assemblymethod;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown _map_texturesize;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage_General;
		private System.Windows.Forms.TabPage tabPage_Images;
		private System.Windows.Forms.ListView _imagelist;
		private System.Windows.Forms.ColumnHeader columnHeader_Name;
		private System.Windows.Forms.ColumnHeader columnHeader_Path;
		private System.Windows.Forms.ContextMenuStrip images_Contextmenu;
		private System.Windows.Forms.ToolStripMenuItem imagesContextMenu_Add;
		private System.Windows.Forms.ToolStripMenuItem imagesContextMenu_Edit;
		private System.Windows.Forms.ToolStripMenuItem imagesContextMenu_Remove;
		private System.Windows.Forms.TabPage tabPage_WorkerTasks;
		private System.Windows.Forms.ListView _WorkerTasks;
		private System.Windows.Forms.ColumnHeader workerColumn_Name;
		private System.Windows.Forms.ColumnHeader workerColumn_Images;
		private System.Windows.Forms.ColumnHeader workerColumn_Maprect;
		private System.Windows.Forms.ContextMenuStrip workerTasks_ContextMenu;
		private System.Windows.Forms.ToolStripMenuItem workerTasks_ContextMenu_Add;
		private System.Windows.Forms.ToolStripMenuItem workerTasks_ContextMenu_Edit;
		private System.Windows.Forms.ToolStripMenuItem workerTasks_ContextMenu_Remove;
		private System.Windows.Forms.TabPage tabPage_MapVersions;
		private System.Windows.Forms.ListView _MapVersions;
		private System.Windows.Forms.ColumnHeader _MapVersions_File;
		private System.Windows.Forms.ColumnHeader _MapVersions_Type;
		private System.Windows.Forms.ColumnHeader _MapVersions_Name;
		private System.Windows.Forms.ColumnHeader _MapVersions_CoordsFile;
		private System.Windows.Forms.ColumnHeader _MapVersions_Layers;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ContextMenuStrip mapVersions_ContextMenu;
		private System.Windows.Forms.ToolStripMenuItem mapVersions_ContextMenu_Add;
		private System.Windows.Forms.ToolStripMenuItem mapVersions_ContextMenu_Edit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem mapVersions_ContextMenu_Remove;
		private System.Windows.Forms.TabPage tabPage_Compile;
		private System.Windows.Forms.Button button_docompile;
		private System.Windows.Forms.Label _label_DebugMessages;
		private System.Windows.Forms.OpenFileDialog _OpenMapConfig;
		private System.Windows.Forms.TextBox _compiler_debugmessages;
		private System.Windows.Forms.SaveFileDialog _SaveMapConfig;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ProgressBar progressBar_imageloader;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ProgressBar progressBar_assembler;
		private System.Windows.Forms.ProgressBar progressBar_worker;
		private System.Windows.Forms.ProgressBar progressBar_imageslicer;
		private System.Windows.Forms.Label statuslabel_imageloader;
		private System.Windows.Forms.Label statuslabel_imageslicer;
		private System.Windows.Forms.Label statuslabel_worker;
		private System.Windows.Forms.Label statuslabel_assembler;
		private System.Windows.Forms.SplitContainer splitContainer_Secondary;
		private System.Windows.Forms.TextBox _HelperBox;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
		private System.Windows.Forms.SplitContainer splitContainer_Main;
		private System.Windows.Forms.ColumnHeader workerColumn_Imageformat;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label _label_compilestatusmsg;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox _map_binname;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox _map_OutFolder;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem2;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.NumericUpDown numericUpDown_SlicePadding;
		private System.Windows.Forms.CheckBox checkBox_SlicePaddingEnabled;
	}
}

