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
            htmldocument = "<html><body>";
            foreach (string t in txt)
            {
                if (t.StartsWith("Maps: "))
                    ProcessMaps(t.Substring(6));
                if (t.StartsWith("Map reader upgrades: "))
                    ProcessMapUpgrades(t.Substring(21));
            }

            htmldocument += "</body></html>";
        }

        private void ProcessMaps(string text)
        {
            htmldocument+="<table>";
            htmldocument+="        <tr>";
            htmldocument+="             <th>lala</th></tr></table>";

            List<string> maps = new List<string>();

            

            if (true)
            {
                string[] t = text.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (string map in t)
                {
                    string mapname = map.Trim();
                    maps.Add(mapname);
                }
            }
            foreach (cfg.ItemList.Map map in this._itemlist.Maps)
            {
                if (maps.Contains(map.zone))
                {
                    //Have map
                }
                else
                {
                    //Don't have map
                }
            }
        }
        private void ProcessMapUpgrades(string upgrades)
        {

        }
    }
}