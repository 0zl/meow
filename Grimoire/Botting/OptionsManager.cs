using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace Grimoire.Botting
{
    public static class OptionsManager
    {
        private static bool _isRunning;

        private static bool _disableAnimations;

        private static bool _hidePlayers;

        private static bool _infRange;

        private static bool _afk;

        private static bool _infMana;

        public static String LoginUsername
        {
            get;
            set;
        }

        public static String LoginPassword
        {
            get;
            set;
        }

        public static bool InfMana
        {
            get => _infMana;
            set
            {
                _infMana = value;
            }
        }

        public static bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            private set
            {
                _isRunning = value;
                StateChanged?.Invoke(value);
            }
        }

        public static bool Buff
        {
            get;
            set;
        }

        public static bool ProvokeMonsters
        {
            get;
            set;
        }

        public static bool EnemyMagnet
        {
            get;
            set;
        }

        public static bool LagKiller
        {
            get;
            set;
        }

        public static bool SkipCutscenes
        {
            get;
            set;
        }

        public static bool AFK
        {
            get => _afk;
            set
            {
                _afk = value;
                if (value)
                    Proxy.Instance.RegisterHandler(HandlerAFK1);
                else
                    Proxy.Instance.UnregisterHandler(HandlerAFK1);
            }
        }

        public static bool DisableAnimations
        {
            get => _disableAnimations;
            set
            {
                _disableAnimations = value;
                if (value)
                    Proxy.Instance.RegisterHandler(HandlerDisableAnimations);
                else
                    Proxy.Instance.UnregisterHandler(HandlerDisableAnimations);
            }
        }

        public static bool HidePlayers
        {
            get => _hidePlayers;
            set
            {
                _hidePlayers = value;
                if (value)
                {
                    Proxy.Instance.RegisterHandler(HandlerHidePlayers);
                    DestroyPlayers();
                }
                else
                {
                    Proxy.Instance.UnregisterHandler(HandlerHidePlayers);
                }
            }
        }
        
        public static bool InfiniteRange
        {
            /*get => _infRange;
            set
            {
                _infRange = value;
                if (value)
                {
                    SetInfiniteRange();
                }
            }*/

            get
            {
                return _infRange;
            }
            set
            {
                _infRange = value;

                if (value)
                {
                    Proxy.Instance.RegisterHandler(HandlerRange);
                    SetInfiniteRange();
                    return;
                }
                Proxy.Instance.UnregisterHandler(HandlerRange);
            }
        }

        public static int WalkSpeed
        {
            get;
            set;
        }

        public static int Timer
        {
            get;
            set;
        } = 1000;

        public static bool Packet
        {
            get;
            set;
        }

        private static readonly string[] empty = new string[0];

        private static List<BSpammer> listSpammer = new List<BSpammer>();

        public static event Action<bool> StateChanged;

        private static void SetInfiniteRange() => Flash.Call("SetInfiniteRange", empty);

        private static void SetProvokeMonsters() => Flash.Call("SetProvokeMonsters", empty);

        private static void SetEnemyMagnet() => Flash.Call("SetEnemyMagnet", empty);

        public static void SetLagKiller() => Flash.Call("SetLagKiller", LagKiller ? bool.TrueString : bool.FalseString);

        public static void DestroyPlayers() => Flash.Call("DestroyPlayers", empty);

        private static void SetSkipCutscenes() => Flash.Call("SetSkipCutscenes", empty);

        public static void SetWalkSpeed() => Flash.Call("SetWalkSpeed", WalkSpeed.ToString());

        public static void Start()
        {
            if (!IsRunning)
            {
                ApplySettings();
            }
        }

        public static void Stop()
        {
            IsRunning = false;
        }

        private static async Task ApplySettings()
        {
            IsRunning = true;
            while (IsRunning && Player.IsLoggedIn)
            {
                if (!Player.IsLoggedIn)
                {
                    return;
                }
                bool flagprovoke = ProvokeMonsters && Player.IsAlive && BotData.BotState != BotData.State.Move && BotData.BotState != BotData.State.Rest && BotData.BotState != BotData.State.Transaction;
                if (flagprovoke)
                {
                    if (BotData.BotState == BotData.State.Quest)
                    {
                        await Task.Delay(1500);
                        SetProvokeMonsters();
                        BotData.BotState = BotData.State.Combat;
                    }
                    SetProvokeMonsters();
                }
                if (EnemyMagnet && Player.IsAlive)
                    SetEnemyMagnet();
                if (SkipCutscenes)
                    SetSkipCutscenes();
                //SetWalkSpeed();
                SetLagKiller();
                await Task.Delay(millisecondsDelay: Timer);
            }
        }
        
        private static IJsonMessageHandler HandlerDisableAnimations
        {
            get;
        } = new HandlerAnimations();

        private static IXtMessageHandler HandlerHidePlayers
        {
            get;
        } = new HandlerPlayers();

        public static IJsonMessageHandler HandlerRange 
        { 
            get;
        } = new HandlerSkills();

        private static IXtMessageHandler HandlerAFK1
        {
            get;
        } = new HandlerAFK();

        static OptionsManager()
        {
            HandlerDisableAnimations = new HandlerAnimations();
            HandlerHidePlayers = new HandlerPlayers();
            HandlerRange = new HandlerSkills();
            HandlerAFK1 = new HandlerAFK();
            WalkSpeed = 8;
        }

    }

    class BSpammer
    {
        private string label;
        private string packet;
        private int delay;
        private bool isStart = false;

        public BSpammer()
        {

        }

        public BSpammer(string label, string packet, int delay)
        {
            this.label = label;
            this.packet = packet;
            this.delay = delay;
        }

        public async void Start()
        {
            isStart = true;
            while (isStart)
            {
                _ = Proxy.Instance.SendToServer(packet);
                await Task.Delay(delay);
            }
        }

        public void Stop()
        {
            isStart = false;
        }
    }
}