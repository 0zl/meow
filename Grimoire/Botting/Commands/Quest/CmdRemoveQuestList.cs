using Grimoire.UI;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting.Commands.Quest
{
	public class CmdRemoveQuestList : IBotCommand
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
			if (instance.Configuration.Quests.FirstOrDefault(x => x.Id == quest.Id) != null)
			{
				BotManager.Instance.Invoke((MethodInvoker)delegate {
					BotManager.Instance.RemoveQuest(Id, ItemId, SafeRelogin);
				});
				var i = instance.Configuration.Quests.FirstOrDefault((Game.Data.Quest q) =>
					q.Id == Id && q.ItemId == ItemId && q.SafeRelogin == SafeRelogin);
				instance.Configuration.Quests.Remove(i);
				if (instance.IsRunning)
				{
					instance.StartQuestList();
				}
			}
		}

		public override string ToString()
		{
			string safe = SafeRelogin ? " [SafeRelogin]" : "";
			string itemId = ItemId != null ? $" {ItemId}" : "";
			return $"Remove Quest list : {Id}{itemId}{safe}";
		}
	}
}

