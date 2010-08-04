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

namespace Demoders_Patcher.DataClasses
{
	public class CreatePatchServerConfig
	{
		#region Members
		/// <summary>
		/// User-readable name of this patchserver.
		/// </summary>
		public string Name = "noname";

		/// <summary>
		/// Where should we look for files to download?
		/// </summary>
		[XmlArray("PatchServers")]
		[XmlArrayItem("PatchServer")]
		public List<string> download_locations = new List<string>();

		/// <summary>
		/// Patchservers GUID
		/// </summary>
		public Guid GUID = Guid.NewGuid();

		/// <summary>
		/// Version
		/// </summary>
		public string Version = "Undefined";

		/// <summary>
		/// Distributions
		/// </summary>
		[XmlElement("Distribution")]
		public List<CreateDistributionConfig> Distributions = new List<CreateDistributionConfig>();

		/// <summary>
		/// Which directory should the patchserver be saved to?
		/// </summary>
		public string OutputDirectory = string.Empty;
		#endregion
	}
}
