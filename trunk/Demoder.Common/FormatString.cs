using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Demoder.Common
{
	public class FormatString
	{
		private Dictionary<string, object> dictionary;
		public FormatString(Dictionary<string, object> dict)
		{
			this.dictionary = dict;
		}

		public string Format(string ToFormat)
		{
			string outstring = ToFormat;
			Regex re = new Regex(@"\{[^}]*\}");
			outstring = re.Replace(outstring, doFormatString);
			return outstring;
		}
		private string doFormatString(Match input)
		{
			string matchkey = input.Value.Substring(1, input.Value.Length - 2).ToLower();
			if (this.dictionary.ContainsKey(matchkey))
				return this.dictionary[matchkey].ToString();
			else
				return string.Empty;
		}
	}
}
