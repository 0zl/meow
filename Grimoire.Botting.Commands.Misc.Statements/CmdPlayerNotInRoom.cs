using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerNotInRoom : StatementCommand, IBotCommand
    {
        public CmdPlayerNotInRoom()
        {
            Tag = "Player";
            Text = "Player is not in room";
        }

        public Task Execute(IBotEngine instance)
        {
            if (World.PlayersInMap.FirstOrDefault((string p) => p.Equals(Value1, StringComparison.OrdinalIgnoreCase)) != null)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player is not in room: " + Value1;
        }
    }
}