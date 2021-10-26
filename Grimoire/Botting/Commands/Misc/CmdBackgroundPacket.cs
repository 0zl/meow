using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
	public class CmdBackgroundPacket : IBotCommand
	{
		public Action ActionW { get; set; }
		public string Label { get; set; }
		public string Packet { get; set; }
		public int Delay { get; set; } = 500;

		public async Task Execute(IBotEngine instance)
		{
			//what?
		}

		public enum Action
		{
			ADD,
			STOP
		}

		public override string ToString()
		{
			string text = "";
			switch (ActionW)
			{
				case Action.ADD:
					text = $"Spammer [ADD]: {Label}";
					break;
				case Action.STOP:
					text = $"Spammer [STOP]: {Label}";
					break;
			}
			return text;
		}
	}
}
