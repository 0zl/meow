using Grimoire.UI;
using System.Threading.Tasks;
using Grimoire;
using Grimoire.Botting;
using Newtonsoft.Json.Linq;
using System.Windows;
using Grimoire.Game;
using System;

namespace Grimoire.Networking.Handlers
{
    public class HandlerMapJoin : IJsonMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "moveToArea"
        };

        public void Handle(JsonMessage message)
        {
            JToken areaName = message.DataObject["areaName"];
            JArray monmap = message.DataObject["monmap"] as JArray;

            Console.WriteLine($"areaName: {areaName}");
            Console.WriteLine($"monmap: {monmap.Count}");

            if (areaName.Type != JTokenType.Null)
            {
                if (((string)areaName).Contains("mobius"))
                {
                    int length = monmap.Count;
                    for (int i = 0; i < length; i++)
                    {
                        JToken monmapItem = monmap[i];
                        if (((string)monmapItem["MonMapID"]) == "9")
                        {
                            monmap.Remove(monmapItem);
                            break;
                        }
                    }
                }

                if (((string)areaName).Contains("rangda"))
                { 

                }
            }

            Console.WriteLine($"monmap after: {monmap.Count}");
        }
    }
}