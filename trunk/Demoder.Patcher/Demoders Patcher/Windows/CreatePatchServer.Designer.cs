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
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_GUID = new System.Windows.Forms.TextBox();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.listView_distributions = new System.Windows.Forms.ListView();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_version = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_Name = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_PatchServerURLs = new System.Windows.Forms.TextBox();
			this.contextMenuStrip_Distributions = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.contextMenuStrip_Distributions.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(177, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "GUID";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Distributions";
			// 
			// textBox_GUID
			// 
			this.textBox_GUID.Location = new System.Drawing.Point(217, 38);
			this.textBox_GUID.Multiline = true;
			this.textBox_GUID.Name = "textBox_GUID";
			this.textBox_GUID.Size = new System.Drawing.Size(209, 20);
			this.textBox_GUID.TabIndex = 5;
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(218, 311);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(75, 23);
			this.button_ok.TabIndex = 8;
			this.button_ok.Text = "OK";
			this.button_ok.UseVisualStyleBackColor = true;
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Location = new System.Drawing.Point(12, 310);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(75, 23);
			this.button_cancel.TabIndex = 9;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// listView_distributions
			// 
			this.listView_distributions.ContextMenuStrip = this.contextMenuStrip_Distributions;
			this.listView_distributions.GridLines = true;
			this.listView_distributions.Location = new System.Drawing.Point(16, 82);
			this.listView_distributions.Name = "listView_distributions";
			this.listView_distributions.Size = new System.Drawing.Size(410, 133);
			this.listView_distributions.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listView_distributions.TabIndex = 10;
			this.listView_distributions.UseCompatibleStateImageBehavior = false;
			this.listView_distributions.View = System.Windows.Forms.View.Details;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Version";
			// 
			// textBox_version
			// 
			this.textBox_version.Location = new System.Drawing.Point(69, 38);
			this.textBox_version.Name = "textBox_version";
			this.textBox_version.Size = new System.Drawing.Size(102, 20);
			this.textBox_version.TabIndex = 12;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name";
			// 
			// textBox_Name
			// 
			this.textBox_Name.Location = new System.Drawing.Point(69, 9);
			this.textBox_Name.Name = "textBox_Name";
			this.textBox_Name.Size = new System.Drawing.Size(357, 20);
			this.textBox_Name.TabIndex = 4;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 218);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(158, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Patchserver URLs (one per line)";
			// 
			// textBox_PatchServerURLs
			// 
			this.textBox_PatchServerURLs.AcceptsReturn = true;
			this.textBox_PatchServerURLs.Location = new System.Drawing.Point(16, 234);
			this.textBox_PatchServerURLs.Multiline = true;
			this.textBox_PatchServerURLs.Name = "textBox_PatchServerURLs";
			this.textBox_PatchServerURLs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_PatchServerURLs.Size = new System.Drawing.Size(410, 70);
			this.textBox_PatchServerURLs.TabIndex = 15;
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
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
			// 
			// CreatePatchServer
			// 
			this.AcceptButton = this.button_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(431, 345);
			this.Controls.Add(this.textBox_PatchServerURLs);
			this.Controls.Add(this.textBox_version);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listView_distributions);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.textBox_GUID);
			this.Controls.Add(this.textBox_Name);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "CreatePatchServer";
			this.Text = "Create PatchServer";
			this.Load += new System.EventHandler(this.CreateDistribution_Load);
			this.contextMenuStrip_Distributions.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_GUID;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_cancel;
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

	}
}