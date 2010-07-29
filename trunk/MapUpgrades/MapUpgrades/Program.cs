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
using System.Windows.Forms;
using Demoder.Common;
using System.IO;

namespace MapUpgrades
{
    static class Program
    {
		internal static xml.AppConfig Configuration = null;
		internal static string ConfigBasePath;
		internal static string ConfigPath;
		internal static string SelectedAOxPortCharacter = string.Empty;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			//Setup configs etc
			Program.ConfigBasePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + Path.DirectorySeparatorChar;
			Program.ConfigPath = Program.ConfigBasePath + Path.DirectorySeparatorChar + "config.xml";
			if (!Directory.Exists(Program.ConfigBasePath)) Directory.CreateDirectory(Program.ConfigBasePath);
			try { Program.Configuration = Xml.Deserialize<xml.AppConfig>(new FileInfo(Program.ConfigPath), false); }
			catch { }
			if (Program.Configuration == null)
				Program.Configuration = new xml.AppConfig();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

		public static void SaveConfig()
		{
			Xml.Serialize<xml.AppConfig>(new FileInfo(Program.ConfigPath), Program.Configuration, false);
		}
    }
}
