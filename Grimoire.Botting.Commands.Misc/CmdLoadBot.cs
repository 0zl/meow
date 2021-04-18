using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdLoadBot : IBotCommand
    {
        public string BotFileName
        {
            get;
            set;
        }

        public string BotFilePath
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            string name = BotFileName;
            string path = BotFilePath;
            if (File.Exists(path))
            {
                try
                {
                    string value;
                    using (TextReader reader = new StreamReader(path))
                    {
                        value = await reader.ReadToEndAsync();
                    }
                    JsonSerializerSettings serializerSettings = new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.All
                    };
                    Configuration newConfiguration = JsonConvert.DeserializeObject<Configuration>(value, serializerSettings);
                    if (newConfiguration != null && newConfiguration.Commands.Count > 0)
                    {
                        instance.OldConfiguration = instance.Configuration;
                        instance.Configuration = newConfiguration;
                        instance.OldIndex = instance.Index;
                        instance.Index = -1;
                        instance.LoadBankItems();
                        instance.LoadAllQuests();
                    }
                }
                catch
                {
                }
            }
        }

        public override string ToString()
        {
            return "Load bot: " + BotFileName;
        }
    }
}