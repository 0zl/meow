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

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            string ItemName = (instance.IsVar(this.ItemName) ? Configuration.Tempvariable[instance.GetVar(this.ItemName)] : this.ItemName);
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.BuyItem));
            Shop.BuyItem(ItemName);
        }

        public override string ToString()
        {
            return "Buy item fast: " + ItemName;
        }
    }
}