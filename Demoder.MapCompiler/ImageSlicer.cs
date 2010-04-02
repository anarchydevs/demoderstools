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
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Threading;

namespace Demoder.MapCompiler
{
	public class ImageSlicer
	{
		#region members
		/// <summary>
		/// Contains the slices produced by the worker.
		/// </summary>
		private List<MemoryStream> _SliceStreams = new List<MemoryStream>(128);
		public int Height { get { return this._slices_height; } }
		public int Width { get { return this._slices_width; } }
		public List<MemoryStream> Slices
		{
			get
			{
				lock (this._SliceStreams)
					return this._SliceStreams;
			}
		}

		/// <summary>
		/// Size of image.
		/// </summary>
		private int _slices_width;
		/// <summary>
		/// Size of image.
		/// </summary>
		private int _slices_height;

		/// <summary>
		/// Each slice is _texturesize x _texturesize pixels.
		/// </summary>
		private int _texturesize = 128; //Texture size.

		#endregion

		#region Members - multithreaded slicer
		//Threads
		private Thread t1;
		private Thread t2;
		//Manual reset events
		private ManualResetEvent r1;
		private ManualResetEvent r2;
		//Images
		private Image i1;
		private Image i2;

		//Slice streams
		private List<MemoryStream> s1;
		private List<MemoryStream> s2;

		#endregion

		/// <summary>
		/// Slice up an image into several smaller pieces
		/// </summary>
		/// <param name="source">Image</param>
		/// <param name="texturesize">Size of each slice</param>
		public ImageSlicer(ImageData source, int texturesize, bool singlethreaded)
		{
			bool threaded = false;
			this._texturesize = texturesize;
			MemoryStream ms = new MemoryStream(source.bytes);
			Image srcimg = Image.FromStream(ms);
			//Bitmap srcimg = Image.FromStream(ms) as Bitmap;
			ms.Close();
			this._slices_width = (int)Math.Ceiling((double)(srcimg.Width / texturesize));
			this._slices_height = (int)Math.Ceiling((double)(srcimg.Height / texturesize));
			//Only do slicer-threading on large slices, since the overhead is so big.
			if (!singlethreaded && (this._slices_height * this._slices_width) > 512)
				threaded = true;
			//threaded = false;
			if (!threaded) this.slicer(srcimg);
			else
			{
				#region Setup images
				int width = (int)Math.Floor((double)(this._slices_width / 2));
				width = (int)Math.Floor((double)(width * texturesize));
				int height = (int)Math.Floor((double)(this._slices_height * texturesize));
				//First image
				this.i1 = new Bitmap(width,height);
				Graphics g = Graphics.FromImage(this.i1);
				Rectangle rect = new Rectangle(0, 0, width, height);
				g.DrawImage(srcimg, 0, 0, rect, GraphicsUnit.Pixel);
				g.Dispose();
				//Second image
				this.i2 = new Bitmap(width, height);
				g = Graphics.FromImage(this.i2);
				int newwidth = srcimg.Width - width;
				rect = new Rectangle(width, 0, newwidth, height);
				g.DrawImage(srcimg, 0, 0, rect, GraphicsUnit.Pixel);
				g.Dispose();
				try
				{
					this.i1.Save("e:/1.png", ImageFormat.Png);
					this.i2.Save("e:/2.png", ImageFormat.Png);
				}
				catch (Exception ex)
				{
					
				}
				#endregion
				
				//Memory streams
				this.s1 = new List<MemoryStream>();
				this.s2 = new List<MemoryStream>();

				#region Setup threads
				//Threads
				this.r1 = new ManualResetEvent(false);
				this.t1 = new Thread(new ParameterizedThreadStart(this.slicer_threaded));
				this.t1.IsBackground = true;
				this.t1.Priority = ThreadPriority.BelowNormal;
				
				this.r2 = new ManualResetEvent(false);
				this.t2 = new Thread(new ParameterizedThreadStart(this.slicer_threaded));
				this.t2.IsBackground = true;
				this.t2.Priority = ThreadPriority.BelowNormal;

				this.t1.Start(1);
				this.t2.Start(2);
				#endregion
				//Wait for threads to finish.
				this.r1.WaitOne();
				this.r2.WaitOne();
				//Assemble the slices.
				this._SliceStreams = new List<MemoryStream>();
				foreach (MemoryStream m in this.s1)
				{
					this._SliceStreams.Add(m);
				}
				foreach (MemoryStream m in this.s2)
				{
					this._SliceStreams.Add(m);
				}
				int blah2 = this._SliceStreams.Count;
			}
		}

