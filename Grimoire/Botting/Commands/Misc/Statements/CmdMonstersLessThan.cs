using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMonstersLessThan : StatementCommand, IBotCommand
    {
        public CmdMonstersLessThan()
        {
            Tag = "Monster";
            Text = "Count is less than";
        }

        public Task Execute(IBotEngine instance)
        {
			if (World.VisibleMonsters.Count >= int.Parse((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Monster count is less than: " + Value1;
        }
    }
}