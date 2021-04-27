using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdManaLessThan : StatementCommand, IBotCommand
    {
        public CmdManaLessThan()
        {
            Tag = "This player";
            Text = "Mana is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.Mana >= int.Parse((IsVar(Value1)  ? Configuration.Tempvariable[GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Mana is less than: " + Value1;
        }
    }
}