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
using System.Text;
using System.Xml.Serialization;

namespace Demoder.Patcher.DataClasses
{
	/// <summary>
	/// This class contains the start and stop patterns used to find a binslice
	/// </summary>
	public class BinSlicePatterns
	{
		[XmlElement("pattern")]
		public List<BinSlicePattern> Entries = new List<BinSlicePattern>();
		
		#region Methods
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
		public BinSlicePatterns Copy()
		{
			BinSlicePatterns bsps = new BinSlicePatterns();
			foreach (BinSlicePattern bsp in this.Entries)
				bsps.Add(new BinSlicePattern(bsp.Name, bsp.StartPattern, bsp.StopPattern)); //Copy them so we don't alter the original.
			return bsps;
		}
		#endregion
	}

	public class BinSlicePattern
	{
		[XmlIgnore]
		public int NextSlice_Start = -1;
		[XmlIgnore]
		public int NextSlice_Stop = -1;
		public string Name = string.Empty;
		[XmlArray(ElementName = "Start")]
		[XmlArrayItem(ElementName = "b")]
		public List<byte> StartPattern = new List<byte>();
		[XmlArray(ElementName = "Stop")]
		[XmlArrayItem(ElementName = "b")]
		public List<byte> StopPattern = new List<byte>();
		#region constructors
		public BinSlicePattern()
		{

		}
		public BinSlicePattern(string Name, byte[] StartPattern, byte[] StopPattern)
		{
			this.Name = Name;
			this.StartPattern = new List<byte>(StartPattern);
			this.StopPattern = new List<byte>(StopPattern);
		}

		public BinSlicePattern(string Name, List<byte> StartPattern, List<byte> StopPattern)
		{
			this.Name = Name;
			this.StartPattern = StartPattern;
			this.StopPattern = StopPattern;
		}
		#endregion
		#region default overrides
		public override bool Equals(object obj)
		{
			try
			{
				if (obj == null) return false;
				BinSlicePattern compare = (BinSlicePattern)obj;
				if (compare == this) return true;
				if (compare.Name != this.Name) return false;
				if (compare.NextSlice_Start != this.NextSlice_Start) return false;
				if (compare.NextSlice_Stop != this.NextSlice_Stop) return false;
				if (compare.StartPattern != this.StartPattern) return false;
				if (compare.StopPattern != this.StopPattern) return false;
				if (compare == this) return true;
			}
			catch
			{
				return false;
			}
			return false;
		}

		public override string ToString()
		{
			return this.Name;
		}
		#endregion
	}
}
