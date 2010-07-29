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
	partial class MainWindow
	{


		#region AOxPort .xml integration
		private void ProcessInput(xml.AOxPort_CharExport ChrInfo)
		{
			if (ChrInfo == null) return;
			UInt32 MapsA = 0, MapsB = 0, MapsC = 0, MapsD = 0;
			int Intelligence = 0, Sense = 0, Psychic = 0, MapNav=0;
			

			List<ItemList.MapUpgrade> havemapupgrades = new List<ItemList.MapUpgrade>();
			List<ItemList.Map> havemaps = new List<ItemList.Map>();
			//Check skills.
			foreach (xml.AOxPort_CharExport.Skill skill in ChrInfo.Skills)
			{
				//Intelligence
				if (skill.ID == 19)
				{
					Intelligence += skill.Value;
				}
				//Sense
				if (skill.ID == 20)
				{
					Sense += skill.Value;
				}
				//Psychic
				if (skill.ID == 21)
				{
					Psychic += skill.Value;
				}

				if (skill.ID == 140) //MapNavigation skill
				{
					int.TryParse(skill.Value.ToString(), out MapNav);
				}
				if (skill.ID == 470) //MapReaderUpgrades - bitflag
				{
					//Go through each map and see if its bitflag is set in the respective skill. (A)
					foreach (ItemList.MapUpgrade mu in this._itemlist.MapUpgrades)
					{
						UInt32 curskill;
						UInt32.TryParse(skill.Value.ToString(), out curskill);
						if ((curskill & mu.bitflag) == mu.bitflag)
							havemapupgrades.Add(mu);
					}
				}
				if (skill.ID == 471) //MapsA - bitflag
				{
					UInt32.TryParse(skill.Value.ToString(), out MapsA);
				}
				if (skill.ID == 472) //MapsB - bitflag
				{
					UInt32.TryParse(skill.Value.ToString(), out MapsB);
				}
				//MapsC is unused.

				if (skill.ID == 586) //MapsD - bitflag
				{
					UInt32.TryParse(skill.Value.ToString(), out MapsD);
				}
			}

			//Check buffs

			//Check equips
			foreach (xml.AOxPort_CharExport.Item item in ChrInfo.Items)
			{
				int mn=0;
				int.TryParse(this.Intrepolation((uint)item.ID, (uint)item.QL).ToString(), out mn);
				MapNav += mn;
			}

			MapNav += (int)System.Math.Round((double)Intelligence / (double)100 / (double)4 * (double)40, 0);
			MapNav += (int)System.Math.Round((double)Sense / (double)100 / (double)4 * (double)50, 0);
			MapNav += (int)System.Math.Round((double)Psychic / (double)100 / (double)4 * (double)10, 0);
			//Check maps.
			foreach (xml.ItemList.Map map in this._itemlist.Maps)
			{
				if (map.MapsA != 0 && ((MapsA & map.MapsA) == map.MapsA))
					havemaps.Add(map);
				if (map.MapsB != 0 && ((MapsB & map.MapsB) == map.MapsB))
					havemaps.Add(map);
				if (map.MapsC != 0 && ((MapsC & map.MapsC) == map.MapsC))
					havemaps.Add(map);
				if (map.MapsD != 0 && ((MapsD & map.MapsD) == map.MapsD))
					havemaps.Add(map);
			}
			uint mnav = 0;
			uint.TryParse(MapNav.ToString(), out mnav);
			ProcessInput_DoUpdate(mnav, havemaps, havemapupgrades);
		}

		private void Update_AOxPort_Characters()
		{
			fsWatcher.EnableRaisingEvents = false;
			if (string.IsNullOrEmpty(Program.Configuration.AOxPort_ExportDirectory)) return;
			if (Directory.Exists(Program.Configuration.AOxPort_ExportDirectory))
			{
				string[] files = Directory.GetFiles(Program.Configuration.AOxPort_ExportDirectory, "*.xml", SearchOption.AllDirectories);
				AOxPort_ListView.Items.Clear();
				foreach (string file in files)
				{
					xml.AOxPort_CharExport chrinfo = new xml.AOxPort_CharExport();
					try
					{
						chrinfo = Xml.Deserialize<xml.AOxPort_CharExport>(new FileInfo(file), false);
					}
					catch { continue; }
					if (chrinfo == null) continue;
					ListViewItem lvi = new ListViewItem();
					lvi.Text = chrinfo.CharacterName;
					lvi.Tag = file;
					AOxPort_ListView.Items.Add(lvi);
					if (lvi.Text == Program.SelectedAOxPortCharacter)
						lvi.Selected = true;
					else
						lvi.Selected = false;
				}
			}
			fsWatcher.Path = Program.Configuration.AOxPort_ExportDirectory;
			fsWatcher.Changed -= new FileSystemEventHandler(fsWatcher_Changed);
			fsWatcher.Changed += new FileSystemEventHandler(fsWatcher_Changed);
			fsWatcher.Deleted -= new FileSystemEventHandler(fsWatcher_Deleted);
			fsWatcher.Deleted += new FileSystemEventHandler(fsWatcher_Deleted);
			fsWatcher.NotifyFilter = NotifyFilters.LastWrite;
			fsWatcher.Filter = "*.xml";
			fsWatcher.EnableRaisingEvents = true;
			
		}

		private void fsWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			foreach (ListViewItem lvi in AOxPort_ListView.Items)
				if (lvi.Tag == e.FullPath)
					if (Program.SelectedAOxPortCharacter == lvi.Text)
						ProcessInput(Xml.Deserialize<xml.AOxPort_CharExport>(new FileInfo(e.FullPath), false));

		}

		private void fsWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			foreach (ListViewItem lvi in AOxPort_ListView.Items)
				if (lvi.Tag == e.FullPath) AOxPort_ListView.Items.Remove(lvi);
		}

		private void AOxPort_ListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView lv = (ListView)sender;
			if (lv.SelectedItems.Count == 0) return;
			
			ListViewItem lvi = lv.SelectedItems[0];
			ProcessInput(Xml.Deserialize<xml.AOxPort_CharExport>(new FileInfo((string)lvi.Tag), false));
		}


		private UInt32 Intrepolation(UInt32 AOID, UInt32 ql)
		{
			uint LowQL=0, HighQL=0, LowMod=0, HighMod=0;
			foreach (xml.ItemList.MapNavEquip mne in this._itemlist.MapNavGear)
			{
				if (mne.AOID == AOID)
				{
					LowQL = mne.QL_Low;
					HighQL = mne.QL_High;
					LowMod = mne.MapNav_Low;
					HighMod = mne.MapNav_High;
					break;
				}
			}
			if (LowQL == ql) return LowMod;
			else if (HighQL == ql) return HighMod;
			else
			{
				uint ModQL = ql - LowQL;
				uint DeltaQL = HighQL - LowQL;
				uint DeltaModifier = HighMod - LowMod;
				double Seed = (double)DeltaModifier / (double)DeltaQL;
				uint i = (uint)Math.Round((double)LowMod + ((double)Seed * (double)ModQL), 0);
				return i;
			}
		}

		#endregion
	}
}
