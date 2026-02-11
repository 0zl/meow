using System.Threading;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.UI;

namespace Grimoire.Networking.Handlers
{
    public class HandlerSpecial
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public async Task Taunt(int cycle = 2, string mon = "*", int second = 12, int count = -1)
        {

            if (count > cycle)
            {
                count %= cycle;
            }
            string prevTarget = "";
            while (!_cts.IsCancellationRequested)
            {
                if (count <= 0)
                {
                    prevTarget = Player.GetTargetName();
                    if (BotManager.Instance.ActiveBotEngine.IsRunning)
                        BotManager.Instance.ActiveBotEngine.paused = true;
                    while (Player.GetAuras(false, "Focus") != 1)
                    {
                        Player.AttackMonster(mon);
                        await Task.Delay(Player.SkillAvailable("5"));
                        Player.UseSkill("5");
                    }
                }
                if (BotManager.Instance.ActiveBotEngine.IsRunning)
                    BotManager.Instance.ActiveBotEngine.paused = false;
                Player.AttackMonster(prevTarget);
                count--;
                await Task.Delay(second / cycle * 1000, _cts.Token);
            }
        }

        public HandlerSpecial(CancellationTokenSource cts)
        {
            this._cts = cts;
            Taunt();
        }
    }
}