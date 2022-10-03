using System;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Map
{
	public class CmdJoin : IBotCommand
	{
		public string Map { get; set; }

		public string Cell { get; set; }

		public string Pad { get; set; }

		public int Try { get; set; } = 1;

		public async Task Execute(IBotEngine instance)
		{
			BotData.BotState = BotData.State.Move;
			await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer), null, 15);

			//[MAP]-[NUMBER]

			string _mapName = this.Map.Contains("-") ? this.Map.Split('-')[0] : this.Map;
			string namName = (instance.IsVar(_mapName) ? Configuration.Tempvariable[instance.GetVar(_mapName)] : _mapName);

			string _roomNumber = this.Map.Contains("-") ? this.Map.Split('-')[1] : "";
			string roomNumber = (instance.IsVar(_roomNumber) ? Configuration.Tempvariable[instance.GetVar(_roomNumber)] : _roomNumber);

			int _try = Try;

			if (!namName.Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
			{
				if (!int.TryParse(roomNumber, out int n) && roomNumber != "")
				{
					Random random = new Random();
					int num = random.Next(1000, 99999);
					roomNumber = num.ToString();
				}
				bool provoke = instance.Configuration.ProvokeMonsters;
				if (provoke) instance.Configuration.ProvokeMonsters = false;
				while (_try > 0 && Player.Map != Map)
				{
					await this.TryJoin(instance, namName, roomNumber);
					_try--;
				}
				if (provoke) instance.Configuration.ProvokeMonsters = true;
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
			await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer), null, 15);
			if (Player.CurrentState == Player.State.InCombat)
			{
				string[] safeCell = ClientConfig.GetValue(ClientConfig.C_SAFE_CELL).Split(',');
				Player.MoveToCell(safeCell[0], safeCell[1]);
				await instance.WaitUntil(() => Player.CurrentState != Player.State.InCombat, timeout: 10);
				await Task.Delay(1500);
			}
			String join = RoomNumber.Length > 0 ? $"{MapName}-{RoomNumber}" : MapName;
			Player.JoinMap(join, this.Cell, this.Pad);
			await instance.WaitUntil(() => Player.Map.Equals(MapName, StringComparison.OrdinalIgnoreCase), null, 10);
			await instance.WaitUntil(() => !World.IsMapLoading, null, 40);
		}

		public override string ToString()
		{
			return string.Concat(new string[]
			{
				$"Join: [{Try}x] ",
				this.Map,
				", ",
				this.Cell,
				", ",
				this.Pad
			});
		}
	}
}
