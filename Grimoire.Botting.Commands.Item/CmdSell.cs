using Grimoire.Game;
using Grimoire.Game.Data;
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

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            string ItemName = (instance.IsVar(this.ItemName) ? Configuration.Tempvariable[instance.GetVar(this.ItemName)] : this.ItemName);
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.SellItem));
            InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(ItemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                Shop.SellItem(ItemName);
                await instance.WaitUntil(() => !Player.Inventory.ContainsItem(item.Name, item.Quantity.ToString()));
            }
        }

        public override string ToString()
        {
            return "Sell: " + ItemName;
        }
    }
}