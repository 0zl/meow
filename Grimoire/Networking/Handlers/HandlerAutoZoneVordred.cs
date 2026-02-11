using Grimoire.Game;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers
{
	public class HandlerAutoZoneVordred: IJsonMessageHandler
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
						Player.WalkToPoint("731", "461");
						break;
					case "B":
						Player.WalkToPoint("700", "321");
						break;
					default:
						Player.WalkToPoint("748", "372");
						break;
				}
			}
			catch { }
		}
	}
}
