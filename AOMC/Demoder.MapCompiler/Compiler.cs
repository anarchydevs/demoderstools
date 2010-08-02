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
	public partial class Compiler
	{
		#region members
		private MapConfig _MapConfig;
		private CompilerConfig _CompilerConfig;
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

		#region Threading
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
		#endregion

		//Work statistics
		int imageslicerTotalQueue;
		List<bool> slicerFinishedTasks;
		int workerTotalQueue;
		List<bool> workerFinishedTasks;

		//Debug stuff
		DateTime starttime = DateTime.Now;
		DateTime WorkerEndTime;
		/// <summary>
		/// Determines whether or not the debug message antispam is enabled.
		/// Developpers should set this to false when debugging their implementation of this library.
		/// Everyone else should probably leave it as true.
		/// </summary>
		public bool EnableDebugMessageAntiSpam = true;
		#endregion

		#region Auto-set configuration members
		/// <summary>
		/// Determines wether or not we're in multithreaded mode.
		/// </summary>
		private bool _multithreaded = false;
		#endregion

		#region Events
		public event DebugEventHandler eventDebug;
		/// <summary>
		/// Called when the compiler aborts work
		/// </summary>
		public event StatusReportEventHandler eventStatus;
		/// <summary>
		/// ImageLoader status reports
		/// </summary>
		public event StatusReportEventHandler eventImageLoader;
		public event StatusReportEventHandler eventImageSlicer;
		public event StatusReportEventHandler eventWorker;
		public event StatusReportEventHandler eventAssembler;

		/// <summary>
		/// Called when the ImageLoader is done with everything
		/// </summary>
		public event EventHandler eventComplete_ImageLoader;
		/// <summary>
		/// Called when the ImageSlicer is done with everything
		/// </summary>
		public event EventHandler eventComplete_ImageSlicer;
		/// <summary>
		/// Called when the Worker is done with everything
		/// </summary>
		public event EventHandler eventComplete_Worker;
		/// <summary>
		/// Called when the Assembler is done with everything
		/// </summary>
		public event EventHandler eventComplete_Assembler;
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
		#region Debug/Status
		private void reportImageLoaderStatus(int percent, string message)
		{
			if (percent != 0 && percent != 100)
				if (((DateTime.Now - this.antispamEventImageLoader).TotalMilliseconds < this.AntiSpamTimeOut) && this.EnableDebugMessageAntiSpam)
					return;
			if (this.eventImageLoader != null)
			{
				lock (this.eventImageLoader)
				{
					this.antispamEventImageLoader = DateTime.Now;
					this.eventImageLoader(this, new StatusReportEventArgs(percent, message));
				}
			}
		}

		private void reportImageSlicerStatus(int percent, string message)
		{
			if (percent != 0 && percent != 100)
				if (((DateTime.Now - this.antispamEventImageSlicer).TotalMilliseconds < this.AntiSpamTimeOut) && this.EnableDebugMessageAntiSpam)
					return;
			if (this.eventImageSlicer != null)
			{
				lock (this.eventImageSlicer)
				{
					this.antispamEventImageSlicer = DateTime.Now;
					this.eventImageSlicer(this, new StatusReportEventArgs(percent, message));
				}
			}
		}

		private void reportWorkerStatus(int percent, string message)
		{
			if (percent != 0 && percent != 100)
				if (((DateTime.Now - this.antispamEventWorker).TotalMilliseconds < this.AntiSpamTimeOut) && this.EnableDebugMessageAntiSpam)
					return;
			if (this.eventWorker != null)
			{
				lock (this.eventWorker)
				{
					this.antispamEventWorker = DateTime.Now;
					this.eventWorker(this, new StatusReportEventArgs(percent, message));
				}
			}
		}

		private void reportAssemblerStatus(int percent, string message)
		{
			if (percent != 0 && percent != 100)
				if (((DateTime.Now - this.antispamEventAssembler).TotalMilliseconds < this.AntiSpamTimeOut) && this.EnableDebugMessageAntiSpam)
					return;
			if (this.eventAssembler != null)
			{
				lock (this.eventAssembler)
				{
					this.antispamEventAssembler = DateTime.Now;
					this.eventAssembler(this, new StatusReportEventArgs(percent, message));
				}
			}
		}
		#endregion

		#region Complete
		/// <summary>
		/// Report that the ImageLoader is done with all its work.
		/// </summary>
		private void reportComplete_ImageLoader()
		{
			if (this.eventComplete_ImageLoader != null)
				lock (this.eventComplete_ImageLoader)
					this.eventComplete_ImageLoader(this, new EventArgs());
		}

		/// <summary>
		/// Report that ImageSlicer is done with all its work.
		/// </summary>
		private void reportComplete_ImageSlicer()
		{
			if (this.eventComplete_ImageSlicer != null)
				lock (this.eventComplete_ImageSlicer)
					this.eventComplete_ImageSlicer(this, new EventArgs());
		}

		/// <summary>
		/// Report that Worker is done with all its work.
		/// </summary>
		private void reportComplete_Worker()
		{
			if (this.eventComplete_Worker != null)
				lock (this.eventComplete_Worker)
					this.eventComplete_Worker(this, new EventArgs());
		}

		/// <summary>
		/// Report that Assembler is done with all its work.
		/// </summary>
		private void reportComplete_Assembler()
		{
			if (this.eventComplete_Assembler != null)
				lock (this.eventComplete_Assembler)
					this.eventComplete_Assembler(this, new EventArgs());
		}

		#endregion

		#endregion

		#region Constructors
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
		#endregion

		#region Deconstruction
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

		//Clear all event references
		public void ClearEvents()
		{
			this.eventDebug = null;
			this.eventStatus = null;
			this.eventImageLoader = null;
			this.eventAssembler = null;
			this.eventImageSlicer = null;
			this.eventWorker = null;
		}
		#endregion

		public void Compile(MapConfig config)
		{
			#region Set default values for members
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
			#endregion

			if (!this.SanitizeMapConfig()) //If the map config sanitizer detects a non-recoverable error
				return;
			this.SanitizeCompilerConfig();

			//Check if there's anything to do.
			this.reportStatus(0, "Working...");

			
			if (this._multithreaded)
			{	//Multi-Threaded mode.
				
				//Image loader
				this._Thread_imageLoader = new Thread(new ThreadStart(this.__threaded_ImageLoader));
				this._Thread_imageLoader.IsBackground = true;
				this._Thread_imageLoader.Start();
				//Image slicer
				this._Thread_imageSlicer = new Thread(new ThreadStart(this.__threaded_ImageSlicer)); //Create the image slicer thread.
				this._Thread_imageSlicer.IsBackground = true;
				this._Thread_imageSlicer.Start();
				//Worker thread (the one that actually compiles the map)
				this._Thread_Worker = new Thread(new ThreadStart(this.__threaded_Worker));
				this._Thread_Worker.IsBackground = true;
				this._Thread_Worker.Priority = ThreadPriority.BelowNormal;
				this._Thread_Worker.Start();
				//Assembler
				this._Thread_assembler = new Thread(new ThreadStart(this.__threaded_Assembler)); //Create the assembler thread.
				this._Thread_assembler.IsBackground = true;
				this._Thread_assembler.Start();
				
				//Wait for the assembler to finish.
				this._MRE_Assembler_Done.WaitOne();
			}
			else
			{	//"single"-threaded mode

				//Image loader
				this.__threaded_ImageLoader();
				//Image slicer
				this.__threaded_ImageSlicer();
				//Worker
				this.__threaded_Worker();
				//Assembler
				this.__threaded_Assembler();
			}
			this.debug("Comp", String.Format("Total runtime: {0} seconds.", (DateTime.Now - this.starttime).TotalSeconds));
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
			while (true)
			{
				LoadImage ie = null;
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
				catch (Exception ex)
				{
					this.debug("IL", ex.ToString());
				}
			}
			this.debug("IL", "Stopped.");
			this.reportImageLoaderStatus(100, "Done");
			this.reportComplete_ImageLoader();
			System.GC.Collect(10, GCCollectionMode.Optimized); //Request garbage collection
		}

		
		#endregion
	}
}
