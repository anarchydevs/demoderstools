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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this._workname = new System.Windows.Forms.TextBox();
			this._maprect = new System.Windows.Forms.TextBox();
			this._Layers = new System.Windows.Forms.ListView();
			this.columnHeader_layername = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_imagename = new System.Windows.Forms.ColumnHeader();
			this.button_ok = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.layers_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.layers_ContextMenu_Add = new System.Windows.Forms.ToolStripMenuItem();
			this.layers_ContextMenu_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.layers_ContextMenu_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.layers_ContextMenu.SuspendLayout();
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
			// _Layers
			// 
			this._Layers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_layername,
            this.columnHeader_imagename});
			this._Layers.ContextMenuStrip = this.layers_ContextMenu;
			this._Layers.LabelEdit = true;
			this._Layers.Location = new System.Drawing.Point(8, 81);
			this._Layers.Name = "_Layers";
			this._Layers.Size = new System.Drawing.Size(235, 137);
			this._Layers.TabIndex = 2;
			this._Layers.UseCompatibleStateImageBehavior = false;
			this._Layers.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader_layername
			// 
			this.columnHeader_layername.Text = "Layer";
			this.columnHeader_layername.Width = 103;
			// 
			// columnHeader_imagename
			// 
			this.columnHeader_imagename.Text = "Image";
			this.columnHeader_imagename.Width = 124;
			// 
			// button_ok
			// 
			this.button_ok.Location = new System.Drawing.Point(8, 224);
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
			this.button_cancel.Location = new System.Drawing.Point(157, 229);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(75, 23);
			this.button_cancel.TabIndex = 4;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// layers_ContextMenu
			// 
			this.layers_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layers_ContextMenu_Add,
            this.layers_ContextMenu_Edit,
            this.layers_ContextMenu_Remove});
			this.layers_ContextMenu.Name = "layers_ContextMenu";
			this.layers_ContextMenu.Size = new System.Drawing.Size(118, 70);
			this.layers_ContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.layers_ContextMenu_Opening);
			// 
			// layers_ContextMenu_Add
			// 
			this.layers_ContextMenu_Add.Name = "layers_ContextMenu_Add";
			this.layers_ContextMenu_Add.Size = new System.Drawing.Size(152, 22);
			this.layers_ContextMenu_Add.Text = "Add";
			this.layers_ContextMenu_Add.Click += new System.EventHandler(this.layers_ContextMenu_Add_Click);
			// 
			// layers_ContextMenu_Edit
			// 
			this.layers_ContextMenu_Edit.Name = "layers_ContextMenu_Edit";
			this.layers_ContextMenu_Edit.Size = new System.Drawing.Size(152, 22);
			this.layers_ContextMenu_Edit.Text = "Edit";
			this.layers_ContextMenu_Edit.Click += new System.EventHandler(this.layers_ContextMenu_Edit_Click);
			// 
			// layers_ContextMenu_Remove
			// 
			this.layers_ContextMenu_Remove.Name = "layers_ContextMenu_Remove";
			this.layers_ContextMenu_Remove.Size = new System.Drawing.Size(152, 22);
			this.layers_ContextMenu_Remove.Text = "Remove";
			this.layers_ContextMenu_Remove.Click += new System.EventHandler(this.layers_ContextMenu_Remove_Click);
			// 
			// workertaskModifyEntry
			// 
			this.AcceptButton = this.button_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_cancel;
			this.ClientSize = new System.Drawing.Size(247, 269);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this._Layers);
			this.Controls.Add(this._maprect);
			this.Controls.Add(this._workname);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "workertaskModifyEntry";
			this.Text = "workertaskModifyEntry";
			this.Load += new System.EventHandler(this.workertaskModifyEntry_Load);
			this.layers_ContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox _workname;
		internal System.Windows.Forms.TextBox _maprect;
		private System.Windows.Forms.ColumnHeader columnHeader_layername;
		private System.Windows.Forms.ColumnHeader columnHeader_imagename;
		internal System.Windows.Forms.Button button_ok;
		internal System.Windows.Forms.ListView _Layers;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.ContextMenuStrip layers_ContextMenu;
		private System.Windows.Forms.ToolStripMenuItem layers_ContextMenu_Add;
		private System.Windows.Forms.ToolStripMenuItem layers_ContextMenu_Edit;
		private System.Windows.Forms.ToolStripMenuItem layers_ContextMenu_Remove;
	}
}