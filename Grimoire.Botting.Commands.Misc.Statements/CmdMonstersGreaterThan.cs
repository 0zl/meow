using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMonstersGreaterThan : StatementCommand, IBotCommand
    {
        public CmdMonstersGreaterThan()
        {
            Tag = "Monster";
            Text = "Count is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (World.VisibleMonsters.Count <= int.Parse((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Monster count is greater than: " + Value1;
        }
    }
}