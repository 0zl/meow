using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerIsNotInCell : StatementCommand, IBotCommand
    {
        public CmdPlayerIsNotInCell()
        {
            Tag = "Player";
            Text = "Player is not in cell";
        }

        public Task Execute(IBotEngine instance)
        {
            string reqs = Flash.Call<string>("CheckCellPlayer", new string[] {
                Value1,
                Value2
            });
            bool isExists = bool.Parse(reqs);

            if (isExists)
            {
                instance.Index++;
            }

            Console.WriteLine(isExists);
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player is not in cell: " + Value1;
        }
    }
}