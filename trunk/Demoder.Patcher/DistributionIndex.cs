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
	public class DistributionIndex
	{
		public class Dir
		{
			#region members
			[XmlAttribute("name")]
			public string name;
			[XmlElement("file")]
			public List<File> files;
			[XmlElement("directory")]
			public List<Dir> directories;
			#endregion

			#region constructors
			public Dir()
			{
				files = new List<File>();
				directories = new List<Dir>();
			}
			public Dir(string path)
			{
				DirectoryInfo di = new DirectoryInfo(path);
				this.name = di.Name;
				files = new List<File>();
				directories = new List<Dir>();
				foreach (string file in Directory.GetFiles(path))
					this.files.Add(new File(file));
				foreach (string directory in Directory.GetDirectories(path))
					this.directories.Add(new Dir(directory));
			}
			#endregion

			#region Operator overrides
			public static bool operator ==(Dir dir1, Dir dir2)
			{
				if (dir1.name != dir2.name) return false;
				//Compare files.
				foreach (File file in dir1.files)
					if (!dir2.files.Contains(file)) return false;
				foreach (File file in dir2.files)
					if (!dir1.files.Contains(file)) return false;

				//Compare directories
				if (dir1.directories.Count != dir2.directories.Count) return false;
				if (dir1.directories.Count != 0)
				{
					bool found = false;
					foreach (Dir d1 in dir1.directories)
					{
						foreach (Dir d2 in dir2.directories)
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
					foreach (Dir d2 in dir2.directories)
					{
						foreach (Dir d1 in dir1.directories)
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
				}
				return true;
			}

			public static bool operator !=(Dir dir1, Dir dir2)
			{
				return dir1 == dir2 ? false : true;
			}
			/// <summary>
			/// To compare two distributions.
			/// </summary>
			/// <param name="dir1">Distribution we want</param>
			/// <param name="dir2">Distribution we have</param>
			/// <returns></returns>
			public static Dir operator -(Dir dir1, Dir dir2)
			{
				//Remove directories.
				foreach (Dir d2 in dir2.directories)
				{
					dir1.directories.Remove(d2);
				}
				//Remove files.
				foreach (File f2 in dir2.files)
				{
					dir1.files.Remove(f2);
				}
				return dir1;
			}

			#endregion

			#region default overrides
			public override bool Equals(object obj)
			{
				try
				{
					Dir compare = (Dir)obj;
					if (compare == this) return true;
					else return false;
				}
				catch
				{
					return false;
				}
			}

			public override string ToString()
			{
				return this.name;
			}
			#endregion

			//Ensure no harmfull paths are provided.
			public void Sanitize()
			{
				this.name = this.Sanitize(this.name);

				foreach (Dir d in this.directories)
					d.Sanitize();
				foreach (File f in this.files)
					f.name = this.Sanitize(f.name);
			}
			private string Sanitize(string str)
			{
				str = str.Replace("..", "");
				str = str.Replace("/", "");
				str = str.Replace("\\", "");
				str = str.Replace("\0", "");
				return str;
			}
		}

		public class File
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
			public File() { }

			public File(string path)
			{
				FileInfo fi = new FileInfo(path);
				this.name = fi.Name;
				this.size = fi.Length;
				this.md5 = GenerateHash.md5_file(path);
				this.sha1 = GenerateHash.sha1_file(path);
			}
			#endregion

			#region operator overrides
			public static bool operator ==(File file1, File file2)
			{
				if (file1.md5 != file2.md5) return false;
				if (file1.sha1 != file2.sha1) return false;
				if (file1.name != file2.name) return false;
				if (file1.size != file2.size) return false;
				return true;
			}
			public static bool operator !=(File file1, File file2)
			{
				return file1 == file2 ? false : true;
			}

			#endregion

			#region default overides
			public override bool Equals(object obj)
			{
				try
				{
					File compare = (File)obj;
					if (compare == this) return true;
					else return false;
				}
				catch
				{
					return false;
				}
			}
			public override string ToString()
			{
				return this.name;
			}
			#endregion
		}
	}
}
