using Grimoire.Game;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers
{
	public class HandlerAutoZoneQueenIona : IJsonMessageHandler
	{
		public string[] HandledCommands { get; } = { "event" };

		public async void Handle(JsonMessage message)
		{
			await Task.Delay(500);
			try
			{
				JObject args = (JObject)message.DataObject["args"];
				string zone = args["zoneSet"].ToString();

				bool positiveCharge = Player.GetAuras(true, "Positive Charge") > 0;
				bool positiveChargeReversed = Player.GetAuras(true, "Positive Charge?") > 0;
				bool negativeCharge = Player.GetAuras(true, "Negative Charge") > 0;
				bool negativeChargeReversed = Player.GetAuras(true, "Negative Charge?") > 0;

				switch (zone)
				{
					case "A":
						if (positiveCharge || negativeChargeReversed)
						{
							Player.WalkToPoint("679", "339");
						}
						else if (negativeCharge || positiveChargeReversed)
						{
							Player.WalkToPoint("272", "379");
						}
						break;
					case "B":
						if (positiveCharge || negativeChargeReversed)
						{
							Player.WalkToPoint("272", "379");
						}
						else if (negativeCharge || positiveChargeReversed)
						{
							Player.WalkToPoint("679", "339");
						}
						break;
					default:
						Player.WalkToPoint("490", "320");
						break;
				}

			}
			catch { }
		}
	}
}