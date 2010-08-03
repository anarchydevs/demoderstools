/*
MIT Licence
Copyright (c) 2010 Demoder <demoder@flw.nu> (project: https://sourceforge.net/projects/demoderstools/)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Demoders_Patcher.DataClasses;
using Demoder.Patcher.DataClasses;

namespace Demoders_Patcher.Windows
{
	public partial class CreatePatchServer : Form
	{
		private CreatePatchServerConfig _cpsc= new CreatePatchServerConfig();
		public CreatePatchServer() {
			InitializeComponent();
		}
		public CreatePatchServer(CreatePatchServerConfig CPSC)
			: this()
		{
			this._cpsc = CPSC;
			this.Text = "Edit Patch Server";
		}


		private void CreateDistribution_Load(object sender, EventArgs e)
		{
			//Set up collumns in listview_distributions
			this.listView_distributions.Columns.Add("Name", "Name", 75);
			this.listView_distributions.Columns.Add("Type", "Type", 75);
			this.listView_distributions.Columns.Add("Path", "Path", 125);

			foreach (CreateDistributionConfig cdc in this._cpsc.Distributions)
			{
				this.listView_distributions.Items.Add(this.createListViewItem(cdc));
			}
			if (this._cpsc.download_locations != null && this._cpsc.download_locations.Count>0)
				this.textBox_PatchServerURLs.Text = String.Join("\r\n", this._cpsc.download_locations.ToArray());
			this.textBox_GUID.Text = this._cpsc.GUID.ToString();
			this.textBox_Name.Text = this._cpsc.Name;
			//Fix collumns & sorting in the listview
			Demoder.Common.Forms.AutoResizeHeaders(this.listView_distributions, ColumnHeaderAutoResizeStyle.ColumnContent);
			this.listView_distributions.ColumnClick += new ColumnClickEventHandler(Demoder.Common.Forms.ListView_ColumnClick);
		}

		private ListViewItem createListViewItem(CreateDistributionConfig cdc) 
		{
			ListViewItem lvi = new ListViewItem(new string[] { cdc.Name, cdc.DistributionType.ToString(), cdc.Directory });
			lvi.Tag = cdc;
			return lvi;
		}

		private void button_ok_Click(object sender, EventArgs e)
		{
			//Simple text fields
			this._cpsc.Name = this.textBox_Name.Text;
			this._cpsc.Version = this.textBox_version.Text;
			this._cpsc.GUID = new Guid(this.textBox_GUID.Text);

			//Distributions
			this._cpsc.Distributions = new List<CreateDistributionConfig>();
			foreach (ListViewItem lvi in this.listView_distributions.Items)
				this._cpsc.Distributions.Add((CreateDistributionConfig)lvi.Tag);
			
			//Mirrors
			this._cpsc.download_locations = new List<string>();
			foreach (string s in this.textBox_PatchServerURLs.Text.Split("\r\n".ToCharArray()))
				if (!String.IsNullOrEmpty(s))
					this._cpsc.download_locations.Add(s);

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		#region contextmenu: Distributions
		private void contextMenuStrip_Distributions_Opening(object sender, CancelEventArgs e)
		{
			ContextMenuStrip cms = (ContextMenuStrip)sender;
			if (this.listView_distributions.SelectedItems.Count > 0)
			{
				cms.Items[0].Enabled = false; //add
				cms.Items[1].Enabled = true;  //edit
				cms.Items[3].Enabled = true;  //remove
			}
			else
			{
				cms.Items[0].Enabled = true;	//add
				cms.Items[1].Enabled = false;	//edit
				cms.Items[3].Enabled = false;	//remove
			}
		}

		private void addToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateDistribution cd = new CreateDistribution();
			DialogResult dr = cd.ShowDialog();
			switch (dr)
			{
				case DialogResult.OK:
					this.listView_distributions.Items.Add(this.createListViewItem(cd.CDConfig));
					break;
			}
		}
		
		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in this.listView_distributions.SelectedItems)
			{
				this.listView_distributions.Items.Remove(lvi);
			}
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in this.listView_distributions.SelectedItems)
			{
				CreateDistributionConfig cdc = (CreateDistributionConfig)lvi.Tag;
				//Since the config is passed as a reference, the window will deal with updating it.
				CreateDistribution cd = new CreateDistribution(ref cdc);
				DialogResult dr = cd.ShowDialog();
				switch (dr)
				{
					case DialogResult.OK:
						ListViewItem lvi2 = createListViewItem(cdc);
						this.listView_distributions.Items.Remove(lvi);
						this.listView_distributions.Items.Add(lvi2);
						break;
				}
			}
		}
		#endregion
	}
}
