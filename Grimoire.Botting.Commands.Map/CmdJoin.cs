using System;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Networking;

namespace Grimoire.Botting.Commands.Map
{
	public class CmdJoin : IBotCommand
	{
		public string Map { get; set; }

		public string Cell { get; set; }

		public string Pad { get; set; }

		public async Task Execute(IBotEngine instance)
		{
			BotData.BotState = BotData.State.Move;
			await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer), null, 15);
			string cmdMap = this.Map.Contains("-") ? this.Map.Split(new char[]
			{
				'-'
			})[0] : this.Map;
			string text = this.Map.Substring(cmdMap.Length);
			if (!cmdMap.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
			{
				int n;
				if (!int.TryParse(text, out n) && text != "")
				{
					Random random = new Random();
					int num = random.Next(1000, 99999);
					text = "-" + num;
				}

				await this.TryJoin(instance, cmdMap, text);
			}
			if (cmdMap.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
			{
				if (!Player.Cell.Equals(this.Cell, StringComparison.OrdinalIgnoreCase))
				{
					Player.MoveToCell(this.Cell, this.Pad);
					await Task.Delay(1250);
				}
				World.SetSpawnPoint();
				BotData.BotMap = cmdMap;
				BotData.BotCell = this.Cell;
				BotData.BotPad = this.Pad;
			}
		}

		public async Task TryJoin(IBotEngine instance, string MapName, string RoomProp = "")
		{
			await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer), null, 15);
			if (Player.CurrentState == Player.State.InCombat)
			{
				Player.MoveToCell(Player.Cell, Player.Pad);
				await Task.Delay(1250);
			}
			while (Player.HasTarget)
			{
				Player.CancelTarget();
				await Task.Delay(500);
			}
			Player.JoinMap(MapName + RoomProp, this.Cell, this.Pad);
			await instance.WaitUntil(() => Player.Map.Equals(MapName, StringComparison.OrdinalIgnoreCase), null, 5);
			await instance.WaitUntil(() => !World.IsMapLoading, null, 40);
		}


		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Join: ",
				this.Map,
				", ",
				this.Cell,
				", ",
				this.Pad
			});
		}
	}
}
