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
		/// This handles the Work Task queue.
		/// </summary>
		public void __threaded_Worker()
		{
			//We should produce sections of binfiles.
			this.debug("W", "Started.");
			this.reportWorkerStatus(0, "Started.");
			bool end = false;
			this.workerThreadPoolThreads = new List<bool>();
			this.workerFinishedTasks = new List<bool>();
			this.workerTotalQueue = this._MapConfig.WorkerTasks.Count;
			int MaxThreads = this._CompilerConfig.MaxWorkerThreads;
			while (true)
			{
				WorkTask worktask = null;
				Dictionary<string, SlicedImage> images = new Dictionary<string, SlicedImage>();
				while (worktask == null)
				{
					if (this._Queue_Worker.Count > 0)
					{
						lock (this._Queue_Worker)
							worktask = this._Queue_Worker.Dequeue();
					}
					else
					{ //There's no data in the queue
						end = true;
						break;
					}
					if (worktask == null) Thread.Sleep(250);
				}
				if (end)
				{
					while (this.slicerThreadPoolThreads.Count > 0 || this.workerThreadPoolThreads.Count > 0)
					{
						this._MRE_WorkerDoWork.WaitOne();
					}

					break;
				}
				//Do work. But wait untill we have everything we need!				
				foreach (string wl in worktask.workentries)
				{
					bool WaitForIt = false;
					do
					{
						this._MRE_WorkerThread.Reset();
						lock (this._Data_SlicedImages)
						{
							if (!this._Data_SlicedImages.ContainsKey(wl))
							{
								WaitForIt = true;
								continue;
							}
							else
							{
								images.Add(wl, this._Data_SlicedImages[wl]);
								this._Data_SlicedImages.Remove(wl); //Remove entry, since nothing else will be referencing it ever again.
								WaitForIt = false;
							}
						}
						if (WaitForIt) this._MRE_WorkerThread.WaitOne(); //Sleep here, because we only want to sleep if we actually have to wait.
					} while (WaitForIt);
				}
				//Console.ReadLine();
				//Run worker.
				bool tryagain = true;
				do
				{
					this._MRE_WorkerDoWork.Reset();
					lock (this.workerThreadPoolThreads)
					{
						if (this.workerThreadPoolThreads.Count < MaxThreads)
						{
							this.workerThreadPoolThreads.Add(true);
							tryagain = false;
						}
					}
					if (tryagain) this._MRE_WorkerDoWork.WaitOne();
				} while (tryagain);

				Dictionary<string, object> work = new Dictionary<string, object>(2);
				work.Add("worktask", worktask);
				work.Add("images", images);
				try
				{
					ThreadPool.QueueUserWorkItem(this.__threaded_Worker_DoWork, work);
				}
				catch (Exception ex)
				{
					throw ex;
				}

			}
			this.debug("W", "Stopped");
			this.WorkerEndTime = DateTime.Now;
			this.reportWorkerStatus(100, "Done.");
			this.reportComplete_Worker();
			this._MRE_Assembler_Start.Set();
			System.GC.Collect(10, GCCollectionMode.Optimized); //Request garbage collection
		}

		/// <summary>
		/// This does what the Work Task tells it to do.
		/// </summary>
		/// <param name="obj"></param>
		private void __threaded_Worker_DoWork(object obj)
		{
			Dictionary<string, object> work = (Dictionary<string, object>)obj;
			WorkTask worktask = (WorkTask)work["worktask"];
			Dictionary<string, SlicedImage> images = (Dictionary<string, SlicedImage>)work["images"];
			this.debug("W", string.Format("Started on {0}", worktask.workname));
			Worker worker = new Worker(worktask, images, this._MapConfig.SlicePadding, this._MapConfig.SlicePaddingEnabled);
			WorkerResult wr = worker.DoWork();
			lock (this._Data_WorkerResults) //Add our result
				this._Data_WorkerResults.Add(wr.Name, wr);
			this.debug("W", string.Format("Processed {0} ", wr.Name));

			lock (this.workerFinishedTasks)
			{
				this.workerFinishedTasks.Add(true);
				this.reportWorkerStatus(Demoder.Common.math.Percent(this.workerTotalQueue, this.workerFinishedTasks.Count), string.Format("Finished work task: {0}", worktask.workname));
			}
			lock (this.workerThreadPoolThreads)
				this.workerThreadPoolThreads.RemoveAt(0);
			this._MRE_WorkerDoWork.Set();
		}
	}
}
