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
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Demoder.Common;
using Demoder.MapCompiler.Events;
using Demoder.MapCompiler.xml;

namespace Demoder.MapCompiler
{
	public class Compiler
	{
		MapConfig _MapConfig;
		CompilerConfig _CompilerConfig;
		#region queues
		/// <summary>
		/// Queue that the image loader pulls form
		/// </summary>
		Queue<LoadImage> _Queue_ImageLoader;
		/// <summary>
		/// Queue that the image slicer pulls from
		/// </summary>
		Queue<ImageData> _Queue_ImageSlicer;
		/// <summary>
		/// Queue that the worker pulls from
		/// </summary>
		Queue<WorkTask> _Queue_Worker;
		#endregion

		#region Data storage
		Dictionary<string, SlicedImage> _Data_SlicedImages;
		Dictionary<string, WorkerResult> _Data_WorkerResults;
		TxtFiles _Data_TxtFiles;
		/// <summary>
		/// End-result binfiles. Booya!
		/// </summary>
		Dictionary<string, MemoryStream> _Data_Binfiles;
		#endregion

		//Threads
		private Thread _Thread_imageLoader;
		private Thread _Thread_imageSlicer;
		private Thread _Thread_Worker = null;
		private Thread _Thread_assembler;


		//Manual reset events
		ManualResetEvent _MRE_imageSlicer = null;
		ManualResetEvent _MRE_WorkerThread = null;
		ManualResetEvent _MRE_WorkerDoWork = null;
		ManualResetEvent _MRE_Assembler_Start = null;
		ManualResetEvent _MRE_Assembler_Done = null;
		//Threadpool toggles
		List<bool> slicerThreadPoolThreads;
		List<bool> workerThreadPoolThreads;
		
		//Work statistics
		int imageslicerTotalQueue;
		List<bool> slicerFinishedTasks;
		int workerTotalQueue;
		List<bool> workerFinishedTasks;

		//Debug stuff
		DateTime starttime = DateTime.Now;
		DateTime WorkerEndTime;


		#region Events
		public event DebugEventHandler eventDebug;
		/// <summary>
		/// Called when the compiler aborts work
		/// </summary>
		public event StatusReportEventHandler eventStatus;
		public event StatusReportEventHandler eventImageLoader;
		public event StatusReportEventHandler eventImageSlicer;
		public event StatusReportEventHandler eventWorker;
		public event StatusReportEventHandler eventAssembler;
		#endregion

		#region Event antispam timers
		DateTime antispamEventImageLoader = DateTime.Now;
		DateTime antispamEventImageSlicer = DateTime.Now;
		DateTime antispamEventWorker = DateTime.Now;
		DateTime antispamEventAssembler = DateTime.Now;
		int AntiSpamTimeOut = 500;
		#endregion

		#region Event methods
		private void debug(string source, string text)
		{
			if (this.eventDebug != null)
			{
				lock (this.eventDebug)
					this.eventDebug(this, new DebugEventArgs(source, text));
			}
		}
		/// <summary>
		/// Used to report overall status.
		/// </summary>
		/// <param name="message"></param>
		private void reportStatus(int percent, string message)
		{
			if (this.eventStatus != null)
				lock (this.eventStatus)
					this.eventStatus(this, new StatusReportEventArgs(percent, message));
		}

			

		private void reportImageLoaderStatus(int percent, string message)
		{
			if (percent!=0 && percent!=100)
				if ((DateTime.Now - this.antispamEventImageLoader).TotalMilliseconds < this.AntiSpamTimeOut)
					return;
			if (this.eventImageLoader != null)
				lock (this.eventImageLoader)
					this.eventImageLoader(this, new StatusReportEventArgs(percent, message));
		}

