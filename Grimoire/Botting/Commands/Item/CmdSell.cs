using Grimoire.Game;
using Grimoire.Game.Data;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdSell : IBotCommand
    {
        public string ItemName
        {
            get;
            set;
        }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public string Qty
        {
            get;
            set;
        } = "1";
        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            string _ItemName = (instance.IsVar(this.ItemName) ? Configuration.Tempvariable[instance.GetVar(this.ItemName)] : this.ItemName);
            int _qty = int.Parse(instance.ResolveVars(Qty));
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.SellItem));
            InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(_ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                using (new pauseProvoke(instance.Configuration))
                {
                    await Player.ExitCombat();
                    Shop.SellItem(_ItemName, _qty);
                    await instance.WaitUntil(() => !Player.Inventory.ContainsItem(item.Name, item.Quantity.ToString()));
                }
            }
        }

        public override string ToString()
        {
            int.TryParse(Qty, out int value);

            if (value == 0)
                return $"Sell (all): {ItemName}";
            else if (value < 0)
                return $"Sell (until): {ItemName} x{Math.Abs(value)}";
            return $"Sell: {ItemName} x{Qty}";
        }
    }
}