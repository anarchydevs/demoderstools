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
			this.comboBox_DistributionType = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_Name = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox_directory = new System.Windows.Forms.TextBox();
			this.button_browse = new System.Windows.Forms.Button();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// comboBox_DistributionType
			// 
			this.comboBox_DistributionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_DistributionType.FormattingEnabled = true;
			this.comboBox_DistributionType.Location = new System.Drawing.Point(53, 31);
			this.comboBox_DistributionType.Name = "comboBox_DistributionType";
			this.comboBox_DistributionType.Size = new System.Drawing.Size(222, 21);
			this.comboBox_DistributionType.Sorted = true;
			this.comboBox_DistributionType.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 34);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Type";
			// 
			// textBox_Name
			// 
			this.textBox_Name.Location = new System.Drawing.Point(53, 5);
			this.textBox_Name.Name = "textBox_Name";
			this.textBox_Name.Size = new System.Drawing.Size(222, 20);
			this.textBox_Name.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Name";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 61);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Directory";
			// 
			// textBox_directory
			// 
			this.textBox_directory.Location = new System.Drawing.Point(53, 58);
			this.textBox_directory.Name = "textBox_directory";
			this.textBox_directory.Size = new System.Drawing.Size(222, 20);
			this.textBox_directory.TabIndex = 14;
			// 
			// button_browse
			// 
			this.button_browse.Location = new System.Drawing.Point(281, 57);
			this.button_browse.Name = "button_browse";
			this.button_browse.Size = new System.Drawing.Size(27, 23);
			this.button_browse.TabIndex = 15;
			this.button_browse.Text = "...";
			this.button_browse.UseVisualStyleBackColor = true;
			this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(53, 81);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(75, 23);
			this.button_ok.TabIndex = 16;
			this.button_ok.Text = "OK";
			this.button_ok.UseVisualStyleBackColor = true;
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(200, 81);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(75, 23);
			this.button_cancel.TabIndex = 17;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// CreateDistribution
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(313, 108);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.button_browse);
			this.Controls.Add(this.textBox_directory);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox_Name);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox_DistributionType);
			this.Controls.Add(this.label3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "CreateDistribution";
			this.Text = "CreateDistribution";
			this.Load += new System.EventHandler(this.CreateDistribution_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox_DistributionType;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_Name;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_directory;
		private System.Windows.Forms.Button button_browse;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
	}
}