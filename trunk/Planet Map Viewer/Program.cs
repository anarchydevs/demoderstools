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
using System.Drawing;
using System.Windows.Forms;
using Demoder.Common;
using Demoder.PlanetMapLoader;

namespace Planet_Map_Viewer
{
	static public class Program
	{

		static internal List<PlanetMapDefinition> planetmaps;
		static internal Configuration Config;
		static internal string ConfigPath;
		static internal List<Image> Layers = new List<Image>();
		static internal int LayerPos = 0;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Program.ConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + Path.DirectorySeparatorChar;
			if (!Directory.Exists(Program.ConfigPath)) Directory.CreateDirectory(Program.ConfigPath);
			Program.Config = Xml.Deserialize.file<Configuration>(string.Format("{0}{1}Configuration.xml",
				Program.ConfigPath,
				Path.DirectorySeparatorChar));
			if (Program.Config == null) Program.Config = new Configuration();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}
	}
}
