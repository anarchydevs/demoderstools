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
using System.IO;

using Demoder.MapCompiler;
using Demoder.MapCompiler.xml;

using System.Diagnostics;

namespace DebugConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
			Demoder.MapCompiler.xml.CompilerConfig compconf = new Demoder.MapCompiler.xml.CompilerConfig();
			compconf.MaxSlicerThreads = 2;
			compconf.MaxWorkerThreads = 1;
			
			Demoder.MapCompiler.Compiler comp = new Demoder.MapCompiler.Compiler(compconf);
			//comp.Compile(Demoder.Common.Xml.Deserialize.file<Demoder.MapCompiler.xml.MapConfig>(@"E:\Development\games\AO\maps\AoSL\test.xml"));
			//comp.Compile(Demoder.Common.Xml.Deserialize.file<Demoder.MapCompiler.xml.MapConfig>(@"E:\Development\games\AO\maps\AoRK\test.xml"));
			comp.Compile(Demoder.Common.Xml.Deserialize.file<Demoder.MapCompiler.xml.MapConfig>(@"E:\Development\games\AO\maps\combine.xml"));

			
			//Compiler comp = new Compiler(new MapConfig());
			/*
			Stream stream = new FileStream(@"C:\Users\Demoder\Pictures\Mapcompiler\normal_test2.png", FileMode.Open);
			Demoder.MapCompiler.Worker mc = new Demoder.MapCompiler.Worker(stream);
			FileStream fs = new FileStream("e:/tmp/blah.png", FileMode.Create);
			foreach (MemoryStream ms in mc.Slices)
			{
				ms.WriteTo(fs);
			}
			fs.Close();
			 */

			Console.ReadLine();
		}
	}
}
