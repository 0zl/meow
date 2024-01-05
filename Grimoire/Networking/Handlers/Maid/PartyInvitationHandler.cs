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
	public class PartyInvitationHandler : IJsonMessageHandler
	{
		public string[] HandledCommands { get; } = { "pi" };

		public void Handle(JsonMessage message)
		{
			try
			{
				string owner = ((JObject)message.DataObject["owner"]).ToString();
				string pid = ((JObject)message.DataObject["pid"]).ToString();

				string packet = $"%xt%zm%gp%1%pa%{pid}%";
				Proxy.Instance.SendToServer(packet);
			}
			catch(Exception e) { 
				Console.WriteLine("err: "+e.ToString());
			}
		}
	}
}
