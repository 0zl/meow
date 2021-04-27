using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdInBank : StatementCommand, IBotCommand
    {
        public CmdInBank()
        {
            Tag = "Item";
            Text = "Is in bank";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.Bank.ContainsItem((IsVar(Value1)  ? Configuration.Tempvariable[GetVar(Value1)] : Value1), (IsVar(Value2)  ? Configuration.Tempvariable[GetVar(Value2)] : Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item is in bank: " + Value1 + ", " + Value2;
        }
    }
}