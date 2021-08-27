using Grimoire.Game;
using Grimoire.Networking;
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

		public async Task Execute(IBotEngine instance)
		{
			string playerName = PlayerName.ToLower();
			int gotoDelay = 2000;
			int gotoTry = 0;
			int maxTry = MaxGotoTry == 0 ? 999 : MaxGotoTry;
			bool following = true;

			Proxy.Instance.ReceivedFromServer += FollowHandler;

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

					CmdKill kill = new CmdKill
					{
						Monster = "*",
						KillPriority = KillPriority
					};

					await kill.Execute(instance);
				}

				await Task.Delay(500);
			}

			Proxy.Instance.ReceivedFromServer -= FollowHandler;
			Console.WriteLine("Unregistered");
		}

		private void FollowHandler(Message message)
		{
			string msg = message.ToString();

			//%xt%uotls%-1%surga%strPad:Right,tx:0,strFrame:r2,ty:0%
			if (msg.Contains("%xt%uotls%-1%" + PlayerName + "%strPad"))
			{
				string cell = getBetweenString(msg, "strFrame:", ",");
				string pad = getBetweenString(msg, "strPad:", ",");
				Player.MoveToCell(cell, pad);
				Player.SetSpawnPoint();
			}

			//%xt%exitArea%-1%21959%surga%
			if (msg.Contains("%xt%exitArea%") && msg.Contains(PlayerName))
			{
				Player.CancelTarget();
			}

			//From server: %xt%warning%-1%User 'surga' could not be found.%
			/*if (msg.Contains($"%xt%warning%-1%User '{playerName}' could not be found.%"))
			{
				stopBot();
			}*/
		}

		public static string getBetweenString(string strSource, string strStart, string strEnd)
		{
			if (strSource.Contains(strStart) && strSource.Contains(strEnd))
			{
				int Start, End;
				int IndexStart = 0;

				Start = strSource.IndexOf(strStart, IndexStart) + strStart.Length;
				End = strSource.IndexOf(strEnd, Start);
				IndexStart = Start;

				return strSource.Substring(Start, End - Start);
			}
			return null;
		}

		public override string ToString()
		{
			return $"Follow: {PlayerName}";
		}

	}
}
