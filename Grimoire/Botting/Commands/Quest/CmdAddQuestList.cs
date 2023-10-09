using Grimoire.Game;
using System.Threading.Tasks;
using Grimoire.Botting.Commands.Quest;
using System.Linq;
using System.Windows.Forms;
using Grimoire.UI;
using System;

namespace Grimoire.Botting.Commands.Quest
{
	public class CmdAddQuestList : IBotCommand
	{
		public int Id
		{
			get;
			set;
		}
		public string ItemId
		{
			get;
			set;
		}
		public bool SafeRelogin
		{
			get;
			set;
		}

		public async Task Execute(IBotEngine instance)
		{
			Game.Data.Quest quest = new Game.Data.Quest
			{
				Id = Id,
				ItemId = ItemId,
				SafeRelogin = SafeRelogin,
			};
			Console.WriteLine($"total: {instance.Configuration.Quests.Count}");
			if (instance.Configuration.Quests.FirstOrDefault(x => x.Id == quest.Id) == null)
			{
				BotManager.Instance.Invoke((MethodInvoker) delegate {
					BotManager.Instance.AddQuest(Id, ItemId, SafeRelogin);
				});
				instance.Configuration.Quests.Add(quest);
				Console.WriteLine($"Adding: {Id}");
				if (instance.IsRunning)
				{
					instance.StopQuestList();
					instance.StartQuestList();
				}
			}
		}

		public override string ToString()
		{
			string safe = SafeRelogin ? " [SafeRelogin]" : "";
			string itemId = ItemId != null ? $" {ItemId}" : "";
			return $"Add Quest list : {Id}{itemId}{safe}";
		}
	}
}
