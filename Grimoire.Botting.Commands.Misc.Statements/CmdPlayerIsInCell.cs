using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerIsInCell : StatementCommand, IBotCommand
    {
        public CmdPlayerIsInCell()
        {
            Tag = "Player";
            Text = "Player is in cell";
        }

        public Task Execute(IBotEngine instance)
        {
            string reqs = Flash.Call<string>("CheckCellPlayer", new string[] {
                Value1,
                Value2
            });
            bool isExists = bool.Parse(reqs);

            if (!isExists)
            {
                instance.Index++;
            }

            Console.WriteLine(isExists);
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player is in cell: " + Value1;
        }
    }
}