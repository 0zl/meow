using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBuyFast : IBotCommand
    {
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
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.BuyItem), timeout: 3);
            using (new pauseProvoke(instance.Configuration))
            {
                await Player.ExitCombat();
                Shop.BuyItemQty(ItemName, Qty);
            }
        }

        public override string ToString()
        {
            return $"Buy item fast [{Qty}x] : {ItemName}";
        }
    }
}