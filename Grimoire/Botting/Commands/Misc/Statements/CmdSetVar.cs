using Grimoire.Game;
using Grimoire.Game.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdSetVar : StatementCommand, IBotCommand
    {
        public CmdSetVar()
        {
            Tag = "Misc";
            Text = "Set Temporary Variable";
        }

        public Task Execute(IBotEngine instance)
        {
            switch (Value1)
            {
                case "{weapon}":
                    Value2 = Player.Inventory.Items.FirstOrDefault(i => InventoryItem.Weapons.Contains(i.Category) && i.IsEquipped)?.Name;
                    break;
                case "{class}":
                    Value2 = Player.Inventory.Items.FirstOrDefault(i => i.Category == "Class" && i.IsEquipped)?.Name;
                    break;
                case "{helm}":
                    Value2 = Player.Inventory.Items.FirstOrDefault(i => i.Category == "Helm" && i.IsEquipped)?.Name;
                    break;
                case "{cape}":
                    Value2 = Player.Inventory.Items.FirstOrDefault(i => i.Category == "Cape" && i.IsEquipped)?.Name;
                    break;
                default:
                    break;
            }
            string RealString = Value2;
            if (RealString.StartsWith("["))
            {
                RealVar = instance.ResolveVars(Value2);
                RealString = RealVar;
            }
            if (!Configuration.Tempvariable.ContainsKey(Value1))
                Configuration.Tempvariable.Add(Value1, RealString);
            else
                Configuration.Tempvariable[Value1] = RealString;

            return Task.FromResult<object>(null);
        }
        private string RealVar;
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(RealVar))
            {
                return $"Variable {Value1}:{Value2} => {RealVar}";
            }
            return $"Variable {Value1}: {Value2}";
        }
    }
}