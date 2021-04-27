using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMapIsNot : StatementCommand, IBotCommand
    {
        public CmdMapIsNot()
        {
            Tag = "Map";
            Text = "Map is not";
        }

        public Task Execute(IBotEngine instance)
        {
            if (((IsVar(Value1)  ? Configuration.Tempvariable[GetVar(Value1)] : Value1).Contains("-") ? (IsVar(Value1)  ? Configuration.Tempvariable[GetVar(Value1)] : Value1).Split('-')[0] : (IsVar(Value1)  ? Configuration.Tempvariable[GetVar(Value1)] : Value1)).Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Map is not: " + Value1;
        }
    }
}