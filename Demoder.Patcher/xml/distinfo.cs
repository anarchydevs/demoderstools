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
