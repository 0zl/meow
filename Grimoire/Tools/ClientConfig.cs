using Grimoire.Botting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Tools
{
	public static class ClientConfig
	{
		public static Config Config = Config.Load(Application.StartupPath + "\\ClientConfig.cfg");
		public const string C_BOTS_DIR = "botsDir";
		public const string C_SKILL_PRESET_PREFIX = "SS.";

		public static string GetValue(string key)
		{
			string value;
			try
			{
				value = Config.Get(key);
			}
			catch
			{
				value = null;
			}
			return value;
		}

		public static void SetValue(string key, string value)
		{
			Config.Set(key, value);
			Config.Save();
		}
	}
}
