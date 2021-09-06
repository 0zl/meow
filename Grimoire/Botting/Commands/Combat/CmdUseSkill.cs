using System;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.Botting.Commands.Combat
{
	public class CmdUseSkill : IBotCommand
	{
		public Skill Skill { get; set; }

		public bool Wait { get; set; }

		public bool Force {get; set;}

		public bool Targeted { get; set; }

		public async Task Execute(IBotEngine instance)
		{
			if (instance.Configuration.SkipAttack && !Force)
			{
				if (Player.HasTarget) Player.CancelTarget(); 
				return;
			}
			if (Targeted)
            {
				if (!Player.HasTarget) return;
            }
			bool waitSkillCD = instance.Configuration.WaitForSkill;
			if (Wait) 
				await Task.Delay(Player.SkillAvailable(Skill.Index));
			Player.UseSkill(Skill.Index);
		}

		public override string ToString()
		{
			return "Skill " + (Wait? "[Wait] " : " ") + (Targeted ? "[Targeted] " : " ") + Skill.Index + ": " + Skill.GetSkillName(Skill.Index);
		}
	}
}
