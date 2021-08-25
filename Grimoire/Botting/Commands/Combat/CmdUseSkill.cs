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

		public async Task Execute(IBotEngine instance)
		{
			bool waitSkillCD = instance.Configuration.WaitForSkill;
			if (Wait) 
				await Task.Delay(Player.SkillAvailable(Skill.Index));
			Player.UseSkill(Skill.Index);
		}

		public override string ToString()
		{
			return "Skill " + (Wait? "[Wait] " : " ") + Skill.Index + ": " + Skill.GetSkillName(Skill.Index);
		}
	}
}
