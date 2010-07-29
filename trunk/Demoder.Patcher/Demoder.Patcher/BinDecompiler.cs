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
using Demoder.Patcher.DataClasses;
using System.Threading;

namespace Demoder.Patcher
{
	public class BinDecompiler
	{

		#region Members
		private BinSlicePatterns _slice_patterns = Xml.Deserialize<BinSlicePatterns>(new FileInfo("binfilepatterns.xml"), false);
		private List<byte> _BinFile;
		private List<byte> _BinFile_t2;
		private BinFile _BinStructure = new BinFile();
		/// <summary>
		/// Tells wether or not this class has decompiled already.
		/// </summary>
		private bool _haveDecompiled = false;
		private bool _isMultiThreaded = true;
		public bool IsMultiThreaded
		{
			set 
			{
				if (this._haveDecompiled) throw new InvalidOperationException("This instance has already decompiled.");
				this._isMultiThreaded = value;
			}
			get
			{
				return this._isMultiThreaded;
			}

		}

		//Threading
		private Thread _t_decompiler1 = null;
		private Thread _t_decompiler2 = null;

		private ManualResetEvent _mre_decompiler1;
		private ManualResetEvent _mre_decompiler2;

		private List<BinFileSlice> _slices_t2;
		#endregion

		#region Constructors
		public BinDecompiler() { }
		/// <summary>
		/// Load a binfile from a file path
		/// </summary>
		/// <param name="BinFile">Path to BinFile</param>
		public BinDecompiler(string BinFile)
		{
			this._BinStructure.Name = (new FileInfo(BinFile).Name);
			this._BinFile = new List<byte>(File.ReadAllBytes(BinFile));
		}
		/// <summary>
		/// Use the provided MemoryStream representing a BinFile
		/// </summary>
		/// <param name="BinFile">MemoryStream representing a BinFile</param>
		public BinDecompiler(MemoryStream BinFile)
		{
			this._BinFile = new List<byte>(BinFile.ToArray());
		}
		/// <summary>
		/// Use a byte array representing a BinFile
		/// </summary>
		/// <param name="BinFile">Byte array representing a BinFile</param>
		public BinDecompiler(byte[] BinFile)
		{
			this._BinFile = new List<byte>(BinFile);
		}
		#endregion

		#region Methods
		
		public BinFile Decompile()
		{
			if (this._haveDecompiled) throw new InvalidOperationException("This instance have already decompiled a binfile.");
			this._haveDecompiled = true;

			this._BinStructure.MD5 = GenerateHash.md5(this._BinFile);
			this._BinStructure.SHA1 = GenerateHash.sha1(this._BinFile);
			DateTime starttime = DateTime.Now;

			if (this._isMultiThreaded)
			{
				int middlepos=(int)Math.Round((double)this._BinFile.Count / (double)2, 0);
				FindPatternPositions_ReturnVal fpprv = this.FindPatternPositions(this._BinFile, this._slice_patterns.Copy(), middlepos, middlepos + (1024 * 1024 * 5));
				if (fpprv.LowestPattern == null)
				{
					this._isMultiThreaded = false;
				}
				else
				{
					//Initiate threads
					middlepos = fpprv.LowestPattern.NextSlice_Start;
					byte[] b = new byte[fpprv.LowestPattern.NextSlice_Start];
					this._BinFile.CopyTo(0, b, 0, fpprv.LowestPattern.NextSlice_Start);
					this._BinFile.RemoveRange(0, fpprv.LowestPattern.NextSlice_Start);
					List<byte> FirstBinFile = new List<byte>(b);
					this._BinFile_t2 = this._BinFile;
					this._BinFile = FirstBinFile;

					this._BinStructure.BinFileSlices = new List<BinFileSlice>();
					this._slices_t2 = new List<BinFileSlice>();


					this._t_decompiler1 = new Thread(new ParameterizedThreadStart(this.threaded_DecompileBinfile));
					this._mre_decompiler1 = new ManualResetEvent(false);
					this._t_decompiler1.IsBackground = true;
					this._t_decompiler1.Priority  = ThreadPriority.Lowest;
					this._t_decompiler1.Name = "Demoder.Patcher.BinDecompiler (1)";
					this._t_decompiler1.SetApartmentState(ApartmentState.MTA);
					this._t_decompiler1.Start(1);

					this._t_decompiler2 = new Thread(new ParameterizedThreadStart(this.threaded_DecompileBinfile));
					this._mre_decompiler2 = new ManualResetEvent(false);
					this._t_decompiler2.IsBackground = true;
					this._t_decompiler2.Priority = ThreadPriority.Lowest;
					this._t_decompiler2.Name = "Demoder.Patcher.BinDecompiler (2)";
					this._t_decompiler2.SetApartmentState(ApartmentState.MTA);
					this._t_decompiler2.Start(2);

					this._mre_decompiler1.WaitOne();
					this._mre_decompiler2.WaitOne();
					this._BinStructure.BinFileSlices.AddRange(this._slices_t2);
					this._slices_t2 = null;

				}
			}
			if (!this._isMultiThreaded)
			{
				this._BinStructure.BinFileSlices = new List<BinFileSlice>();
				this.threaded_DecompileBinfile(0);

			}

			Console.WriteLine("Total ammount of time to decompile: "+ (DateTime.Now - starttime).TotalSeconds.ToString()+"s");
			return this._BinStructure;
		}

