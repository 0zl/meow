using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerIsInMyCell : StatementCommand, IBotCommand
    {
        public CmdPlayerIsInMyCell()
        {
            Tag = "Player";
            Text = "Player is in my cell";
        }

        public Task Execute(IBotEngine instance)
        {
            string reqs = Flash.Call<string>("GetCellPlayers", new string[] { Value1 });
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
            return "Player is in my cell: " + Value1;
        }
    }
}