using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers
{
	public class HandlerWarnings : IXtMessageHandler
	{
		public string[] HandledCommands
		{
			get;
		} = new string[1]
		{
			"warning"
		};

		public void Handle(XtMessage message)
		{
			if (message.Arguments[4] == "Please slow down. Last action was too soon!" && Root.Instance.chkStartBot.Checked)
			{
				message.Send = false;
			}
		}
	}
}