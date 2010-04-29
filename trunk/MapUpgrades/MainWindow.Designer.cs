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
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this._mapNav = new System.Windows.Forms.TextBox();
			this._inputText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
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
			this.timer_UpdateUploadable = new System.Windows.Forms.Timer(this.components);
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.button1);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this._mapNav);
			this.splitContainer1.Panel1.Controls.Add(this._inputText);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(474, 422);
			this.splitContainer1.SplitterDistance = 143;
			this.splitContainer1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(175, 113);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Apply";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "MapNav:";
			// 
			// _mapNav
			// 
			this._mapNav.Location = new System.Drawing.Point(69, 113);
			this._mapNav.Name = "_mapNav";
			this._mapNav.Size = new System.Drawing.Size(100, 20);
			this._mapNav.TabIndex = 3;
			// 
			// _inputText
			// 
			this._inputText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this._inputText.Location = new System.Drawing.Point(3, 25);
			this._inputText.Multiline = true;
			this._inputText.Name = "_inputText";
			this._inputText.Size = new System.Drawing.Size(459, 82);
			this._inputText.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(123, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(115, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Pase from target-self+t:";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(474, 275);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.Uploadable);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(466, 249);
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
			this.Uploadable.Size = new System.Drawing.Size(460, 243);
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
			this.tabPage3.Size = new System.Drawing.Size(466, 249);
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
			this.Available.Size = new System.Drawing.Size(460, 243);
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
			this.tabPage2.Size = new System.Drawing.Size(466, 249);
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
			this.Uploaded.Size = new System.Drawing.Size(460, 243);
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
			// timer_UpdateUploadable
			// 
			this.timer_UpdateUploadable.Interval = 500;
			this.timer_UpdateUploadable.Tick += new System.EventHandler(this.timer_RemoveUploadedItems_Action);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 422);
			this.Controls.Add(this.splitContainer1);
			this.Name = "MainWindow";
			this.Text = "What maps am I missing?";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
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
    }
}

