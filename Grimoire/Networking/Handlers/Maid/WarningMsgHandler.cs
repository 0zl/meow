using Grimoire.Game;
using Grimoire.Networking;
using MaidRemake.LockedMapHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers.Maid
{
    public class WarningMsgHandler : IXtMessageHandler
    {
        public string[] HandledCommands { get; } = { "warning" };
        public string targetUsername => UI.Maid.MaidRemake.Instance.cmbGotoUsername.Text.ToLower();
        public bool isLockedMapHandlerEnabled => UI.Maid.MaidRemake.Instance.cbHandleLockedMap.Checked;

        public void Handle(XtMessage message)
        {
            /*
               %xt%warning%-1%Cannot goto to player in a Locked zone.%
                0 =
                1 = xt
                2 = warning
                3 = -1
                4 = Cannot goto to player in a Locked zone.
             */

            if(isLockedMapHandlerEnabled && message.Arguments[4].Contains("Cannot goto to player in a Locked zone.") && Player.IsLoggedIn)
            {
                JoinAltMap();
            }
            else if (Player.IsLoggedIn && Player.Map != "whitemap")
            {
                if (message.Arguments[4].Contains("Cannot goto to player in a Locked zone.") && Player.IsLoggedIn)
                {
                    GotoSafeMap();
                }
                else if (message.Arguments[4].Contains("Room join failed, destination room is full.") && Player.IsLoggedIn)
                {
                    GotoSafeMap();
                }
                else if (message.Arguments[4].Contains($"Player '{targetUsername}' could not be found.") && Player.IsLoggedIn)
                {
                    GotoSafeMap();
                }
            }
        }

        public async void JoinAltMap()
        {
            await Task.Delay(new Random().Next(750, 1300));
            string[] mapInfo = AlternativeMap.GetNext().Split(';');
            Player.JoinMap(mapInfo[0], mapInfo[1], mapInfo[2]);
        }

        public async void GotoSafeMap()
        {
            await Task.Delay(new Random().Next(750, 1300));
            Player.JoinMap($"whitemap-{new Random().Next(9999, 99999)}", "Enter", "Spawn");
        }
    }
}
