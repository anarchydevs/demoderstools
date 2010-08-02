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
		/// Check map config syntax, and adjust it to make more sense where apropiate.
		/// </summary>
		private bool SanitizeMapConfig()
		{
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
				return false;
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
			//Check if output directory exists, and if it contains other directories.
			DirectoryInfo di = new DirectoryInfo(this._MapConfig.OutputDirectory);
			if (di.Exists)
				if (di.GetDirectories().Length > 0) //Contains directories.. abort
				{
					this.debug("compiler", "Output directories contain other directories.");
					this.reportStatus(0, "Aborted: Output directories contain other directories.");
				}

			//BinName
			//If .bin is in the shortname, remove it.
			string add_extension = string.Empty;
			if (this._MapConfig.BinName.ToLower().EndsWith(".bin"))
				this._MapConfig.BinName = this._MapConfig.BinName.Substring(0, this._MapConfig.BinName.Length - 4);

			//SlicePadding
			if (this._MapConfig.SlicePadding < 8)
				this._MapConfig.SlicePaddingEnabled = false;
			return true;
		}

		/// <summary>
		/// Check compiler config, and adjust it to make more sense where apropiate.
		/// </summary>
		private void SanitizeCompilerConfig()
		{
			//Don't spawn more slicer threads than there are images to slice. (unnessecary overhead)
			if (this._CompilerConfig.MaxSlicerThreads > this._Queue_ImageLoader.Count)
				this._CompilerConfig.MaxSlicerThreads = this._Queue_ImageLoader.Count;
			//Autooptimize thread settings
			bool threaded;
			if (this._CompilerConfig.AutoOptimizeThreads)
			{
					int MaxThreads = Environment.ProcessorCount;
					if (MaxThreads <=1) //If there's just one CPU, or in case there's some errornous reporting.
					{
						this._CompilerConfig.MaxSlicerThreads = 1;
						this._CompilerConfig.MaxWorkerThreads = 1;
						this._CompilerConfig.singlethreaded = true;
						threaded = false;
					}
					else if (MaxThreads == 2)
					{
						// Overtax the CPU. We just have to, since the worker is dependant on the slicer..
						// ..and not overtaxing would cause a lot of idling.
						this._CompilerConfig.MaxSlicerThreads = 2;
						this._CompilerConfig.MaxWorkerThreads = 1;
						this._CompilerConfig.singlethreaded = false;
						threaded = true;
					}
					else
					{
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
							workerscore += (int)Math.Round((decimal)wt.workentries.Count * bumper, 0);
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
					}
				//Sanitize number of slicers.	
				if (this._CompilerConfig.MaxSlicerThreads > this._MapConfig.Images.Count)
					this._CompilerConfig.MaxSlicerThreads = this._MapConfig.Images.Count;
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
			//report threaded status to the rest of the compiler
			this._multithreaded = threaded;

			if (threaded)
				this.debug("Initializing", String.Format("Starting with {0} slicer and {1} worker threads", this._CompilerConfig.MaxSlicerThreads, this._CompilerConfig.MaxWorkerThreads));
			else
				this.debug("Initializing", "Starting in singlethreaded mode.");
			
		}
	}
}
