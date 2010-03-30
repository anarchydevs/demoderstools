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
//using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Demoder.MapCompiler
{
	public class Worker
	{
		/// <summary>
		/// Slice positions within streams.
		/// </summary>
		private Dictionary<string, List<long>> _StreamPos;
		/// <summary>
		/// Memory stream containing the 'binfile' for this worker.
		/// </summary>
		private MemoryStream _ms;

		private Dictionary<string, SlicedImage> _slicedImages;
		private WorkTask _worktask;
		private WorkerResult _WorkResult;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="layernames"></param>
		/// <param name="memorystreams"></param>
		public Worker(WorkTask worktask, Dictionary<string,SlicedImage> slicedImages)
		{
			//Store data.
			this._worktask = worktask;
			this._slicedImages = slicedImages;
			this._WorkResult = new WorkerResult();
			this._WorkResult.MapRect = worktask.maprect;
			this._WorkResult.Name = worktask.workname;

			//Initialize memory stream
			this._ms = new MemoryStream();

			//Initialize fileposition list.
			this._StreamPos = new Dictionary<string, List<long>>(worktask.workentries.Count);
			foreach (string wl in worktask.workentries)
			{
				this._StreamPos.Add(wl, new List<long>());
			}
			//Copy slice information.
			foreach (KeyValuePair<string,SlicedImage> kvp in this._slicedImages)
			{
				this._WorkResult.LayerSliceInfo.Add(kvp.Key, new WorkResult_SliceInfo(kvp.Value.Width, kvp.Value.Height));
			}
		}


		public WorkerResult DoWork()
		{
			switch (this._slicedImages.Count)
			{
				case 0: throw new ArgumentNullException("There's nothing to work on!");
				case 1: return this._DoWork_Single();
				default: return this._DoWork_Compare();
			}

		}

		/* Could've made a single method to deal with both types of work, but that'd mean a lot of wasted cycles in the "single" run,
		 * even if excluding the compare code with (bool)DoCompare. */

		#region workers

		/// <summary>
		/// Compile the layer, without comparing. Only done when there's one image.
		/// </summary>
		/// <returns></returns>
		private WorkerResult _DoWork_Single()
		{
			Dictionary<string, long> _md5s = new Dictionary<string, long>(); //Which streampos was that md5 at?
			Dictionary<string, long> _LastPos = new Dictionary<string, long>(); //Last position for a given layer
			string wl = this._worktask.workentries[0];
			SlicedImage simg = this._slicedImages[wl];
			long curPos;
			foreach (MemoryStream slice in simg.Slices)
			{
				curPos = this._ms.Length;
				this._StreamPos[wl].Add(curPos); //To stream positions
				if (this._worktask.imageformat != ImageFormats.Png)
				{
					ImageFormat imf;
					switch (this._worktask.imageformat)
					{
						default:
						case ImageFormats.Jpeg:
							imf = ImageFormat.Jpeg;
							break;
					}
					Image img = Image.FromStream(slice);
					MemoryStream ms = new MemoryStream();
					img.Save(ms, imf);
					ms.WriteTo(this._ms);
				}
				else 
					slice.WriteTo(this._ms);
			}
			return this._DoWork_Finish();
		}

		


		/// <summary>
		/// Perform a compare on the work. This is always done if there's more than one image
		/// </summary>
		/// <returns></returns>
		private WorkerResult _DoWork_Compare()
		{
			Dictionary<string, long> _md5s = new Dictionary<string,long>(); //Which streampos was that md5 at?
			Dictionary<string, long> _LastPos = new Dictionary<string, long>(); //Last position for a given layer
			int index = 0;
			bool DoAgain;
			do {
				DoAgain = false;
				foreach (string wl in this._worktask.workentries) {
					SlicedImage simg = this._slicedImages[wl];
					if (index >= (simg.Slices.Count)) break; //If this entry doesn't have more to process, stop.
					bool treated = false;
					long curPos = this._ms.Length; //Offset is always length of stream, or we'd be overwriting the last byte.
					byte[] simg_slice=simg.Slices[index].ToArray();
					//File.WriteAllBytes(string.Format("e:/tmp/blah2/{0}_{1}.png", wl.layername, index), simg_slice);
					#region Check md5
					string md5 = Demoder.Common.GenerateMD5Hash.md5(simg_slice);
					if (_md5s.ContainsKey(md5))
					{
						if (_LastPos.ContainsKey(wl))
						{
							if (_LastPos[wl] < _md5s[md5])
							{
								this._StreamPos[wl].Add(_md5s[md5]); //We have already added a identical slice.
								_LastPos[wl] = _md5s[md5];
								treated = true;
							}
						}
						else
						{
							this._StreamPos[wl].Add(_md5s[md5]); //We have already added a identical slice.
							_LastPos.Add(wl, _md5s[md5]);
							treated = true;
						}
					}
					#endregion
					if (!treated)
					{ //Add slice.
						#region Register md5
						this._StreamPos[wl].Add(curPos); //To stream positions
						if (_md5s.ContainsKey(md5))
							_md5s[md5] = curPos;
						else
							_md5s.Add(md5, curPos); //To md5<>position lookup
						#endregion
						if (_LastPos.ContainsKey(wl))
							_LastPos[wl] = curPos;
						else
							_LastPos.Add(wl, curPos);
						if (this._worktask.imageformat != ImageFormats.Png) //AFTER md5'ing to compare.. check if we should save in another format.
						{
							Image img = Image.FromStream(simg.Slices[index]);
							MemoryStream ms = new MemoryStream();
							ImageFormat imf;
							switch (this._worktask.imageformat)
							{
								default:
								case ImageFormats.Jpeg:
									imf = ImageFormat.Jpeg;
									break;
							}
							img.Save(ms, imf);
							byte[] msb = ms.ToArray();
							this._ms.Write(msb, 0, msb.Length);
						}
						else 
							this._ms.Write(simg_slice, 0, simg_slice.Length);
						treated = true;
					}
					if (index < (simg.Slices.Count)) DoAgain = true;
				}
				index++; //Bump index by one.
			} while (DoAgain);

			//Finish the work.
			return this._DoWork_Finish();
		}
		#endregion

		/// <summary>
		/// This rounds up the work.
		/// </summary>
		/// <returns></returns>
		private WorkerResult _DoWork_Finish()
		{
			//Move the data from memorystream to WorkResult.
			this._WorkResult.Data = this._ms.ToArray();
			this._ms.Close();
			this._WorkResult.LayerPositionInfo = this._StreamPos; //Layers positions within the stream.
			return this._WorkResult;
		}
	}
}
