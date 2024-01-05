using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
	public class CmdCancelTarget : IBotCommand
	{
		public bool LeaveCombat { get; set; }
		public async Task Execute(IBotEngine instance)
		{
			Player.CancelTarget();
			Player.CancelTargetSelf();
			await Task.Delay(500);
			Player.CancelAutoAttack();
			if (LeaveCombat)
			{
				string cell = Player.Cell;
				string pad = Player.Pad;
				Player.MoveToCell(cell, pad);
				await Task.Delay(500);
			}
		}

		public override string ToString()
		{
			return LeaveCombat ? "Leave Combat" : "Stop Attack";
		}
	}
}