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
using System.Threading;

using Demoder.Common;
using Demoder.Patcher.DataClasses;

namespace Demoder.Patcher.DataClasses
{
	/// <summary>
	/// This class represents a single distribution
	/// </summary>
	public class Distribution
	{
		#region members
		[XmlAttribute("name")]
		public string DistributionName = DateTime.Now.Ticks.ToString();
		/// <summary>
		/// Globally Unique IDentifier. Used to identify all versions of this distribution.
		/// </summary>
		[XmlAttribute("guid")]
		public Guid GUID = new Guid();
		
		[XmlElement("directory")]
		/// <summary>
		/// Distribution content
		/// </summary>
		public List<DistributionIndex.Dir> Directories;

		[XmlAttribute("type")]
		public DistributionType DistType = DistributionType.Other;
		#endregion
		
		#region constructors
		public Distribution() {
			this.Directories = new List<DistributionIndex.Dir>();
		}

		/// <summary>
		/// Creates an overview of a distribution.
		/// </summary>
		/// <param name="paths">Paths to index</param>
		/// <param name="DistributionType">Distribution type</param>
		/// <param name="DownloadLocations">Location to download updates</param>
		public Distribution(List<string> Directories, DistributionType DistType)
		{
			this.DistType = DistType;
			this.Directories = new List<DistributionIndex.Dir>();
			foreach (string dir in Directories)
				this.Directories.Add(new DistributionIndex.Dir(dir, true));
		}
		#endregion

		#region Methods
		public void DecompileBinFiles()
		{
			//Linear mode.
			//To spawn multiple decompiles at the same time, use dir.GetBinFiles() to generate a working queue.
			foreach (DistributionIndex.Dir dir in this.Directories)
				dir.DecompileBinFiles();
		}

		/// <summary>
		/// Retrieves this distributions binfiles.
		/// </summary>
		/// <param name="RemoveEntry">Set to true to remove entry from this instance.</param>
		/// <returns></returns>
		public List<BinFile> GetBinFiles(bool RemoveEntry)
		{
			List<BinFile> bf = new List<BinFile>();
			foreach (DistributionIndex.Dir dir in this.Directories)
				bf.AddRange(dir.GetBinFiles(RemoveEntry));
			return bf;
		}
		#endregion


		#region static members
		/// <summary>
		/// Gets the base directory of a given distribution type.
		/// </summary>
		/// <param name="DistributionType"></param>
		/// <param name="BaseDirectory"></param>
		/// <returns></returns>
		public static string GetRelativeBaseDir(DistributionType DistributionType, string BaseDirectory)
		{
			string basedir = BaseDirectory;
			switch (DistributionType)
			{
				case DistributionType.AO_Map:
					basedir += string.Format("{0}cd_image{0}textures{0}PlanetMap",
						Path.DirectorySeparatorChar);
					break;
				case DistributionType.AO_GUI:
					basedir += string.Format("{0}cd_image{0}gui",
						Path.DirectorySeparatorChar);
					break;
				case DistributionType.AO_GUI_Textures:
					basedir += string.Format("{0}cd_image{0}textures{0}GUI",
						Path.DirectorySeparatorChar);
					break;

				case DistributionType.AO_Script:
					basedir += string.Format("{0}scripts",
						Path.DirectorySeparatorChar);
					break;
				default:
				case DistributionType.Other:

					break;
			}
			return basedir;
		}

		public static DirectoryInfo GetRelativeBaseDir(DistributionType DistributionType, DirectoryInfo DirectoryInfo)
		{
			return new DirectoryInfo(GetRelativeBaseDir(DistributionType, DirectoryInfo.FullName));
		}

		public static bool IsBannedDirectory(Distribution.DistributionType DistType, string dir)
		{
			if (dir.Length == 0) return true;
			if (dir.StartsWith(".")) return true;
			if (dir.Contains("..")) return true;
			if (dir.Contains("/")) return true;
			if (dir.Contains("\\")) return true;
			switch (DistType)
			{
				case Distribution.DistributionType.AO_GUI:
					if (dir.ToLower() == "default") return true;
					break;
				case Distribution.DistributionType.AO_GUI_Textures: //Nothing to check for here
					break;
				case Distribution.DistributionType.AO_Map:
					if (dir.ToLower() == "normal") return true;
					if (dir.ToLower() == "shadowlands") return true;
					break;
				case Distribution.DistributionType.AO_Script: //Nothing to check for here
					break;
				case Distribution.DistributionType.Other: //Nothing to check for here.
					break;
			}
			return false;
		}

		#endregion

		#region operators
		public static bool operator ==(Distribution di1, Distribution di2) {
			if (di1.DistributionName != di2.DistributionName) return false;
			if (di1.DistType != di2.DistType) return false;
			//Directories
			if (di1.Directories != di2.Directories) return false;
			return true;
		}

		public static bool operator !=(Distribution di1, Distribution di2)
		{
			return di1 == di2 ? false : true;
		}
		#endregion

		#region overrides
		public override bool Equals(object obj)
		{
			try
			{
				Distribution di = (Distribution)obj;
				return this == di;
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region Enums
		public enum DistributionType
		{
			AO_Map,
			AO_GUI_Textures,
			AO_GUI,
			AO_Script,
			Other
		}
		#endregion

		#region classes
		private class BinDecompileWorkTask
		{
			public BinFile binfile;
			public bool MultiThread=false;
		}
		#endregion
	}
}
