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
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Demoder.Common;
using Demoder.Patcher;
using Demoder.Patcher.DataClasses;


namespace Demoders_Patcher__Console_
{
	class Program
	{
		static void Main(string[] args)
		{
			//GUI Test
			/*
			List<string> dirs = new List<string>();
			dirs.Add(@"C:\Games\AO\Anarchy Online\cd_image\gui\Demoder");
			Distribution mapfiles = new Distribution(dirs, Distribution.DistributionTypes.AO_GUI);
			
			List<string> dirs2 = new List<string>();
			dirs2.Add(@"C:\Games\AO\Anarchy Online\cd_image\textures\archives\Demoder");
			Distribution maptextures = new Distribution(dirs2, Distribution.DistributionTypes.AO_GUI_Textures);

			PatchServer patchserver = new PatchServer();
			patchserver.Distributions.Add(mapfiles);
			patchserver.Distributions.Add(maptextures);


			patchserver.Export("e:/tmp/patchserver2/");
			*/
			
			
			
			
			// MAP test
			/*
			List<string> dirs = new List<string>();
			dirs.Add(@"C:\Games\AO\Anarchy Online\cd_image\textures\planetmap\AoSL");
			Distribution mapfiles = new Distribution(dirs, Distribution.DistributionTypes.AO_Map);

			PatchServer patchserver = new PatchServer();
			patchserver.Distributions.Add(mapfiles);
			
			 * //Xml.Serialize.file<PatchServer>("e:/test.xml", patchserver);
			patchserver.Export("e:/tmp/patchserver/");
			*/
			/*
			BinFile bf = Xml.Deserialize.file<BinFile>(@"E:\tmp\patchserver\bin_275A203205CCD70B8A7FF38357438711.xml");
			BinAssembler ba = new BinAssembler(bf, new DirectoryInfo(@"E:\tmp\patchserver\binslices"));
			List<byte> assembled = ba.Assemble();
			File.WriteAllBytes("E:/tmp/binfile.bin", assembled.ToArray());
			*/


			
			PatchServer ps = Xml.Deserialize<PatchServer>(new Uri("http://10.0.1.11/~demoder/patchserver/PatchServer.xml"));
			//PatchServer ps = Xml.Deserialize<PatchServer>(new Uri("http://patchserver.flw.nu/.test/PatchServer.xml"));
			//ps.download_locations.Add("http://patchserver.flw.nu/.test");
			//Xml.Serialize<PatchServer>(new FileInfo("e:/test.xml"), ps, false);
			/*bool failed = false;
			foreach (Distribution remoteDist in ps.Distributions)
			{
				UpdateDistribution ud = new UpdateDistribution(new DirectoryInfo(@"e:\tmp\Anarchy Online"), remoteDist, ps.download_locations);
				if (!ud.Success)
				{
					failed = true;
					break;
				}
			}
			if (failed) Console.WriteLine("Patching failed.");
			 */

			DoPatch dp = new DoPatch(ps);
			dp.InstallPatchedDistributions(new DirectoryInfo(@"E:\tmp\Anarchy Online"));
			Console.ReadLine();
		}
	}
}
