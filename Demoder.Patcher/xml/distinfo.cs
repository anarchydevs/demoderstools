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

namespace Demoder.Patcher.xml
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
		public List<DistInfo_Directory> content;
		#endregion
		#region constructors
		public DistInfo() {
			this.content = new List<DistInfo_Directory>();
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
			this.content = new List<DistInfo_Directory>();
			this.DistributionType = DistributionType;
			this.download_locations = new List<string>();
			foreach (string path in paths)
				this.content.Add(new DistInfo_Directory(path));
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

	public class DistInfo_Directory
	{
		#region members
		[XmlAttribute("name")]
		public string name;
		[XmlElement("file")]
		public List<DistInfo_FileInfo> files;
		[XmlElement("directory")]
		public List<DistInfo_Directory> directories;
		#endregion
		#region constructors
		public DistInfo_Directory()
		{
			files = new List<DistInfo_FileInfo>();
			directories = new List<DistInfo_Directory>();
		}
		public DistInfo_Directory(string path)
		{
			files = new List<DistInfo_FileInfo>();
			directories = new List<DistInfo_Directory>();
			foreach (string file in Directory.GetFiles(path))
				this.files.Add(new DistInfo_FileInfo(path + Path.DirectorySeparatorChar + file));
			foreach (string directory in Directory.GetDirectories(path))
				this.directories.Add(new DistInfo_Directory(path + Path.DirectorySeparatorChar + directory));
		}
		#endregion
		#region Operator overrides
		public static bool operator ==(DistInfo_Directory dir1, DistInfo_Directory dir2)
		{
			if (dir1.name != dir2.name) return false;
			//Compare files.
			foreach (DistInfo_FileInfo file in dir1.files)
				if (!dir2.files.Contains(file)) return false;
			foreach (DistInfo_FileInfo file in dir2.files)
				if (!dir1.files.Contains(file)) return false;
			
			//Compare directories
			bool found = false;
			foreach (DistInfo_Directory d1 in dir1.directories) {
				foreach (DistInfo_Directory d2 in dir2.directories)
				{
					if (d1 == d2)
					{
						found = true;
						break;
					}
				}
				if (found) break;
			}
			if (!found) return false;
			found = false;
			foreach (DistInfo_Directory d2 in dir2.directories)
			{
				foreach (DistInfo_Directory d1 in dir1.directories)
				{
					if (d1 == d2)
					{
						found = true;
						break;
					}
				}
				if (found) break;
			}
			if (!found) return false;

			return true;
		}
		public static bool operator !=(DistInfo_Directory dir1, DistInfo_Directory dir2)
		{
			return dir1 == dir2 ? false : true;
		}
		#endregion
		#region default overrides
		public override string ToString()
		{
			return this.name;
		}
		#endregion
	}

	public class DistInfo_FileInfo
	{
		#region members
		[XmlAttribute("name")]
		/// <summary>
		/// Filename
		/// </summary>
		public string name;

		[XmlAttribute("md5")]
		/// <summary>
		/// Files MD5 checksum
		/// </summary>
		public string md5;

		[XmlAttribute("sha1")]
		/// <summary>
		/// Files sha1 checksum
		/// </summary>
		public string sha1;

		[XmlAttribute("size")]
		/// <summary>
		/// Filesize in bytes
		/// </summary>
		public long size;
		#endregion
		#region constructors
		public DistInfo_FileInfo() {	}

		public DistInfo_FileInfo(string path)
		{
			FileInfo fi = new FileInfo(path);
			this.name = fi.Name+"."+fi.Extension;
			this.size=fi.Length;
			this.md5 = GenerateHash.md5_file(path);
			this.sha1 = GenerateHash.sha1_file(path);
		}
		#endregion

		#region operator overrides
		public static bool operator ==(DistInfo_FileInfo file1, DistInfo_FileInfo file2)
		{
			if (file1.md5 != file2.md5) return false;
			if (file1.sha1 != file2.sha1) return false;
			if (file1.name != file2.name) return false;
			if (file1.size != file2.size) return false;
			return true;
		}
		public static bool operator !=(DistInfo_FileInfo file1, DistInfo_FileInfo file2)
		{
			return file1 == file2 ? false : true;
		}
		#endregion

		#region default overides
		public override string ToString()
		{
			return this.name;
		}
		#endregion
	}
}
