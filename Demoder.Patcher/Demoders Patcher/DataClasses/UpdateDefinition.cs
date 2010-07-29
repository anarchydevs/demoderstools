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
using System.Xml.Serialization;
using System.Text;
using System.IO;
using Demoder.Patcher;
using Demoder.Patcher.DataClasses;
using Demoder.Common;

namespace Demoders_Patcher.DataClasses
{
	/// <summary>
	/// Class describing a set of distributions.
	/// </summary>
	public class UpdateDefinition
	{
		[XmlAttribute("name")]
		public string Name = "Noname";
		[XmlAttribute("description")]
		public string Description = "Some description";
		[XmlAttribute("guid")]
		public Guid GUID = Guid.NewGuid();
		[XmlAttribute("version")]
		public string Version = string.Empty;
		[XmlAttribute("type")]
		public DefinitionType DefinitionType = DefinitionType.Other;
		[XmlElement("server")]
		public List<string> UpdateServers = new List<string>();

		/// <summary>
		/// Check if this distribution exists locally
		/// </summary>
		/// <param name="AODir"></param>
		/// <param name="any">true: return true if any first-level subdirectory exists. false: return true if all first-level subdirectories exist.</param>
		/// <returns></returns>
		public bool Exists(DirectoryInfo AODir, bool any)
		{
			PatchServer ps = null;
			string[] usa = new string[this.UpdateServers.Count];
			this.UpdateServers.CopyTo(usa);
			List<string> us = new List<string>(usa);
			do
			{
				int mirrornum = (new Random()).Next(0, us.Count -1);
				string mirror = us[mirrornum];
				us.RemoveAt(mirrornum);
				ps = Xml.Deserialize<PatchServer>(new Uri(mirror));
			} while (ps == null && us.Count>0);
			if (ps == null)
			{
				throw new Exception("Unable to fetch patch info");
			}
			//We have patch info.
			foreach (Distribution distribution in ps.Distributions)
			{
				string relativedir = Distribution.GetRelativeBaseDir(distribution.DistType, AODir.FullName);
				foreach (DistributionIndex.Dir dir in distribution.Directories)
				{
					if (Directory.Exists(relativedir + Path.DirectorySeparatorChar + dir.name))
					{
						if (any) return true;
					}
					else if (!any) //If it doesn't exist, and we need to match all...
						return false;
				}
			}
			return false;
		}
	}
}
