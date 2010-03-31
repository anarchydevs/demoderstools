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
using System.IO;
using System.Windows.Forms;
using Demoder.Common;
using Demoder.MapCompiler;
using Demoder.MapCompiler.xml;

namespace AOMC
{
	static internal class Program
	{
#warning fixme: Make this load from appdata or something.
		internal static CompilerConfig Config_Compiler;
		internal static MapConfig Config_Map = new MapConfig();
		internal static AOMC_Config Config_AOMC = new AOMC_Config();
		internal static bool Config_Map_Changed = false;
		internal static string ConfigPath = "";
		internal static string MapConfigSavePath = string.Empty;
		internal static cmdParams cmdParams;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//Load commandline arguments.
			Program.cmdParams = new cmdParams(args);

			//Default config directory.
			if (Program.cmdParams.Flag("portable"))
			{
				Program.ConfigPath="cfg/";
			}
			else {
				Program.ConfigPath = Environment.SpecialFolder.ApplicationData+"/"+System.Reflection.Assembly.GetExecutingAssembly().GetName().Name+"/";
			}

			if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);


			//Initialize variables
			Program.Config_Compiler = Xml.Deserialize.file<CompilerConfig>(string.Format("{0}compiler_config.xml", Program.ConfigPath));
			Program.Config_AOMC = Xml.Deserialize.file<AOMC_Config>(string.Format("{0}aomc_config.xml", Program.ConfigPath));
			if (Program.Config_Compiler == null) { 
				Program.Config_Compiler = new CompilerConfig();
			}
			if (Program.Config_AOMC == null)
			{
				Program.Config_AOMC = new AOMC_Config();
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}
	}
}
