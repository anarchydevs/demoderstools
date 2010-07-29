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
	partial class CreateDistribution
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_Name = new System.Windows.Forms.TextBox();
			this.textBox_GUID = new System.Windows.Forms.TextBox();
			this.comboBox_DistributionType = new System.Windows.Forms.ComboBox();
			this.listBox_Directories = new System.Windows.Forms.ListBox();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "GUID";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Type";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Directories";
			// 
			// textBox_Name
			// 
			this.textBox_Name.Location = new System.Drawing.Point(69, 9);
			this.textBox_Name.Name = "textBox_Name";
			this.textBox_Name.Size = new System.Drawing.Size(228, 20);
			this.textBox_Name.TabIndex = 4;
			// 
			// textBox_GUID
			// 
			this.textBox_GUID.Location = new System.Drawing.Point(69, 35);
			this.textBox_GUID.Name = "textBox_GUID";
			this.textBox_GUID.Size = new System.Drawing.Size(228, 20);
			this.textBox_GUID.TabIndex = 5;
			// 
			// comboBox_DistributionType
			// 
			this.comboBox_DistributionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_DistributionType.FormattingEnabled = true;
			this.comboBox_DistributionType.Location = new System.Drawing.Point(69, 61);
			this.comboBox_DistributionType.Name = "comboBox_DistributionType";
			this.comboBox_DistributionType.Size = new System.Drawing.Size(228, 21);
			this.comboBox_DistributionType.Sorted = true;
			this.comboBox_DistributionType.TabIndex = 6;
			// 
			// listBox_Directories
			// 
			this.listBox_Directories.FormattingEnabled = true;
			this.listBox_Directories.Location = new System.Drawing.Point(16, 104);
			this.listBox_Directories.Name = "listBox_Directories";
			this.listBox_Directories.Size = new System.Drawing.Size(281, 95);
			this.listBox_Directories.TabIndex = 7;
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(222, 205);
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
			this.button_cancel.Location = new System.Drawing.Point(16, 204);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(75, 23);
			this.button_cancel.TabIndex = 9;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// CreateDistribution
			// 
			this.AcceptButton = this.button_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(309, 239);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.listBox_Directories);
			this.Controls.Add(this.comboBox_DistributionType);
			this.Controls.Add(this.textBox_GUID);
			this.Controls.Add(this.textBox_Name);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "CreateDistribution";
			this.Text = "CreateDistribution";
			this.Load += new System.EventHandler(this.CreateDistribution_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_Name;
		private System.Windows.Forms.TextBox textBox_GUID;
		private System.Windows.Forms.ComboBox comboBox_DistributionType;
		private System.Windows.Forms.ListBox listBox_Directories;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_cancel;

	}
}