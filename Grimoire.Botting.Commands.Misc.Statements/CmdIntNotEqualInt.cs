using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdIntNotEqualInt : StatementCommand, IBotCommand
    {
        public CmdIntNotEqualInt()
        {
            Tag = "Misc";
            Text = "Int is not equal to Int";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Configuration.Tempvalues[(IsVar(Value1)  ? Configuration.Tempvariable[GetVar(Value1)] : Value1)] != Configuration.Tempvalues[(IsVar(Value2)  ? Configuration.Tempvariable[GetVar(Value2)] : Value2)])
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"{Value1} != {Value2}";
        }
    }
}
