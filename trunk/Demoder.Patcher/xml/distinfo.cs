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

	public class DistInfo
	{
		public DistInfo() {
			this.content = new List<DistInfo_Directory>();
			this.DistributionType = "";
			this.download_locations = new List<string>();
		}

		public DistInfo(List<string> paths, string DistributionType, List<string> DownloadLocations)
		{
			this.content = new List<DistInfo_Directory>();
			this.DistributionType = "";
			this.download_locations = new List<string>();
			foreach (string path in paths)
				this.content.Add(new DistInfo_Directory(path));
		}
		/// <summary>
		/// What distribution type is this?
		/// </summary>
		public string DistributionType;

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
	}

	public class DistInfo_Directory
	{
		public DistInfo_Directory() {
			files = new List<DistInfo_FileInfo>();
			directories=new List<DistInfo_Directory>()
		}
		public DistInfo_Directory(string path)
		{
			files = new List<DistInfo_FileInfo>();
			directories=new List<DistInfo_Directory>();
			foreach (string file in Directory.GetFiles(path))
				this.files.Add(new DistInfo_FileInfo(path + Path.DirectorySeparatorChar + file));
			foreach (string directory in Directory.GetDirectories(path))
				this.directories.Add(new DistInfo_Directory(path + Path.DirectorySeparatorChar + directory);
		}
		[XmlAttribute("name")]
		public string name;
		[XmlElement("file")]
		public List<DistInfo_FileInfo> files;
		[XmlElement("directory")]
		public List<DistInfo_Directory> directories;
	}

	public class DistInfo_FileInfo
	{
		public DistInfo_FileInfo() {	}

		public DistInfo_FileInfo(string path)
		{
			FileInfo fi = new FileInfo(path);
			this.name = fi.Name+"."+fi.Extension;
			this.size=fi.Length;
			this.md5 = GenerateHash.md5_file(path);
			this.sha1 = GenerateHash.sha1_file(path);
			this.type = "file"; //Replace or remove this.. not sure yet.
		}

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

		[XmlAttribute("type")]
		/// <summary>
		/// Type. file, bin
		/// </summary>
		public string type;
	}
}