		private void reportImageSlicerStatus(int percent, string message)
		{
			if (percent != 0 && percent != 100)
				if ((DateTime.Now - this.antispamEventImageSlicer).TotalMilliseconds < this.AntiSpamTimeOut)
					return;
			if (this.eventImageSlicer != null)
				lock (this.eventImageSlicer)
					this.eventImageSlicer(this, new StatusReportEventArgs(percent, message));
		}

		private void reportWorkerStatus(int percent, string message)
		{
			if (percent != 0 && percent != 100)
				if ((DateTime.Now - this.antispamEventWorker).TotalMilliseconds < this.AntiSpamTimeOut)
					return;
			if (this.eventWorker != null)
				lock (this.eventWorker)
					this.eventWorker(this, new StatusReportEventArgs(percent, message));
		}

		private void reportAssemblerStatus(int percent, string message)
		{
			if (percent != 0 && percent != 100)
				if ((DateTime.Now - this.antispamEventAssembler).TotalMilliseconds < this.AntiSpamTimeOut)
					return;
			if (this.eventAssembler != null)
				lock (this.eventAssembler)
					this.eventAssembler(this, new StatusReportEventArgs(percent, message));
		}

		#endregion

		//Clear all event references
		public void ClearEvents() {
			this.eventDebug = null;
			this.eventStatus = null;
			this.eventImageLoader = null;
			this.eventAssembler = null;
			this.eventImageSlicer = null;
			this.eventWorker = null;
		}

		/// <summary>
		/// Clean out all references.
		/// </summary>
		public void Dispose()
		{
			this.ClearEvents(); //clear out events
			this._Data_Binfiles = null;
			this._CompilerConfig = null;
			if (this._Data_SlicedImages != null)
				foreach (KeyValuePair<string, SlicedImage> kvp in this._Data_SlicedImages)
					foreach (MemoryStream ms in kvp.Value.Slices)
						ms.Dispose();
			this._Data_SlicedImages = null;
			this._Data_TxtFiles = null;
			if (this._Data_WorkerResults != null)
				foreach (KeyValuePair<string, WorkerResult> kvp in this._Data_WorkerResults)
				{
					kvp.Value.Data = null;
				}
			this._Data_WorkerResults = null;
			this._MapConfig = null;
			this._MRE_imageSlicer = null;
			this._MRE_WorkerDoWork = null;
			this._MRE_WorkerThread = null;
			this._MRE_Assembler_Done = null;
			this._Queue_ImageLoader = null;
			this._Queue_ImageSlicer = null;
			this._Queue_Worker = null;
			this._Thread_assembler = null;
			this._Thread_imageLoader = null;
			this._Thread_imageSlicer = null;
			this._Thread_Worker = null;
			this.slicerFinishedTasks = null;
			this.slicerThreadPoolThreads = null;
			this.workerFinishedTasks = null;
			this.workerThreadPoolThreads = null;
			System.GC.Collect(15, GCCollectionMode.Forced); //Request garbage collection
		}

		public Compiler(xml.CompilerConfig cfg)
		{
			this._CompilerConfig = cfg;
			#region sanitize compiler configuration
			if (this._CompilerConfig.MaxWorkerThreads < 1) //Ensure we have at least one worker thread.
				this._CompilerConfig.MaxWorkerThreads = 1;
			if (this._CompilerConfig.MaxSlicerThreads < 1)
				this._CompilerConfig.MaxSlicerThreads = 1;
			#endregion
		}

