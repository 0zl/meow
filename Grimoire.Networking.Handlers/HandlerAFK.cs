using Grimoire.Botting;
using Grimoire.Game;

namespace Grimoire.Networking.Handlers
{
    public class HandlerAFK : IXtMessageHandler
    {
        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "afk"
        };

        public void Handle(XtMessage message)
        {
            if (message.Arguments[5] == "true" && Bot.Instance.IsRunning)
                Player.Logout();
        }
    }
}