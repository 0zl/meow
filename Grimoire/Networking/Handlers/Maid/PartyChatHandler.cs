using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Tools;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers.Maid
{
	public class PartyChatHandler : IXtMessageHandler
	{
		public string[] HandledCommands { get; } = { "chatm" };

		public string targetUsername => UI.Maid.MaidRemake.Instance.cmbGotoUsername.Text.ToLower();

		public async void Handle(XtMessage message)
		{
			/*  % xt % chatm % 32123 % party~asdasdasdsa % player % 2049 % 32123 % 0 %
				0 =
				1 = xt
				2 = chatm
				3 = 32123
				4 = party~asdasdasdsa
				5 = player
			*/

			try
			{
				string[] chat = message.Arguments[4].ToString().Split('~');
				string type = chat[0];
				string msg = chat[1];

				if (type.Equals("party") && msg.StartsWith("."))
				{
					string[] command = msg.Split(' ');
					switch (command[0])
					{
						case ".tp":
							string map = command[1];
							if (map.Contains("tercessuinotlim"))
								Player.MoveToCell("m22", "Left");
							Player.JoinMap(map, "Enter", "Spawn");
							break;
						case ".join":
							string join = msg.Remove(0, 5);
							Player.JoinMap(join.Trim());
							break;
						case ".acc":
							string qid = msg.Remove(0, 5);

							Flash.Call("GetQuests", qid);
							await Task.Delay(1000);

							if (qid.Contains(","))
							{
								foreach (string q in qid.Split(','))
								{
									Flash.Call("Accept", q);
									await Task.Delay(1000);
								}
							}
							else
							{
								Flash.Call("Accept", qid);
							}
							break;
						case ".turnin":
							string qidt = msg.Remove(0, 8);
							if (qidt.Contains(","))
							{
								foreach (string q in qidt.Split(','))
								{
									Flash.Call("Complete", q);
									await Task.Delay(1000);
								}
							}
							else
							{
								Flash.Call("Complete", qidt);
							}
							break;
						case ".target":
							UI.Maid.MaidRemake.Instance.cmbGotoUsername.Text = msg.Remove(0, 8);
							break;
						case ".stop":
							UI.Maid.MaidRemake.Instance.cbEnablePlugin.Checked = false;
							break;
						case ".start":
							UI.Maid.MaidRemake.Instance.cbEnablePlugin.Checked = true;
							break;
					}
				}
			}
			catch { }
		}
	}
}
