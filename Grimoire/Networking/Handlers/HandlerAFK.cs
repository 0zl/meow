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
            "uotls"
        };

        public void Handle(XtMessage message)
        {
            //% xt % uotls % -1 % jeazt % afk:true %
            if (OptionsManager.AFK && 
                message.Arguments[4] == Player.Username &&
                message.Arguments[5] == "afk:true" && 
                Root.Instance.chkStartBot.Checked)
            {
                Player.Logout();
                LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Logout on AFK.");
            }
        }
    }
}