using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demoder.Common
{
	public static class Math
	{
		public static int Percent (int max, int cur) {
			return (int)System.Math.Round((decimal)(cur * 100 / max), 0);
		}
	}
}
