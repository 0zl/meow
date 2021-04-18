using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdReturn : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
            try
            {
                Configuration oldConfig = instance.OldConfiguration;
                if (oldConfig != null && oldConfig.Commands.Count > 0)
                {
                    instance.Configuration = oldConfig;
                    instance.Index = instance.OldIndex;
                    instance.LoadBankItems();
                    instance.LoadAllQuests();
                }
            }
            catch
            {
                MessageBox.Show("broken return");
            }
        }

        public override string ToString()
        {
            return "Return";
        }
    }
}