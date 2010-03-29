namespace AOMC.contextmenuWindows
{
	partial class workertaskModifyEntry_ModifyLayer
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
			this._image = new System.Windows.Forms.ComboBox();
			this._layername = new System.Windows.Forms.TextBox();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Layer:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Image:";
			// 
			// _image
			// 
			this._image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._image.FormattingEnabled = true;
			this._image.Location = new System.Drawing.Point(53, 32);
			this._image.MaxDropDownItems = 32;
			this._image.Name = "_image";
			this._image.Size = new System.Drawing.Size(121, 21);
			this._image.Sorted = true;
			this._image.TabIndex = 1;
			// 
			// _layername
			// 
			this._layername.Location = new System.Drawing.Point(53, 6);
			this._layername.Name = "_layername";
			this._layername.Size = new System.Drawing.Size(121, 20);
			this._layername.TabIndex = 0;
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(10, 65);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(75, 32);
			this.button_ok.TabIndex = 2;
			this.button_ok.Text = "Apply";
			this.button_ok.UseVisualStyleBackColor = true;
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Location = new System.Drawing.Point(99, 70);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(75, 23);
			this.button_cancel.TabIndex = 3;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// workertaskModifyEntry_ModifyLayer
			// 
			this.AcceptButton = this.button_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(176, 103);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this._layername);
			this.Controls.Add(this._image);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "workertaskModifyEntry_ModifyLayer";
			this.Text = "workertaskModifyEntry_ModifyLayer";
			this.Load += new System.EventHandler(this.workertaskModifyEntry_ModifyLayer_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.ComboBox _image;
		internal System.Windows.Forms.TextBox _layername;
		internal System.Windows.Forms.Button button_ok;
		internal System.Windows.Forms.Button button_cancel;
	}
}