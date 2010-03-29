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
			int blah = Environment.ProcessorCount;
			_MaxSlicers.Value = Program.Config_Compiler.MaxSlicerThreads;
			_MaxWorkers.Value = Program.Config_Compiler.MaxWorkerThreads;
		}

		private void button_apply_Click(object sender, EventArgs e)
		{
			//Save settings.
			Program.Config_Compiler.MaxSlicerThreads = (int)_MaxSlicers.Value;
			Program.Config_Compiler.MaxWorkerThreads = (int)_MaxWorkers.Value;
			Program.Config_Compiler.singlethreaded = this._singleThread.Checked;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
