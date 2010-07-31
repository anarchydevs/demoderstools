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

namespace Demoders_Patcher.DataClasses
{
	public class PatcherConfig
	{
		#region members
		public List<string> CentralUpdateServer;
		[XmlIgnore]
		public DirectoryInfo AnarchyOnlinePath
		{
			get
			{
				return new DirectoryInfo(this.AnarchyOnlinePath_string);
			}
			set
			{
				if (value == null) this.AnarchyOnlinePath_string = string.Empty;
				else
					this.AnarchyOnlinePath_string = value.FullName;
			}
		}
		public string AnarchyOnlinePath_string = string.Empty;
		/// <summary>
		/// How old should the local cache of the central definitions be before being considered outdated? Value is in seconds.
		/// </summary>
		public Int64 CentralDefinitions_CacheExpirityTime = 3600 * 24 * 7; //One week per default
		public List<PatchStatus> PatchStatus;
		#endregion

		#region constructors
		public PatcherConfig()
		{
			this.CentralUpdateServer = new List<string>();
			this.AnarchyOnlinePath = null;
			this.CentralUpdateServer.Add("http://ps.flw.nu/Central/Repository.xml");
			this.PatchStatus = new List<PatchStatus>();
		}
		#endregion
		public PatchStatus GetPatchStatus(Guid GUID)
		{
			lock (this.PatchStatus)
				foreach (PatchStatus ps in this.PatchStatus)
					if (ps.GUID == GUID)
						return ps;
			return null;
		}
		public void SetPatchStatus(Guid GUID, string version)
		{
			lock (this.PatchStatus)
			{
				foreach (PatchStatus ps in this.PatchStatus)
				{
					if (ps.GUID == GUID)
					{
						ps.Version = version;
						this.Save();
						break;
					}
				}
			}
		}

		public void Save() {
			Demoder.Common.Xml.Serialize<PatcherConfig>(Program.PatcherConfigPath, this, false);
		}
	}
}
