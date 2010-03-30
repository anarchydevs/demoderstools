using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demoder.MapCompiler.Events
{
	public class StatusReportEventArgs
	{
		private readonly int _percent = 0;
		private readonly string _message = null;

		public StatusReportEventArgs(int Percent, string Message)
		{
			this._percent = Percent;
			this._message = Message;
		}

		public int Percent { get { return this._percent; } }
		public string Message { get { return this._message; } }
	}
}