		private void threaded_DecompileBinfile(object o)
		{
			int threadnum = (int)o;

			bool done = false;
			List<byte> BinFile = null;
			//Get the proper binfile
			if (threadnum == 0 || threadnum == 1)
				BinFile = this._BinFile;
			if (threadnum == 2)
				BinFile = this._BinFile_t2;
			
			List<BinFileSlice> binFileSlices = new List<BinFileSlice>();

			BinSlicePatterns bsps = this._slice_patterns.Copy();
			while (!done)
			{
				FindPatternPositions_ReturnVal fpprv = FindPatternPositions(BinFile, bsps, 0, BinFile.Count);
				//Find which slice is the first.
				if (fpprv.LowestPattern == null)
				{
					//No more matched patterns.Get from lastStopPos to end of file
					if (BinFile.Count >= 1)
						binFileSlices.Add(getSlice(BinFile, 0, BinFile.Count - 1, true));
					done = true;
				}
				else
				{
					//*Move* the slice from the BinFile stream to the slices array. We don't want stuff to hang around twice.
					if (fpprv.LowestPattern.NextSlice_Start != 0)
					{ //If the match isn't at the beginning of the remainder, we have an unidentified slice.
						binFileSlices.Add(getSlice(BinFile, 0, fpprv.LowestPattern.NextSlice_Start - 1, true)); //-1 because we don't want to add the first byte of the next slice
						fpprv.LowestPattern.NextSlice_Stop -= fpprv.LowestPattern.NextSlice_Start;
						fpprv.LowestPattern.NextSlice_Start = 0;
					}
					binFileSlices.Add(getSlice(BinFile, fpprv.LowestPattern.NextSlice_Start, fpprv.LowestPattern.NextSlice_Stop + fpprv.LowestPattern.StopPattern.Count - 1, true));
				}
			}
			if (threadnum == 0 || threadnum == 1)
			{
				lock (this._BinStructure.BinFileSlices)
					this._BinStructure.BinFileSlices = binFileSlices;
				if (threadnum == 1)
					this._mre_decompiler1.Set();
			}
			if (threadnum == 2)
			{
				lock (this._slices_t2)
					this._slices_t2 = binFileSlices;
				this._mre_decompiler2.Set();
			}
		}

		private FindPatternPositions_ReturnVal FindPatternPositions(List<byte> BinFile, BinSlicePatterns BinSlicePatterns, int StartPos, int StopPos)
		{
			FindPatternPositions_ReturnVal ret = new FindPatternPositions_ReturnVal();
			int lowestPos = BinFile.Count;
			if (StopPos >= BinFile.Count) StopPos = BinFile.Count - 1;
			if (lowestPos > StopPos) lowestPos = StopPos;
			List<BinSlicePattern> toRemove = new List<BinSlicePattern>();
			//Find each slices start & stop positions.
			foreach (BinSlicePattern bsp in BinSlicePatterns.Entries)
			{
				try
				{
					bsp.NextSlice_Start = Misc.FindPos(BinFile, bsp.StartPattern, StartPos, lowestPos);
					if (bsp.NextSlice_Start < lowestPos) lowestPos = bsp.NextSlice_Start; //Update the next patterns stop-looking-here position.
					bsp.NextSlice_Stop = Misc.FindPos(BinFile, bsp.StopPattern, bsp.NextSlice_Start);
				}
				catch (Exception ex)
				{
					//Couldn't find either the start or stop pattern.
					ret.BinSlicePatterns_ToRemove.Add(bsp);
				}
			}
			ret.BinSlicePatterns = BinSlicePatterns.Entries;

			foreach (BinSlicePattern bsp in ret.BinSlicePatterns_ToRemove)
				ret.BinSlicePatterns.Remove(bsp);

			BinSlicePattern lowest = null;
			foreach (BinSlicePattern bsp in ret.BinSlicePatterns)
			{ //Find the pattern which has the first start position.

				if (lowest == null)
				{
					lowest = bsp;
					continue;
				}

				if (bsp.NextSlice_Start < lowest.NextSlice_Start)
				{
					lowest = bsp;
					continue;
				}
			}
			ret.LowestPattern = lowest;
			return ret;
		}

		/// <summary>
		/// Retrieves a set of bytes from a list of bytes.
		/// </summary>
		/// <param name="BinFile">Binfile to act on</param>
		/// <param name="Start">Starting byte position</param>
		/// <param name="Stop">Stopping byte position</param>
		/// <param name="Move">true to remove the slice from the binfile, false to copy the slice</param>
		/// <returns></returns>
		private BinFileSlice getSlice(List<byte> BinFile, int Start, int Stop, bool Remove)
		{
			int count = Stop - Start +1; //we're keeping the last byte, too. (+1)
			byte[] b = new byte[count];
			BinFile.CopyTo(Start, b, 0, count);
			if (Remove) BinFile.RemoveRange(Start, count);
			return new BinFileSlice(b);
		}

		#endregion Methods

		#region Classes
		public class FindPatternPositions_ReturnVal
		{
			public List<BinSlicePattern> BinSlicePatterns = new List<BinSlicePattern>();
			public List<BinSlicePattern> BinSlicePatterns_ToRemove = new List<BinSlicePattern>();
			public BinSlicePattern LowestPattern = new BinSlicePattern();
		}
		#endregion
	}
}
