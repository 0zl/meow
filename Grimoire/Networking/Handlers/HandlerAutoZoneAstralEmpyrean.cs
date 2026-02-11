using Grimoire.Game;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers
{
	public class HandlerAutoZoneAstralEmpyrean: IJsonMessageHandler
	{
		public string[] HandledCommands { get; } = { "event" };

		public void Handle(JsonMessage message)
		{
			try
			{
				JObject args = (JObject)message.DataObject["args"];
				string zone = args["zoneSet"].ToString();
				switch (zone)
				{
					case "A":
						Player.WalkToPoint("708", "447");
						break;
					case "B":
						Player.WalkToPoint("287", "191");
						break;
					default:
						Player.WalkToPoint("461", "329");
						break;
				}
			}
			catch { }
		}
	}
}