		public void Compile(MapConfig config)
		{
			this._MapConfig = config; //Set config.
			//Initialize queues.
			this._Queue_ImageLoader = new Queue<LoadImage>();
			this._Queue_ImageSlicer = new Queue<ImageData>();
			this._Queue_Worker = new Queue<WorkTask>();

			//Initialize data storage
			this._Data_SlicedImages = new Dictionary<string, SlicedImage>();
			this._Data_WorkerResults = new Dictionary<string, WorkerResult>();
			if (this._MapConfig.TxtFiles.Count == 0) return; //there's nothing to output to - don't waste cpu cycles!
			this._Data_TxtFiles = new TxtFiles(this._MapConfig.TxtFiles);
			this._Data_Binfiles = new Dictionary<string, MemoryStream>();

			//Manual reset events
			this._MRE_WorkerThread = new ManualResetEvent(false);
			this._MRE_imageSlicer = new ManualResetEvent(false);
			this._MRE_WorkerDoWork = new ManualResetEvent(false);
			this._MRE_Assembler_Start = new ManualResetEvent(false);
			this._MRE_Assembler_Done = new ManualResetEvent(false);

			#region Sanitize map configuration
			//Add work. Don't add nonexisting images to queue. Don't add worktasks using nonexisting images.
			bool changed;
			int totalnumworks = 0;
			do
			{
				changed = false;
				foreach (WorkTask wt in this._MapConfig.WorkerTasks)
				{
					int numworks = 0; //Number of works this worker will actually peform.
					foreach (string wl in wt.workentries) //For each work layer
					{
						bool exists = false; //Use this to determine if the image exists.
						if (this._Data_TxtFiles.Contains(TxtFiles.SearchType.Layer, wl)) //If we're going to use this layer
						{
							foreach (LoadImage le in this._MapConfig.Images)
							{ //Check the image list if the image name is the same one
								if (le.name == wl)
								{
									if (File.Exists(le.path))
									{ //If this file exists, add it.
										if (!this._Queue_ImageLoader.Contains(le))
											this._Queue_ImageLoader.Enqueue(le); //Add it to load task
										numworks++;
										exists = true;
										break;
									}
									else
									{ //File doesn't exist. Make the TxtFiles class remove any reference to this layer.
										this._Data_TxtFiles.RemoveLayerEntry(wl);
										break;
									}
								}
							}
						}
						if (!exists)
						{
							wt.workentries.Remove(wl);
							changed = true;
							break;
						}
					}
					if (numworks > 0)
					{ //If there's work to do
						if (!this._Queue_Worker.Contains(wt)) //Prevent adding the same task twice.
							this._Queue_Worker.Enqueue(wt);
						totalnumworks++;
					}
				}
			} while (changed == true);

			//Check config syntax.
			if (totalnumworks == 0 || this._Queue_ImageLoader.Count == 0)
			{
				this.debug("compiler", "There's nothing to do.");
				this.reportStatus(0, "There's nothing to do");
				this.Dispose();
				return;
			}
			//MapDir
			if (true)
			{
				switch (this._MapConfig.MapDir.Substring((this._MapConfig.MapDir.Length - 2), 1))
				{
					case "/":
					case "\\":
						break;
					default:
						this._MapConfig.MapDir += "/";
						break;
				}
			}

			#endregion

			#region Optimize compiler configuration based on map configuration
			//Don't spawn more slicer threads than there are images to slice. (unnessecary overhead)
			if (this._CompilerConfig.MaxSlicerThreads > this._Queue_ImageLoader.Count)
				this._CompilerConfig.MaxSlicerThreads = this._Queue_ImageLoader.Count;
			//Autooptimize thread settings
			bool threaded;
			if (this._CompilerConfig.AutoOptimizeThreads)
			{
				do
				{ //So we can break.
					int MaxThreads = Environment.ProcessorCount;
					if (MaxThreads == 1)
					{
						this._CompilerConfig.MaxSlicerThreads = 1;
						this._CompilerConfig.MaxWorkerThreads = 1;
						this._CompilerConfig.singlethreaded = true;
						threaded = false;
						break;
					}
					if (MaxThreads == 2)
					{
						// Overtax the CPU. We just have to, since the worker is dependant on the slicer..
						// ..and not overtaxing would cause a lot of idling.
						this._CompilerConfig.MaxSlicerThreads = 2;
						this._CompilerConfig.MaxWorkerThreads = 1;
						this._CompilerConfig.singlethreaded = false;
						threaded = true;

						break;
					}
					int workerscore = 0;
					int imagescore = (int)System.Math.Ceiling((double)(this._MapConfig.Images.Count * 8));
					int overshoot = 0;
					foreach (WorkTask wt in this._MapConfig.WorkerTasks)
					{
						int bumper;
						if (wt.imageformat == ImageFormats.Png)
						{
							bumper = 1;
							overshoot++;
						}
						else
						{
							bumper = 3;
							overshoot--;
						}
						workerscore += (int)Math.Round((decimal)wt.workentries.Count * bumper,0);
					}
					//Now compare the two.

					int total = workerscore + imagescore;
					int workerPercent = math.Percent(total, workerscore);
					int slicerPercent = math.Percent(total, imagescore);
					int slicethreads = math.dePercent(MaxThreads, slicerPercent);
					int workthreads = math.dePercent(MaxThreads, workerPercent);
					int overshootthreads = math.dePercent(MaxThreads, overshoot);
					if (overshootthreads > 0) workthreads++;
					this._CompilerConfig.MaxSlicerThreads = (slicethreads < 1) ? 1 : slicethreads;
					this._CompilerConfig.MaxWorkerThreads = (workthreads < 1) ? 1 : (workthreads);
					this._CompilerConfig.singlethreaded = false;
					threaded = true;
				} while (false);
			}
			else
			{
				threaded = this._CompilerConfig.singlethreaded == true ? false : true; //Should we run singlethreaded mode?
				switch (this._CompilerConfig.singlethreaded)
				{
					case true: threaded = false; break;
					default:
					case false: threaded = true; break;
				}
				if (!threaded)
				{
					this._CompilerConfig.MaxSlicerThreads = 1;
				}
			}
			if (threaded)
				this.debug("Initializing", String.Format("Starting with {0} slicer and {1} worker threads", this._CompilerConfig.MaxSlicerThreads, this._CompilerConfig.MaxWorkerThreads));
			else
				this.debug("Initializing", "Starting in singlethreaded mode.");
			#endregion

			//Check if there's anything to do.


			this.reportStatus(0, "Working...");
			//Start threads.
			//Image loader
			this._Thread_imageLoader = new Thread(new ThreadStart(this.__threaded_ImageLoader));
			this._Thread_imageLoader.IsBackground = true;
			if (threaded) this._Thread_imageLoader.Start();
			else this.__threaded_ImageLoader();

			//Image slicer
			this._Thread_imageSlicer = new Thread(new ThreadStart(this.__threaded_ImageSlicer)); //Create the image slicer thread.
			this._Thread_imageSlicer.IsBackground = true;
			if (threaded) this._Thread_imageSlicer.Start();
			else this.__threaded_ImageSlicer();

			//Worker thread (the one that actually compiles the map)
			this._Thread_Worker = new Thread(new ThreadStart(this.__threaded_Worker));
			this._Thread_Worker.IsBackground = true;
			this._Thread_Worker.Priority = ThreadPriority.BelowNormal;
			if (threaded) this._Thread_Worker.Start();
			else this.__threaded_Worker();

			this._Thread_assembler = new Thread(new ThreadStart(this.__threaded_Assembler)); //Create the assembler thread.
			this._Thread_assembler.IsBackground = true;

			if (threaded) this._Thread_assembler.Start();
			else this.__threaded_Assembler();

			
			//Wait for the assembler thread to finish.
			this._MRE_Assembler_Done.WaitOne();
			this.reportStatus(100, "Done.");
		}


