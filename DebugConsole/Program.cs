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

using System.Diagnostics;
using Demoder.Common;
using Demoder.Patcher;
using Demoder.Patcher.xml;
namespace DebugConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
			distinfo DistInfo = new distinfo();
			DistInfo.DistributionType = "map";
			DistInfo.download_locations = new List<string>();
			DistInfo.download_locations.Add("http://aork.flw.nu");
			DistInfo.download_locations.Add("http://aosl.flw.nu");
			DistInfo.content = new List<distinfo_directory>();
			distinfo_directory dir = new distinfo_directory();
			distinfo_directory dir2 = new distinfo_directory();
			dir.name = "AoRK";
			dir.files = new List<distinfo_fileinfo>();
			dir2.files = new List<distinfo_fileinfo>();
			dir2.name = "help";
			distinfo_fileinfo file = new distinfo_fileinfo();
			file.md5 = "dfsgdsfhhSHFGs";
			file.sha1 = "dsfsfdsfsdh";
			file.name = "Test.txt";
			file.size = 56;
			file.type = "file";
			dir.files.Add(file);
			dir.directories = new List<distinfo_directory>();
			dir2.files = new List<distinfo_fileinfo>();
			dir2.files.Add(file);
			dir.directories.Add(dir2);
			DistInfo.content.Add(dir);
			Console.ReadLine();
			Xml.Serialize.file<distinfo>("e:/test.xml", DistInfo);
			Console.ReadLine();
		}
	}
}
