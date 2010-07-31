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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Demoder.MapPlotter;
using Demoder.Common;
using DynamicMaps.DataClasses;

namespace DynamicMaps
{
	public partial class MainWindow : Form
	{
		private Plotter _plotter;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			/*Demoder.MapPlotter.DataClasses.BookmarkCoordinates b = new Demoder.MapPlotter.DataClasses.BookmarkCoordinates();
			b.Coordinates.Add(new Demoder.MapPlotter.DataClasses.BookmarkCoordinates.BookmarkCoordinate("BS1", new Point(177,2818)));
			Xml.Serialize<Demoder.MapPlotter.DataClasses.BookmarkCoordinates>(new FileInfo(@"Data/BookmarkCoords_TowerMap.xml"), b, false);
			*/
			try
			{
				this._plotter = new Plotter(new FileInfo(@"Data/BookmarkCoords_TowerMap.xml"), MapCoords_Type.BOOKMARK_COORDINATES, Image.FromFile(@"Data/TowerMap.png"));
			}
			catch { }
			this._plotter.DrawTowerInfo(this._plotter.GetCoord("BS1"),
				1,
				"Clan",
				"undefined",
				80,
				150);
			this.pictureBox1.Image = this._plotter.Image;
		}
	}
}
