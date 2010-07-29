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
namespace MapUpgrades
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this._inputText = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this._mapNav = new System.Windows.Forms.TextBox();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.AOxPort_ListView = new System.Windows.Forms.ListView();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.Uploadable = new System.Windows.Forms.ListView();
			this.uploadable_Item = new System.Windows.Forms.ColumnHeader();
			this.uploadable_MapNavReq = new System.Windows.Forms.ColumnHeader();
			this.uploadable_Comment = new System.Windows.Forms.ColumnHeader();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.Available = new System.Windows.Forms.ListView();
			this.Available_Item = new System.Windows.Forms.ColumnHeader();
			this.Available_MapNavReq = new System.Windows.Forms.ColumnHeader();
			this.Available_Comment = new System.Windows.Forms.ColumnHeader();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.Uploaded = new System.Windows.Forms.ListView();
			this.Uploaded_Item = new System.Windows.Forms.ColumnHeader();
			this.Uploaded_MapNavReq = new System.Windows.Forms.ColumnHeader();
			this.Uploaded_Comment = new System.Windows.Forms.ColumnHeader();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.ActivePFs = new System.Windows.Forms.ListView();
			this.button_SaveActivePFs = new System.Windows.Forms.Button();
			this.timer_UpdateUploadable = new System.Windows.Forms.Timer(this.components);
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.mapsReaderDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.activePFDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.checkForProgramUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.linksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.anarchyOnlineToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.anarchyOnlineUniverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.demodersToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.famousLastWordsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.howToUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabControl2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(986, 452);
			this.splitContainer1.SplitterDistance = 165;
			this.splitContainer1.TabIndex = 0;
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.tabPage5);
			this.tabControl2.Controls.Add(this.tabPage6);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.Location = new System.Drawing.Point(0, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(986, 165);
			this.tabControl2.TabIndex = 5;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this._inputText);
			this.tabPage5.Controls.Add(this.button1);
			this.tabPage5.Controls.Add(this.label1);
			this.tabPage5.Controls.Add(this.label2);
			this.tabPage5.Controls.Add(this._mapNav);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(978, 139);
			this.tabPage5.TabIndex = 0;
			this.tabPage5.Text = "Manual";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// _inputText
			// 
			this._inputText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._inputText.Location = new System.Drawing.Point(8, 22);
			this._inputText.Multiline = true;
			this._inputText.Name = "_inputText";
			this._inputText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._inputText.Size = new System.Drawing.Size(459, 82);
			this._inputText.TabIndex = 1;
			this._inputText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._anyTxt_KeyPress);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(180, 110);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Apply";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Paste from target-self+t:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 113);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "MapNav:";
			// 
			// _mapNav
			// 
			this._mapNav.Location = new System.Drawing.Point(74, 110);
			this._mapNav.Name = "_mapNav";
			this._mapNav.Size = new System.Drawing.Size(100, 20);
			this._mapNav.TabIndex = 3;
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.AOxPort_ListView);
			this.tabPage6.Location = new System.Drawing.Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage6.Size = new System.Drawing.Size(978, 139);
			this.tabPage6.TabIndex = 1;
			this.tabPage6.Text = "AOxPort";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// AOxPort_ListView
			// 
			this.AOxPort_ListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AOxPort_ListView.Location = new System.Drawing.Point(3, 3);
			this.AOxPort_ListView.MultiSelect = false;
			this.AOxPort_ListView.Name = "AOxPort_ListView";
			this.AOxPort_ListView.Size = new System.Drawing.Size(972, 133);
			this.AOxPort_ListView.TabIndex = 0;
			this.AOxPort_ListView.UseCompatibleStateImageBehavior = false;
			this.AOxPort_ListView.View = System.Windows.Forms.View.List;
			this.AOxPort_ListView.SelectedIndexChanged += new System.EventHandler(this.AOxPort_ListView_SelectedIndexChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(986, 283);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.Uploadable);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(978, 257);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Uploadable";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// Uploadable
			// 
			this.Uploadable.CheckBoxes = true;
			this.Uploadable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.uploadable_Item,
            this.uploadable_MapNavReq,
            this.uploadable_Comment});
			this.Uploadable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Uploadable.Location = new System.Drawing.Point(3, 3);
			this.Uploadable.Name = "Uploadable";
			this.Uploadable.Size = new System.Drawing.Size(972, 251);
			this.Uploadable.TabIndex = 0;
			this.Uploadable.UseCompatibleStateImageBehavior = false;
			this.Uploadable.View = System.Windows.Forms.View.Details;
			this.Uploadable.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.Uploadable_ItemChecked);
			this.Uploadable.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this._lv_ColumnClick);
			// 
			// uploadable_Item
			// 
			this.uploadable_Item.Text = "Item";
			this.uploadable_Item.Width = 205;
			// 
			// uploadable_MapNavReq
			// 
			this.uploadable_MapNavReq.Text = "MapNav";
			this.uploadable_MapNavReq.Width = 79;
			// 
			// uploadable_Comment
			// 
			this.uploadable_Comment.Text = "Comment";
			this.uploadable_Comment.Width = 141;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.Available);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(978, 257);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Available";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// Available
			// 
			this.Available.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Available_Item,
            this.Available_MapNavReq,
            this.Available_Comment});
			this.Available.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Available.Location = new System.Drawing.Point(3, 3);
			this.Available.Name = "Available";
			this.Available.Size = new System.Drawing.Size(972, 251);
			this.Available.TabIndex = 0;
			this.Available.UseCompatibleStateImageBehavior = false;
			this.Available.View = System.Windows.Forms.View.Details;
			this.Available.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this._lv_ColumnClick);
			// 
			// Available_Item
			// 
			this.Available_Item.Text = "Item";
			this.Available_Item.Width = 231;
			// 
			// Available_MapNavReq
			// 
			this.Available_MapNavReq.Text = "MapNav";
			this.Available_MapNavReq.Width = 56;
			// 
			// Available_Comment
			// 
			this.Available_Comment.Text = "Comment";
			this.Available_Comment.Width = 139;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.Uploaded);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(978, 257);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Uploaded";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// Uploaded
			// 
			this.Uploaded.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Uploaded_Item,
            this.Uploaded_MapNavReq,
            this.Uploaded_Comment});
			this.Uploaded.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Uploaded.Location = new System.Drawing.Point(3, 3);
			this.Uploaded.Name = "Uploaded";
			this.Uploaded.Size = new System.Drawing.Size(972, 251);
			this.Uploaded.TabIndex = 0;
			this.Uploaded.UseCompatibleStateImageBehavior = false;
			this.Uploaded.View = System.Windows.Forms.View.Details;
			this.Uploaded.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this._lv_ColumnClick);
			// 
			// Uploaded_Item
			// 
			this.Uploaded_Item.Text = "Item";
			this.Uploaded_Item.Width = 204;
			// 
			// Uploaded_MapNavReq
			// 
			this.Uploaded_MapNavReq.Text = "MapNav";
			this.Uploaded_MapNavReq.Width = 82;
			// 
			// Uploaded_Comment
			// 
			this.Uploaded_Comment.Text = "Comment";
			this.Uploaded_Comment.Width = 145;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.splitContainer2);
			this.tabPage4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(978, 257);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Active PFs";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.ActivePFs);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.button_SaveActivePFs);
			this.splitContainer2.Size = new System.Drawing.Size(972, 251);
			this.splitContainer2.SplitterDistance = 222;
			this.splitContainer2.TabIndex = 0;
			// 
			// ActivePFs
			// 
			this.ActivePFs.CheckBoxes = true;
			this.ActivePFs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ActivePFs.Location = new System.Drawing.Point(0, 0);
			this.ActivePFs.Name = "ActivePFs";
			this.ActivePFs.Size = new System.Drawing.Size(972, 222);
			this.ActivePFs.TabIndex = 0;
			this.ActivePFs.UseCompatibleStateImageBehavior = false;
			this.ActivePFs.View = System.Windows.Forms.View.List;
			this.ActivePFs.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ActivePFs_ItemChecked);
			// 
			// button_SaveActivePFs
			// 
			this.button_SaveActivePFs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button_SaveActivePFs.Location = new System.Drawing.Point(0, 0);
			this.button_SaveActivePFs.Name = "button_SaveActivePFs";
			this.button_SaveActivePFs.Size = new System.Drawing.Size(972, 25);
			this.button_SaveActivePFs.TabIndex = 0;
			this.button_SaveActivePFs.Text = "Save changes";
			this.button_SaveActivePFs.UseVisualStyleBackColor = true;
			this.button_SaveActivePFs.Click += new System.EventHandler(this.button_SaveActivePFs_Click);
			// 
			// timer_UpdateUploadable
			// 
			this.timer_UpdateUploadable.Interval = 500;
			this.timer_UpdateUploadable.Tick += new System.EventHandler(this.timer_RemoveUploadedItems_Action);
			// 
			// toolStripContainer1
			// 
			this.toolStripContainer1.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(986, 452);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(986, 476);
			this.toolStripContainer1.TabIndex = 5;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(986, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem1,
            this.optionsToolStripMenuItem});
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.settingsToolStripMenuItem.Text = "Tools";
			// 
			// updateToolStripMenuItem1
			// 
			this.updateToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapsReaderDefinitionsToolStripMenuItem,
            this.activePFDefinitionsToolStripMenuItem,
            this.toolStripSeparator3,
            this.checkForProgramUpdateToolStripMenuItem});
			this.updateToolStripMenuItem1.Name = "updateToolStripMenuItem1";
			this.updateToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
			this.updateToolStripMenuItem1.Text = "Update";
			// 
			// mapsReaderDefinitionsToolStripMenuItem
			// 
			this.mapsReaderDefinitionsToolStripMenuItem.Name = "mapsReaderDefinitionsToolStripMenuItem";
			this.mapsReaderDefinitionsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.mapsReaderDefinitionsToolStripMenuItem.Text = "Item Definitions";
			// 
			// activePFDefinitionsToolStripMenuItem
			// 
			this.activePFDefinitionsToolStripMenuItem.Name = "activePFDefinitionsToolStripMenuItem";
			this.activePFDefinitionsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.activePFDefinitionsToolStripMenuItem.Text = "Active PF Definitions";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(224, 6);
			// 
			// checkForProgramUpdateToolStripMenuItem
			// 
			this.checkForProgramUpdateToolStripMenuItem.Name = "checkForProgramUpdateToolStripMenuItem";
			this.checkForProgramUpdateToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.checkForProgramUpdateToolStripMenuItem.Text = "Check for application update";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.optionsToolStripMenuItem.Text = "Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.toolStripSeparator1,
            this.linksToolStripMenuItem,
            this.howToUseToolStripMenuItem});
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.aboutToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem1
			// 
			this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
			this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
			this.aboutToolStripMenuItem1.Text = "About";
			this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
			// 
			// linksToolStripMenuItem
			// 
			this.linksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anarchyOnlineToolStripMenuItem1,
            this.anarchyOnlineUniverseToolStripMenuItem,
            this.toolStripSeparator2,
            this.demodersToolsToolStripMenuItem,
            this.famousLastWordsToolStripMenuItem1});
			this.linksToolStripMenuItem.Name = "linksToolStripMenuItem";
			this.linksToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
			this.linksToolStripMenuItem.Text = "Links";
			// 
			// anarchyOnlineToolStripMenuItem1
			// 
			this.anarchyOnlineToolStripMenuItem1.Name = "anarchyOnlineToolStripMenuItem1";
			this.anarchyOnlineToolStripMenuItem1.Size = new System.Drawing.Size(204, 22);
			this.anarchyOnlineToolStripMenuItem1.Text = "Anarchy Online";
			this.anarchyOnlineToolStripMenuItem1.Click += new System.EventHandler(this.anarchyOnlineToolStripMenuItem1_Click);
			// 
			// anarchyOnlineUniverseToolStripMenuItem
			// 
			this.anarchyOnlineUniverseToolStripMenuItem.Name = "anarchyOnlineUniverseToolStripMenuItem";
			this.anarchyOnlineUniverseToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.anarchyOnlineUniverseToolStripMenuItem.Text = "Anarchy Online Universe";
			this.anarchyOnlineUniverseToolStripMenuItem.Click += new System.EventHandler(this.anarchyOnlineUniverseToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
			// 
			// demodersToolsToolStripMenuItem
			// 
			this.demodersToolsToolStripMenuItem.Name = "demodersToolsToolStripMenuItem";
			this.demodersToolsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.demodersToolsToolStripMenuItem.Text = "Demoders Tools";
			this.demodersToolsToolStripMenuItem.Click += new System.EventHandler(this.demodersToolsToolStripMenuItem_Click);
			// 
			// famousLastWordsToolStripMenuItem1
			// 
			this.famousLastWordsToolStripMenuItem1.Name = "famousLastWordsToolStripMenuItem1";
			this.famousLastWordsToolStripMenuItem1.Size = new System.Drawing.Size(204, 22);
			this.famousLastWordsToolStripMenuItem1.Text = "Famous Last Words";
			this.famousLastWordsToolStripMenuItem1.Click += new System.EventHandler(this.famousLastWordsToolStripMenuItem1_Click);
			// 
			// howToUseToolStripMenuItem
			// 
			this.howToUseToolStripMenuItem.Name = "howToUseToolStripMenuItem";
			this.howToUseToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
			this.howToUseToolStripMenuItem.Text = "How to use";
			this.howToUseToolStripMenuItem.Click += new System.EventHandler(this.howToUseToolStripMenuItem_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(986, 476);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainWindow";
			this.Text = "What maps am I missing?";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			this.tabPage6.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox _mapNav;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _inputText;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ListView Uploadable;
		private System.Windows.Forms.ColumnHeader uploadable_Item;
		private System.Windows.Forms.ColumnHeader uploadable_MapNavReq;
		private System.Windows.Forms.ColumnHeader uploadable_Comment;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView Uploaded;
		private System.Windows.Forms.ColumnHeader Uploaded_Item;
		private System.Windows.Forms.ColumnHeader Uploaded_MapNavReq;
		private System.Windows.Forms.ColumnHeader Uploaded_Comment;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.ListView Available;
		private System.Windows.Forms.ColumnHeader Available_Item;
		private System.Windows.Forms.ColumnHeader Available_MapNavReq;
		private System.Windows.Forms.ColumnHeader Available_Comment;
		private System.Windows.Forms.Timer timer_UpdateUploadable;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem linksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem anarchyOnlineToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem anarchyOnlineUniverseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem famousLastWordsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem demodersToolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Button button_SaveActivePFs;
		private System.Windows.Forms.ListView ActivePFs;
		private System.Windows.Forms.ToolStripMenuItem howToUseToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem mapsReaderDefinitionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem activePFDefinitionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem checkForProgramUpdateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ListView AOxPort_ListView;
    }
}

