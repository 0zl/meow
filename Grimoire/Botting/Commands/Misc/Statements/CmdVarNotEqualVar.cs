using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
	public class CmdVarNotEqualVar : StatementCommand, IBotCommand
	{
		public CmdVarNotEqualVar()
		{
			Tag = "Misc";
			Text = "Var is not equal to Var";
		}

		public Task Execute(IBotEngine instance)
		{
			var v1 = (instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1);
			var v2 = (instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2);

			if (v1.ToString() == v2.ToString())
			{
				instance.Index++;
			}
			return Task.FromResult<object>(null);
		}

		public override string ToString()
		{
			return $"{Value1} != {Value2}";
		}
	}
}
