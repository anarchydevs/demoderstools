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
using System.Text;

namespace Demoder.Patcher.Events
{
	public class DownloadStatusReportEventArgs
	{
		#region members
		private readonly uint _downloadedfiles = 0;
		private readonly uint _remainingfiles = 0;
		private readonly uint _downloadedbytes = 0;
		private readonly uint _remainingbytes = 0;
		private readonly TimeSpan _timespan= new TimeSpan();
		#endregion
		public DownloadStatusReportEventArgs(uint DownloadedFiles, uint RemainingFiles, uint DownloadedBytes, uint RemainingBytes, TimeSpan TimeSpan)
		{
			this._downloadedfiles = DownloadedFiles;
			this._remainingfiles = RemainingFiles;
			this._downloadedbytes = DownloadedBytes;
			this._remainingbytes = RemainingBytes;
			this._timespan = TimeSpan;
		}

		public uint DownloadedFiles { get { return this._downloadedfiles; } }
		public uint RemainingFiles { get { return this._remainingfiles; } }
		public uint TotalFiles { get { return this._downloadedfiles + this._remainingfiles; } }
		
		public uint DownloadedBytes { get { return this._downloadedbytes; } }
		public uint RemainingBytes { get { return this._remainingbytes; } }
		public uint TotalBytes
		{
			get
			{
				if (this.RemainingBytes > 0)
					return this._downloadedbytes + this._remainingbytes;
				else
					return 0;
			}
		}

		public double FilesPerSecond
		{
			get
			{
				return Math.Round((double)this._downloadedfiles / (double)this._timespan.TotalSeconds, 2);
			}
		}
		public double BytesPerSecond
		{
			get
			{
				return Math.Round((double)this._downloadedbytes / (double)this._timespan.TotalSeconds, 2);
			}
		}

		public int PercentDone
		{
			get
			{
				if (this._remainingbytes <= 0 && this._remainingfiles>0)
				{
					double onepercent = ((double)this._remainingfiles + (double)this._downloadedfiles) / (double)100;
					int percent = (int)Math.Round((double)this._downloadedfiles / onepercent, 0);
					return percent;
				}
				else
				{
					double onepercent = ((double)this._remainingbytes + (double)this._downloadedbytes) / (double)100;
					int percent = (int)Math.Round((double)this._downloadedbytes / onepercent, 0);
					return percent;
				}
			}
		}

		public TimeSpan TotalTime { get { return this._timespan; } }
	}
}
