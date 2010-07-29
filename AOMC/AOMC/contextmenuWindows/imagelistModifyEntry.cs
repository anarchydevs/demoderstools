using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AOMC.contextmenuWindows
{
	public partial class imagelistModifyEntry : Form
	{
		public imagelistModifyEntry()
		{
			InitializeComponent();
		}

		private void _path_DoubleClick(object sender, EventArgs e)
		{
			DialogResult dr = _selectFile.ShowDialog();
			switch (dr)
			{
				case DialogResult.OK:
					_path.Text = _selectFile.FileName;
					break;
			}
		}

		private void _cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void _ok_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void _path_Click(object sender, EventArgs e)
		{
			if (this._path.Text.Length == 0)
				this._path_DoubleClick(sender, e);
		}

	}
}
