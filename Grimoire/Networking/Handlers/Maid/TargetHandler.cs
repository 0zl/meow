using Grimoire.Game;
using Grimoire.Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers.Maid
{
	public class TargetHandler : IJsonMessageHandler
	{
		public string[] HandledCommands { get; } = { "ct" };

		public string pMaster;

		public void Handle(JsonMessage message)
		{
			try
			{
				JObject p = ((JObject)message.DataObject["p"]);
				JObject m = ((JObject)message.DataObject["m"]);

				if (p.ContainsKey(pMaster))
				{
					Player.AttackMonster("*");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("err: " + e.ToString());
			}
		}
	}
}
