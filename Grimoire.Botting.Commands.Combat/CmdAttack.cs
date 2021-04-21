using System;
using System.Threading.Tasks;
using Grimoire.Botting;
using Grimoire.Game;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdAttack : IBotCommand
    {
        public string Monster
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            Player.AttackMonster(Monster);
        }
        
        public override string ToString()
        {
            return "Attack: " + this.Monster;
        }
    }
}