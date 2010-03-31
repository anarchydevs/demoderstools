namespace AOMC.contextmenuWindows
{
	partial class workertaskModifyEntry
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
			this._workname = new System.Windows.Forms.TextBox();
			this._maprect = new System.Windows.Forms.TextBox();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this._addedImages = new System.Windows.Forms.ListBox();
			this._availImages = new System.Windows.Forms.ListBox();
			this.button_addImg = new System.Windows.Forms.Button();
			this.button_remImg = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this._imageformat = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Maprect:";
			// 
			// _workname
			// 
			this._workname.Location = new System.Drawing.Point(59, 7);
			this._workname.Name = "_workname";
			this._workname.Size = new System.Drawing.Size(184, 20);
			this._workname.TabIndex = 0;
			// 
			// _maprect
			// 
			this._maprect.Location = new System.Drawing.Point(59, 40);
			this._maprect.Name = "_maprect";
			this._maprect.Size = new System.Drawing.Size(184, 20);
			this._maprect.TabIndex = 1;
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(8, 200);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(76, 33);
			this.button_ok.TabIndex = 3;
			this.button_ok.Text = "Apply";
			this.button_ok.UseVisualStyleBackColor = true;
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Location = new System.Drawing.Point(157, 205);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(75, 23);
			this.button_cancel.TabIndex = 4;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// _addedImages
			// 
			this._addedImages.FormattingEnabled = true;
			this._addedImages.Location = new System.Drawing.Point(8, 112);
			this._addedImages.Name = "_addedImages";
			this._addedImages.Size = new System.Drawing.Size(120, 82);
			this._addedImages.TabIndex = 5;
			this._addedImages.DoubleClick += new System.EventHandler(this._addedImages_DoubleClick);
			// 
			// _availImages
			// 
			this._availImages.FormattingEnabled = true;
			this._availImages.Location = new System.Drawing.Point(176, 112);
			this._availImages.Name = "_availImages";
			this._availImages.Size = new System.Drawing.Size(120, 82);
			this._availImages.TabIndex = 6;
			this._availImages.DoubleClick += new System.EventHandler(this._availImages_DoubleClick);
			// 
			// button_addImg
			// 
			this.button_addImg.Location = new System.Drawing.Point(134, 122);
			this.button_addImg.Name = "button_addImg";
			this.button_addImg.Size = new System.Drawing.Size(35, 23);
			this.button_addImg.TabIndex = 7;
			this.button_addImg.Text = "<";
			this.button_addImg.UseVisualStyleBackColor = true;
			this.button_addImg.Click += new System.EventHandler(this.button_addImg_Click);
			// 
			// button_remImg
			// 
			this.button_remImg.Location = new System.Drawing.Point(134, 161);
			this.button_remImg.Name = "button_remImg";
			this.button_remImg.Size = new System.Drawing.Size(35, 23);
			this.button_remImg.TabIndex = 8;
			this.button_remImg.Text = ">";
			this.button_remImg.UseVisualStyleBackColor = true;
			this.button_remImg.Click += new System.EventHandler(this.button_remImg_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 73);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Image format";
			// 
			// _imageformat
			// 
			this._imageformat.FormattingEnabled = true;
			this._imageformat.Location = new System.Drawing.Point(82, 70);
			this._imageformat.Name = "_imageformat";
			this._imageformat.Size = new System.Drawing.Size(121, 21);
			this._imageformat.Sorted = true;
			this._imageformat.TabIndex = 10;
			// 
			// workertaskModifyEntry
			// 
			this.AcceptButton = this.button_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(301, 237);
			this.Controls.Add(this._imageformat);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button_remImg);
			this.Controls.Add(this.button_addImg);
			this.Controls.Add(this._availImages);
			this.Controls.Add(this._addedImages);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this._maprect);
			this.Controls.Add(this._workname);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "workertaskModifyEntry";
			this.Text = "workertaskModifyEntry";
			this.Load += new System.EventHandler(this.workertaskModifyEntry_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox _workname;
		internal System.Windows.Forms.TextBox _maprect;
		internal System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.ListBox _addedImages;
		private System.Windows.Forms.ListBox _availImages;
		private System.Windows.Forms.Button button_addImg;
		private System.Windows.Forms.Button button_remImg;
		private System.Windows.Forms.Label label3;
		internal System.Windows.Forms.ComboBox _imageformat;
	}
}