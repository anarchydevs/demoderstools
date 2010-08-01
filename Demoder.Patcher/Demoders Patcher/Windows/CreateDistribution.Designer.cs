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
			this.textBox_Version = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// comboBox_DistributionType
			// 
			this.comboBox_DistributionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_DistributionType.FormattingEnabled = true;
			this.comboBox_DistributionType.Location = new System.Drawing.Point(60, 12);
			this.comboBox_DistributionType.Name = "comboBox_DistributionType";
			this.comboBox_DistributionType.Size = new System.Drawing.Size(154, 21);
			this.comboBox_DistributionType.Sorted = true;
			this.comboBox_DistributionType.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Type";
			// 
			// textBox_Name
			// 
			this.textBox_Name.Location = new System.Drawing.Point(60, 39);
			this.textBox_Name.Name = "textBox_Name";
			this.textBox_Name.Size = new System.Drawing.Size(154, 20);
			this.textBox_Name.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Name";
			// 
			// textBox_Version
			// 
			this.textBox_Version.Location = new System.Drawing.Point(60, 65);
			this.textBox_Version.Name = "textBox_Version";
			this.textBox_Version.Size = new System.Drawing.Size(154, 20);
			this.textBox_Version.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Version";
			// 
			// CreateDistribution
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(446, 317);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox_Version);
			this.Controls.Add(this.textBox_Name);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox_DistributionType);
			this.Controls.Add(this.label3);
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
		private System.Windows.Forms.TextBox textBox_Version;
		private System.Windows.Forms.Label label2;
	}
}