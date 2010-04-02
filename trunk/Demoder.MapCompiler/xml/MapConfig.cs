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

namespace Demoder.MapCompiler.xml
{
	[XmlRoot("MapConfig")]
	public class MapConfig
	{
		public MapConfig()
		{
		}
		public MapConfig(string name, VersionInfo version, string mapdir)
		{
			this.Name = name;
			this.Version = version;
			this.MapDir = mapdir;
		}

		/// <summary>
		/// Map name
		/// </summary>
		public string Name = "Some name longer than Map";
		/// <summary>
		/// Short map name - used when assembling into multiple binfiles.
		/// </summary>
		public string ShortName = "Map";
		/// <summary>
		/// Where to store the assembler result?
		/// </summary>
		public string OutputDirectory = "tmp" + Path.DirectorySeparatorChar;
		/// <summary>
		/// Version information
		/// </summary>
		public VersionInfo Version = new VersionInfo(0, 0, 0);
		public string MapDir = "TestMap";
		/// <summary>
		/// How to assemble the binfile?
		/// </summary>
		public AssemblyMethod Assembler = AssemblyMethod.Single;
		public int TextureSize = 128;
		/// <summary>
		/// List of images
		/// </summary>
		public List<LoadImage> Images = new List<LoadImage>();
		//Worker tasks
		public List<WorkTask> WorkerTasks = new List<WorkTask>();
		//List of map versions.
		public List<TxtFile> TxtFiles = new List<TxtFile>();

		#region methods: worktask
		/// <summary>
		/// Retrieves a work task.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public WorkTask GetWorkTask(string name)
		{
			lock (this.WorkerTasks)
				foreach (WorkTask wt in this.WorkerTasks)
					if (wt.workname == name)
						return wt;
			return null;
		}

		/// <summary>
		/// Do we have a work task by this name?
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool ContainsWorkTask(string name)
		{
			lock (this.WorkerTasks)
				foreach (WorkTask wt in this.WorkerTasks)
					if (wt.workname == name) return true;
			return false;
		}

		/// <summary>
		/// Removes a work task.
		/// </summary>
		/// <param name="name"></param>
		public void RemoveWorkTask(string name)
		{
			lock (this.WorkerTasks)
				foreach (WorkTask wt in this.WorkerTasks)
					if (wt.workname == name)
					{
						this.WorkerTasks.Remove(wt);
						return;
					}
		}

		/// <summary>
		/// Replaces an existing work task. If it doesn't already exist, adds.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="worktask"></param>
		public void ReplaceWorkTask(string name, WorkTask worktask)
		{
			lock (this.WorkerTasks)
				foreach (WorkTask wt in this.WorkerTasks)
					if (wt.workname == name)
					{
						int index = this.WorkerTasks.IndexOf(wt);
						this.WorkerTasks.Remove(wt);
						this.WorkerTasks.Insert(index, worktask);
						return;
					}
			this.WorkerTasks.Add(worktask);
		}

		#endregion

		#region methods: layers
		public bool ContainsLayer(string name)
		{
			lock (this.WorkerTasks)
			{
				foreach (WorkTask wt in this.WorkerTasks)
				{
					if (wt.workentries.Contains(name))
						return true;
				}
			}
			return false;
		}

		public string[] GetAllLayerNames()
		{
			List<string> o = new List<string>();
			lock (this.WorkerTasks) {
				foreach (WorkTask wt in this.WorkerTasks)
				{
					o.AddRange(wt.workentries);
				}

			}
			return o.ToArray();
		}

		/// <summary>
		/// Retrieve an array of work task names containing layer name
		/// </summary>
		/// <param name="LayerName"></param>
		/// <returns></returns>
		public string WorkTasksContainingLayer(string LayerName)
		{
			lock (this.WorkerTasks)
			{
				foreach (WorkTask wt in this.WorkerTasks)
					if (wt.workentries.Contains(LayerName))
						return wt.workname;
			}
			return null;
		}

		#endregion

		#region methods: TxtFiles
		public bool ContainsTxtFile(string txtFile)
		{
			lock (this.TxtFiles)
			{
				foreach (TxtFile tf in this.TxtFiles)
					if (tf.File.ToLower() == txtFile.ToLower())
						return true;
			}
			return false;
		}

		public TxtFile GetTxtFile(string txtFile)
		{
			lock (this.TxtFiles)
			{
				foreach (TxtFile tf in this.TxtFiles)
					if (tf.File.ToLower() == txtFile.ToLower())
						return tf;
			}
			return null;
		}

		public void ReplaceTxtFile(string txtFile, TxtFile txt)
		{
			lock (this.TxtFiles)
			{
				this.RemoveTxtFile(txtFile);
				this.TxtFiles.Add(txt);
			}
		}

		public void RemoveTxtFile(string txtFile)
		{
			lock (this.TxtFiles)
			{
				foreach (TxtFile tf in this.TxtFiles)
				{
					if (tf.File.ToLower() == txtFile.ToLower())
					{
						this.TxtFiles.Remove(tf);
						break;
					}
				}
			}
		}
		#endregion

		/// <summary>
		/// Return an identical copy which may be used safely w/o refering this instance
		/// </summary>
		public MapConfig Copy() {
			MapConfig o = new MapConfig();
			lock (this)
			{
				o.Assembler = this.Assembler;
				o.MapDir = this.MapDir;
				o.Name = this.Name;
				o.ShortName = this.ShortName;
				o.OutputDirectory = this.OutputDirectory;
				o.TextureSize = this.TextureSize;
				//Version
				o.Version.Major = this.Version.Major;
				o.Version.Minor = this.Version.Minor;
				o.Version.Build = this.Version.Build;
				
				//Images
				foreach (LoadImage li in this.Images)
				{
					o.Images.Add(new LoadImage(li.name, li.path));
				}
				//Worker tasks
				foreach (WorkTask wt in this.WorkerTasks)
				{
					WorkTask nwt = new WorkTask();
					nwt.maprect = wt.maprect;
					nwt.workname = wt.workname;
					nwt.imageformat = wt.imageformat;
					foreach (string wl in wt.workentries)
					{
						nwt.workentries.Add(wl);
					}
					o.WorkerTasks.Add(nwt);
				}
				//Text files
				foreach (TxtFile txt in this.TxtFiles)
				{
					TxtFile ntxt = new TxtFile();
					ntxt.CoordsFile = txt.CoordsFile;
					ntxt.File = txt.File;
					ntxt.Name = txt.Name;
					ntxt.Type = txt.Type;
					foreach (string s in txt.Layers)
					{
						ntxt.Layers.Add(s);
					}
					o.TxtFiles.Add(ntxt);
				}
			}
			return o;
		}

		#region subclasses
		public class VersionInfo
		{
			public VersionInfo() { }
			public VersionInfo(uint major, uint minor, uint build)
			{
				this.Major = major;
				this.Minor = minor;
				this.Build = build;
			}
			[XmlAttribute("major")]
			public uint Major = 0;
			[XmlAttribute("minor")]
			public uint Minor = 0;
			[XmlAttribute("build")]
			public uint Build = 0;
			public override string ToString()
			{
				return string.Format("{0}.{1}.{2}", this.Major, this.Minor, this.Build);
			}
		}
		#endregion

		#region Enums

		public enum AssemblyMethod
		{
			Single,
			Multi
		}
		#endregion
	}
}
