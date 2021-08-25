using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting.Commands.Misc
{
    class CmdPing : IBotCommand
    {
        public bool PopMsg { get; set; } = false;
        public int Count { get; set; } = 10;
        public async Task Execute(IBotEngine instance)
        {
            for (int i = 0; i < Count; i++)
                Console.Beep();
            if (PopMsg) MessageBox.Show($"Your ping !");
        }

        public override string ToString()
        {
            return $"Ping: {Count}x beep {(PopMsg ? "and pop msg" : "")}";
        }
    }
}
