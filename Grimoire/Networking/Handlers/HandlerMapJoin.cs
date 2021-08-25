using Newtonsoft.Json.Linq;

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
            if (message.DataObject["monmap"].Type != JTokenType.Null)
            {
                JArray monmap = message.DataObject["monmap"] as JArray;

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
            }
        }
    }
}