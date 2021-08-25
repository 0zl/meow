using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
	public class CmdProvoke : IBotCommand
	{
		public bool Set { get; set; }

		public async Task Execute(IBotEngine instance)
		{
			OptionsManager.ProvokeMonsters = Set;
		}

		public override string ToString()
		{
			if (Set)
			{
				return "Provoke ON";
			}
			else
			{
				return "Provoke OFF";
			}
		}
	}
}
