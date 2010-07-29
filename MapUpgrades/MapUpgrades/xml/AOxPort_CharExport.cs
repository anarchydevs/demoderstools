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
	[XmlRoot("AOxPort")]
	public class AOxPort_CharExport
	{
		[XmlAttribute("CharacterName")]
		public string CharacterName = string.Empty;
		[XmlAttribute("TimeStamp")]
		public String TimeStamp = String.Empty;
		[XmlAttribute("Version")]
		public String Version = string.Empty;
		[XmlElement("Skill")]
		public List<Skill> Skills = new List<Skill>(1024);
		[XmlElement("Item")]
		public List<Item> Items = new List<Item>(128);
		[XmlElement("Nano")]
		public List<Nano> Nanos = new List<Nano>(32);


		public class Skill
		{
			[XmlAttribute("ID")]
			public int ID = 0;
			[XmlAttribute("Value")]
			public Int32 Value = 0;
		}

		public class Item
		{
			[XmlAttribute("ID")]
			public int ID;
			[XmlAttribute("QL")]
			public int QL;
			[XmlAttribute("Slot")]
			public int Slot;
		}

		public class Nano
		{
			[XmlAttribute("ID")]
			public int ID;
		}
	}
}
