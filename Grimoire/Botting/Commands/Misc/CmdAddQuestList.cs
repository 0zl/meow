using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Grimoire.Botting.Commands.Misc
{
	public class CmdAddQuestList : IBotCommand
	{
		public int QuestID { get; set; }
		public string ItemID { get; set; }
		public bool SafeRelogin { get; set; }

		public async Task Execute(IBotEngine instance)
		{
			//what?
		}


		public override string ToString()
		{
			return "Add Quest list : " + QuestID;
		}
	}
}
