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
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MapUpgrades.cfg
{
    [XmlRoot("Items")]
    public class ItemList
    {
        public List<Map> Maps;
        public List<MapUpgrade> MapUpgrades;
        /// <summary>
        /// Map entry.
        /// </summary>
        public struct Map
        {
            [XmlAttribute("zone")]
            public string zone;
            [XmlAttribute("name")]
            public string name;
            [XmlAttribute("mapnav")]
            public uint mapnav;
            [XmlAttribute("active_pf")]
            /// <summary>
            /// Is the playfield active?
            /// </summary>
            public bool active_pf;
        }

        public struct MapUpgrade
        {
            [XmlAttribute("name")]
            public string name;
            [XmlAttribute("mapnav")]
            public uint mapnav;
            [XmlAttribute("listname")]
            public string listname;
        }

    }
}
