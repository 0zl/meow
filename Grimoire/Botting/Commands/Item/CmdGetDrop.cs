using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdGetDrop : IBotCommand
    {
        public string ItemName
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            string ItemName = (instance.IsVar(this.ItemName) ? Configuration.Tempvariable[instance.GetVar(this.ItemName)] : this.ItemName);
            await World.DropStack.GetDrop(ItemName);
            await Task.Delay(500);
            //await instance.WaitUntil(() => !World.DropStack.Contains(ItemName));
        }

        public override string ToString()
        {
            return "Get drop: " + ItemName;
        }
    }
}