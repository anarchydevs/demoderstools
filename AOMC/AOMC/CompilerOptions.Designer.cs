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
            this.label_numworkers = new System.Windows.Forms.Label();
            this._MaxWorkers = new System.Windows.Forms.NumericUpDown();
            this.label_numslicers = new System.Windows.Forms.Label();
            this._MaxSlicers = new System.Windows.Forms.NumericUpDown();
            this.button_apply = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this._singleThread = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._autoOptimizeThreads = new System.Windows.Forms.CheckBox();
            this._showCompilerDebugMessages = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._showHelpsystem = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._MaxWorkers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._MaxSlicers)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_numworkers
            // 
            this.label_numworkers.AutoSize = true;
            this.label_numworkers.Location = new System.Drawing.Point(6, 40);
            this.label_numworkers.Name = "label_numworkers";
            this.label_numworkers.Size = new System.Drawing.Size(89, 13);
            this.label_numworkers.TabIndex = 10;
            this.label_numworkers.Text = "Max # of workers";
            // 
            // _MaxWorkers
            // 
            this._MaxWorkers.AutoSize = true;
            this._MaxWorkers.Location = new System.Drawing.Point(101, 38);
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
            // label_numslicers
            // 
            this.label_numslicers.AutoSize = true;
            this.label_numslicers.Location = new System.Drawing.Point(6, 17);
            this.label_numslicers.Name = "label_numslicers";
            this.label_numslicers.Size = new System.Drawing.Size(81, 13);
            this.label_numslicers.TabIndex = 10;
            this.label_numslicers.Text = "Max # of slicers";
            // 
            // _MaxSlicers
            // 
            this._MaxSlicers.AutoSize = true;
            this._MaxSlicers.Location = new System.Drawing.Point(101, 13);
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
            this.button_apply.Location = new System.Drawing.Point(12, 134);
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
            this.button_cancel.Location = new System.Drawing.Point(99, 139);
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
            this._singleThread.Location = new System.Drawing.Point(6, 61);
            this._singleThread.Name = "_singleThread";
            this._singleThread.Size = new System.Drawing.Size(88, 17);
            this._singleThread.TabIndex = 11;
            this._singleThread.Text = "Single thread";
            this._singleThread.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._autoOptimizeThreads);
            this.groupBox1.Controls.Add(this.label_numslicers);
            this.groupBox1.Controls.Add(this._showCompilerDebugMessages);
            this.groupBox1.Controls.Add(this._singleThread);
            this.groupBox1.Controls.Add(this.label_numworkers);
            this.groupBox1.Controls.Add(this._MaxWorkers);
            this.groupBox1.Controls.Add(this._MaxSlicers);
            this.groupBox1.Location = new System.Drawing.Point(6, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 126);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compiler";
            // 
            // _autoOptimizeThreads
            // 
            this._autoOptimizeThreads.AutoSize = true;
            this._autoOptimizeThreads.Location = new System.Drawing.Point(6, 84);
            this._autoOptimizeThreads.Name = "_autoOptimizeThreads";
            this._autoOptimizeThreads.Size = new System.Drawing.Size(127, 17);
            this._autoOptimizeThreads.TabIndex = 14;
            this._autoOptimizeThreads.Text = "Auto-optimize threads";
            this._autoOptimizeThreads.UseVisualStyleBackColor = true;
            this._autoOptimizeThreads.CheckedChanged += new System.EventHandler(this._autoOptimizeThreads_CheckedChanged);
            // 
            // _showCompilerDebugMessages
            // 
            this._showCompilerDebugMessages.AutoSize = true;
            this._showCompilerDebugMessages.Location = new System.Drawing.Point(6, 107);
            this._showCompilerDebugMessages.Name = "_showCompilerDebugMessages";
            this._showCompilerDebugMessages.Size = new System.Drawing.Size(175, 17);
            this._showCompilerDebugMessages.TabIndex = 0;
            this._showCompilerDebugMessages.Text = "Show compiler debugmessages";
            this._showCompilerDebugMessages.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._showHelpsystem);
            this.groupBox2.Location = new System.Drawing.Point(193, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(119, 110);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Visual";
            // 
            // _showHelpsystem
            // 
            this._showHelpsystem.AutoSize = true;
            this._showHelpsystem.Location = new System.Drawing.Point(6, 19);
            this._showHelpsystem.Name = "_showHelpsystem";
            this._showHelpsystem.Size = new System.Drawing.Size(108, 17);
            this._showHelpsystem.TabIndex = 1;
            this._showHelpsystem.Text = "Show helpsystem";
            this._showHelpsystem.UseVisualStyleBackColor = true;
            // 
            // CompilerOptions
            // 
            this.AcceptButton = this.button_apply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(315, 172);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_apply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CompilerOptions";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.CompilerOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this._MaxWorkers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._MaxSlicers)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label_numworkers;
		private System.Windows.Forms.NumericUpDown _MaxWorkers;
		private System.Windows.Forms.Label label_numslicers;
		private System.Windows.Forms.NumericUpDown _MaxSlicers;
		private System.Windows.Forms.Button button_apply;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.CheckBox _singleThread;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox _showHelpsystem;
		private System.Windows.Forms.CheckBox _showCompilerDebugMessages;
		private System.Windows.Forms.CheckBox _autoOptimizeThreads;

	}
}