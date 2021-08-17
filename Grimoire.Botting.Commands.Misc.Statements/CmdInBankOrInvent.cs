using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
	public class CmdInBankOrInvent : StatementCommand, IBotCommand
	{
		public CmdInBankOrInvent()
		{
			Tag = "Item";
			Text = "Is in bank or inventory";
		}

		public Task Execute(IBotEngine instance)
		{
			string Value1 = instance.IsVar(this.Value1) ? Configuration.Tempvariable[instance.GetVar(this.Value1)] : this.Value1;
			string Value2 = instance.IsVar(this.Value2) ? Configuration.Tempvariable[instance.GetVar(this.Value2)] : this.Value2;

			Console.WriteLine($"Value1: {Value1} | Value2: {Value2}");

			bool inBank = Player.Bank.ContainsItem(Value1, Value2);
			bool inInventory = Player.Inventory.ContainsItem(Value1, Value2);

			Console.WriteLine($"inBank: {inBank} | inInventory: {inInventory}");

			if (inBank || inInventory)
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
			return "Is in bank or invent: " + Value1 + ", " + Value2;
		}
	}
}
