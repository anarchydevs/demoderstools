using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapUpgrades.xml
{
	public class AppConfig
	{
		public string AOxPort_ExportDirectory = string.Empty;
		public SelectedTab selectedTab = SelectedTab.Manual;


		public enum SelectedTab
		{
			Manual,
			AOxPort
		}
	}
}
