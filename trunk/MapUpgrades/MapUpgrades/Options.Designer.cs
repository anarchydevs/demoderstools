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
	partial class Options
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.AOxPort_BrowsePath = new System.Windows.Forms.Button();
			this.AOxPort_Path = new System.Windows.Forms.TextBox();
			this.folderBrowserDialog_AOxPorts = new System.Windows.Forms.FolderBrowserDialog();
			this.button_Apply = new System.Windows.Forms.Button();
			this.button_Discard = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.inputmethod_Manual = new System.Windows.Forms.RadioButton();
			this.inputmethod_AOxPort = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.AOxPort_BrowsePath);
			this.groupBox1.Controls.Add(this.AOxPort_Path);
			this.groupBox1.Location = new System.Drawing.Point(159, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(406, 48);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "AOxPort settings";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Export directory";
			// 
			// AOxPort_BrowsePath
			// 
			this.AOxPort_BrowsePath.Location = new System.Drawing.Point(356, 15);
			this.AOxPort_BrowsePath.Name = "AOxPort_BrowsePath";
			this.AOxPort_BrowsePath.Size = new System.Drawing.Size(47, 21);
			this.AOxPort_BrowsePath.TabIndex = 1;
			this.AOxPort_BrowsePath.Text = "...";
			this.AOxPort_BrowsePath.UseVisualStyleBackColor = true;
			this.AOxPort_BrowsePath.Click += new System.EventHandler(this.AOxPort_BrowsePath_Click);
			// 
			// AOxPort_Path
			// 
			this.AOxPort_Path.Location = new System.Drawing.Point(92, 16);
			this.AOxPort_Path.Name = "AOxPort_Path";
			this.AOxPort_Path.Size = new System.Drawing.Size(258, 20);
			this.AOxPort_Path.TabIndex = 0;
			// 
			// folderBrowserDialog_AOxPorts
			// 
			this.folderBrowserDialog_AOxPorts.Description = "Please choose the directory AOxPort exports character configurations to";
			// 
			// button_Apply
			// 
			this.button_Apply.Location = new System.Drawing.Point(143, 130);
			this.button_Apply.Name = "button_Apply";
			this.button_Apply.Size = new System.Drawing.Size(75, 23);
			this.button_Apply.TabIndex = 1;
			this.button_Apply.Text = "Apply";
			this.button_Apply.UseVisualStyleBackColor = true;
			this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
			// 
			// button_Discard
			// 
			this.button_Discard.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_Discard.Location = new System.Drawing.Point(224, 130);
			this.button_Discard.Name = "button_Discard";
			this.button_Discard.Size = new System.Drawing.Size(75, 23);
			this.button_Discard.TabIndex = 2;
			this.button_Discard.Text = "Discard";
			this.button_Discard.UseVisualStyleBackColor = true;
			this.button_Discard.Click += new System.EventHandler(this.button_Discard_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.inputmethod_AOxPort);
			this.groupBox2.Controls.Add(this.inputmethod_Manual);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(141, 87);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Default Input Method";
			// 
			// inputmethod_Manual
			// 
			this.inputmethod_Manual.AutoSize = true;
			this.inputmethod_Manual.Location = new System.Drawing.Point(7, 20);
			this.inputmethod_Manual.Name = "inputmethod_Manual";
			this.inputmethod_Manual.Size = new System.Drawing.Size(60, 17);
			this.inputmethod_Manual.TabIndex = 0;
			this.inputmethod_Manual.TabStop = true;
			this.inputmethod_Manual.Text = "Manual";
			this.inputmethod_Manual.UseVisualStyleBackColor = true;
			// 
			// inputmethod_AOxPort
			// 
			this.inputmethod_AOxPort.AutoSize = true;
			this.inputmethod_AOxPort.Location = new System.Drawing.Point(7, 44);
			this.inputmethod_AOxPort.Name = "inputmethod_AOxPort";
			this.inputmethod_AOxPort.Size = new System.Drawing.Size(64, 17);
			this.inputmethod_AOxPort.TabIndex = 1;
			this.inputmethod_AOxPort.TabStop = true;
			this.inputmethod_AOxPort.Text = "AOxPort";
			this.inputmethod_AOxPort.UseVisualStyleBackColor = true;
			// 
			// Options
			// 
			this.AcceptButton = this.button_Apply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_Discard;
			this.ClientSize = new System.Drawing.Size(565, 166);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.button_Discard);
			this.Controls.Add(this.button_Apply);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Options";
			this.Text = "Options";
			this.Load += new System.EventHandler(this.Options_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button AOxPort_BrowsePath;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_AOxPorts;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_Apply;
		private System.Windows.Forms.Button button_Discard;
		internal System.Windows.Forms.TextBox AOxPort_Path;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton inputmethod_AOxPort;
		private System.Windows.Forms.RadioButton inputmethod_Manual;
	}
}