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
        cfg.ItemList _itemlist = Xml.Deserialize.file<cfg.ItemList>("xml/Items.xml");

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
					if (map.active_pf)
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
    }
}