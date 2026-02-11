using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdLoad : IBotCommand
    {
        public int ShopId
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Transaction;
            using (new pauseProvoke(instance.Configuration))
            {
                await instance.WaitUntil(() =>
                World.IsActionAvailable(LockActions.BuyItem) &&
                World.IsActionAvailable(LockActions.LoadShop), timeout: 3);
                    await Player.ExitCombat(); //To ensure out of combat
                Shop.Load(ShopId);
                await instance.WaitUntil(() => Shop.IsShopLoaded,timeout:3);
            }
        }

        public override string ToString()
        {
            return "Load Shop: " + ShopId;
        }
    }
}