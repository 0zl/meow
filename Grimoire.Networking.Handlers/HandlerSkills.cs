using Grimoire.Game;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Grimoire.Botting;
using System.Windows.Forms;

namespace Grimoire.Networking.Handlers
{
    public class HandlerSkills : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "sAct"
        };

        public void Handle(JsonMessage message)
        {
            JToken jToken = message.DataObject["actions"];
            if (jToken.Type != JTokenType.Null)
            {
                JToken passives = jToken["passive"];
                if (passives.Type != JTokenType.Null)
                    foreach (JToken item in passives)
                    { 
                        item["range"] = OptionsManager.InfiniteRange ? "20000" : item["range"];
                        item["mp"] = "0";
                    }
                JToken actives = jToken["active"];
                if (actives.Type != JTokenType.Null)
                    foreach (JToken item in actives)
                    {
                        item["range"] = OptionsManager.InfiniteRange ? "20000" : item["range"];
                        item["mp"] = "0";
                    }
            }
        }
    }
}