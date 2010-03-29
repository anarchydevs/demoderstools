using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Demoder.MapCompiler.Events;
namespace Demoder.MapCompiler
{
	public delegate void DebugEventHandler(Compiler compiler, DebugEventArgs e);
	public delegate void StatusReportEventHandler(Compiler compiler, StatusReportEventArgs e);

}