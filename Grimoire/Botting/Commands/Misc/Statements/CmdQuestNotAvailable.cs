using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdQuestNotAvailable : StatementCommand, IBotCommand
    {
        public CmdQuestNotAvailable()
        {
            Tag = "Quest";
            Text = "Quest is not available";
        }

        public Task Execute(IBotEngine instance)
        {
            int id = int.Parse(instance.ResolveVars(Value1));
            if (Player.Quests.IsAvailable(id))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Quest is not available: " + Value1;
        }
    }
}