using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demoder.MapCompiler.Events
{
	public class DebugEventArgs
	{
		private readonly string _source = null;
		private readonly string _message=null;

        public DebugEventArgs(string Source, string Message)
        {
			this._source = Source;
			this._message = Message;
        }

		public string Source { get { return this._source; } }
		public string Message { get { return this._message; } }
	}
}
