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
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using Demoder.MapCompiler.xml;
using Demoder.Common;

namespace Demoder.MapCompiler
{
	public partial class Compiler
	{
		/// <summary>
		/// The ImageSlicer queue manager. This is run in a separate thread. It will slice up images as they become available.
		/// </summary>
		private void __threaded_ImageSlicer()
		{
			DateTime dt = DateTime.Now;
			this.debug("IS", "Started.");
			this.reportImageSlicerStatus(0, "Started.");
			//Initialize worker threads.
			int MaxThreads;
			lock (this._CompilerConfig)
			{
				MaxThreads = this._CompilerConfig.MaxSlicerThreads;
			}
			this.slicerThreadPoolThreads = new List<bool>();
			this.slicerFinishedTasks = new List<bool>();
			while (true) //Thread true
			{
				ImageData imgdata = null;

				bool end = false;
				while (imgdata == null) //Fetch data
				{
					if (this._Queue_ImageSlicer.Count > 0)
						lock (this._Queue_ImageSlicer)
							imgdata = this._Queue_ImageSlicer.Dequeue();
					else
					{ //There's no data in the queue

						if (this._Thread_imageLoader == null || !this._Thread_imageLoader.IsAlive)
						{
							end = true; //He's dead, Jim.
							break;
						}
					}
					if (imgdata == null) Thread.Sleep(50);
				}
				if (end)
				{ //end this thread when all work is done
					while (this.slicerThreadPoolThreads.Count > 0)
					{
						this._MRE_imageSlicer.WaitOne();
					}
					break;
				}

				bool queued = false;
				do
				{
					this._MRE_imageSlicer.Reset();
					if (this.slicerThreadPoolThreads.Count < MaxThreads)
					{
						lock (this.slicerThreadPoolThreads)
							this.slicerThreadPoolThreads.Add(true);
						ThreadPool.QueueUserWorkItem(this.__threaded_ImageSlicer_DoWork, imgdata);
						int workthreads, asyncthreads;
						ThreadPool.GetAvailableThreads(out workthreads, out asyncthreads);
						this.debug("IS", "Added work to queue.");
						queued = true;
					}
					if (!queued) this._MRE_imageSlicer.WaitOne(1000);
				} while (!queued);
			}
			DateTime dt2 = DateTime.Now;
			this.debug("IS", string.Format("Stopped after {0} seconds.", (dt2 - dt).TotalSeconds));
			this.reportImageSlicerStatus(100, "Done");
			this.reportComplete_ImageSlicer();
			System.GC.Collect(10, GCCollectionMode.Optimized); //Request garbage collection
		}



		/// <summary>
		/// This is the ImageSlicer worker. Called by the ImageSlicer queue handler
		/// </summary>
		/// <param name="obj"></param>
		private void __threaded_ImageSlicer_DoWork(object obj)
		{
			ImageData imgdata = (ImageData)obj;
			ImageSlicer imgslicer = null;
			this.debug("IS", string.Format("Slicing {0}", imgdata.name));
			imgslicer = new ImageSlicer(imgdata, this._MapConfig.TextureSize, this._CompilerConfig.singlethreaded);
			SlicedImage si = new SlicedImage(imgslicer.Width, imgslicer.Height, this._MapConfig.TextureSize, imgslicer.Slices);
			//Add the result to the sliced images store.
			lock (this._Data_SlicedImages)
				this._Data_SlicedImages.Add(imgdata.name, si);
			imgslicer.Dispose();
			this._MRE_WorkerThread.Set(); //Tell the worker thread there's work available.
			this.debug("IS", string.Format("Sliced {0}", imgdata.name));
			lock (this.slicerThreadPoolThreads)
				this.slicerThreadPoolThreads.RemoveAt(0);
			this._MRE_imageSlicer.Set();
			lock (this.slicerFinishedTasks)
			{
				this.slicerFinishedTasks.Add(true);
				this.reportImageSlicerStatus(Demoder.Common.math.Percent(this.imageslicerTotalQueue, this.slicerFinishedTasks.Count), string.Format("Sliced {0}", imgdata.name));
			}
		}
	}
}
