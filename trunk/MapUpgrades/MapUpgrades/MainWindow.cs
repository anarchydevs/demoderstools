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
using Demoder.Common;
using MapUpgrades.xml;
using System.IO;

namespace MapUpgrades
{
    public partial class MainWindow : Form
    {
        string htmldocument=string.Empty;
		xml.ItemList _itemlist;
		xml.ActivePFs _activePFs;
		System.Net.WebClient wc = new System.Net.WebClient();
		FileSystemWatcher fsWatcher = new FileSystemWatcher();	

        public MainWindow()
        {
            InitializeComponent();
        }

		private void MainWindow_Load(object sender, EventArgs e)
		{
			wc.OpenReadCompleted += new System.Net.OpenReadCompletedEventHandler(wc_OpenReadCompleted);

			_itemlist = Xml.Deserialize<xml.ItemList>(new FileInfo("cfg/Items.xml"), false);
			_activePFs = Xml.Deserialize<xml.ActivePFs>(new FileInfo("cfg/ActivePFs.xml"), false);

			//Setup sorting.
			//Uploadable maps
			Forms.ListViewSorter lvs = new Forms.ListViewSorter();
			lvs.LastSort = lvs.ByColumn = 0;
			Uploadable.ListViewItemSorter = lvs;
			Uploadable.Sorting = SortOrder.Descending; //Somehow, setting this to ascending makes it sort descending, and vice versa....

			//Available
			Forms.ListViewSorter lvs2 = new Forms.ListViewSorter();
			lvs2.LastSort = lvs2.ByColumn = 1;
			Available.ListViewItemSorter = lvs2;
			Available.Sorting = SortOrder.Descending;

			//Uploaded
			Forms.ListViewSorter lvs3 = new Forms.ListViewSorter();
			lvs3.LastSort = lvs3.ByColumn = 0;
			Uploaded.ListViewItemSorter = lvs3;
			Uploaded.Sorting = SortOrder.Ascending;
			try
			{
				update_ActivePFs();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			Update_AOxPort_Characters();

			switch (Program.Configuration.selectedTab)
			{
				case MapUpgrades.xml.AppConfig.SelectedTab.Manual: tabControl2.SelectedIndex = 0; break;
				case MapUpgrades.xml.AppConfig.SelectedTab.AOxPort: tabControl2.SelectedIndex = 1; break;
			}
		}

		private void button1_Click(object sender, EventArgs e)
        {
            
            string[] text = _inputText.Text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			uint mapnav;
            uint.TryParse(this._mapNav.Text, out mapnav);
            ProcessInput(text, mapnav);

        }

		private void ProcessInput_StartUpdate()
		{
			Uploadable.BeginUpdate();
			Uploadable.Items.Clear();
			Uploadable.Groups.Clear();

			Uploaded.BeginUpdate();
			Uploaded.Items.Clear();
			Uploaded.Groups.Clear();

			Available.BeginUpdate();
			Available.Items.Clear();
			Available.Groups.Clear();
		}

		private void ProcessInput_DoUpdate(uint mapnav, List<xml.ItemList.Map> maps, List<xml.ItemList.MapUpgrade> mapupgrades)
		{
			ProcessInput_StartUpdate();

			#region MapReaderUpgrades
			ListViewGroup lvg2 = new ListViewGroup("lvg_mapreaderupgrades", "Map Readers");
			lvg2.HeaderAlignment = HorizontalAlignment.Center;
			Uploaded.Groups.Add(lvg2);
			Uploadable.Groups.Add(lvg2);
			Available.Groups.Add(lvg2);
			foreach (xml.ItemList.MapUpgrade upgrade in this._itemlist.MapUpgrades)
			{
				ListViewItem lvi = new ListViewItem(new string[] { upgrade.name, upgrade.mapnav.ToString(), upgrade.listname });

				lvi.Group = lvg2;
				if (mapupgrades.Contains(upgrade))
				{
					//Have map
					Uploaded.Items.Add(lvi);
				}
				else
				{
					//Don't have map
					if (mapnav >= upgrade.mapnav)
						Uploadable.Items.Add(lvi);
					else
						Available.Items.Add(lvi);
				}
			}
			#endregion
			
			#region Maps
			ListViewGroup lvg = new ListViewGroup("lvg_maps", "Maps");
			lvg.HeaderAlignment = HorizontalAlignment.Center;
			Uploaded.Groups.Add(lvg);
			Uploadable.Groups.Add(lvg);
			Available.Groups.Add(lvg);
			foreach (xml.ItemList.Map map in this._itemlist.Maps)
			{
				ListViewItem lvi = new ListViewItem(new string[] { map.name, map.mapnav.ToString(), map.zone });
				lvi.Group = lvg;
				if (maps.Contains(map))
				{
					//Have map
					Uploaded.Items.Add(lvi);
				}
				else
				{
					//Don't have map

					if (_activePFs == null || _activePFs.activePFs.Contains(map.zone))
					{
						if (mapnav >= map.mapnav)
							Uploadable.Items.Add(lvi);
						else
							Available.Items.Add(lvi);
					}
				}
			}
			#endregion
			ProcessInput_EndUpdate();
		}

		private void ProcessInput_EndUpdate()
		{
			Uploadable.EndUpdate();
			Uploaded.EndUpdate();
			Available.EndUpdate();
		}

		/// <summary>
		/// Method to handle sorting of ListViews based on collumns
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _lv_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			Forms.ListView_ColumnClick(sender, e);
		}

