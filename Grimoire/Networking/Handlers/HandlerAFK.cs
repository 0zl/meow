using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.UI;
using System;

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
            if (OptionsManager.AFK && message.Arguments[5] == "true" && Root.Instance.chkStartBot.Checked)
            {
                LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Logout on AFK.");
                Player.Logout();
            }
        }
    }
}