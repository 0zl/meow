using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Tools;
using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting
{
	public class Bot : IBotEngine
	{
		public bool IsVar(string value)
		{
			return Regex.IsMatch(value, @"\[([^\)]*)\]");
		}

		public string GetVar(string value)
		{
			return Regex.Replace(value, @"[\[\]']+", "");
		}

		public static Bot Instance = new Bot();

		private int _index;

		private Configuration _config;

		private bool _isRunning;

		private bool _isRunningQuestList;

		private bool _onCompletingQuest;

		private CancellationTokenSource _ctsBot;

		private Stopwatch _questDelayCounter;

		private Stopwatch _boostDelayCounter;

		private List<string> _bsLabels = new List<string>();

		private string lastCommand = null;

		public int Index
		{
			get
			{
				return _index;
			}
			set
			{
				_index = (value < Configuration.Commands.Count) ? value : 0;
			}
		}

		public Configuration Configuration
		{
			get
			{
				return _config;
			}
			set
			{
				if (value != _config)
				{
					_config = value;
					this.ConfigurationChanged?.Invoke(_config);
				}
			}
		}

		public static Dictionary<int, Configuration> Configurations = new Dictionary<int, Configuration>();

		public static Dictionary<int, int> OldIndex = new Dictionary<int, int>();

		public int CurrentConfiguration { get; set; } = 0;

		public bool IsRunning
		{
			get
			{
				return _isRunning;
			}
			set
			{
				_isRunning = value;
				this.IsRunningChanged?.Invoke(_isRunning);
			}
		}

		public event Action<bool> IsRunningChanged;

		public event Action<int> IndexChanged;

		public event Action<Configuration> ConfigurationChanged;

		public void Start(Configuration config)
		{
			IsRunning = true;
			Configuration = config;
			Index = 0;
			BotData.BotState = BotData.State.Others;
			_ctsBot = new CancellationTokenSource();
			_questDelayCounter = new Stopwatch();
			_boostDelayCounter = new Stopwatch();
			World.ItemDropped += OnItemDropped;
			Player.Quests.QuestsLoaded += OnQuestsLoaded;
			Player.Quests.QuestCompleted += OnQuestCompleted;
			_questDelayCounter.Start();
			this.LoadAllQuests();
			this.LoadBankItems();
			CheckBoosts();
			_boostDelayCounter.Start();
			OptionsManager.Start();
			Task.Factory.StartNew(Activate, _ctsBot.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
			BotData.BotMap = null;
			BotData.BotCell = null;
			BotData.BotPad = null;
			BotData.BotSkill = null;
			BotData.BotState = BotData.State.Others;
			BotData.SkillSet.Clear();
			for (int i = 0; i < Configuration.Skills.Count; i++)
			{
				if (Configuration.Skills[i].Type == Skill.SkillType.Label)
				{
					BotData.SkillSet.Add(Configuration.Skills[i].Text.ToUpper(), i);
				}
			}
		}

		public void Stop()
		{
			_ctsBot?.Cancel(throwOnFirstException: false);
			World.ItemDropped -= OnItemDropped;
			Player.Quests.QuestsLoaded -= OnQuestsLoaded;
			Player.Quests.QuestCompleted -= OnQuestCompleted;
			_questDelayCounter.Stop();
			_boostDelayCounter.Stop();
			OptionsManager.Stop();
			StopBackGroundSpammer();
			IsRunning = false;
			BotData.BotState = BotData.State.Others;
			this.StopCommands();
		}

		private async Task Activate()
		{
			if (Configuration.Quests.Count > 0)
				StartQuestList();

			while (!_ctsBot.IsCancellationRequested)
			{
				if (Player.IsLoggedIn && !Player.IsAlive)
				{
					World.SetSpawnPoint();
					await this.WaitUntil(() => Player.IsAlive, () => IsRunning && Player.IsLoggedIn, -1);
					await Task.Delay(1000);
					Index = Configuration.RestartUponDeath ? 0 : Index - 1;
				}

				if (!Player.IsLoggedIn)
				{
					LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Disconnected. Last cmd: [{Index}]{lastCommand}");
					StopQuestList();
					StopBackGroundSpammer();

					if (Configuration.AutoRelogin)
					{
						bool infiniteRange = OptionsManager.InfiniteRange;
						bool provoke = OptionsManager.ProvokeMonsters;
						bool lagKiller = OptionsManager.LagKiller;
						bool skipCutscene = OptionsManager.SkipCutscenes;
						bool playerAnim = OptionsManager.DisableAnimations;
						bool enemyMagnet = OptionsManager.EnemyMagnet;
						bool reloginOnAFK = OptionsManager.AFK;

						OptionsManager.Stop();
						await AutoRelogin.Login(Configuration.Server, Configuration.RelogDelay, _ctsBot, Configuration.RelogRetryUponFailure);
						Index = 0;
						this.LoadAllQuests();
						this.LoadBankItems();
						OptionsManager.Start();
						if (Configuration.Quests.Count > 0)
							StartQuestList();
						LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Relogin success.");

						OptionsManager.InfiniteRange = infiniteRange;
						OptionsManager.ProvokeMonsters = provoke;
						OptionsManager.LagKiller = lagKiller;
						OptionsManager.SkipCutscenes = skipCutscene;
						OptionsManager.DisableAnimations = playerAnim;
						OptionsManager.EnemyMagnet = enemyMagnet;
						OptionsManager.AFK = reloginOnAFK;
					}
					else
					{
						Stop();
						return;
					}
				}

				if (_ctsBot.IsCancellationRequested)
					return;

				if (Configuration.RestIfHp)
					await RestHealth();

				if (_ctsBot.IsCancellationRequested)
					return;

				if (Configuration.RestIfMp)
					await RestMana();

				if (_ctsBot.IsCancellationRequested)
					return;

				IndexChanged?.Invoke(Index);
				IBotCommand cmd = Configuration.Commands[Index];
				if (cmd is CmdBackgroundPacket)
				{
					ToggleSpammer(cmd);
				}
				else
				{
					lastCommand = cmd.ToString();
					await cmd.Execute(this);
				}

				if (_ctsBot.IsCancellationRequested)
					return;

				if (Configuration.BotDelay > 0 &&
					(!Configuration.SkipDelayIndexIf || Configuration.SkipDelayIndexIf && cmd.RequiresDelay()))
					await Task.Delay(_config.BotDelay);

				if (_ctsBot.IsCancellationRequested)
					return;

				if (Configuration.Boosts.Count > 0)
					CheckBoosts();

				if (!_ctsBot.IsCancellationRequested)
					Index++;
			}
		}

		private Dictionary<int, int> qFailures = new Dictionary<int, int>();

		public async void StartQuestList()
		{
			_isRunningQuestList = true;
			int questDelay = (int)BotManager.Instance.numQuestDelay.Value;
			if (Configuration.Quests.Count > 0)
			{
				qFailures.Clear();
				foreach (var quest in Configuration.Quests)
				{
					qFailures.Add(quest.Id, 0);
				}
				while (!_ctsBot.IsCancellationRequested && _isRunningQuestList && Player.IsLoggedIn)
				{
					Quest quest = Configuration.Quests.FirstOrDefault((Quest q) => q.CanComplete);
					if (quest != null)
					{
						BotData.State TempState = BotData.BotState;
						BotData.BotState = BotData.State.Quest;
						_onCompletingQuest = true;

						quest.Complete();
						await Task.Delay(questDelay);

						if (quest.CanComplete)
						{
							int f = qFailures[quest.Id];
							if (f >= 5)
							{
								Player.Logout();
								LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Failed to complete quest [{quest.Id}] {f} times.");
							}
							else
							{
								qFailures[quest.Id] = f++;
								Console.WriteLine($"qFailures[{quest.Id}] : {f++}");
							}
						}
						else
						{
							qFailures[quest.Id] = 0;
						}

						BotData.BotState = TempState;
						_onCompletingQuest = false;
					}
					else
					{
						await Task.Delay(questDelay);
					}
				}
			}
		}

		public void StopQuestList()
		{
			_isRunningQuestList = false;
		}

		public void StopBackGroundSpammer()
		{
			_bsLabels.Clear();
		}

		private async void ToggleSpammer(IBotCommand cmd)
		{
			CmdBackgroundPacket bSpammer = (CmdBackgroundPacket)cmd;
			switch (bSpammer.ActionW)
			{
				case CmdBackgroundPacket.Action.ADD:
					_bsLabels.Add(bSpammer.Label);
					break;

				case CmdBackgroundPacket.Action.STOP:
					_bsLabels.RemoveAll(label => label == bSpammer.Label);
					break;
			}

			while (_bsLabels.Contains(bSpammer.Label) && bSpammer.Packet != null)
			{
				if (!_onCompletingQuest)
				{
					await Proxy.Instance.SendToServer(bSpammer.Packet);
					await Task.Delay(bSpammer.Delay);
				}
			}
		}

		private async Task RestHealth()
		{
			if (Player.Health / (double)Player.HealthMax <= Configuration.RestHp / 100.0)
			{
				BotData.State TempState = BotData.BotState;
				BotData.BotState = BotData.State.Rest;
				bool provokeMons = this.Configuration.ProvokeMonsters;
				if (provokeMons) this.Configuration.ProvokeMonsters = false;
				if (Configuration.ExitCombatBeforeRest)
				{
					Player.MoveToCell(Player.Cell, Player.Pad);
					await Task.Delay(2000);
				}
				Player.Rest();
				await this.WaitUntil(() => Player.Health >= Player.HealthMax);
				BotData.BotState = TempState;
				if (provokeMons) this.Configuration.ProvokeMonsters = true;
			}
		}

		private async Task RestMana()
		{
			if (Player.Mana / (double)Player.ManaMax <= Configuration.RestMp / 100.0)
			{
				BotData.State TempState = BotData.BotState;
				BotData.BotState = BotData.State.Rest;
				bool provokeMons = this.Configuration.ProvokeMonsters;
				if (provokeMons) this.Configuration.ProvokeMonsters = false;
				if (Configuration.ExitCombatBeforeRest)
				{
					Player.MoveToCell(Player.Cell, Player.Pad);
					await Task.Delay(2000);
				}
				Player.Rest();
				await this.WaitUntil(() => Player.Mana >= Player.ManaMax);
				BotData.BotState = TempState;
				if (provokeMons) this.Configuration.ProvokeMonsters = true;
			}
		}

		private void CheckBoosts()
		{
			if (_boostDelayCounter.ElapsedMilliseconds >= 10000)
			{
				foreach (InventoryItem boost in Configuration.Boosts)
				{
					if (!Player.HasActiveBoost(boost.Name))
					{
						Player.UseBoost(boost.Id);
					}
				}
				_boostDelayCounter.Restart();
			}
		}

		private void OnItemDropped(InventoryItem drop)
		{
			NotifyDrop(drop);
			bool flag = Configuration.Drops.Any((string d) => d.Equals(drop.Name, StringComparison.OrdinalIgnoreCase));
			if (Configuration.EnablePickupAll)
			{
				World.DropStack.GetDrop(drop.Id);
			}
			else if (Configuration.EnablePickup && flag)
			{
				World.DropStack.GetDrop(drop.Id);
			}

			if (Configuration.EnablePickupAcTagged)
			{
				if (drop.IsAcItem)
				{
					World.DropStack.GetDrop(drop.Id);
				}
			}
		}

		private void NotifyDrop(InventoryItem drop)
		{
			if (Configuration.NotifyUponDrop.Count > 0 && Configuration.NotifyUponDrop.Any((string d) => d.Equals(drop.Name, StringComparison.OrdinalIgnoreCase)))
			{
				for (int i = 0; i < 10; i++)
				{
					Console.Beep();
				}
			}
		}

		private void OnQuestsLoaded(List<Quest> quests)
		{
			List<Quest> qs = quests.Where((Quest q) => Configuration.Quests.Any((Quest qq) => qq.Id == q.Id)).ToList();
			int count = qs.Count;
			if (qs.Count <= 0)
			{
				return;
			}
			if (count == 1)
			{
				qs[0].Accept();
				return;
			}
			for (int i = 0; i < count; i++)
			{
				int ii = i;
				Task.Run(async delegate
				{
					await Task.Delay(1000 * ii);
					qs[ii].Accept();
				});
			}
		}

		private void OnQuestCompleted(CompletedQuest quest)
		{
			Configuration.Quests.FirstOrDefault((Quest q) => q.Id == quest.Id)?.Accept();
		}
	}
}