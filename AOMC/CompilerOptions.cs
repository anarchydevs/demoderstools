using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AOMC
{
	public partial class CompilerOptions : Form
	{
		public CompilerOptions()
		{
			InitializeComponent();
		}

		private void CompilerOptions_Load(object sender, EventArgs e)
		{
			//Load configuration.
			//Max # Slicer threads
			this._MaxSlicers.Value = Program.Config_Compiler.MaxSlicerThreads;
			this._MaxWorkers.Value = Program.Config_Compiler.MaxWorkerThreads;
			this._showHelpsystem.Checked = Program.Config_AOMC.show_helpsystem;
			this._showCompilerDebugMessages.Checked = Program.Config_AOMC.show_compiler_debugmessages;
			this._autoOptimizeThreads.Checked = Program.Config_Compiler.AutoOptimizeThreads;
			this.EnableDisableStuff();
		}

		private void EnableDisableStuff()
		{
			if (this._autoOptimizeThreads.Checked)
			{
				this._MaxSlicers.Enabled = false;
				this._MaxWorkers.Enabled = false;
				this._singleThread.Enabled = false;
				this.label_numslicers.Enabled = false;
				this.label_numworkers.Enabled = false;
			}
			else
			{
				this._MaxSlicers.Enabled = true;
				this._MaxWorkers.Enabled = true;
				this._singleThread.Enabled = true;
				this.label_numslicers.Enabled = true;
				this.label_numworkers.Enabled = true;
			}
		}

		private void button_apply_Click(object sender, EventArgs e)
		{
			//Save settings.
			Program.Config_Compiler.MaxSlicerThreads = (int)_MaxSlicers.Value;
			Program.Config_Compiler.MaxWorkerThreads = (int)_MaxWorkers.Value;
			Program.Config_Compiler.singlethreaded = this._singleThread.Checked;
			Program.Config_AOMC.show_compiler_debugmessages = this._showCompilerDebugMessages.Checked;
			Program.Config_AOMC.show_helpsystem = this._showHelpsystem.Checked;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void _autoOptimizeThreads_CheckedChanged(object sender, EventArgs e)
		{
			this.EnableDisableStuff();
		}
	}
}
