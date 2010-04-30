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
	#region might be moved into application
	public class MapDist
	{
		#region members
		[XmlElement("patchserver")]
		/// <summary>
		/// Where should we look for files to download?
		/// </summary>
		public List<string> download_locations;

		[XmlElement("directory")]
		/// <summary>
		/// Distribution content
		/// </summary>
		public DistributionIndex.Dir MapDir;
		#endregion
		#region constructors
		public MapDist() {
			this.MapDir = new DistributionIndex.Dir();
			this.download_locations = new List<string>();
		}

		/// <summary>
		/// Creates an overview of a distribution.
		/// </summary>
		/// <param name="paths">Paths to index</param>
		/// <param name="DistributionType">Distribution type</param>
		/// <param name="DownloadLocations">Location to download updates</param>
		public MapDist(string MapDir, List<string> DownloadLocations)
		{
			this.MapDir = new DistributionIndex.Dir(MapDir);
			this.download_locations = DownloadLocations;
		}
		#endregion

		#region operators
		public static bool operator ==(MapDist di1, MapDist di2) {
			if (di1.download_locations != di2.download_locations) return false;
			//Directories
			if (di1.MapDir != di2.MapDir) return false;
			return true;
		}

		public static bool operator !=(MapDist di1, MapDist di2)
		{
			return di1 == di2 ? false : true;
		}
		#endregion

		#region overrides
		public override bool Equals(object obj)
		{
			try
			{
				MapDist di = (MapDist)obj;
				return this == di;
			}
			catch
			{
				return false;
			}
		}
		#endregion
	
	}
	#endregion
}
