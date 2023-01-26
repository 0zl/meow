using Grimoire.Botting;
using System.Windows.Forms;

namespace Grimoire.Tools
{
	public static class ClientConfig
	{
		public static Config Config = Config.Load(Application.StartupPath + "\\ClientConfig.cfg");
		public const string C_SKILL_PRESET_PREFIX = "SS.";
		public const string C_BOTS_DIR = "botsDir";
		public const string C_FLASH = "flash";
		public const string C_LOG_DEBUG_SWF = "logDebugSwf";
		public const string C_SAFE_CELL = "safeCell";
		public const string C_QUEST_LIST_DELAY = "questListDelay";
		public const string C_ACCESS_LEVEL = "accessLevel";
		public const string C_NAME_COLOR = "nameColor";

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
