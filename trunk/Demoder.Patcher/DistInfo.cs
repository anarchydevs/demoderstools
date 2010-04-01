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
	#region might be moved into application
	public class DistInfo
	{
		#region members
		/// <summary>
		/// What distribution type is this?
		/// </summary>
		public DistributionType DistributionType;

		[XmlElement("download_location")]
		/// <summary>
		/// Where should we look for files to download?
		/// </summary>
		public List<string> download_locations;

		[XmlElement("directory")]
		/// <summary>
		/// Distribution content
		/// </summary>
		public List<DistributionIndex.Directory> content;
		#endregion
		#region constructors
		public DistInfo() {
			this.content = new List<DistributionIndex.Directory>();
			this.DistributionType = DistributionType.Other;
			this.download_locations = new List<string>();
		}

		/// <summary>
		/// Creates an overview of a distribution.
		/// </summary>
		/// <param name="paths">Paths to index</param>
		/// <param name="DistributionType">Distribution type</param>
		/// <param name="DownloadLocations">Location to download updates</param>
		public DistInfo(List<string> paths, DistributionType DistributionType, List<string> DownloadLocations)
		{
			this.content = new List<DistributionIndex.Directory>();
			this.DistributionType = DistributionType;
			this.download_locations = new List<string>();
			foreach (string path in paths)
				this.content.Add(new DistributionIndex.Directory(path));
		}
		#endregion
	}

	public enum DistributionType
	{
		GUI,
		Map,
		Script,
		Other
	}
	#endregion
}
