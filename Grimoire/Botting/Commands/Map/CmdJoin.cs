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

			//[MAP]-[NUMBER]

			string _mapName = this.Map.Contains("-") ? this.Map.Split('-')[0] : this.Map;
			string namName = (instance.IsVar(_mapName) ? Configuration.Tempvariable[instance.GetVar(_mapName)] : _mapName);

			string _roomNumber = this.Map.Contains("-") ? this.Map.Split('-')[1] : "";
			string roomNumber = (instance.IsVar(_roomNumber) ? Configuration.Tempvariable[instance.GetVar(_roomNumber)] : _roomNumber);

			if (!namName.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
			{
				if (!int.TryParse(roomNumber, out int n) && roomNumber != "")
				{
					Random random = new Random();
					int num = random.Next(1000, 99999);
					roomNumber = num.ToString();
				}
				await this.TryJoin(instance, namName, roomNumber);
			}

			if (namName.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
			{
				if (!Player.Cell.Equals(this.Cell, StringComparison.OrdinalIgnoreCase))
				{
					Player.MoveToCell(this.Cell, this.Pad);
					await Task.Delay(500);
				}
				World.SetSpawnPoint();
				BotData.BotMap = namName;
				BotData.BotCell = this.Cell;
				BotData.BotPad = this.Pad;
			}
		}

		public async Task TryJoin(IBotEngine instance, string MapName, string RoomNumber)
		{
			bool provoke = instance.Configuration.ProvokeMonsters;
			if (provoke) instance.Configuration.ProvokeMonsters = false;
			await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer), null, 15);
			if (Player.CurrentState == Player.State.InCombat)
			{
				Player.MoveToCell(Player.Cell, Player.Pad);
				await instance.WaitUntil(() => Player.CurrentState != Player.State.InCombat);
				await Task.Delay(1500);
			}
			String join = RoomNumber.Length > 0 ? $"{MapName}-{RoomNumber}" : MapName;
			Player.JoinMap(join, this.Cell, this.Pad);
			await instance.WaitUntil(() => Player.Map.Equals(MapName, StringComparison.OrdinalIgnoreCase), null, 10);
			await instance.WaitUntil(() => !World.IsMapLoading, null, 40);
			if (provoke) instance.Configuration.ProvokeMonsters = true;
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
