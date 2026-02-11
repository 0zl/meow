using System;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers
{
    public class HandlerChat : IXtMessageHandler
    {
        
        public string[] HandledCommands
        {
            get;
        } = new string[4]
        {
            "chatm",
            "warning",
            "whisper",
            "message"
        };

        public void Handle(XtMessage message)
		{
            string type = message.Arguments[2];
            string tolog = message.Arguments[4];
            //message.Arguments[5] = (message.Arguments[5] == Player.Username && OptionsManager.ChangeChat) ? "You" : message.Arguments[5];
            switch (type)
            {
                case "chatm":
                    tolog = (message.Arguments[5] + message.Arguments[4])
                        .Replace("zone~", ": ")
                        .Replace("guild~", "[GUILD]: ")
                        .Replace("party~", "[PARTY]: ");
                    break;

                case "whisper":
                    tolog = message.Arguments[6] == Player.Username ? "From " + message.Arguments[5] : "To " + message.Arguments[6];
                    tolog = $"{tolog}: {message.Arguments[4]}";
                    break;
            }
            LogForm.Instance.AppendChat(string.Format("[{0:hh:mm:ss}] {1}", DateTime.Now, tolog));
        }
	}
}
