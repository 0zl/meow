using Grimoire.Game;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Grimoire.Tools;
using Newtonsoft.Json.Linq;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerInMyCellLessThan : StatementCommand, IBotCommand
    {
        public CmdPlayerInMyCellLessThan()
        {
            Tag = "Player";
            Text = "Player count is less than [cell]";
        }

        public Task Execute(IBotEngine instance)
        {
            int count = 0;
            List<string> playerInMap = World.PlayersInMap;
            foreach (string pl in playerInMap)
            {
                string reqs = Flash.Call<string>("CheckCellPlayer", new string[] {
                    pl,
                    Player.Cell
                });
                if (bool.TryParse(reqs, out bool result) && result)
                {
                    count++;
                }
            }
            int inputCount = int.Parse(Bot.Instance.ResolveVars(Value1));
            if (count >= inputCount)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player count is less than [cell]: " + Value1;
        }
    }
}

