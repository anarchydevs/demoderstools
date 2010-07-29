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
		private void ProcessInput(string[] txt, uint mapnav)
		{
			string maps = string.Empty;
			string mapupgrades = string.Empty;

			foreach (string t in txt)
			{
				if (t.StartsWith("Maps: "))
					maps = t.Substring(6);
				if (t.StartsWith("Map reader upgrades: "))
					mapupgrades = t.Substring(21);
			}

			List<xml.ItemList.MapUpgrade> havemapupgrades = ProcessMapUpgrades(mapupgrades, mapnav);
			List<xml.ItemList.Map> havemaps = ProcessMaps(maps, mapnav);
			ProcessInput_DoUpdate(mapnav, havemaps, havemapupgrades);
		}

		private List<xml.ItemList.Map> ProcessMaps(string text, uint mapnav)
		{
			List<xml.ItemList.Map> havemaps = new List<xml.ItemList.Map>();
			if (true)
			{
				string[] t = text.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

				foreach (string t2 in t)
				{
					string map = t2.Trim();
					if (!string.IsNullOrEmpty(map))
						foreach (xml.ItemList.Map m in this._itemlist.Maps)
							if (m.zone == map)
								havemaps.Add(m);
				}
			}
			return havemaps;
		}

		private List<xml.ItemList.MapUpgrade> ProcessMapUpgrades(string upgrades, uint mapnav)
		{
			List<xml.ItemList.MapUpgrade> haveupgrades = new List<xml.ItemList.MapUpgrade>();
			if (true)
			{
				string[] t = upgrades.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				foreach (string t2 in t)
				{
					string upgrade = t2.Trim();
					if (!string.IsNullOrEmpty(upgrade))
						foreach (xml.ItemList.MapUpgrade m in this._itemlist.MapUpgrades)
							if (m.listname == upgrade)
								haveupgrades.Add(m);
				}
			}
			return haveupgrades;
		}
	}
}
