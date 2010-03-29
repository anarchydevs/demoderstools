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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Demoder.MapCompiler
{
	public class LoadImage
	{
		public LoadImage() { }
		public LoadImage(string name, string path)
		{
			this.name = name;
			this.path = path;
		}
		[XmlAttribute("name")]
		public string name;
		[XmlAttribute("path")]
		public string path;
	}
	public class ImageData
	{
		public ImageData() { }
		public ImageData(string name, byte[] bytes)
		{
			this.name = name;
			this.bytes = bytes;
		}
		public string name;
		[XmlIgnore]
		public byte[] bytes;
	}
	public class SlicedImage
	{
		public SlicedImage() { }
		/// <summary>
		/// Contains a sliced image
		/// </summary>
		/// <param name="width">Slices</param>
		/// <param name="height">Slices</param>
		/// <param name="slices"></param>
		public SlicedImage(int width, int height, int slicesize, List<MemoryStream> slices)
		{
			this.Width = width;
			this.Height = height;
			this.SliceSize = slicesize;
			this.Slices = slices;
		}
		/// <summary>
		/// Slices width
		/// </summary>
		public int Width;
		/// <summary>
		/// Slices height
		/// </summary>
		public int Height;
		/// <summary>
		/// Size of a slice.
		/// </summary>
		public int SliceSize;
		[XmlIgnore]
		public List<MemoryStream> Slices;
	}


	public class WorkTask //A single task to be performed by a single worker
	{
		public WorkTask()
		{
			this.workentries = new List<string>();
		}
		[XmlAttribute("workname")]
		public string workname;
		[XmlAttribute("maprect")]
		public string maprect;
		[XmlElement("WorkEntry")]
		public List<string> workentries;

	}
		
	public class WorkerResult
	{
		public WorkerResult() {
			this.LayerPositionInfo = new Dictionary<string, List<long>>();
			this.LayerSliceInfo = new Dictionary<string, WorkResult_SliceInfo>();
		}

		public WorkerResult(string Name, string MapRect, byte[] Data)
		{
			this.Name = Name;
			this.MapRect = MapRect;
			this.Data = Data;
			this.LayerPositionInfo = new Dictionary<string, List<long>>();
			this.LayerSliceInfo = new Dictionary<string, WorkResult_SliceInfo>();

		}
		/// <summary>
		/// Name of binfile, if multifile assembly
		/// </summary>
		public string Name;
		/// <summary>
		/// String containing the maprect info.
		/// </summary>
		public string MapRect;
		/// <summary>
		/// Binfile data
		/// </summary>
		public byte[] Data;

		//Need to add: amount of slices x/y for each image.
		/// <summary>
		/// Information about slice dimensions for each layer. Key: Layer name.
		/// </summary>
		public Dictionary<string, WorkResult_SliceInfo> LayerSliceInfo;

		/// <summary>
		/// Where each slice is located in the data stream. key: Layer name. Value: List of filepositions.
		/// </summary>
		public Dictionary<string, List<long>> LayerPositionInfo;
	}
	public class WorkResult_SliceInfo
	{
		public WorkResult_SliceInfo(int Width, int Height)
		{
			this.Width = Width;
			this.Height = Height;
		}
		/// <summary>
		/// Number of slices wide
		/// </summary>
		public int Width;
		/// <summary>
		/// Number of slices tall
		/// </summary>
		public int Height;
	}
	
	public class TxtFiles
	{
		private Queue<TxtFile> _txts;
		public Queue<TxtFile> Txts { get { return this._txts; } }

		public TxtFiles(List<TxtFile> txtFiles)
		{
			this._txts = new Queue<TxtFile>(txtFiles);
		}	
		
		/// <summary>
		/// How many text files are within this 
		/// </summary>
		/// <returns></returns>
		public int Count() {
			lock (this._txts)
				return this._txts.Count;
		}

		public TxtFile Dequeue()
		{
			lock (this._txts)
				return this._txts.Dequeue();
		}
		

		/// <summary>
		/// Removes all references to an layer name.
		/// </summary>
		/// <param name="layername"></param>
		public void RemoveLayerEntry(string layername)
		{
			lock (this._txts) {
				TxtFile[] txts = this._txts.ToArray();
				bool changed=false;
				for (int i=0; i<txts.Length; i++) {
					TxtFile txt = txts[i];
					if (txt.Layers.Contains(layername))
					{
						txt.Layers.Remove(layername);
						txts[i]=txt; //replace the old entry.
						changed=true;
					}
				}

				if (changed) //If there was a change, update.
				{
					this._txts = new Queue<TxtFile>(txts);
				}
			}
		}

		public bool Contains(SearchType s, string needle)
		{
			switch (s)
			{
				default:
					throw new NotImplementedException();
				case SearchType.Layer :
					lock (this._txts) {
						foreach (TxtFile tf in this._txts)
						{
							if (tf.Layers.Contains(needle)) return true;
						}
					}
					break;
			}
			return false;
		}


		#region enums
		public enum SearchType
		{
			Layer
		}
		#endregion
	}
	public class TxtFile
	{
		public TxtFile()
		{
			this.Layers = new List<string>();
		}
		[XmlAttribute("name")]
		public string Name;
		[XmlAttribute("file")]
		public string File;
		[XmlAttribute("type")]
		public MapTypeList Type;

		[XmlAttribute("coordsfile")]
		public string CoordsFile;
		[XmlElement("layer")]
		public List<string> Layers;
	}

	public class LayerInfo
	{
		public LayerInfo(string inBinfile,
			List<long> Filepos,
			WorkResult_SliceInfo Sliceinfo,
			string MapRect)
		{
			this.InBinfile = inBinfile;
			this.FilePos = Filepos;
			this.Sliceinfo = Sliceinfo;
			this.MapRect = MapRect;
		}
		public string InBinfile;
		public WorkResult_SliceInfo Sliceinfo;
		public List<long> FilePos;
		public string MapRect;
	}

	#region enums
	public enum MapTypeList
	{
		Rubika,
		Shadowlands
	}
	#endregion
}
