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
using Demoder.Common;
using Demoder.Patcher.DataClasses;

namespace Demoder.Patcher
{
	public class BinAssembler
	{
		#region Members
		private BinFile _binStructure = new BinFile();
		private Dictionary<string, BinFileSlice> _availableSlices = new Dictionary<string, BinFileSlice>();
		private bool _hasAssembled = false;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a BinAssembler instance
		/// </summary>
		/// <param name="BinStructure"></param>
		/// <param name="AvailableSlices"></param>
		public BinAssembler(BinFile BinStructure, Dictionary<string, BinFileSlice> AvailableSlices)
		{
			this._binStructure = BinStructure;
			this._availableSlices = AvailableSlices;
		}
		/// <summary>
		/// Initialize a BinAssembler instance
		/// </summary>
		/// <param name="BinStructure"></param>
		/// <param name="AvailableSlices"></param>
		public BinAssembler(BinFile BinStructure, List<BinFileSlice> AvailableSlices)
		{
			this._binStructure = BinStructure;
			foreach (BinFileSlice bfs in AvailableSlices)
				if (!this._availableSlices.ContainsKey(bfs.MD5)) this._availableSlices.Add(bfs.MD5, bfs);
		}

		/// <summary>
		/// Initialize a BinAssembler instance
		/// </summary>
		/// <param name="BinStructure"></param>
		/// <param name="AvailableSlices"></param>
		public BinAssembler(BinFile BinStructure, DirectoryInfo AvailableSlices)
		{
			this._binStructure = BinStructure;

			FileInfo[] fis = AvailableSlices.GetFiles("*.slice", SearchOption.AllDirectories);
			foreach (FileInfo fi in fis)
			{
				string MD5 = fi.Name.Replace(".slice","");
				if (!this._availableSlices.ContainsKey(MD5)) //Quick-check if we should bother reading this into memory.
				{
					BinFileSlice bfs = new BinFileSlice(File.ReadAllBytes(fi.FullName));
					if (!this._availableSlices.ContainsKey(bfs.MD5)) //Check the real MD5 of the slice to determine if we should add it.
						this._availableSlices.Add(bfs.MD5, bfs);
				}
			}
		}

		/// <summary>
		/// Initialize a BinAssembler instance
		/// </summary>
		/// <param name="BinStructure"></param>
		/// <param name="AvailableSlices">List of BinFiles to use as source for slices</param>
		public BinAssembler(BinFile BinStructure, List<BinFile> AvailableSlices)
		{
			this._binStructure = BinStructure;
			foreach (BinFile bf in AvailableSlices)
			{
				foreach  (BinFileSlice bfs in bf.BinFileSlices)
				{
					if (!this._availableSlices.ContainsKey(bfs.MD5)) //Check the real MD5 of the slice to determine if we should add it.
						this._availableSlices.Add(bfs.MD5, bfs);
				}
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Assemble a binfile
		/// </summary>
		/// <returns>The final file</returns>
		public List<byte> Assemble()
		{
			if (this._hasAssembled) throw new InvalidOperationException(".Assemble() has already been run for this instance.");
			this._hasAssembled = true;
			
			List<byte> binBytes = new List<byte>();
			foreach (BinFileSlice bfs in this._binStructure.BinFileSlices)
			{
				if (!this._availableSlices.ContainsKey(bfs.MD5)) throw new Exception("Missing slice: "+bfs.MD5);
				binBytes.AddRange(this._availableSlices[bfs.MD5].Bytes);
			}
			#region verify integrity
			string MD5 = GenerateHash.md5(binBytes);
			if (MD5 != this._binStructure.MD5) throw new Exception("Assembled binfiles MD5 hash is wrong.", new Exception("MD5 should be: "+this._binStructure.MD5+". MD5 is: "+MD5));
			string SHA1 = GenerateHash.sha1(binBytes);
			if (SHA1 != this._binStructure.SHA1) throw new Exception("Assembled binfiles SHA1 hash is wrong.", new Exception("SHA1 should be: " + this._binStructure.SHA1 + ". SHA1 is: " + SHA1));
			#endregion
			return binBytes;
		}
		#endregion
	}
}
