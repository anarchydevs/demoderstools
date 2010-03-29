namespace AOMC
{
	partial class CompilerOptions
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
			this._MaxWorkers = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this._MaxSlicers = new System.Windows.Forms.NumericUpDown();
			this.button_apply = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this._singleThread = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this._MaxWorkers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._MaxSlicers)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Max # of workers";
			// 
			// _MaxWorkers
			// 
			this._MaxWorkers.AutoSize = true;
			this._MaxWorkers.Location = new System.Drawing.Point(107, 31);
			this._MaxWorkers.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this._MaxWorkers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._MaxWorkers.Name = "_MaxWorkers";
			this._MaxWorkers.Size = new System.Drawing.Size(44, 20);
			this._MaxWorkers.TabIndex = 1;
			this._MaxWorkers.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Max # of slicers";
			// 
			// _MaxSlicers
			// 
			this._MaxSlicers.AutoSize = true;
			this._MaxSlicers.Location = new System.Drawing.Point(107, 6);
			this._MaxSlicers.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this._MaxSlicers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._MaxSlicers.Name = "_MaxSlicers";
			this._MaxSlicers.Size = new System.Drawing.Size(44, 20);
			this._MaxSlicers.TabIndex = 0;
			this._MaxSlicers.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// button_apply
			// 
			this.button_apply.Location = new System.Drawing.Point(12, 75);
			this.button_apply.Name = "button_apply";
			this.button_apply.Size = new System.Drawing.Size(81, 32);
			this.button_apply.TabIndex = 2;
			this.button_apply.Text = "Apply";
			this.button_apply.UseVisualStyleBackColor = true;
			this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Location = new System.Drawing.Point(99, 80);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(52, 23);
			this.button_cancel.TabIndex = 3;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// _singleThread
			// 
			this._singleThread.AutoSize = true;
			this._singleThread.Location = new System.Drawing.Point(21, 57);
			this._singleThread.Name = "_singleThread";
			this._singleThread.Size = new System.Drawing.Size(132, 17);
			this._singleThread.TabIndex = 11;
			this._singleThread.Text = "DEBUG: Single thread";
			this._singleThread.UseVisualStyleBackColor = true;
			// 
			// CompilerOptions
			// 
			this.AcceptButton = this.button_apply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(205, 120);
			this.Controls.Add(this._singleThread);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_apply);
			this.Controls.Add(this._MaxSlicers);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._MaxWorkers);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "CompilerOptions";
			this.Text = "Options";
			this.Load += new System.EventHandler(this.CompilerOptions_Load);
			((System.ComponentModel.ISupportInitialize)(this._MaxWorkers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._MaxSlicers)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown _MaxWorkers;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown _MaxSlicers;
		private System.Windows.Forms.Button button_apply;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.CheckBox _singleThread;

	}
}