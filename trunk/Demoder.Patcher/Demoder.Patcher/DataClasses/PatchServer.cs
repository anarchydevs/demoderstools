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
using System.IO;
using Demoder.Common;

namespace Demoder.Patcher.DataClasses
{
	public class PatchServer
	{
		#region Members
		
		/// <summary>
		/// Where should we look for files to download?
		/// </summary>
		[XmlArray("PatchServers")]
		[XmlArrayItem("PatchServer")]
		public List<string> download_locations = new List<string>();
		
		[XmlAttribute("guid")]
		public Guid GUID = Guid.NewGuid();
		
		[XmlAttribute("name")]
		public string Name = String.Empty;
		
		[XmlAttribute("version")]
		public string Version="Undefined";
		
		[XmlArray("Distributions")]
		[XmlArrayItem("Distribution")]
		public List<Distribution> Distributions = new List<Distribution>();

		#endregion

		#region Methods
		public void Export(string DestinationDirectory)
		{
			if (File.Exists(DestinationDirectory)) throw new InvalidOperationException("The target directory is actually a file.");
			foreach (Distribution dist in this.Distributions)
			{
				string exportpath = DestinationDirectory + Path.DirectorySeparatorChar + dist.DistType + Path.DirectorySeparatorChar;
				if (!Directory.Exists(exportpath)) Directory.CreateDirectory(exportpath);
				foreach (DistributionIndex.Dir dir in dist.Directories)
				{
					dir.DecompileBinFiles();
					dir.Export(DestinationDirectory, exportpath);
				}
			}
			Xml.Serialize<PatchServer>(new FileInfo(DestinationDirectory + Path.DirectorySeparatorChar + "PatchServer.xml"), this, false);
		}
		#endregion
	}
}