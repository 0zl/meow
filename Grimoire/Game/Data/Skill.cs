using System;
using Grimoire.Tools;

namespace Grimoire.Game.Data
{
	public class Skill
	{
		public string Text { 
			get; 
			set; 
		}
		public string Index { get; set; }

		public Skill.SkillType Type { get; set; }

		public Skill.SafeType SType { get; set; }

		public bool IsSafeMp { get; set; }

		public int SafeValue { get; set; }

		public static string GetSkillName(string index)
		{
			return Flash.Call<string>("GetSkillName", new string[]
			{
				index
			});
		}

		public enum SkillType
		{
			Normal,
			Safe,
			Label
		}

		public enum SafeType
		{
			LowerThan,
			GreaterThan,
			Equals
		}

		public void ExecuteSkill()
		{
			Skill s = this;
			if (s.Type == Skill.SkillType.Safe)
			{
				if (s.IsSafeMp)
				{
					switch (s.SType)
					{
						case Skill.SafeType.LowerThan:
							if ((double)Player.Mana / Player.ManaMax * 100 <= s.SafeValue)
								Player.UseSkill(s.Index);
							break;
						case Skill.SafeType.GreaterThan:
							if ((double)Player.Mana / Player.ManaMax * 100 >= s.SafeValue)
								Player.UseSkill(s.Index);
							break;
						case Skill.SafeType.Equals:
							if ((double)Player.Mana / Player.ManaMax * 100 == s.SafeValue)
								Player.UseSkill(s.Index);
							break;
					}
				}
				else
				{
					switch (s.SType)
					{
						case Skill.SafeType.LowerThan:
							if ((double)Player.Health / Player.HealthMax * 100 <= s.SafeValue)
								Player.UseSkill(s.Index);
							break;
						case Skill.SafeType.GreaterThan:
							if ((double)Player.Health / Player.HealthMax * 100 >= s.SafeValue)
								Player.UseSkill(s.Index);
							break;
						case Skill.SafeType.Equals:
							if ((double)Player.Health / Player.HealthMax * 100 == s.SafeValue)
								Player.UseSkill(s.Index);
							break;
					}
				}
			}
			else
			{
				Player.UseSkill(s.Index);
			}
		}

		public override string ToString()
		{
			string text = Text;
			if (text != null)
				if (text.StartsWith("1: ") || text.StartsWith("2: ") || text.StartsWith("3: ") || text.StartsWith("4: "))
				{
					text = text.Remove(0, 3);
				}
			string skillName = text ?? Skill.GetSkillName(Index);
			string safeType = IsSafeMp ? "MP" : "HP";
			string safeTypeS = SType == SafeType.GreaterThan ? ">=" : "<=";

			string skillText = "";

			if (Type == SkillType.Normal)
			{
				skillText = $"{Index}: {skillName}";

			}
			else if (Type == SkillType.Safe)
			{
				skillText = $"[{safeType} {safeTypeS} {SafeValue}%] {Index}: {skillName}";
			}
			else if (Type == SkillType.Label)
			{
				skillText = $"[{Text}]";
			}

			return skillText;
		}
	}
}
