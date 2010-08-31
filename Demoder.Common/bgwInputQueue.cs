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
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Demoder.Common
{
	/// <summary>
	/// This wraps a backgroundworker, providing a work input queue.
	/// </summary>
	public abstract class bgwInputQueue
	{
		#region members
		private BackgroundWorker _backgroundWorker = new BackgroundWorker();
		private ManualResetEvent _bgwMRE = new ManualResetEvent(false);
		private Queue<object> _queue = new Queue<object>(16);

		protected object QueuePeek
		{
			get
			{
				lock (this._queue)
					return this._queue.Peek();
			}
		}
		protected int QueueCount { get { return this._queue.Count; } }

		/// <summary>
		/// Has 'cancel' been signaled?
		/// </summary>
		private bool _cancel = false; 
		#endregion
		#region Events
		/// <summary>
		/// Fired when the a work task is done
		/// </summary>
		protected event RunWorkerCompletedEventHandler RunWorkerCompleted;
		/// <summary>
		/// Fired when a work task reports progress change
		/// </summary>
		protected event ProgressChangedEventHandler ProgressChanged;
		/// <summary>
		/// Fired when the queue manager has found some work to do.
		/// </summary>
		protected event DoWorkEventHandler DoWork;
		#endregion

		#region constructors
		public bgwInputQueue(bool ReportProgress, bool SupportsCancellation)
		{
			this._backgroundWorker.WorkerReportsProgress = ReportProgress;
			this._backgroundWorker.WorkerSupportsCancellation = SupportsCancellation;
			this._backgroundWorker.DoWork += new DoWorkEventHandler(this.worker_PullQueue);
			this._backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_WorkCompleted);
			this._backgroundWorker.RunWorkerAsync();
		}
		#endregion

		private void worker_PullQueue(object sender, DoWorkEventArgs e)
		{
			do
			{
				Object worktask = null;
				if (this._queue.Count == 0)
				{
					this._bgwMRE.Reset();
					this._bgwMRE.WaitOne(10000);
					continue;
				}
				else
				{
					lock (this._queue)
						worktask = this._queue.Dequeue();
					//Submit work task to the DoWork event. WORK TASK IS STORED AS SENDER!
#warning Dirty workaround to support cancellation of work tasks: Submit work task as sender, pass on DoWorkEventArgs as-is.
					this.DoWork(worktask, e);
					return;
				}
			} while (!this._cancel);
		}

		/// <summary>
		/// This will start the worker again when it's complete.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void worker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e) 
		{
			//Start the worker again.
			this._backgroundWorker.RunWorkerAsync();
		}

		protected void Enqueue(object obj)
		{
			lock (this._queue)
				this._queue.Enqueue(obj);
			this._bgwMRE.Set();
		}
	}
}