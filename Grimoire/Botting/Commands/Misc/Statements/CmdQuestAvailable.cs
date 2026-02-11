using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdQuestAvailable : StatementCommand, IBotCommand
    {
        public CmdQuestAvailable()
        {
            Tag = "Quest";
            Text = "Quest is available";
        }

        public Task Execute(IBotEngine instance)
        {
            int id = int.Parse(instance.ResolveVars(Value1));
            //int req = Player.Quests.Quest(id).IValue;
            if (!Player.Quests.IsAvailable(id))
            {
                instance.Index++;
            }
            //else if(Player.Quests.progress(id) >= req && Player.Quests.Quest(id).IsNotRepeatable)
            //{
            //    instance.Index++;
            //}
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Quest is available:{Value1}" ;
        }
    }
}