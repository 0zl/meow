using Grimoire.Botting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.Tools
{
	public static class MapConfig
	{
		public static Config Config = Config.Load(Application.StartupPath + "\\MapConfig.cfg");
		public const string MapDot = "Map.";
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

        public static List<string> GetMapNames()
        {
            string path = Application.StartupPath + "\\MapConfig.cfg";
            if (!File.Exists(path))
                return new List<string>();

            return File.ReadAllLines(path)
                .Where(line => line.StartsWith(MapDot) && line.Contains('='))
                .Select(line =>
                {
                    string key = line.Split('=')[0]; // ambil sebelum '='
                    return key.Substring(MapDot.Length); // hapus "Map."
                })
                .ToList();
        }

        static void bangke()
		{
			MapConfig.GetValue(MapDot); //Map.Ultradarkon=Enter;Spawn;8733
        }	
	}
}
