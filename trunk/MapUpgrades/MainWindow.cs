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
using MapUpgrades.cfg;

namespace MapUpgrades
{
    public partial class MainWindow : Form
    {
        string htmldocument=string.Empty;
		cfg.ItemList _itemlist;
		cfg.ActivePFs _activePFs;
		System.Net.WebClient wc = new System.Net.WebClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string[] text = _inputText.Text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            uint mapnav;
            uint.TryParse(this._mapNav.Text, out mapnav);
            ProcessInput(text, mapnav);

        }

        private void ProcessInput(string[] txt, uint mapnav)
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


			string maps = string.Empty;
			string mapupgrades = string.Empty;

            foreach (string t in txt)
            {
                if (t.StartsWith("Maps: "))
                    maps = t.Substring(6);
				if (t.StartsWith("Map reader upgrades: "))
					mapupgrades = t.Substring(21);
            }

			ProcessMapUpgrades(mapupgrades, mapnav);
			ProcessMaps(maps, mapnav);

			Uploadable.EndUpdate();
			Uploaded.EndUpdate();
			Available.EndUpdate();
        }

        private void ProcessMaps(string text, uint mapnav)
        {
			List<string> havemaps = new List<string>();
			ListViewGroup lvg = new ListViewGroup("lvg_maps", "Maps");
			lvg.HeaderAlignment = HorizontalAlignment.Center;
			Uploaded.Groups.Add(lvg);
			Uploadable.Groups.Add(lvg);
			Available.Groups.Add(lvg);

			if (true)
            {
                string[] t = text.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (string t2 in t)
                {
					string map = t2.Trim();
					if (!string.IsNullOrEmpty(map))
						havemaps.Add(map);
                }
            }

            foreach (cfg.ItemList.Map map in this._itemlist.Maps)
            {
				ListViewItem lvi = new ListViewItem(new string[] { map.name, map.mapnav.ToString(), map.zone });
				lvi.Group = lvg;
                if (havemaps.Contains(map.zone))
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
			
        }
        private void ProcessMapUpgrades(string upgrades, uint mapnav)
        {
			List<string> haveupgrades = new List<string>();
			ListViewGroup lvg = new ListViewGroup("lvg_mapreaderupgrades", "Map Readers");
			lvg.HeaderAlignment = HorizontalAlignment.Center;
			Uploaded.Groups.Add(lvg);
			Uploadable.Groups.Add(lvg);
			Available.Groups.Add(lvg);

			if (true)
			{
				string[] t = upgrades.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

				foreach (string t2 in t)
				{
					string upgrade = t2.Trim();
					if (!string.IsNullOrEmpty(upgrade))
						haveupgrades.Add(upgrade);
				}
			}
			
			foreach (cfg.ItemList.MapUpgrade upgrade in this._itemlist.MapUpgrades)
			{
				ListViewItem lvi = new ListViewItem(new string[] { upgrade.name, upgrade.mapnav.ToString(), upgrade.listname });

				lvi.Group = lvg;
				if (haveupgrades.Contains(upgrade.listname))
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
			}


		/// <summary>
		/// Method to handle sorting of ListViews based on collumns
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _lv_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			ListView lv = (ListView)sender;
			ListViewSorter lvs = new ListViewSorter();

			if (!(lv.ListViewItemSorter is ListViewSorter))
				lv.ListViewItemSorter = lvs;
			else
				lvs = (ListViewSorter)lv.ListViewItemSorter;
			if (lvs.LastSort == e.Column)
			{
				if (lv.Sorting == SortOrder.Ascending)
					lv.Sorting = SortOrder.Descending;
				else
					lv.Sorting = SortOrder.Ascending;
			}
			else
			{
				lv.Sorting = SortOrder.Descending;
			}
			lvs.ByColumn = e.Column;
			lv.Sort();
		}


		#region This class is taken from http://www.java2s.com/Code/CSharp/GUI-Windows-Form/SortaListViewbyAnyColumn.htm
		public class ListViewSorter : System.Collections.IComparer
		{
			public int Compare(object o1, object o2)
			{
				if (!(o1 is ListViewItem))
					return (0);
				if (!(o2 is ListViewItem))
					return (0);

				ListViewItem lvi1 = (ListViewItem)o2;
				string str1 = lvi1.SubItems[ByColumn].Text;
				ListViewItem lvi2 = (ListViewItem)o1;
				string str2 = lvi2.SubItems[ByColumn].Text;
				int result;
				if (lvi1.ListView.Sorting == SortOrder.Ascending)
					result = String.Compare(str1, str2);
				else
					result = String.Compare(str2, str1);
				LastSort = ByColumn;
				return (result);
			}
			public int ByColumn
			{
				get { return Column; }
				set { Column = value; }
			}
			int Column = 0;

			public int LastSort
			{
				get { return LastColumn; }
				set { LastColumn = value; }
			}
			int LastColumn = 0;
		}
		#endregion this class is taken from http://www.java2s.com/Code/CSharp/GUI-Windows-Form/SortaListViewbyAnyColumn.htm

		private void MainWindow_Load(object sender, EventArgs e)
		{
			wc.OpenReadCompleted += new System.Net.OpenReadCompletedEventHandler(wc_OpenReadCompleted);

			_itemlist = Xml.Deserialize.file<cfg.ItemList>("xml/Items.xml");
			_activePFs = Xml.Deserialize.file<cfg.ActivePFs>("xml/ActivePFs.xml");

			//Setup sorting.
			//Uploadable maps
			ListViewSorter lvs = new ListViewSorter();
			lvs.LastSort = lvs.ByColumn = 0;
			Uploadable.ListViewItemSorter = lvs;
			Uploadable.Sorting = SortOrder.Descending; //Somehow, setting this to ascending makes it sort descending, and vice versa....
			
			//Available
			ListViewSorter lvs2 = new ListViewSorter();
			lvs2.LastSort = lvs2.ByColumn = 1;
			Available.ListViewItemSorter = lvs2;
			Available.Sorting = SortOrder.Descending;

			//Uploaded
			ListViewSorter lvs3 = new ListViewSorter();
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
		}
		private void update_ActivePFs()
		{
			ActivePFs.BeginUpdate();
			ActivePFs.Items.Clear();
			//Active PFs
			if (_itemlist != null)
				foreach (cfg.ItemList.Map map in _itemlist.Maps)
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
			Xml.Serialize.file<cfg.ActivePFs>("xml/ActivePFs.xml", _activePFs);
		}

		
		


		private void mapsReaderDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wc.OpenReadAsync(new Uri("http://flw.nu/tools/MapUpgrades/Items.xml"), "items");
		}

		private void activePFDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			wc.OpenReadAsync(new Uri("http://flw.nu/tools/MapUpgrades/ActivePFs.xml"), "apf");
		}


		private void wc_OpenReadCompleted(object obj, System.Net.OpenReadCompletedEventArgs e)
		{
			string tag = (string)e.UserState;
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message, "Error updating", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			switch (tag)
			{
				case "items":
					cfg.ItemList il = Xml.Deserialize.stream<cfg.ItemList>(e.Result, true);
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
							Xml.Serialize.file<cfg.ItemList>("xml/Items.xml", _itemlist);
							MessageBox.Show("Update successfull.", "Update: Item list");
						}
					}
					else
					{
						MessageBox.Show("Invalid content", "Error updating", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					break;
				case "apf":
					cfg.ActivePFs apf = Xml.Deserialize.stream<cfg.ActivePFs>(e.Result, true);
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
							Xml.Serialize.file<cfg.ActivePFs>("xml/Items.xml", _activePFs);
							MessageBox.Show("Update successfull", "Update: Active playfields");
						}
					}
					else
						MessageBox.Show("Invalid content", "Error updating", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
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
	}
}