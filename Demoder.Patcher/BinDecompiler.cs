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
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Demoder.Common;

namespace Demoder.Patcher
{
	public class BinDecompiler
	{
		
		/// <summary>
		/// Key: md5 hash.  Value: Slice bytes.
		/// </summary>
		private Dictionary<string, byte[]> _slices;
		private BinSlicePatterns _slice_patterns;

		public BinDecompiler(string binfile)
		{
			byte[] bin = File.ReadAllBytes(binfile);
			/*
			_slices = new Dictionary<string,byte[]>();
			//Slice patterns.
			_slice_patterns = new BinSlicePatterns();
			_slice_patterns.Add(new BinSlicePattern("png",
				new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }, //89 50 4E 47 0D 0A 1A 0A
				new byte[] { 73, 69, 78, 68, 174, 66, 96, 130 })); //49 45 4e 44 AE 42 60 82
			_slice_patterns.Add(new BinSlicePattern("jpg",
				new byte[] { 255, 216, 255, 224, 0, 16, 74, 70, 73, 70, 0 }, //FF D8 FF E0 00 10 4A 46 49 46 00
				new byte[] { 0, 0 }));

			Xml.Serialize.file<BinSlicePatterns>("binfilepatterns.xml", this._slice_patterns);
			*/
			this._slice_patterns = Xml.Deserialize.file<BinSlicePatterns>("binfilepatterns.xml");
		}
	}


	public class BinSlicePatterns
	{
		[XmlElement("pattern")]
		public List<BinSlicePattern> Entries = new List<BinSlicePattern>();
		public BinSlicePattern Get(string name)
		{
			lock (this.Entries)
				foreach (BinSlicePattern bsp in this.Entries)
				{
					if (bsp.Name == name)
						return bsp;
				}
			return null;
		}
		public void Add(BinSlicePattern bsp)
		{
			if (this.Entries.Contains(bsp))
				return;
			BinSlicePattern bsp2 = this.Get(bsp.Name);
			if (bsp2 != null)
				this.Entries.Remove(bsp2);

			this.Entries.Add(bsp);
		}
	}

	public class BinSlicePattern
	{
	[XmlIgnore]
		public long NextSlice_Start = -1;
	[XmlIgnore]
		public long NextSlice_Stop = -1;
		public string Name=string.Empty;
		[XmlArray(ElementName="Start")]
		[XmlArrayItem(ElementName="b")]
		public List<byte> StartPattern=new List<byte>();
		[XmlArray(ElementName = "Stop")]
		[XmlArrayItem(ElementName = "b")]
		public List<byte> StopPattern=new List<byte>();
		public BinSlicePattern()
		{

		}
		public BinSlicePattern(string Name, byte[] StartPattern, byte[] StopPattern)
		{
			this.Name = Name;
			this.StartPattern = new List<byte>(StartPattern);
			this.StopPattern = new List<byte>(StopPattern);
		}

		#region Operator overrides
		public static bool operator ==(BinSlicePattern bsp1, BinSlicePattern bsp2)
		{
			try
			{
				if (bsp1.Name != bsp2.Name) return false;
				if (bsp1.NextSlice_Start != bsp2.NextSlice_Start) return false;
				if (bsp1.NextSlice_Stop != bsp2.NextSlice_Stop) return false;
				if (bsp1.StartPattern != bsp2.StartPattern) return false;
				if (bsp1.StopPattern != bsp2.StopPattern) return false;
			}
			catch { return false; }
			return true;
		}

		public static bool operator !=(BinSlicePattern bsp1, BinSlicePattern bsp2)
		{
			return bsp1 == bsp2 ? false : true;
		}
		#endregion

		#region default overrides
		public override bool Equals(object obj)
		{
			try
			{
				BinSlicePattern compare = (BinSlicePattern)obj;
				if (compare == this) return true;
				else return false;
			}
			catch
			{
				return false;
			}
		}

		public override string ToString()
		{
			return this.Name;
		}
		#endregion
	}
}