		public void Dispose()
		{
			if (this.i1!=null) this.i1.Dispose();
			if (this.i2 != null) this.i2.Dispose();
			if (this.s1 != null) this.s1 = null;
			if (this.s2 != null)this.s2 = null;
			if (this.r1 != null) this.r1 = null;
			if (this.r2 != null) this.r2 = null;
			if (this.t1 != null) this.t1 = null;
			if (this.t2 != null) this.t2 = null;
			this._SliceStreams = null;
			System.GC.Collect(1, GCCollectionMode.Optimized); //Request garbage collection
		}

		#region Multithreaded slicer
		private void slicer_threaded(object obj)
		{
			Image srcimg=null;
			int worknum = (int)obj;
			switch (worknum)
			{
				case 1: srcimg = this.i1; break;
				case 2: srcimg = this.i2; break;
			}

			int swidth = srcimg.Width;
			int sheight = srcimg.Height;
			MemoryStream ms;
			Image timg;
			int pos_width = 0, pos_height = 0; //Track current position.
			do
			{
				timg = this.copySlice(srcimg, pos_width, pos_height);
				ms = new MemoryStream();
				timg.Save(ms, ImageFormat.Png);
				switch (worknum)
				{
					case 1: this.s1.Add(ms); break;
					case 2: this.s2.Add(ms); break;
				}

				pos_height += this._texturesize; //Increment height position by one slice.
				if (pos_height >= sheight)
				{
					pos_height = 0;
					pos_width += this._texturesize;
				}
			} while (pos_width < swidth);
			switch (worknum)
			{
				case 1: this.r1.Set(); break;
				case 2: this.r2.Set(); break;
			}
		}


		#endregion

		#region Singlethreaded slicer
		private void slicer (Image srcimg) {
			int swidth = srcimg.Width;
			int sheight = srcimg.Height;
			MemoryStream ms;
			Image timg;
			int pos_width = 0, pos_height = 0; //Track current position.
			do
			{
				timg = this.copySlice(srcimg, pos_width, pos_height);
				ms = new MemoryStream();
				timg.Save(ms, ImageFormat.Png);
				
				//File.WriteAllBytes(string.Format("e:/tmp/blah/{0}_{1}_{2}.png",source.name, pos_width, pos_height), ms.ToArray());
				this._SliceStreams.Add(ms);

				pos_height += this._texturesize; //Increment height position by one slice.
				if (pos_height >= sheight)
				{
					pos_height = 0;
					pos_width += this._texturesize;
				}
			} while (pos_width < swidth);
		}
		#endregion

		


		#region methods
		private Image copySlice(Image src, int start_x, int start_y)
		{
			int tsize = this._texturesize;
			int swidth = src.Width;
			int sheight = src.Height;
			Image tmpimg = new Bitmap(tsize, tsize,PixelFormat.Format24bppRgb);
			//tmpimg.PixelFormat;
			Graphics g = Graphics.FromImage(tmpimg);
			Rectangle rect = new Rectangle(start_x, start_y, tsize, tsize);
			g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel);
			g.Dispose();
			return tmpimg;
		}


		/// <summary>
		/// Copy an image slice starting at position x,y
		/// </summary>
		/// <param name="src"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private Bitmap copySlice_old(Bitmap src, int start_x, int start_y)
		{
			int x = 0;
			int y = 0;
			int tsize;
			tsize = this._texturesize;

			Bitmap tmpimg = new Bitmap(tsize, tsize);
			while (x < tsize) //If horizontal is less than max, add. (0-index)
			{
				//Source x/y coordinates.
				int sx = x + start_x;
				int sy = y + start_y;

				Color color;
				if (sy > src.Height || sx > src.Width)
					color = Color.Black;
				else
					color = src.GetPixel((start_x + x), (start_y + y));

				tmpimg.SetPixel(x, y, color);

				y++; //Increment vertical position by one.
				if (y == tsize)
				{ //If vertical is equal max (0-index)
					y = 0; //Set vertical to 0
					x++; //Increment horizontal
				}
			}
			return tmpimg; //Retun the result.
		}
		#endregion
	}
}
