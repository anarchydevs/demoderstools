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
using System.Drawing;
using System.Linq;
using System.Text;
using DynamicMaps.DataClasses;


namespace DynamicMaps
{
	public class oldMapPlotter
	{
		private MapCoordinates _MapCoordinates = new MapCoordinates();
		private Size _MapImageSize = new Size();
		/// <summary>
		/// MapRectangle; what part of image to utilize.
		/// </summary>
		private MapRect _MapRectangle = new MapRect();
		/// <summary>
		/// This ingame coordinate is the 0x0 point within the maprect on our map. (bottom left)
		/// </summary>
		private Point _LowestCoord = new Point();
		/// <summary>
		/// This ingame coordinate is the max point within the maprect on our map. (top right)
		/// </summary>
		private Point _HighestCoord = new Point();

		private Dictionary<int, Point> _temporary_ZoneLocationDict = new Dictionary<int, Point>();


		public oldMapPlotter(MapCoordinates MapCoordinates, Size ImageSize, MapRect MapRectangle)
		{
			this._MapCoordinates = MapCoordinates;
			this._MapImageSize = ImageSize;
			this._MapRectangle = MapRectangle;
			//Find the lowest & highest coord.
			double lowest_x = 1000000000, lowest_z = 1000000000;
			double highest_x = 0, highest_z = 0;

			foreach (MapCoordinates.Playfield pf in this._MapCoordinates.Playfields)
			{
				//lowest
				if (pf.x < lowest_x) lowest_x = pf.x;
				if (pf.z < lowest_z) lowest_z = pf.z;
				//highest
				
				if (pf.id == 505) {	//Avalon is the northmost zone 
					//highest_z = pf.z + 4436 + 25; // Fits over bor jobe wompah
					highest_z = pf.z + 4638;
				}
				if (pf.id == 595)
				{ //Deep Artery Valley is the eastmost zone
					//highest_x = pf.x + 3574 + 280; // Fits over bor jobe wompah
					highest_x = pf.x + 3600;
				}
				 
			}
			this._HighestCoord.X = 49867;
			this._LowestCoord = new Point((int)Math.Round(lowest_x, 0), (int)Math.Round(lowest_z,0));
			this._HighestCoord = new Point((int)Math.Round(highest_x, 0), (int)Math.Round(highest_z,0));

			//Hackintouch



		}

		public Point GetPoint(int pfid, double xpos, double zpos)
		{
			MapCoordinates.Playfield pf = null;
			try
			{
				pf = this._MapCoordinates.GetPF(pfid);
			}
			catch (Exception ex)
			{
			}

			//X = 30900 & Z = 24750 seems to be 0x0 (bottom left corner) of map image.
			if (pf == null) return new Point();

			
			//Vector C
			Point GlobalCoord = new Point((int)Math.Round(pf.x + (xpos * pf.xscale),0), (int)Math.Round(pf.z + (zpos * pf.zscale), 0));

			//Vector D: this._LowestCoord

			//Vector E:
			Point RelativePosition = new Point (
				GlobalCoord.X - this._LowestCoord.X,
				GlobalCoord.Y - this._LowestCoord.Y);

			//Scale
			double ImageScaleX = (this._HighestCoord.X - this._LowestCoord.X) / (this._MapRectangle.Width - this._MapRectangle.StartX);
			double ImageScaleZ = (this._HighestCoord.Y - this._LowestCoord.Y) / (this._MapRectangle.Height - this._MapRectangle.StartZ);
			
			//double ImageScaleX = (this._HighestCoord.X - this._LowestCoord.X) / (this._MapRectangle.Width);
			//double ImageScaleZ = (this._HighestCoord.Y - this._LowestCoord.Y) / (this._MapRectangle.Height);

			//Image position, not adjusted with maprect:
			Point ImagePosition = new Point(
				(int)Math.Round((double)RelativePosition.X / ImageScaleX),
				this._MapImageSize.Height - (int)Math.Round((double)RelativePosition.Y / ImageScaleZ));

			//Adjust image position with maprect.
			int adjust = (int)Math.Round(this._MapRectangle.StartX,0);
			ImagePosition.X += adjust;
			//ImagePosition.Y += (int)Math.Round(this._MapRectangle.StartZ,0);

			//Return image position.
			return ImagePosition;
		}
	}
}
