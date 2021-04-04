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
            if (!Player.Bank.ContainsItem(Value1, Value2))
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