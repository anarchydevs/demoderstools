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
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Demoder.Patcher.xml
{

	public class distinfo
	{
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
		public List<distinfo_directory> content;
	}

	public class distinfo_directory
	{
		[XmlAttribute("name")]
		public string name;
		[XmlElement("file")]
		public List<distinfo_fileinfo> files;
		[XmlElement("directory")]
		public List<distinfo_directory> directories;
	}

	public class distinfo_fileinfo
	{
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
		public uint size;

		[XmlAttribute("type")]
		/// <summary>
		/// Type. file, bin
		/// </summary>
		public string type;
	}
}
