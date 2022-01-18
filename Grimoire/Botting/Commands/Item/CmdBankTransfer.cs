using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdBankTransfer : IBotCommand
    {
        public bool TransferFromBank
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            string ItemName = (instance.IsVar(this.ItemName) ? Configuration.Tempvariable[instance.GetVar(this.ItemName)] : this.ItemName);
            if (TransferFromBank)
            {
                if (Player.Bank.GetItemByName(ItemName) != null)
                {
                    Player.Bank.TransferFromBank(ItemName);
                    //await instance.WaitUntil(() => Player.Inventory.ContainsItem(ItemName, "*"));
                    await Task.Delay(500);
                }
            }
            else if (Player.Inventory.GetItemByName(ItemName) != null)
            {
                Player.Bank.TransferToBank(ItemName);
                //await instance.WaitUntil(() => !Player.Inventory.ContainsItem(ItemName, "*"));
                await Task.Delay(500);
            }
        }

        public override string ToString()
        {
            return (TransferFromBank ? "Bank -> Inv: " : "Inv -> Bank: ") + ItemName;
        }
    }
}