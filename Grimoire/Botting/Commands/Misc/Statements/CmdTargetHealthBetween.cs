using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdTargetHealthBetween : StatementCommand, IBotCommand
    {
        public CmdTargetHealthBetween()
        {
            Tag = "This player";
            Text = "Target health between";
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

            if (CheckHealth.Contains(","))
			{
                string[] HPs = CheckHealth.Split(',');
                int.TryParse(HPs[0], out int s);
                int.TryParse(HPs[1], out int e);
                if (Player.GetTargetHealth() > s && Player.GetTargetHealth() < e)
                {
                    return Task.FromResult<object>(null);
                }
            }

            instance.Index++;
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Target health between: {Value1}";
        }
    }
}
