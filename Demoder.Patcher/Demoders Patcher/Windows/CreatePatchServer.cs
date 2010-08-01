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
				ListViewItem lvi = new ListViewItem(new string[] { cdc.Name, cdc.DistributionType.ToString(), cdc.Directory });
				lvi.Tag = cdc;
				this.listView_distributions.Items.Add(lvi);
			}

			this.textBox_GUID.Text = this._cpsc.GUID.ToString();
			this.textBox_Name.Text = this._cpsc.Name;
			
		}

		private void button_ok_Click(object sender, EventArgs e)
		{
			/*
			this.CDConfig.GUID = new Guid(this.textBox_GUID.Text);
			this.CDConfig.Name = this.textBox_Name.Text;
			this.CDConfig.DistributionType = (Distribution.DistributionType)Enum.Parse(
				typeof(Distribution.DistributionType),
				this.comboBox_DistributionType.Items[comboBox_DistributionType.SelectedIndex].ToString());
			//Directories
			this.CDConfig.Directories.Clear();
			foreach (string dir in this.listBox_Directories.Items)
			{
				this.CDConfig.Directories.Add(dir);
			}
			*/
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
