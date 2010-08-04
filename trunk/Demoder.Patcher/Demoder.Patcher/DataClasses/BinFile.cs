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
	/// This class represents a binfiles structure.
	/// </summary>
	public class BinFile
	{
		[XmlAttribute("name")]
		public string Name = string.Empty;
		[XmlAttribute("MD5")]
		public string MD5 = string.Empty;
		[XmlAttribute("SHA1")]
		public string SHA1 = string.Empty;
		[XmlElement("bs")]
		public List<BinFileSlice> BinFileSlices;

		#region overrides
		public override bool Equals(object obj)
		{
			BinFile bf = null;
			try
			{
				bf = (BinFile)obj;
			}
			catch { return false; }
			if (this.MD5 == bf.MD5 && this.SHA1 == bf.SHA1) return true;
			return false;
		}
		#endregion
	}
}
