using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayersGreaterThan : StatementCommand, IBotCommand
    {
        public CmdPlayersGreaterThan()
        {
            Tag = "Player";
            Text = "Count is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (World.PlayersInMap.Count <= int.Parse((IsVar(Value1)  ? Configuration.Tempvariable[GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player count is greater than: " + Value1;
        }
    }
}