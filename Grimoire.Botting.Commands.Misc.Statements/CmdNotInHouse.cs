using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdNotInHouse : StatementCommand, IBotCommand
    {
        public CmdNotInHouse()
        {
            Tag = "Item";
            Text = "Is not in house";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.House.ContainsItem(Value1, Value2))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Item is not in house: " + Value1 + ", " + Value2;
        }
    }
}