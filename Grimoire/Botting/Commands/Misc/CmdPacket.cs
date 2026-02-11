using Grimoire.Game;
using Grimoire.Networking;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdPacket : RegularExpression, IBotCommand
    {
        public string Packet
        {
            get;
            set;
        }

        public int SpamTimes
        {
            get;
            set;
        } = 1;

        public int Delay
        {
            get;
            set;
        } = 1000;

        public bool ForClient
        {
            get;
            set;
        } = false;

        public bool Client
        {
            get;
            set;
        } = false;

        public async Task Execute(IBotEngine instance)
        {
            string text;

            int i = 1;
            while (i <= SpamTimes && Player.IsLoggedIn && instance.IsRunning)
			{
                /*if (IsVar(Packet))
                {
                    text = Configuration.Tempvariable[GetVar(Packet)];
                }
                else
                {
                    text = Packet;
                }*/
                text = Bot.Instance.ResolveVars(Packet);

                text = text.Replace("{ROOM_ID}", World.RoomId.ToString()).Replace("{ROOM_NUMBER}", World.RoomNumber.ToString()).Replace("PLAYERNAME", Player.Username);
                text = text.Replace("{GETMAP}", Player.Map);
                while (text.Contains("--"))
                {
                    text = new Regex("-{1,}", RegexOptions.IgnoreCase).Replace(text, (Match m) => "-");
                }
                text = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(text, (Match m) => new Random().Next(1001, 99999).ToString());

                if (ForClient || Client)
                {
                    if (text.Split(' ')[0] == "Level")
                    {
                        text = "{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"levelUp\",\"intExpToLevel\":\"200000\",\"intLevel\":" + text.Split(' ')[1] + "}}}";
                    }

                    await Proxy.Instance.SendToClient(text);
                    await Task.Delay(Delay);
                }
                else
                {
                    await Proxy.Instance.SendToServer(text);
                    await Task.Delay(Delay);
                }
                i++;
			}
        }

        public override string ToString()
        {
            string text = ForClient || Client ? "To client" : "To server";
            return $"{text} [{SpamTimes}x]: {Packet}";
        }
    }
}