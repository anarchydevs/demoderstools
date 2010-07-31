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
namespace DynamicMaps.DataClasses
{
	[XmlRoot("root")]
	public class MapCoordinates
	{
		[XmlElement("Playfield")]
		public List<Playfield> Playfields = new List<Playfield>();

		public Playfield GetPF(int id)
		{
			lock (this.Playfields)
				foreach (Playfield pf in this.Playfields)
					if (pf.id == id)
						return pf;
			return null;
		}

		public class Playfield
		{
			[XmlAttribute("id")]
			public int id = 0;
			[XmlAttribute("name")]
			public string name = string.Empty;
			[XmlAttribute("x")]
			public double x = 0;
			[XmlAttribute("xscale")]
			public double xscale = 0;
			[XmlAttribute("z")]
			public double z = 0;
			[XmlAttribute("zscale")]
			public double zscale = 0;
		}
	}
}
