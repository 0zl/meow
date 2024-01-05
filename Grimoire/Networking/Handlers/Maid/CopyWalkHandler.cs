using Grimoire.Game;
using Grimoire.Networking;

namespace Grimoire.Networking.Handlers.Maid
{
    public class CopyWalkHandler : IXtMessageHandler
    {
        public string[] HandledCommands { get; } = { "uotls" };

        public string targetUsername => UI.Maid.MaidRemake.Instance.cmbGotoUsername.Text.ToLower();

        public void Handle(XtMessage message)
        {
            /*  When walk type 1
                % xt % uotls % -1 % username % sp:8,tx:714,ty:356,strFrame:r1 %
                0 =
                1 = xt
                2 = uotls
                3 = -1
                4 = username
                5 = sp:8,tx:714,ty:356,strFrame:r1

                When walk type 2 - jump cell
                % xt % uotls % -1 % username % mvts:1646524997575,sp:9,px:759,py:384,mvtd:503,tx:662,ty:346,strFrame:Enter %
                0 =
                1 = xt
                2 = uotls
                3 = -1
                4 = username
                5 = mvts:1646524997575,sp:9,px:759,py:384,mvtd:503,tx:662,ty:346,strFrame:Enter
            */

            try
            {
                // current Username
                string currUsername = message.Arguments[4].ToLower();

                if ((currUsername == targetUsername) && !World.IsMapLoading)
                {
                    string movement = message.Arguments[5].ToString();
                    string cell = null;
                    string pad = null;
                    float x = 0f;
                    float y = 0f;
                    foreach (string m in movement.Split(','))
                    {
                        if (m.Split(':')[0] == "strFrame")
                            cell = m.Split(':')[1];
                        if (m.Split(':')[0] == "strPad")
                            pad = m.Split(':')[1];
                        if (m.Split(':')[0] == "tx")
                            x = float.Parse(m.Split(':')[1]);
                        if (m.Split(':')[0] == "ty")
                            y = float.Parse(m.Split(':')[1]);
                    }
                    if (x != 0f && y != 0f)
                    {
                        System.Console.WriteLine($"Walk = x:{x} y:{y}");
                        Player.WalkToPoint(x.ToString(), y.ToString());
                    }
                }
            }
            catch { }
        }
    }
}
