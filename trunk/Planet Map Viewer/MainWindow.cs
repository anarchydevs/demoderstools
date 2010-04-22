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
using Demoder.Common;
using Demoder.PlanetMapLoader;

namespace Planet_Map_Viewer
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			Program.planetmaps = AvailableMaps.GetMaps(Program.Config.PlanetMapDir);
			//Program.planetmaps.Sort();
			foreach (PlanetMapDefinition pmd in Program.planetmaps) {
				if (pmd.Name == "Atlas of Shadowlands v1.2.3")
				{
					this.label_Status.Text = "Loading map...";
					this.backgroundWorker1.RunWorkerAsync(pmd);
					break;
				}
			}
		}

		private void viewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//Stuff to select map here.
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			PlanetMapDefinition pmd = (PlanetMapDefinition)e.Argument;
			Dictionary<string, byte[]> binfiles = new Dictionary<string, byte[]>();
			LoadMapSlices lms = new LoadMapSlices();
			foreach (PlanetMapLayer pml in pmd.Layers)
			{
				if (!binfiles.ContainsKey(pml.File))
					binfiles.Add(pml.File, File.ReadAllBytes(Program.Config.PlanetMapDir + Path.DirectorySeparatorChar + pml.File));
				lms.FromDefinition(pml, binfiles[pml.File]);
			}
			binfiles.Clear();
			e.Result = lms.layers;
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Program.Layers = (List<LoadMapSlices.layer>)e.Result;
			//Set the default layer.
			Program.LayerPos = 0;
			this.DisplayLayer(Program.LayerPos);
		}

		private List<PictureBox> pictureboxes = null;
		private void DisplayLayer(int layerpos){
			this.flowLayoutPanel1.Controls.Clear();
			if (this.pictureboxes != null)
				foreach (PictureBox pb in this.pictureboxes)
				{
					pb.Dispose();
				}
			this.flowLayoutPanel1.BorderStyle = BorderStyle.None;
			this.flowLayoutPanel1.SuspendLayout();
			LoadMapSlices.layer layer = Program.Layers[layerpos];
			this.pictureboxes = new List<PictureBox>(layer.tiles_height * layer.tiles_width);
			this.flowLayoutPanel1.Size = layer.size;
			this.label_Status.Text = "";
			int x = 0, y = 0;
			while (y<layer.tiles_height){
				PictureBox pb = new PictureBox();
				pb.Size = new Size(layer.texturesize, layer.texturesize); //Make the picture box the right size
				pb.SizeMode = PictureBoxSizeMode.StretchImage; //If the config file defined the wrong texture size... well, adjust the image to fit the provided texture size.
				try
				{
					pb.Image = layer.GetSlice(x, y);
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("{0}", ex));
				}
				pb.Margin = new Padding(0);
				pb.MouseDoubleClick += new MouseEventHandler(this.map_MouseDoubleClick);
				pictureboxes.Add(pb);
				this.flowLayoutPanel1.Controls.Add(pb);
				x++;
				if (x>=layer.tiles_width) {
					x=0;
					y++;
				}
				this.flowLayoutPanel1.ResumeLayout();
			}
		}

		private void map_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				Program.LayerPos++;
			if (e.Button == MouseButtons.Right)
				Program.LayerPos--;
			if (Program.LayerPos < 0) Program.LayerPos = 0;
			else if (Program.LayerPos >= Program.Layers.Count) Program.LayerPos = Program.Layers.Count - 1;
			this.DisplayLayer(Program.LayerPos);
		}

		private bool mousedown = false;
		private Point mousePos;

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mousedown = true;
				mousePos = e.Location;
			}
			if (e.Delta > 0 || e.Delta < 0)
			{
				VScrollProperties vs = splitContainer2.Panel1.VerticalScroll;
				vs.Value += e.Delta * 10;
			}
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			
			if (mousedown)
			{
				if (mousePos.IsEmpty)
					mousePos = e.Location;
				int xdelta = (mousePos.X - e.X);
				int ydelta = (mousePos.Y - e.Y);

				int filter = 1000;
				if (xdelta > filter || xdelta < -filter || ydelta > filter || ydelta < -filter)
				{
					return;
				}
				bool xchanged = true, ychanged = true;
				if (xdelta == 0) xchanged = false;
				if (ydelta == 0) ychanged = false;
				if (!xchanged && !ychanged) return;



				if (ychanged)
				{
					VScrollProperties vs = splitContainer2.Panel1.VerticalScroll;
					vs.Enabled = false;
					int new_y = vs.Value + ydelta;
					if (new_y < vs.Minimum) new_y = 0;
					else if (new_y > vs.Maximum) vs.Value = vs.Maximum;
					else vs.Value = new_y;
					//label_Status.Text += "Y changed: " + ydelta.ToString();
					//mousePos.Y = e.Y;
					vs.Enabled = true;
				}
				if (xchanged)
				{
					HScrollProperties hs = splitContainer2.Panel1.HorizontalScroll;
					hs.Enabled = false;
					int new_x = hs.Value + xdelta;
					if (new_x < hs.Minimum) hs.Value = hs.Minimum;
					else if (new_x > hs.Maximum) hs.Value = hs.Maximum;
					else hs.Value = new_x;
					//label_Status.Text += " X changed: " + xdelta.ToString();
					//mousePos.X = e.X;
					hs.Enabled = true;
				}
			}
			mousedown = false;
			mousePos = new Point();

		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			label_Status.Text = string.Format("Mouse: {0}x{1}", e.X, e.Y);
			
		}
	}
}
