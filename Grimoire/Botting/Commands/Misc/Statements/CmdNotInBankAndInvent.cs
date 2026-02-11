using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
	public class CmdNotInBankAndInvent : StatementCommand, IBotCommand
	{
		public CmdNotInBankAndInvent()
		{
			Tag = "Item";
			Text = "Is not in bank and inventory";
		}

		public Task Execute(IBotEngine instance)
		{
			string name = Bot.Instance.ResolveVars(Value1);
            string qty  = Bot.Instance.ResolveVars(Value2);
			bool inBank = Player.Bank.ContainsItem(name, qty);
			bool inInventory = Player.Inventory.ContainsItem(name, qty);

			if (!inBank && !inInventory)
			{

			}
			else
            {
				instance.Index++;
			}
			return Task.FromResult<object>(null);
		}

		public override string ToString()
		{
			return "Is not in bank and invent: " + Value1 + ", " + Value2;
		}
	}
}
