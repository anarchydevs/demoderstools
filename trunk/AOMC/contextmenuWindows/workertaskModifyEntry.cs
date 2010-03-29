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
	public partial class workertaskModifyEntry : Form
	{
		public workertaskModifyEntry()
		{
			InitializeComponent();
		}

		internal WorkTask workerTask = new WorkTask();
		internal List<LoadImage> images = new List<LoadImage>();

		private void workertaskModifyEntry_Load(object sender, EventArgs e)
		{
			LoadEntries();
		}

		private void LoadEntries()
		{
			//Load settings from workerTask
			this._maprect.Text = this.workerTask.maprect;
			this._workname.Text = this.workerTask.workname;
			this._availImages.Items.Clear();
			this._addedImages.Items.Clear();

			foreach (LoadImage li in this.images)
				if (!this.workerTask.workentries.Contains(li.name))
					this._availImages.Items.Add(li.name);
			foreach (string img in this.workerTask.workentries)
				this._addedImages.Items.Add(img);
		}

		private void button_ok_Click(object sender, EventArgs e)
		{
			this.workerTask.workname = this._workname.Text;
			this.workerTask.maprect = this._maprect.Text;
			this.workerTask.workentries = new List<string>();
			foreach (string lvi in this._addedImages.Items)
			{
				this.workerTask.workentries.Add(lvi);
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		#region image add/remove buttons
		private void button_addImg_Click(object sender, EventArgs e)
		{
			if (this._availImages.SelectedIndex != -1)
			{
				string img = (string)this._availImages.Items[this._availImages.SelectedIndex];
				this._availImages.Items.Remove(img);
				this._addedImages.Items.Add(img);
			}
		}

		private void button_remImg_Click(object sender, EventArgs e)
		{
			if (this._addedImages.SelectedIndex != -1)
			{
				string img = (string)this._addedImages.Items[this._addedImages.SelectedIndex];
				this._addedImages.Items.Remove(img);
				this._availImages.Items.Add(img);
			}
		}
		#endregion
	}
}
