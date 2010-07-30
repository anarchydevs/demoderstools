namespace AOMC.contextmenuWindows
{
	partial class mapversions_ModifyEntry
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
            this._layers = new System.Windows.Forms.ListBox();
            this._name = new System.Windows.Forms.TextBox();
            this._file = new System.Windows.Forms.TextBox();
            this._coordsfile = new System.Windows.Forms.TextBox();
            this._mapType = new System.Windows.Forms.ComboBox();
            this._button_ok = new System.Windows.Forms.Button();
            this._button_cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_remlayer = new System.Windows.Forms.Button();
            this._availableLayers = new System.Windows.Forms.ListBox();
            this.button_addlayer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "TxtFile";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "CoordsFile";
            // 
            // _layers
            // 
            this._layers.FormattingEnabled = true;
            this._layers.Location = new System.Drawing.Point(6, 34);
            this._layers.Name = "_layers";
            this._layers.Size = new System.Drawing.Size(105, 121);
            this._layers.Sorted = true;
            this._layers.TabIndex = 4;
            this._layers.DoubleClick += new System.EventHandler(this._layers_DoubleClick);
            // 
            // _name
            // 
            this._name.Location = new System.Drawing.Point(69, 6);
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(121, 20);
            this._name.TabIndex = 0;
            // 
            // _file
            // 
            this._file.Location = new System.Drawing.Point(69, 37);
            this._file.Name = "_file";
            this._file.Size = new System.Drawing.Size(121, 20);
            this._file.TabIndex = 1;
            // 
            // _coordsfile
            // 
            this._coordsfile.Location = new System.Drawing.Point(69, 68);
            this._coordsfile.Name = "_coordsfile";
            this._coordsfile.Size = new System.Drawing.Size(121, 20);
            this._coordsfile.TabIndex = 2;
            // 
            // _mapType
            // 
            this._mapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._mapType.FormattingEnabled = true;
            this._mapType.Location = new System.Drawing.Point(69, 99);
            this._mapType.Name = "_mapType";
            this._mapType.Size = new System.Drawing.Size(121, 21);
            this._mapType.TabIndex = 3;
            // 
            // _button_ok
            // 
            this._button_ok.Location = new System.Drawing.Point(13, 126);
            this._button_ok.Name = "_button_ok";
            this._button_ok.Size = new System.Drawing.Size(82, 34);
            this._button_ok.TabIndex = 5;
            this._button_ok.Text = "Apply";
            this._button_ok.UseVisualStyleBackColor = true;
            this._button_ok.Click += new System.EventHandler(this._button_ok_Click);
            // 
            // _button_cancel
            // 
            this._button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._button_cancel.Location = new System.Drawing.Point(101, 132);
            this._button_cancel.Name = "_button_cancel";
            this._button_cancel.Size = new System.Drawing.Size(75, 23);
            this._button_cancel.TabIndex = 6;
            this._button_cancel.Text = "Cancel";
            this._button_cancel.UseVisualStyleBackColor = true;
            this._button_cancel.Click += new System.EventHandler(this._button_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button_remlayer);
            this.groupBox1.Controls.Add(this._availableLayers);
            this.groupBox1.Controls.Add(this.button_addlayer);
            this.groupBox1.Controls.Add(this._layers);
            this.groupBox1.Location = new System.Drawing.Point(212, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 163);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Layers";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Assigned";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Available";
            // 
            // button_remlayer
            // 
            this.button_remlayer.Location = new System.Drawing.Point(117, 86);
            this.button_remlayer.Name = "button_remlayer";
            this.button_remlayer.Size = new System.Drawing.Size(41, 23);
            this.button_remlayer.TabIndex = 7;
            this.button_remlayer.Text = ">";
            this.button_remlayer.UseVisualStyleBackColor = true;
            this.button_remlayer.Click += new System.EventHandler(this.button_remlayer_Click);
            // 
            // _availableLayers
            // 
            this._availableLayers.FormattingEnabled = true;
            this._availableLayers.Location = new System.Drawing.Point(162, 35);
            this._availableLayers.Name = "_availableLayers";
            this._availableLayers.Size = new System.Drawing.Size(105, 121);
            this._availableLayers.Sorted = true;
            this._availableLayers.TabIndex = 8;
            this._availableLayers.DoubleClick += new System.EventHandler(this._availableLayers_DoubleClick);
            // 
            // button_addlayer
            // 
            this.button_addlayer.Location = new System.Drawing.Point(117, 59);
            this.button_addlayer.Name = "button_addlayer";
            this.button_addlayer.Size = new System.Drawing.Size(41, 23);
            this.button_addlayer.TabIndex = 6;
            this.button_addlayer.Text = "<";
            this.button_addlayer.UseVisualStyleBackColor = true;
            this.button_addlayer.Click += new System.EventHandler(this.button_addlayer_Click);
            // 
            // mapversions_ModifyEntry
            // 
            this.AcceptButton = this._button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._button_cancel;
            this.ClientSize = new System.Drawing.Size(486, 173);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._button_cancel);
            this.Controls.Add(this._button_ok);
            this.Controls.Add(this._mapType);
            this.Controls.Add(this._coordsfile);
            this.Controls.Add(this._file);
            this.Controls.Add(this._name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "mapversions_ModifyEntry";
            this.Text = "Map version";
            this.Load += new System.EventHandler(this.mapversions_ModifyEntry_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		internal System.Windows.Forms.ListBox _layers;
		internal System.Windows.Forms.TextBox _name;
		internal System.Windows.Forms.TextBox _file;
		internal System.Windows.Forms.TextBox _coordsfile;
		internal System.Windows.Forms.ComboBox _mapType;
		internal System.Windows.Forms.Button _button_ok;
		internal System.Windows.Forms.Button _button_cancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button_addlayer;
		private System.Windows.Forms.Button button_remlayer;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox _availableLayers;
	}
}