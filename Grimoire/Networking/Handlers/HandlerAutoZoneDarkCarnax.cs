using Grimoire.Game;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers
{
	public class HandlerAutoZoneDarkCarnax: IJsonMessageHandler
	{
		public string[] HandledCommands { get; } = { "event" };

		//{"t":"xt","b":{"r":-1,"o":{"cmd":"event","args":{"zoneSet":"A"}}}}
		public void Handle(JsonMessage message)
		{
			try
			{
				JObject args = (JObject)message.DataObject["args"];
				string zone = args["zoneSet"].ToString();
				switch (zone)
				{
					case "A":
						Player.WalkToPoint("860", "400");
						break;
					case "B":
						Player.WalkToPoint("49", "400");
						break;
					default:
						Player.WalkToPoint("426", "372");
						break;
				}
			}
			catch { }
		}
	}
}
