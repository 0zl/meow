using Grimoire.Botting.Commands.Item;
using Grimoire.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
	public class CmdKillFor : IBotCommand
	{
		public string Monster
		{
			get;
			set;
		}

		public string ItemName
		{
			get;
			set;
		}

		public ItemType ItemType
		{
			get;
			set;
		}

		public string Quantity
		{
			get;
			set;
		}
		public string KillPriority { get; set; } = "";
		public bool AntiCounter { get; set; } = false;
		public string QuestId { get; set; }
		public int DelayAfterKill { get; set; } = 500;

		private Configuration config;
		public async Task Execute(IBotEngine instance)
		{
			string Monster = (instance.IsVar(this.Monster) ? Configuration.Tempvariable[instance.GetVar(this.Monster)] : this.Monster);
			string ItemName = ((instance.IsVar(this.ItemName) ? Configuration.Tempvariable[instance.GetVar(this.ItemName)] : this.ItemName)).Trim();

			BotData.BotState = BotData.State.Combat;
			CmdKill kill = new CmdKill {
				Monster = Monster,
				KillPriority = KillPriority,
				AntiCounter = AntiCounter
			};

			int id;
			if (int.TryParse(QuestId, out id))
			{
				while (!Player.Quests.IsInProgress(id))
				{
					Player.Quests.Accept(id);
					await Task.Delay(1000);
				}
				while (instance.IsRunning && Player.IsLoggedIn && Player.IsAlive && !Player.Quests.CanComplete(id))
				{
					await kill.Execute(instance);
					await Task.Delay(DelayAfterKill);
				}
			}
			else
			{
				List<string> removedList = new List<string>();
				config = instance.Configuration;

				string[] itemsName = ItemName.Split(new char[] { ',' });

				string[] quantities = Quantity.Split(new char[] { ',' });

				if (ItemType == ItemType.Items)
				{
					while (instance.IsRunning && 
						Player.IsLoggedIn && 
						Player.IsAlive &&
						!Enumerable.Range(0, itemsName.Length).All(i => Player.Inventory.ContainsItem(itemsName[i], quantities[i]))
						)
					{
						await kill.Execute(instance);
						await Task.Delay(DelayAfterKill);
					}
				}
				else
				{
					while (instance.IsRunning && 
						Player.IsLoggedIn && 
						Player.IsAlive &&
						!Player.TempInventory.ContainsItem(ItemName, Quantity))
					{
						await kill.Execute(instance);
						await Task.Delay(DelayAfterKill);
					}
				}

				Player.CancelTarget();
				await Task.Delay(500);
			}
		}

		public override string ToString()
		{
			string text;
			if (int.TryParse(QuestId, out _))
			{
				text = $"KFQuest: [{QuestId}] [{Monster}]";
			}
			else if (ItemType == ItemType.Items)
			{
				text = $"KFItems: [{ItemName} {Quantity}x] [{Monster}]";
			}
			else
			{
				text = $"KFTemps: [{ItemName} {Quantity}x] [{Monster}]";
			}

			return text;
		}
	}
}