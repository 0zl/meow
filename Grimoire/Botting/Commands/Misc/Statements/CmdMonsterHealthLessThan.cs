using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMonsterHealthLessThan : StatementCommand, IBotCommand
    {
        public CmdMonsterHealthLessThan()
        {
            Tag = "Monster";
            Text = "Monster HP less than";
        }

        public Task Execute(IBotEngine instance)
        {
            string Monster = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            string Health = instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2;

            int.TryParse(Health, out int i);
            if (World.GetMonsterHealth(Monster) > i)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            string text = Value1 ?? "Monster";
            return $"{text}'s HP less than: {Value2}";
        }
    }
}