using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.UI;

namespace Grimoire.Botting.Commands.Combat
{
	public class CmdKill : IBotCommand
	{
		public string Monster { get; set; }
		public string KillPriority { get; set; } = "";

		private bool antiCounter = false;

		private bool onPause = false;

		public async Task Execute(IBotEngine instance)
		{
			string Monster = instance.IsVar(this.Monster) ? Configuration.Tempvariable[instance.GetVar(this.Monster)] : this.Monster;

			antiCounter = instance.Configuration.AntiCounter;

			await instance.WaitUntil(() => World.IsMonsterAvailable(Monster), null, 3);

			if (instance.Configuration.WaitForAllSkills)
			{
				await Task.Delay(Player.AllSkillsAvailable);
			}

			if (!instance.IsRunning || !Player.IsAlive || !Player.IsLoggedIn)
				return;

			if (antiCounter) Proxy.Instance.ReceivedFromServer += CapturePlayerData;

			Player.AttackMonster(Monster);

			if (instance.Configuration.Skills.Count > 0)
				Task.Run(() => UseSkillsSet(instance));

			await instance.WaitUntil(() => !Player.HasTarget && !onPause, null, 360);
			Player.CancelTarget();

			if (antiCounter) Proxy.Instance.ReceivedFromServer -= CapturePlayerData;

			_cts?.Cancel(false);
		}

		private CancellationTokenSource _cts;

		private int _skillIndex;

		private int Index;
		private async Task UseSkillsSet(IBotEngine instance)
		{
			this._cts = new CancellationTokenSource();
			int ClassIndex = -1;
			bool flag = BotData.SkillSet != null && BotData.SkillSet.ContainsKey("[" + BotData.BotSkill + "]");
			if (flag)
			{
				ClassIndex = BotData.SkillSet["[" + BotData.BotSkill + "]"] + 1;
			}
			int Count = instance.Configuration.Skills.Count - 1;
			this.Index = ClassIndex;
			while (!this._cts.IsCancellationRequested && !onPause)
			{
				bool flag2 = !Player.IsLoggedIn || !Player.IsAlive;
				if (flag2)
				{
					while (Player.HasTarget)
					{
						Player.CancelTarget();
						await Task.Delay(500);
					}
					return;
				}

				switch (this.Monster.ToLower())
				{
					case "escherion":
						if (World.IsMonsterAvailable("Staff of Inversion"))
							Player.AttackMonster("Staff of Inversion");
						break;
					case "vath":
						if (World.IsMonsterAvailable("Stalagbite"))
							Player.AttackMonster("Stalagbite");
						break;
					case "ultra avatar tyndarius":
						if (World.IsMonsterAvailable("Ultra Fire Orb"))
							Player.AttackMonster("Ultra Fire Orb");
						break;
				}

				if (KillPriority.Length > 0) {
					List<string> priorities =  new List<string>();
					if (KillPriority.Contains(","))
					{
						foreach (string p in KillPriority.Split(','))
						{
							priorities.Add(p);
						}
					}
					else {
						priorities.Add(KillPriority);
					}

					foreach (string p in priorities) {
						if (World.IsMonsterAvailable(p)) {
							Player.AttackMonster(p);
							break;
						}
					}
				}

				if (ClassIndex != -1)
				{
					//with label
					Skill s = instance.Configuration.Skills[this.Index];
					if (s.Type == Skill.SkillType.Label)
					{
						this.Index = ClassIndex;
						continue;
					}

					if (instance.Configuration.WaitForSkill)
					{
						BotManager.Instance.OnSkillIndexChanged(Index);
						await Task.Delay(Player.SkillAvailable(s.Index));
					}

					s.ExecuteSkill();

					int index;
					if (this.Index < Count)
					{
						int num3 = this.Index + 1;
						this.Index = num3;
						index = num3;
					}
					else
					{
						index = ClassIndex;
					}
					this.Index = index;
					s = null;
				}
				else
				{
					//non label
					Skill s = instance.Configuration.Skills[_skillIndex];

					if (instance.Configuration.WaitForSkill)
					{
						BotManager.Instance.OnSkillIndexChanged(Index);
						await Task.Delay(Player.SkillAvailable(s.Index));
					}

					s.ExecuteSkill();

					int count = instance.Configuration.Skills.Count - 1;

					_skillIndex = _skillIndex >= count ? 0 : ++_skillIndex;
					await Task.Delay(instance.Configuration.SkillDelay);
				}
				await Task.Delay(instance.Configuration.SkillDelay);
			}

			while (Player.HasTarget)
			{
				Player.CancelTarget();
				await Task.Delay(500);
			}
		}

		private void CapturePlayerData(Message message)
		{
			string msg = message.ToString();

			try
			{
				//"cmd":"aura++","auras":[{"nam":"Counter Attack"
				//prepares a counter attack!!
				string c1 = "\"cmd\":\"aura++\",\"auras\":[{\"nam\":\"Counter Attack\"";
				string c2 = "prepares a counter attack";
				if (msg.Contains(c2))
				{
					Console.WriteLine("Counter Attack: active");
					Player.CancelTarget();
					onPause = true;
				}

				//"cmd":"aura--","aura":{"nam":"Counter Attack"
				if (msg.Contains("\"cmd\":\"aura--\",\"aura\":{\"nam\":\"Counter Attack\""))
				{
					Console.WriteLine("Counter Attack: fades");
					onPause = false;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("MyMsg: " + msg);
				Console.WriteLine("MyError: " + e.Message);
			}
		}

		public override string ToString()
		{
			return $"Kill {Monster}";
		}
	}
}