		#region Threaded methods.

		/// <summary>
		/// This method is run in a separate thread. It will load images from disk.
		/// </summary>
		private void __threaded_ImageLoader()
		{
			this.debug("IL", "Started.");
			this.reportImageLoaderStatus(0, "started");
			//this._RawImages = new Dictionary<string, MemoryStream>(this._Config.Images.Count);
			int startqueue = this._Queue_ImageLoader.Count;
			this.imageslicerTotalQueue = startqueue; //the image slicer will be slicing all the images we load.
			while (true) {
				LoadImage ie=null;
				if (this._Queue_ImageLoader.Count > 0)
					lock (this._Queue_ImageLoader)
						ie = this._Queue_ImageLoader.Dequeue();
				else break;
				try
				{
					byte[] bytes = File.ReadAllBytes(ie.path);
					lock (this._Queue_ImageSlicer)
						this._Queue_ImageSlicer.Enqueue(new ImageData(ie.name, bytes));
					this.debug("IL", string.Format("Loaded {0}", ie.name));
					this.reportImageLoaderStatus(Demoder.Common.math.Percent(startqueue, (startqueue - this._Queue_ImageLoader.Count)), string.Format("Loaded {0}", ie.name));
				}
				catch (Exception ex) {
					this.debug("IL", ex.ToString());
				}
			}
			this.debug("IL", "Stopped.");
			this.reportImageLoaderStatus(100, "Done");
			System.GC.Collect(10, GCCollectionMode.Optimized); //Request garbage collection
		}

