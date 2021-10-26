using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Tools;
using Grimoire.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
	public class CmdFollow : IBotCommand
	{
		public string PlayerName { get; set; }

		public string KillPriority { get; set; } = "";

		public int MaxGotoTry { get; set; } = 5;

		public bool WaitForever { get; set; } = false;
		public bool AntiCounter { get; set; } = false;

		public async Task Execute(IBotEngine instance)
		{

			string PlayerName = instance.IsVar(this.PlayerName) ? Configuration.Tempvariable[instance.GetVar(this.PlayerName)] : this.PlayerName;
			string playerName = PlayerName.ToLower();
			int gotoDelay = 2000;
			int gotoTry = 0;
			int maxTry = MaxGotoTry == 0 ? 999 : MaxGotoTry;
			bool following = true;

			//Proxy.Instance.ReceivedFromServer += FollowHandler;
			Flash.FlashCall += FollowHandler;

			while (instance.IsRunning && Player.IsLoggedIn && (following && !WaitForever))
			{
				if (!Player.IsAlive)
				{
					await Task.Delay(1000);
					continue;
				}

				List<string> mapPlayers = World.PlayersInMap;
				mapPlayers.ConvertAll<string>(a => a.ToLower());

				if (!mapPlayers.Contains(playerName))
				{
					Player.MoveToCell("Blank");
					await instance.WaitUntil(() => Player.State.InCombat != Player.CurrentState);
					await Task.Delay(gotoDelay);
					Player.GoToPlayer(playerName);

					gotoTry++;
					following = gotoTry < maxTry;

					if (WaitForever && !following)
					{
						gotoDelay = 10000;
					} 
					else
					{
						gotoDelay = 2000;
					}
				}
				else
				{
					gotoTry = 0;

					Player.SetSpawnPoint();
					CmdKill kill = new CmdKill
					{
						Monster = "*",
						KillPriority = KillPriority,
						AntiCounter = AntiCounter
					};

					await kill.Execute(instance);
				}

				await Task.Delay(500);
			}

			//Proxy.Instance.ReceivedFromServer -= FollowHandler;
			Flash.FlashCall -= FollowHandler;
		}

		private void FollowHandler(AxShockwaveFlashObjects.AxShockwaveFlash flash, string function, params object[] args)
		{
			string msg = args[0].ToString();
			if (!msg.StartsWith("{")) return;
			if (function == "pext")
			{
				dynamic packet = JsonConvert.DeserializeObject<dynamic>(msg);
				string type = packet["params"].type;
				dynamic data = packet["params"].dataObj;

				if (type == "str")
					if (data[0] == "uotls")
						if (data[2] == PlayerName)
						{
							string movement = data[3];
							string cell = null;
							string pad = null;
							foreach (string m in movement.Split(','))
							{
								if (m.Split(':')[0] == "strFrame")
									cell = m.Split(':')[1];
								if (m.Split(':')[0] == "strPad")
									pad = m.Split(':')[1];
							}
							if (cell != null && pad != null)
							{
								Player.MoveToCell(cell, pad);
								Player.SetSpawnPoint();
							}
						}

				if (msg.Contains("exitArea") && msg.Contains(PlayerName))
				{
					Player.CancelAutoAttack();
					Player.CancelTarget();
				}
			}
		}

		public override string ToString()
		{
			return $"Follow kills: {PlayerName}";
		}

	}
}
