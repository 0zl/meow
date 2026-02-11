using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdHealthGreaterThan : StatementCommand, IBotCommand
    {
        public CmdHealthGreaterThan()
        {
            Tag = "This player";
            Text = "Health is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            string checkHP = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            int playerHP = Player.Health;

            if (checkHP.Contains("%"))
            {
                int targetPercent = int.Parse(checkHP.TrimEnd('%'));

                int currentPercent = (playerHP * 100) / Player.HealthMax;
                if (currentPercent <= targetPercent)
                {
                    instance.Index++;
                }
            }
            else if (playerHP <= int.Parse(checkHP))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Health is greater than: " + Value1;
        }
    }
}