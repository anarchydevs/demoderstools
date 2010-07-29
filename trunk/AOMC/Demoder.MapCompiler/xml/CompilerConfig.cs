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
using System.Linq;
using System.Text;
using System.Threading;

namespace Demoder.MapCompiler.xml
{
	public class CompilerConfig
	{
		public CompilerConfig() { }
		/// <summary>
		/// How many threads should the image slicer be allowed to use. 0 for unlimited (threadpool)
		/// </summary>
		public int MaxSlicerThreads = (Environment.ProcessorCount > 1) ? Environment.ProcessorCount - 1 : 1;
		/// <summary>
		/// How many worker threads should we spawn? One is usually enough; shouldn't need more than two, ever.
		/// </summary>
		public int MaxWorkerThreads = 1;
		/// <summary>
		/// Should the compiler try to use only one thread?
		/// </summary>
		public bool singlethreaded = (Environment.ProcessorCount > 1) ? false : true;

		/// <summary>
		/// Should the compiler autooptimize ammount of threads?
		/// </summary>
		public bool AutoOptimizeThreads = true;

	}
}
