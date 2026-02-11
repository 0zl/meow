using AxShockwaveFlashObjects;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Botting.Commands.Item;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Grimoire.Botting
{
    public static class BotUtilities
    {
        public static ManualResetEvent BankLoadEvent;

        public static AxShockwaveFlash flash;

        public static async Task WaitUntil(this IBotEngine instance, Func<bool> condition, Func<bool> prerequisite = null, int timeout = 15, int interval = 1000)
        {
            int iterations = 0;
            while ((prerequisite ?? (() => instance.IsRunning && Player.IsLoggedIn && Player.IsAlive))() && !condition() && (iterations < timeout || timeout == -1))
            {
                await Task.Delay(interval);
                iterations++;
            }
        }

        public static bool RequiresDelay(this IBotCommand cmd)
        {
            if (cmd is StatementCommand || cmd is CmdIndex || cmd is CmdLabel || cmd is CmdGotoLabel || cmd is CmdBlank || cmd is CmdSkillSet)
                return false;
            return true;
        }

        

        public static void LoadAllQuests(this IBotEngine instance)
        {
            List<int> list = new List<int>();
			
			HashSet<int> loadedQuests = new HashSet<int>(
				Player.Quests.QuestTree?.Select(q => q.Id) ?? Enumerable.Empty<int>()
			);
			
            void AddUnique(int id)
            {
                if (!list.Contains(id) && !loadedQuests.Contains(id))
                    list.Add(id);
            }
            foreach (IBotCommand command in instance.Configuration.Commands)
            {
                if (command is CmdAcceptQuest Accept)
                    AddUnique(Accept.Quest.Id);
                else if (command is CmdCompleteQuest Complete)
                    AddUnique(Complete.Quest.Id);
                else if (command is CmdAddQuestList AddQList)
                    AddUnique(AddQList.Id);
                else if (command is CmdQuestHunt Hunt)
                    if (Hunt.QID != 0) AddUnique(Hunt.QID);                
            } //changed to not load the quest (again) if already loaded
            foreach (var q in instance.Configuration.Quests)
                AddUnique(q.Id);
            // list.AddRange(instance.Configuration.Quests.Select((Quest q) => q.Id));
            if (list.Count > 0)
            {
                Task.Run(async () =>
                {
                    instance.paused = true;
                    const int batchSize = 30; //max GetQuest
                    for (int i = 0; i < list.Count; i += batchSize)
                    {
                        int take = Math.Min(batchSize, list.Count - i);
                        var batch = list.GetRange(i, take);
                        Player.Quests.Get(batch);
                        await Task.Delay(600);
                    }
                    instance.paused = false;
                });
            }
        }


        public static async void StopCommands(this IBotEngine instance)
        {
            foreach (IBotCommand command in instance.Configuration.Commands)
            {
                if (command is CmdAddQuestList cmdAddQuestList)
                {
                    var remove = new CmdRemoveQuestList
					{
                        Id = cmdAddQuestList.Id,
                        ItemId = cmdAddQuestList.ItemId,
                        SafeRelogin = cmdAddQuestList.SafeRelogin,
					};
					await remove.Execute(instance);
                }
            }
        }

        public static void LoadBankItems(this IBotEngine instance)
        {
			if (instance.Configuration.Commands.Any((IBotCommand c) =>
				c is CmdBankSwap || 
                c is CmdBankTransfer || 
                c is CmdInBank || 
                c is CmdNotInBank || 
                c is CmdInBankOrInvent || 
                c is CmdNotInBankAndInvent || 
                c is CmdBankList))
			{
				//Player.Bank.LoadItems();
                Player.Bank.GetBank();
            }
        }

        static BotUtilities()
        {
            BankLoadEvent = new ManualResetEvent(initialState: false);
        }
    }

    class pauseProvoke : IDisposable
    {
        private readonly Configuration _config;
        private readonly bool _originalValue;

        public pauseProvoke(Configuration config)
        {
            _config = config;
            _originalValue = config.ProvokeMonsters;
            _config.ProvokeMonsters = false;
        }

        public void Dispose()
        {
            _config.ProvokeMonsters = _originalValue;
        }
    }
}