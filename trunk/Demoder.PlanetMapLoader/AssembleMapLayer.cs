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
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;

namespace Demoder.PlanetMapLoader
{
	public static class AssembleMapLayer
	{
		public static Bitmap FromDefinition(PlanetMapLayer pml, byte[] binfile)
		{
			float Resolution_x;
			float Resolution_z;
			PixelFormat Pixelformat;
			if (true) {
				//Get bitmap information.
				long LastSlice = pml.GetFilePos(pml.Tiles.Width, pml.Tiles.Height);
				MemoryStream ms = new MemoryStream(binfile, (int)LastSlice, (int)(binfile.Length - LastSlice));
				Image img = Bitmap.FromStream(ms);
				Resolution_x = img.HorizontalResolution;
				Resolution_z = img.VerticalResolution;
				Pixelformat = img.PixelFormat;
			}
			//Create image that we're drawing.
			Bitmap outimg = new Bitmap(pml.Size.Width, pml.Size.Height, Pixelformat);
			outimg.SetResolution(Resolution_x, Resolution_z);
			Graphics g = Graphics.FromImage(outimg);
			int x=0,z=0;
			while (x < pml.Tiles.Width)
			{
				long FilePos = pml.GetFilePos(x, z);
				long NextFilePos = pml.GetFilePos(x, z + 1);
				if (NextFilePos == 0 || NextFilePos == FilePos) NextFilePos = binfile.Length - 1;
				MemoryStream ms = new MemoryStream(binfile, (int)FilePos, (int)(NextFilePos - FilePos), false);
				int pos_x = (int)Math.Floor((double)x * (double)pml.TextureSize);
				int pos_z = (int)Math.Floor((double)z * (double)pml.TextureSize);
				//Load image
				Image img = Bitmap.FromStream(ms);
				//Draw image
				g.DrawImage(img, pos_x, pos_z);
				z++; //Increment vertical size
				if (z >= pml.Tiles.Height)
				{
					z = 0; //Reset vertical position
					x++; //Increment horizontal position.
				}
			}
			g.Dispose();
			binfile = null;
			outimg.Save("e:/test.png", ImageFormat.Png);
			return outimg;
		}
	}
}
