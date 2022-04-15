using Grimoire.Game;
using Newtonsoft.Json.Linq;

namespace Grimoire.Networking.Handlers
{
	public class HandlerAutoZoneUltraDage: IJsonMessageHandler
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
						Player.WalkToPoint("107", "400");
						break;
					case "B":
						Player.WalkToPoint("843", "400");
						break;
					default:
						Player.WalkToPoint("503", "276");
						break;
				}
			}
			catch { }
		}
	}
}
