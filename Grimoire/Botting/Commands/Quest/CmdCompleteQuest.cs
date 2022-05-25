using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Quest
{
    public class CmdCompleteQuest : IBotCommand
    {
        public Game.Data.Quest Quest
        {
            get;
            set;
        }
        public int CompleteTry { get; set; } = 1;
        public bool ReAccept { get; set; } = false;
        public bool InBlank { get; set; } = false;
        public int Delay { get; set; } = 1000;

        public async Task Execute(IBotEngine instance)
        {
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete));
            bool provokeMons = instance.Configuration.ProvokeMonsters;

            int i = 0;
            if (!Player.Quests.AcceptedQuests.Contains(Quest)) Quest.Accept();
            while (Player.Quests.CanComplete(Quest.Id) && i < CompleteTry && instance.IsRunning && Player.IsLoggedIn)
            {
                if (provokeMons) instance.Configuration.ProvokeMonsters = false;
                if (instance.Configuration.ExitCombatBeforeQuest && !InBlank)
                {
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await instance.WaitUntil(() => Player.CurrentState != Player.State.InCombat);
                    await Task.Delay(1000);
                }
                if (InBlank && !Player.Cell.Equals("Blank"))
                {
                    Player.MoveToCell("Blank", "Spawn");
                    await Task.Delay(2000);
                }
                Quest.Complete();
                if (CompleteTry > 1) await Task.Delay(Delay);
                i++;
                //await instance.WaitUntil(() => !Player.Quests.IsInProgress(Quest.Id));
            }
            instance.Configuration.ProvokeMonsters = provokeMons;

            if (ReAccept)
            {
                await Task.Delay(1000);
                Quest.Accept();
            }
        }

        public override string ToString()
        {
            return $"Complete quest [{CompleteTry}x]: {(Quest.ItemId != null && Quest.ItemId != "0" ? $"{Quest.Id}^{Quest.ItemId}" : Quest.Id.ToString())} {(InBlank ? "[InBlank]" : "")}";
        }
    }
}