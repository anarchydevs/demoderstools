﻿/*
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
using System.Windows.Forms;
using Demoders_Patcher.DataClasses;
using Demoder.Common;

namespace Demoders_Patcher
{
	internal static class Program
	{
		#region members
		/// <summary>
		/// Path to the PatcherConfig .xml file
		/// </summary>
		public static FileInfo PatcherConfigPath = null;
		/// <summary>
		/// Patchers configuration
		/// </summary>
		public static PatcherConfig PatcherConfig = null;
		/// <summary>
		/// Update definitions fetched from the central repisory
		/// </summary>
		public static UpdateDefinitions UpdateDefinitions_Central = new UpdateDefinitions();
		/// <summary>
		/// Update definitions from the local configuration
		/// </summary>
		public static UpdateDefinitions UpdateDefinitions_Local = new UpdateDefinitions();
		/// <summary>
		/// Directory containing all of the patchers configurations.
		/// </summary>
		public static DirectoryInfo ConfigDir = null;

		/// <summary>
		/// Log of statusbar updates.
		/// </summary>
		public static List<StatusbarUpdate> StatusbarUpdates = new List<StatusbarUpdate>();
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			#region make sure appdata paths exist
			Program.ConfigDir = new DirectoryInfo(string.Format("{1}{0}{2}",
				Path.DirectorySeparatorChar,
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"DemodersPatcher"));
			if (!Program.ConfigDir.Exists)
				Program.ConfigDir.Create();
			Program.PatcherConfigPath = new FileInfo(string.Format("{1}{0}{2}",
				Path.DirectorySeparatorChar,
				Program.ConfigDir,
				"PatcherConfig.xml"));
			#endregion
			#region Patcher config
			if (!Program.PatcherConfigPath.Exists)
			{
				Program.PatcherConfig = new PatcherConfig();
				Xml.Serialize<PatcherConfig>(Program.PatcherConfigPath, Program.PatcherConfig, false);
			}
			else
			{
				//Load patcher configuration here.
				Program.PatcherConfig = Xml.Deserialize<PatcherConfig>(Program.PatcherConfigPath, false);
				if (Program.PatcherConfig == null)
					Program.PatcherConfig = new PatcherConfig();
			}
			//Xml.Serialize<PatcherConfig>(Program.PatcherConfigPath, Program.PatcherConfig, false);
			#endregion
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Windows.MainWindow());
		}
	}
}