using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Demoder.MapCompiler;

namespace AOMC.contextmenuWindows
{
	public partial class workertaskModifyEntry_ModifyLayer : Form
	{
		public workertaskModifyEntry_ModifyLayer()
		{
			InitializeComponent();
		}
		internal List<LoadImage> images = new List<LoadImage>();
		internal string selectedimage = "none";

		private void workertaskModifyEntry_ModifyLayer_Load(object sender, EventArgs e)
		{
			//Set images
			this._image.Items.Clear();
			foreach (LoadImage li in this.images)
			{
				this._image.Items.Add(li.name);
				if (li.name == this.selectedimage)
					this._image.SelectedIndex = (this._image.Items.Count - 1);
			}
		}

		private void button_ok_Click(object sender, EventArgs e)
		{
			this.selectedimage = this._image.Text;
			this.DialogResult = DialogResult.OK;
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}