		/// <summary>
		/// This thread is run in a separate thread. It will slice up images as they become available.
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
						if (!this._Thread_imageLoader.IsAlive)
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
			System.GC.Collect(10, GCCollectionMode.Optimized); //Request garbage collection
		}

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
			lock (this.slicerFinishedTasks) {
				this.slicerFinishedTasks.Add(true);
				this.reportImageSlicerStatus(Demoder.Common.math.Percent(this.imageslicerTotalQueue, this.slicerFinishedTasks.Count), string.Format("Sliced {0}", imgdata.name));
			}
		}

		/// <summary>
		/// This thread processes work tasks to produce layers.
		/// </summary>
		public void __threaded_Worker()
		{
			//We should produce sections of binfiles.
			this.debug("W","Started.");
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

				Dictionary<string,object> work = new Dictionary<string,object>(2);
				work.Add("worktask", worktask);
				work.Add("images", images);
				ThreadPool.QueueUserWorkItem(this.__threaded_Worker_DoWork, work);

			}
			this.debug("W", "Stopped");
			this.WorkerEndTime = DateTime.Now;
			this.reportWorkerStatus(100, "Done.");
			this._MRE_Assembler_Start.Set();
			System.GC.Collect(10, GCCollectionMode.Optimized); //Request garbage collection
		}

		private void __threaded_Worker_DoWork(object obj)
		{
			Dictionary<string, object> work = (Dictionary<string, object>)obj;
			WorkTask worktask = (WorkTask)work["worktask"];
			Dictionary<string, SlicedImage> images = (Dictionary<string, SlicedImage>)work["images"];
			this.debug("W", string.Format("Started on {0}", worktask.workname));
			Worker worker = new Worker(worktask, images);
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

		private void __threaded_Assembler()
		{
			this.debug("A", "Started");

			/*
			 * To assemble a map you need to know the following:
			 * - Which file is layer X located in
			 * - What is layer X's offset in that file
			 * - What relative file positions does each layer contain?
			 */
			byte[] headerimg_bytes;
			if (true)
			{
				System.Reflection.AssemblyName callerassembly = System.Reflection.Assembly.GetEntryAssembly().GetName();
				System.Reflection.AssemblyName myassembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();

				string[] headerstring = new string[] {
					string.Format("Compiled with {0} v{1}",
						callerassembly.Name,
						callerassembly.Version),
					string.Format("    using {0} v{1}",
						myassembly.Name,
						myassembly.Version),
					string.Format("Map name: {0}", this._MapConfig.Name),
					string.Format("Compilation time: {0}-{1}-{2} {3}:{4}:{5}", 
						DateTime.UtcNow.Year, 
						DateTime.UtcNow.Month, 
						DateTime.UtcNow.Day, 
						DateTime.UtcNow.Hour, 
						DateTime.UtcNow.Minute, 
						DateTime.UtcNow.Second) 
			};
				Bitmap headerimg = new Bitmap(640, (int)System.Math.Ceiling((double)(10 * headerstring.Length)), PixelFormat.Format24bppRgb); //Image to add to the beginning of every binfile
				Graphics g = Graphics.FromImage(headerimg);
				for (int i = 0; i < headerstring.Length; i++)
					g.DrawString(headerstring[i], new Font(FontFamily.GenericMonospace, 8, FontStyle.Regular), Brushes.White, 2, (int)System.Math.Ceiling((double)(9 * i)));
				g.Dispose();
				MemoryStream ms_headerimg = new MemoryStream(1024);
				headerimg.Save(ms_headerimg, ImageFormat.Png);
				headerimg_bytes = ms_headerimg.ToArray();
			}

#warning fixme: This is NOT optimized for a multi-binfile enviorenment.
			//multibinfile: Grab what it has, spew out the binfiles as they become available.
			//Single binfile: Process like now.
			this.reportAssemblerStatus(0, "Started.");
			lock (this._Data_TxtFiles)
			{
				foreach (TxtFile task in this._Data_TxtFiles.Txts)
				{
					//Check if we have everything we need to do this work.
					foreach (string layer in task.Layers)
					{
						bool WaitForIt;
						do
						{
							WaitForIt = true; //Default to wait
							lock (this._Data_WorkerResults)
							{
								foreach (KeyValuePair<string, WorkerResult> kvp in this._Data_WorkerResults)
								{
									if (kvp.Value.LayerSliceInfo.ContainsKey(layer))
									{ //We have this layer. Don't wait for it.
										WaitForIt = false;
										continue;
									}
								}
							}
							if (WaitForIt) Thread.Sleep(100); //Sleep 100ms before we try again.
						} while (WaitForIt);
					}
				}
			}
			this.reportAssemblerStatus(10, "Haver everything we need.");
			this.debug("A", "Have everything we need.");
			//We have everything we need now.
			//Assemble based on work list.


			/*
			 * Move binfile into list of binfiles. (append to whatever is already there)
			 * If appending, bump FilePos values.
			 * 
			 */

			// Contains all the info you need about each layer.
			// It's to be used by the textfile generator.
			Dictionary<string, LayerInfo> layerInfo = new Dictionary<string, LayerInfo>();

			this.debug("A", "Preparing binfiles.");
			//Prepare the binfiles and layers.

			#region combined
			for (int i = 0; i < this._MapConfig.WorkerTasks.Count; i++)
			{
				WorkTask wtask = this._MapConfig.WorkerTasks[i];
				string binname=string.Empty;
				switch (this._MapConfig.Assembler)
				{
					case MapConfig.AssemblyMethod.Single:
						binname = this._MapConfig.ShortName;
						break;
					case MapConfig.AssemblyMethod.Multi:
						binname = wtask.workname;
						break;
				}
				lock (this._Data_Binfiles)
					if (!this._Data_Binfiles.ContainsKey(binname))
					{ //If this binfile doesn't exist in the list, add it.
						this._Data_Binfiles.Add(binname,new MemoryStream());
						this._Data_Binfiles[binname].Write(headerimg_bytes, 0, headerimg_bytes.Length);
					}

				MemoryStream binfile = this._Data_Binfiles[binname]; //Temporary memorystream for binfile.
				long BinOffset = binfile.Length;

				lock (this._Data_WorkerResults)
				{
					WorkerResult wresult = this._Data_WorkerResults[wtask.workname]; //Worker result
					lock (this._Data_Binfiles)
						binfile.Write(wresult.Data, 0, wresult.Data.Length);
					//Data written to binfile stream... Now process file positions.
					foreach (KeyValuePair<string, List<long>> kvp in wresult.LayerPositionInfo)
					{
						List<long> filepos = new List<long>();
						foreach (long fpos in kvp.Value)
						{
							filepos.Add(fpos + BinOffset);
						}
						LayerInfo li = new LayerInfo(
							binname,
							filepos,
							wresult.LayerSliceInfo[kvp.Key],
							wresult.MapRect);
						layerInfo.Add(kvp.Key, li);
					}
				}
			}
			#endregion

			this.reportAssemblerStatus(75, "Binfiles prepared.");
					
			this.debug("A", string.Format("Writing {0} binfiles.", this._Data_Binfiles.Count));
			if (!Directory.Exists(this._MapConfig.OutputDirectory)) Directory.CreateDirectory(this._MapConfig.OutputDirectory);
			//Write the bin files.
			foreach (KeyValuePair<string, MemoryStream> kvp in this._Data_Binfiles)
			{
				try
				{
					FileStream fs = File.Open(this._MapConfig.OutputDirectory + Path.DirectorySeparatorChar + kvp.Key + ".bin", FileMode.Append);
					byte[] b = kvp.Value.ToArray();
					kvp.Value.Close();
					fs.Write(b, 0, b.Length);
					fs.Close();
					b = null;
					this._Data_Binfiles[kvp.Key].Dispose();
				}
				catch { }
			}
			this.reportAssemblerStatus(90, "Binfiles written to disk.");
			this.debug("A", string.Format("Writing {0} text files.", this._Data_TxtFiles.Count()));
			//Write the text files.
			TxtFile tf = null;
			FormatString sf;
			if (true)
			{
				Dictionary<string, object> dict = new Dictionary<string, object>();
				dict.Add("name", this._MapConfig.Name);
				dict.Add("shortname", this._MapConfig.ShortName);
				dict.Add("version", this._MapConfig.Version.ToString());
				dict.Add("subfolder", this._MapConfig.MapDir);
				sf = new FormatString(dict);
			}
			do
			{
				if (this._Data_TxtFiles.Count() > 0)
					tf = this._Data_TxtFiles.Dequeue();
				else tf = null;
				if (tf != null)
				{
					string txtfile = String.Format("Name \"{0}\"\r\nType {1}\r\nCoordsFile {2}\r\n",
						sf.Format(tf.Name),
						tf.Type,
						sf.Format(tf.CoordsFile));
					foreach (string layer in tf.Layers)
					{
						LayerInfo li = layerInfo[layer];
						txtfile += string.Format("\r\nFile {0}{1}.bin", this._MapConfig.MapDir, li.InBinfile);
						txtfile += string.Format("\r\nTextureSize {0}\r\n", this._MapConfig.TextureSize);
						txtfile += string.Format("\r\nSize {0} {1}", (li.Sliceinfo.Width * this._MapConfig.TextureSize), (li.Sliceinfo.Height * this._MapConfig.TextureSize));
						txtfile += string.Format("\r\nTiles {0} {1}", li.Sliceinfo.Width, li.Sliceinfo.Height);
						txtfile += string.Format("\r\nMapRect {0}\r\n", li.MapRect);
						foreach (long filepos in li.FilePos)
						{
							txtfile += string.Format("FilePos {0}\r\n", filepos);
						}
					}
					File.WriteAllText(this._MapConfig.OutputDirectory + Path.DirectorySeparatorChar + tf.File, txtfile);
				}
			} while (tf != null);
			DateTime dt2 = DateTime.Now;
			this.reportAssemblerStatus(100, "Done.");
			this.debug("A", String.Format("Stopped after {0} seconds.", (dt2 - this.WorkerEndTime).TotalSeconds));
			this.debug("App", String.Format("Total runtime: {0} seconds.", (dt2 - this.starttime).TotalSeconds));
			this._MRE_Assembler_Done.Set();
			System.GC.Collect(10, GCCollectionMode.Optimized); //Request garbage collection
		}
		#endregion
	}
}
