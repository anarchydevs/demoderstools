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
using Demoder.Common;
namespace Demoders_Patcher.DataClasses
{
	/// <summary>
	/// Class to keep track of local updatedefinition versions
	/// </summary>
	public class PatchStatus
	{
		[XmlAttribute("guid")]
		public Guid GUID = new Guid();
		[XmlAttribute("version")]
		public string Version = string.Empty;
		[XmlIgnore]
		public Presence Present = Presence.Unknown;
		[XmlIgnore]
		public StatusFlag StatusFlag = StatusFlag.Unknown;

		public PatchStatus(Guid GUID, string Version)
		{
			this.GUID = GUID;
			this.Version = Version;
		}
		public PatchStatus() { }

		public enum Presence
		{
			Present,
			NotPresent,
			Unknown
		}

		public override string ToString()
		{
			return this.GUID.ToString() + ": " + this.Present.ToString() + "/" + this.StatusFlag.ToString()+ "/" + this.Version;
		}
	}
}
