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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapUpgrades
{
	public partial class Options : Form
	{
		public Options()
		{
			InitializeComponent();
		}

		private void AOxPort_BrowsePath_Click(object sender, EventArgs e)
		{
			folderBrowserDialog_AOxPorts.SelectedPath = AOxPort_Path.Text;
			switch (folderBrowserDialog_AOxPorts.ShowDialog())
			{
				case DialogResult.OK:
					AOxPort_Path.Text = folderBrowserDialog_AOxPorts.SelectedPath;
					break;
			}
		}

		private void Options_Load(object sender, EventArgs e)
		{
			AOxPort_Path.Text = Program.Configuration.AOxPort_ExportDirectory;
			switch (Program.Configuration.selectedTab)
			{
				case MapUpgrades.xml.AppConfig.SelectedTab.Manual: inputmethod_Manual.Checked = true; break;
				case MapUpgrades.xml.AppConfig.SelectedTab.AOxPort: inputmethod_AOxPort.Checked = true; break;
			}
		}

		private void button_Apply_Click(object sender, EventArgs e)
		{
			Program.Configuration.AOxPort_ExportDirectory = AOxPort_Path.Text;
			if (inputmethod_Manual.Checked) Program.Configuration.selectedTab = MapUpgrades.xml.AppConfig.SelectedTab.Manual;
			else if (inputmethod_AOxPort.Checked) Program.Configuration.selectedTab = MapUpgrades.xml.AppConfig.SelectedTab.AOxPort;
			DialogResult = DialogResult.OK;
			this.Close();

		}

		private void button_Discard_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

	}
}
