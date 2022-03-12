using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Networking.Handlers
{
    public class HandlerDropItem : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "dropItem"
        };

        public void Handle(JsonMessage message)
        {
            /*JToken jToken = message.DataObject?["items"];
            if (jToken != null)
            {
                InventoryItem item = jToken.ToObject<Dictionary<int, InventoryItem>>().First().Value;
                if (BotManager.Instance.ActiveBotEngine.IsRunning)
                {
                    Configuration configuration = BotManager.Instance.ActiveBotEngine.Configuration;
                    message.Send = !configuration.EnableRejection || !configuration.Drops.All((string d) => !d.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                }
                World.OnItemDropped(item);
            }*/

            JObject items = (JObject)message.DataObject["items"];
            if (items != null)
            {
                InventoryItem item = items.ToObject<Dictionary<int, InventoryItem>>().First().Value;
                World.OnItemDropped(item);
                if (BotManager.Instance.ActiveBotEngine.IsRunning)
                {
                    Configuration configuration = BotManager.Instance.ActiveBotEngine.Configuration;
                    List<string> whiteList = new List<string>();
                    if (configuration.Drops != null)
                        whiteList = configuration.Drops;
                    if (configuration.EnableRejection && !whiteList.ConvertAll<string>(a => a.ToLower()).Contains(item.Name.ToLower()))
                    {
                        Flash.Call("RejectDrop", new string[2] { item.Name.ToLower(), item.Id.ToString() });
                    }
                }
            }
        }
    }
}