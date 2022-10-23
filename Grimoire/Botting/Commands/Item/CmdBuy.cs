using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBuy : IBotCommand
    {
        public int ShopId
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public int Qty
        {
            get;
            set;
        } = 1;

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            string ItemName = (instance.IsVar(this.ItemName) ? Configuration.Tempvariable[instance.GetVar(this.ItemName)] : this.ItemName);
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.BuyItem));
            Shop.ResetShopInfo();
            Shop.Load(ShopId);
            await instance.WaitUntil(() => Shop.IsShopLoaded);
            InventoryItem i = Player.Inventory.Items.FirstOrDefault((InventoryItem item) => item.Name.Equals(ItemName, StringComparison.OrdinalIgnoreCase));
            if (i != null)
            {
                Shop.BuyItemQty(ItemName, Qty);
                await instance.WaitUntil(() => Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Name.Equals(ItemName, StringComparison.OrdinalIgnoreCase)).Quantity != i.Quantity);
            }
            else
            {
                Shop.BuyItemQty(ItemName, Qty);
                await instance.WaitUntil(() => Player.Inventory.Items.FirstOrDefault((InventoryItem it) => it.Name.Equals(ItemName, StringComparison.OrdinalIgnoreCase)) != null);
            }
        }

        public override string ToString()
        {
            return $"Buy item [{Qty}x] : {ItemName}";
        }
    }
}