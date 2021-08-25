using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Grimoire.Tools.Plugins
{
	public class PluginManager
	{
		public string LastError { get; private set; }

		public bool Load(string path)
		{
			GrimoirePlugin grimoirePlugin = null;
			if (File.Exists(path))
			{
				grimoirePlugin = new GrimoirePlugin(path);
				if (grimoirePlugin.Load())
				{
					this.LoadedPlugins.Add(grimoirePlugin);
					return true;
				}
			}
			this.LastError = ((grimoirePlugin != null) ? grimoirePlugin.LastError : null);
			return false;
		}

		public bool Unload(GrimoirePlugin plugin)
		{
			if (plugin.Unload())
			{
				this.LoadedPlugins.Remove(plugin);
				return true;
			}
			this.LastError = plugin.LastError;
			return false;
		}

		public bool LoadRange(string[] paths)
		{
			return paths.All(new Func<string, bool>(this.Load));
		}

		public bool UnloadAll()
		{
			return this.LoadedPlugins.All((GrimoirePlugin p) => p.Unload());
		}

		public List<GrimoirePlugin> LoadedPlugins = new List<GrimoirePlugin>();
	}
}
