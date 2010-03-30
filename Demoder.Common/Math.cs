using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demoder.Common
{
	public static class math
	{
		public static int Percent (int max, int cur) {
			return (int)System.Math.Round((double)(cur * 100 / max), 0);
		}
		/// <summary>
		/// Determines how much of 'full' the provided 'percent' means.
		/// </summary>
		/// <param name="full">Number of units</param>
		/// <param name="partial">Percent</param>
		/// <returns></returns>
		public static int dePercent(int full, int percent)
		{
			return (int)Math.Round(percent * ((double)full / 100), 0);
		}
	}
}
