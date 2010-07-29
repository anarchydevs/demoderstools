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

namespace MapUpgrades.xml
{
    [XmlRoot("Items")]
    public class ItemList
    {
		public UInt64 VersionTag = 0;
        public List<Map> Maps;
        public List<MapUpgrade> MapUpgrades;
		public List<MapNavBuff> MapNavBuffs;
		public List<MapNavEquip> MapNavGear;
        /// <summary>
        /// Map entry.
        /// </summary>
        public class Map
        {
            [XmlAttribute("zone")]
            public string zone;
            [XmlAttribute("name")]
            public string name;
            [XmlAttribute("mapnav")]
            public uint mapnav;

			[XmlAttribute("MapsA")]
			public UInt32 MapsA;
			[XmlAttribute("MapsB")]
			public UInt32 MapsB;
			[XmlAttribute("MapsC")]
			public UInt32 MapsC;
			[XmlAttribute("MapsD")]
			public UInt32 MapsD;
        }

        public class MapUpgrade
        {
            [XmlAttribute("name")]
            public string name;
            [XmlAttribute("mapnav")]
            public uint mapnav;
            [XmlAttribute("listname")]
            public string listname;
			[XmlAttribute("bitflag")]
			public UInt32 bitflag;
        }

		public class MapNavBuff
		{
			[XmlAttribute("aoid")]
			public UInt32 AOID=0;
			[XmlAttribute("mapnav")]
			public UInt32 MapNav = 0;
		}

		public class MapNavEquip
		{
			// <Equip AOID="101597" LowQL="1" HighQL="1" LowValue="2" HighValue="3" />
			[XmlAttribute("AOID")]
			public UInt32 AOID = 0;
			[XmlAttribute("LowQL")]
			public UInt32 QL_Low = 0;
			[XmlAttribute("HighQL")]
			public UInt32 QL_High = 0;
			[XmlAttribute("LowValue")]
			public UInt32 MapNav_Low = 0;
			[XmlAttribute("HighValue")]
			public UInt32 MapNav_High = 0;

		}

    }
}