		private void update_ActivePFs()
		{
			ActivePFs.BeginUpdate();
			ActivePFs.Items.Clear();
			//Active PFs
			if (_itemlist != null)
				foreach (xml.ItemList.Map map in _itemlist.Maps)
				{
					ListViewItem lvi = new ListViewItem(map.zone);
					lvi.Checked = _activePFs.activePFs.Contains(map.zone);
					ActivePFs.Items.Add(lvi);
				}
			ActivePFs.EndUpdate();
		}

		private void Uploadable_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (e.Item.Checked)
			{
				timer_UpdateUploadable.Stop();
				timer_UpdateUploadable.Start();
			}

		}

		private void timer_RemoveUploadedItems_Action(object o, EventArgs e)
		{
			timer_UpdateUploadable.Stop();
			foreach (ListViewItem lvi in Uploadable.Items)
			{
				if (lvi.Checked)
				{
					Uploadable.Items.Remove(lvi);
					Uploaded.Items.Add(lvi);
				}
			}
		}

		#region Menu entry: About
		private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			AboutBox1 ab = new AboutBox1();
			ab.ShowDialog();
		}


		private void anarchyOnlineToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.anarchy-online.com");
		}

		#endregion

		private void anarchyOnlineUniverseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.ao-universe.com");
		}

		private void demodersToolsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://forums.anarchy-online.com/showthread.php?t=529799");
		}

		private void famousLastWordsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://flw.nu/");
		}

		private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://forums.anarchy-online.com/showthread.php?p=5192302#post5192302");
		}

		private void ActivePFs_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (e.Item.Checked)
				_activePFs.activePFs.Add(e.Item.Text);
			else
				_activePFs.activePFs.Remove(e.Item.Text);
		}

		private void button_SaveActivePFs_Click(object sender, EventArgs e)
		{
			Xml.Serialize<xml.ActivePFs>(new FileInfo("xml/ActivePFs.xml"), _activePFs, false);
		}

		private void mapsReaderDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!wc.IsBusy)
				wc.OpenReadAsync(new Uri("http://demoder.flw.nu/tools/MapUpgrades/Items.xml"), "items");
		}

		private void activePFDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!wc.IsBusy)
				wc.OpenReadAsync(new Uri("http://demoder.flw.nu/tools/MapUpgrades/ActivePFs.xml"), "apf");
		}

		private void checkForProgramUpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!wc.IsBusy)
				wc.OpenReadAsync(new Uri("http://demoder.flw.nu/tools/MapUpgrades/Version.xml"), "version");
		}

		private void wc_OpenReadCompleted(object obj, System.Net.OpenReadCompletedEventArgs e)
		{
			string tag = (string)e.UserState;
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			switch (tag)
			{
				case "items":
					xml.ItemList il = Xml.Deserialize<xml.ItemList>(e.Result, true);
					if (il != null)
					{
						if (_itemlist.VersionTag == il.VersionTag)
							MessageBox.Show("Already have latest version.", "Update: Item list");
						else if (_itemlist.VersionTag>=il.VersionTag)
							MessageBox.Show("Local version newer than remote version.", "Update: Item list");
						else
						{
							_itemlist = il;
							System.IO.File.Move("xml/Items.xml",
								String.Format("xml/Items.{0}-{1}-{2}.{3}-{4}-{5}.xml",
								DateTime.Now.Year,
								DateTime.Now.Month,
								DateTime.Now.Day,
								DateTime.Now.Hour,
								DateTime.Now.Minute,
								DateTime.Now.Second));
							Xml.Serialize<xml.ItemList>(new FileInfo("cfg/Items.xml"), _itemlist, false);
							MessageBox.Show("Update successfull.", "Update: Item list");
						}
					}
					else
					{
						goto case "InvalidContent";
					}
					break;
				case "apf":
					xml.ActivePFs apf = Xml.Deserialize<xml.ActivePFs>(e.Result, true);
					if (apf != null)
					{
						if (_activePFs.Version == apf.Version)
							MessageBox.Show("Already have latest version.", "Update: Active playfields");
						else if (_activePFs.Version >= apf.Version)
							MessageBox.Show("Local version newer than remote version.", "Update: Active playfields");
						else
						{
							_activePFs = apf;
							System.IO.File.Move("xml/Items.xml",
								String.Format("xml/Items.{0}-{1}-{2}.{3}-{4}-{5}.xml",
								DateTime.Now.Year,
								DateTime.Now.Month,
								DateTime.Now.Day,
								DateTime.Now.Hour,
								DateTime.Now.Minute,
								DateTime.Now.Second));
							Xml.Serialize<xml.ActivePFs>(new FileInfo("cfg/Items.xml"), _activePFs, false);
							MessageBox.Show("Update successfull", "Update: Active playfields");
						}
					}
					else
						goto case "InvalidContent";
					break;

				case "version":
					Version remoteversion = null;
					try
					{
						 remoteversion = new Version(Xml.Deserialize<xml.VersionInfo>(e.Result, false).Version);
					}
					catch { goto case "InvalidContent"; }
					Version localversion = new Version(new xml.VersionInfo().Version);
					if (remoteversion > localversion)
					{
						DialogResult dr = MessageBox.Show("Go to download page?", "Application update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (dr == DialogResult.Yes)
							goto case "UpdateLocalClient";
					}
					else if (remoteversion < localversion)
						MessageBox.Show("No updates available. (local newer than remote)", "Application update",MessageBoxButtons.OK, MessageBoxIcon.Information);
					else if (remoteversion==localversion)
						MessageBox.Show("No updates available.", "Application update", MessageBoxButtons.OK, MessageBoxIcon.Information);
						
					e.Result.Close();
					e.Result.Dispose();
					break;
				#region Cases for handling processed data
				case "InvalidContent":
					MessageBox.Show("Remote server provided invalid content", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
				case "UpdateLocalClient":
					System.Diagnostics.Process.Start("https://sourceforge.net/projects/demoderstools/files/Map%20Upgrades/");
					break;
				#endregion
			}
		}

		private void _anyTxt_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\x1')
			{
				((TextBox)sender).SelectAll();
				e.Handled = true;
			}
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Options options = new Options();
			DialogResult dr = options.ShowDialog();
			switch (dr)
			{
				case DialogResult.OK:
					Program.SaveConfig();
					Update_AOxPort_Characters();
					break;
			}
		}
	}
}