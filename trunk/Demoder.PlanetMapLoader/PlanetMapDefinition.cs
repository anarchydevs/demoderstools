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
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Demoder.PlanetMapLoader
{
	public class PlanetMapDefinition
	{
		#region members
		public string Name = "NoName";
		public string Type = "Rubika";
		public string CoordsFile = "MapCoords.xml";
		[XmlElement("layer")]
		public List<PlanetMapLayer> Layers = new List<PlanetMapLayer>();
		#endregion
		#region constructors
		public PlanetMapDefinition(string MapDefinitionFile)
		{
			if (!File.Exists(MapDefinitionFile)) throw new FileNotFoundException(MapDefinitionFile);
			string[] filelines = File.ReadAllLines(MapDefinitionFile);
			bool isinlayer = false; //Are we dealing with layers yet?
			PlanetMapLayer pml = null;
			foreach (string s in filelines)
			{
				string[] w = s.Split(" ".ToCharArray());
				if (w.Length < 2) continue; //Invalid entry
				w[0] = w[0].ToLower();

				if (!isinlayer)
				{
					if (w[0] == "name")
					{
						List<string> l = new List<string>(w);
						l.RemoveAt(0);
						this.Name = string.Join(" ", l.ToArray()).Replace("\"", "");
						continue;
					}
					else if (w[0] == "type") { this.Type = w[1]; continue; }
					else if (w[0] == "coordsfile")
					{
						List<string> l = new List<string>(w);
						l.RemoveAt(0);
						this.CoordsFile = string.Join(" ", l.ToArray());
						continue;
					}
				}
				if (w[0] == "file")
				{
					isinlayer = true;
					if (pml != null) this.Layers.Add(pml);
					pml = new PlanetMapLayer();
					List<string> l = new List<string>(w);
					l.RemoveAt(0);
					pml.File = string.Join(" ", l.ToArray());
					continue;
				}
				else if (w[0] == "texturesize")
				{
					int ts;
					if (!int.TryParse(w[1], out ts)) ts = 128; //If we can't parse the string into an int, default to 128.
					pml.TextureSize = ts;
					continue;
				}
				else if (w[0] == "size")
				{
					int x, z;
					int.TryParse(w[1], out x);
					int.TryParse(w[2], out z);
					pml.Size.Width = x;
					pml.Size.Height = z;
					continue;
				}
				else if (w[0] == "tiles")
				{
					int x, z;
					int.TryParse(w[1], out x);
					int.TryParse(w[2], out z);
					pml.Tiles.Width = x;
					pml.Tiles.Height = z;
					continue;
				}
				else if (w[0] == "maprect")
				{
					int x, z, xs, zs;
					int.TryParse(w[1], out x);
					int.TryParse(w[2], out z);
					int.TryParse(w[3], out xs);
					int.TryParse(w[4], out zs);
					pml.MapRect.Start_X = x;
					pml.MapRect.Start_Z = z;
					pml.MapRect.Stop_X = xs;
					pml.MapRect.Stop_Z = zs;
					continue;
				}
				else if (w[0] == "filepos")
				{
					long fp;
					if (long.TryParse(w[1], out fp))
						pml.FilePos.Add(fp);
					continue;
				}
			}
		}
		#endregion
		#region overrides
		public override string ToString()
		{
			return this.Name;
		}
		#endregion
	}
		

	public class PlanetMapLayer
	{
		[XmlAttribute("file")]
		public string File;
		[XmlAttribute("texturesize")]
		public int TextureSize;
		public LayerSize Size = new LayerSize();
		public LayerTiles Tiles = new LayerTiles();
		public LayerMapRect MapRect = new LayerMapRect();
		public List<long> FilePos = new List<long>();

		/// <summary>
		/// Retrieves the filepos of a given tile
		/// </summary>
		/// <param name="collumn">0-based collumn #</param>
		/// <param name="row">0-based row #</param>
		/// <returns></returns>
		public long GetFilePos(int collumn, int row) {
			int col_add = collumn * this.Tiles.Height;
			int index = (col_add + row);
			if (index >= this.FilePos.Count) return 0;
			else return this.FilePos[index];
		}


		#region Data classes
		public class LayerSize
		{
			[XmlAttribute("width")]
			public int Width;
			[XmlAttribute("height")]
			public int Height;
		}
		public class LayerTiles
		{
			[XmlAttribute("width")]
			public int Width;
			[XmlAttribute("height")]
			public int Height;
		}
		public class LayerMapRect
		{
			[XmlAttribute("start_x")]
			public int Start_X;
			[XmlAttribute("start_z")]
			public int Start_Z;
			[XmlAttribute("stop_x")]
			public int Stop_X;
			[XmlAttribute("stop_z")]
			public int Stop_Z;
		}
		#endregion
	}
}