using Grimoire.Game;
using Grimoire.Tools;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdTargetHealthGreaterThan : StatementCommand, IBotCommand
    {
        public CmdTargetHealthGreaterThan()
        {
            Tag = "This player";
            Text = "Target health greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            string CheckHealth = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            string Default = instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2;

            if (!Player.HasTarget)
            {
                if (Default.ToLower() == "false")
                {
                    instance.Index++;
                }
                return Task.FromResult<object>(null);
            }

            int.TryParse(CheckHealth, out int i);
            if (Player.GetTargetHealth() < i)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Target health greater than: {Value1}";
        }
    }
}
