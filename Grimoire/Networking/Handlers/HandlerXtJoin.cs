using Grimoire.UI;
using System;

namespace Grimoire.Networking.Handlers
{
	public class HandlerXtJoin : IXtMessageHandler
	{
		public string[] HandledCommands
		{
			get;
		} = new string[1]
		{
			"server"
		};

		public void Handle(XtMessage message)
		{
			if (!message.RawContent.Contains("You joined "))
				return;
			if (BotManager.Instance.CustomName != null)
				BotManager.Instance.CustomName = BotManager.Instance.CustomName;
			if (BotManager.Instance.CustomGuild != null)
				BotManager.Instance.CustomGuild = BotManager.Instance.CustomGuild;
			//LogForm.Instance.AppendChat(string.Format("[{0:hh:mm:ss}] {1}", DateTime.Now, message.Arguments[4]));
		}
	}

	public class HandlerPrivateJoin : IXtMessageHandler
	{
		public string[] HandledCommands
		{
			get;
		} = new string[1]
		{
			"tfer"
		};

		public string Room
		{
			get;
			set;
		} = "rand";

		public void Handle(XtMessage message)
		{
			Random random = new Random();
			int num = random.Next(1000, 99999);

			string map = message.Arguments[7].ToString();
			string room = Room;
			string parts;

			if (map.Contains("-") && !Room.StartsWith("f"))
			{
				parts = map;
			} 
			else
			{
				if (Room.StartsWith("f")) {
					room = Room.Replace("f", "");
					map = map.Split('-')[0];
				}
				if (int.TryParse(room, out int i))
				{
					parts = $"{map}-{i}";
				} 
				else
				{
					parts = $"{map}-{num}";
				}
			}

			message.Arguments[7] = parts.ToString();
		}
	}
}