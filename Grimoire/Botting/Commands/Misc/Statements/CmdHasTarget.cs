using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdHasTarget : StatementCommand, IBotCommand
    {
        public CmdHasTarget()
        {
            Tag = "This player";
            Text = "Is has target";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.HasTarget)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Is has target";
        }
    }
}