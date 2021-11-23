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

		public bool Targeted { get; set; }

		public string Target { get; set; } = "*";

		public async Task Execute(IBotEngine instance)
		{
			if (instance.Configuration.SkipAttack)
			{
				if (Player.HasTarget) Player.CancelTarget(); 
				return;
			}
			if (Target != null)
			{
				Player.AttackMonster(Target);
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
			return "Skill " + $"[{Target}] " + (Wait? "[Wait] " : " ") + (!Targeted ? "[Force] " : " ") + Skill.Index + ": " + Skill.GetSkillName(Skill.Index);
		}
	}
}
