using Grimoire.Game;
using Grimoire.Networking;
using MaidRemake.WhitelistMap;
using System.Collections.Generic;

namespace Grimoire.Networking.Handlers.Maid
{
	public class JoinMapHandler : IXtMessageHandler
	{
		public string[] HandledCommands { get; } = { "server" };

		private List<string> maps => WhitelistMapForm.Instance.getAlternativeMap;

		public void Handle(XtMessage message)
		{
			if (message.RawContent.Contains("You joined "))
			{
				// on whitelisted map
				if (UI.Maid.MaidRemake.Instance.cbWhitelistMap.Checked)
				{
					string map = Player.Map.ToLower();
					string cell = Player.Cell.ToLower();
					string configMap = maps.Find(i => i.Split(';')[0].ToLower() == map);
					if (configMap != null)
					{
						string configCell = configMap.Split(';')[1].ToLower();
						//MaidRemake.Instance.logDebug($"config=>{configCell} || actual=>{cell.ToLower()}");
						if (configCell == cell.ToLower() || configCell == "all")
						{
							UI.Maid.MaidRemake.Instance.pauseFollow();
						}
						else
						{
							UI.Maid.MaidRemake.Instance.resumeFollow();
						}
					}
					else
					{
						UI.Maid.MaidRemake.Instance.resumeFollow();
					}
				}
			}
		}
	}
}
