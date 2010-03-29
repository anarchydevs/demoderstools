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
			this._Layers.Items.Clear();
			foreach (WorkLayer wl in this.workerTask.workentries)
			{
				this._Layers.Items.Add(new ListViewItem(new string[] { wl.layername, wl.imagename }));
			}
		}
		private void button_ok_Click(object sender, EventArgs e)
		{
			this.workerTask.workname = this._workname.Text;
			this.workerTask.maprect = this._maprect.Text;
			this.workerTask.workentries = new List<WorkLayer>();
			foreach (ListViewItem lvi in this._Layers.Items)
			{
				this.workerTask.workentries.Add(new WorkLayer(lvi.Text, lvi.SubItems[1].Text));
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		#region Context menu strip
		private void layers_ContextMenu_Opening(object sender, CancelEventArgs e)
		{
			this.layers_ContextMenu.Items[0].Enabled = true;
			if (this._Layers.SelectedItems.Count > 0)
			{
				this.layers_ContextMenu.Items[1].Enabled = true;
				this.layers_ContextMenu.Items[2].Enabled = true;
			}
			else
			{
				this.layers_ContextMenu.Items[1].Enabled = false;
				this.layers_ContextMenu.Items[2].Enabled = false;
			}
		}

	

		private void layers_ContextMenu_Add_Click(object sender, EventArgs e)
		{
			workertaskModifyEntry_ModifyLayer ml = new workertaskModifyEntry_ModifyLayer();
				ml.Text = "Add layer";
				WorkLayer wl = new WorkLayer();
				ml._layername.Text = "";
				ml.selectedimage = "";
				ml.images = this.images;
				bool error;
				do
				{
					error = false;
					DialogResult dr = ml.ShowDialog();
					switch (dr)
					{
						case DialogResult.OK:
							if (Program.Config_Map.ContainsLayer(ml._layername.Text)
								&& (Program.Config_Map.WorkTasksContainingLayer(ml._layername.Text) != this.workerTask.workname))
							{
								MessageBox.Show(string.Format("Layer already exists: {0}", ml._layername.Text), "Duplicate layer name", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error = true;
								continue;
							}
							if (ml.selectedimage.Length == 0)
							{
								MessageBox.Show("You must select an image.", "Missing image", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error = true;
								continue;
							}
							//Save the changes.
							wl.layername = ml._layername.Text;
							wl.imagename = ml.selectedimage;
							this.workerTask.workentries.Add(wl);
							break;
					}
				} while (error);
				this.LoadEntries();
		}

		private void layers_ContextMenu_Edit_Click(object sender, EventArgs e)
		{
			if (this._Layers.SelectedItems.Count > 0)
			{
				foreach (ListViewItem lvi in this._Layers.SelectedItems)
				{
					workertaskModifyEntry_ModifyLayer ml = new workertaskModifyEntry_ModifyLayer();
					WorkLayer wl = this.workerTask.GetWorkEntry(lvi.Text);
					if (wl != null)
					{
						ml.Text = "Edit layer";
						ml._layername.Text = wl.layername;
						ml.selectedimage = wl.imagename;
						ml.images = this.images;
						bool error;
						do
						{
							error = false;
							DialogResult dr = ml.ShowDialog();
							switch (dr)
							{
								case DialogResult.OK:
									if ((ml._layername.Text != wl.layername)
										&& Program.Config_Map.ContainsLayer(ml._layername.Text))
									{
										MessageBox.Show(string.Format("Layer already exists: {0}", ml._layername.Text), "Duplicate layer name", MessageBoxButtons.OK, MessageBoxIcon.Error);
										error = true;
										continue;
									}
									//Save the changes.
									wl.layername = ml._layername.Text;
									wl.imagename = ml.selectedimage;
									break;
							}
						} while (error);
						this.LoadEntries();
					}
				}
			}
		}

		private void layers_ContextMenu_Remove_Click(object sender, EventArgs e)
		{
			if (this._Layers.SelectedItems.Count > 0)
			{
				foreach (ListViewItem lvi in this._Layers.SelectedItems)
				{
					this.workerTask.Remove(lvi.Text);
				}
				this.LoadEntries();
			}
		}
		#endregion
	}
}
