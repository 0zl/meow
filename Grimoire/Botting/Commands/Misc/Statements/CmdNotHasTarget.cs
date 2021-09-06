using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdNotHasTarget : StatementCommand, IBotCommand
    {
        public CmdNotHasTarget()
        {
            Tag = "This player";
            Text = "Is not has target";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.HasTarget)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Is not has target";
        }
    }
}