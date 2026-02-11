using Grimoire.Game;
using Grimoire.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers
{
    public class MapItemHandler : IJsonMessageHandler
    {

        private readonly TaskCompletionSource<bool> _tcs;
        private readonly int mapItemid;

        public MapItemHandler(TaskCompletionSource<bool> tcs, int itemid)
        {
            _tcs = tcs;
            mapItemid = itemid;
        }
        public string[] HandledCommands { get; } = { "addItems" };
        //{"t":"xt","b":{"r":-1,"o":{"cmd":"addItems","items":{"23725":{"ItemID":23725,"sElmt":"None","sLink":"","bStaff":0,"iRng":10,"iDPS":0,"bCoins":0,"sES":"None","metaValues":{},"sType":"Quest Item","iCost":0,"iRty":13,"iQSValue":0,"iQty":1,"sReqQuests":3487,"sIcon":"iibag","iLvl":1,"bTemp":1,"bPTR":0,"iQSIndex":-1,"iStk":12,"sDesc":"...","bHouse":0,"bUpg":0,"sName":"Balmblossom"}}}}}
        public void Handle(JsonMessage message)
        {
            //Console.WriteLine(message.ToString());
            try
            {
                // Ambil "items"
                JObject items = message.DataObject["items"] as JObject;
                if (items == null)
                {
                    LogForm.Instance.devDebug("[AddItemHandler] 'items' not found in packet.");
                    return;
                }
                
                foreach (var item in items)
                {
                    var obj2 = item.Value as JObject;
                    if (obj2 == null)
                        continue;

                    string name = item.Value["sName"]?.ToString() ?? "blank";

                    lock (Player.recentMapItem)
                    {
                        if (!Player.recentMapItem.ContainsKey(mapItemid))
                            Player.recentMapItem[mapItemid] = name;
                    }

                    LogForm.Instance.devDebug($"[AddItemHandler] MapItem added: {name} ({mapItemid})");
                }
                _tcs.TrySetResult(true);
            }
            catch (Exception ex)
            {
                _tcs.TrySetResult(false);
            }
            //Proxy.Instance.UnregisterHandler(this);
        }
    }
}
