using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdNotInInventory : StatementCommand, IBotCommand
    {
        public CmdNotInInventory()
        {
            Tag = "Item";
            Text = "Is not in inventory";
        }

        public Task Execute(IBotEngine instance)
        {
            string ItemName = (instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1);
            string Quantity = (instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2);
            InventoryItem item = Player.Inventory.GetItemByName(ItemName);
            if (item != null)
            {
                if (Int32.TryParse(Quantity, out int qty))
                {
                    if (item.Quantity > qty) instance.Index++;
                }
                else
				{
                    instance.Index++;
                }
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item is not in inventory: " + Value1 + ", " + Value2;
        }
    }
}