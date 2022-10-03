using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers
{

    public class HandlerFollow : IXtMessageHandler
    {
        public string[] HandledCommands { get; } = { "uotls" };

        public string targetUsername => BotManager.Instance.tbFollowPlayer2.Text.ToLower();

        public void Handle(XtMessage message)
        {
            try
            {
                string currUsername = message.Arguments[4].ToLower();
                if (currUsername == targetUsername && !World.IsMapLoading)
                {
                    string movement = message.Arguments[5].ToString();
                    string cell = null;
                    string pad = null;
                    foreach (string m in movement.Split(','))
                    {
                        if (m.Split(':')[0] == "strFrame")
                            cell = m.Split(':')[1];
                        if (m.Split(':')[0] == "strPad")
                            pad = m.Split(':')[1];
                    }
                    if (cell != null && pad != null)
                    {
                        Player.MoveToCell(cell, pad);
                        Player.SetSpawnPoint();
                    }
                }
            }
            catch { }
        }
    }
}
