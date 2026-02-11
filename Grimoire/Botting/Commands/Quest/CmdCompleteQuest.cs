using Grimoire.Game;
using Newtonsoft.Json;
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
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ReAccept { get; set; } = false;
        public bool InBlank { get; set; } = false;

        public async Task Execute(IBotEngine instance)
        {
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.TryQuestComplete));
            bool provokeMons = instance.Configuration.ProvokeMonsters;

            //if (!Player.Quests.AcceptedQuests.Contains(Quest)) Quest.Accept();
            //if (Player.Quests.CanComplete(Quest.Id) && instance.IsRunning && Player.IsLoggedIn)
            //{
            //}
            if (provokeMons) instance.Configuration.ProvokeMonsters = false;
            if (instance.Configuration.ExitCombatBeforeQuest)
            {
                Player.MoveToCell(Player.Cell, Player.Pad);
                await instance.WaitUntil(() => Player.CurrentState != Player.State.InCombat);
                await Task.Delay(1000);
            }
            bool max = false;
            if (CompleteTry <= -1)
                max = true;

            Quest.Complete(CompleteTry,max);
            //await instance.WaitUntil(() => !Player.Quests.IsInProgress(Quest.Id));
            instance.Configuration.ProvokeMonsters = provokeMons;

            if (ReAccept)
            {
                await Task.Delay(1200);
                Quest.Accept();
            }
        }
        
        public override string ToString()
        {
            string quant = this.CompleteTry <= -1 ? "Ma" : CompleteTry.ToString();
            return $"Complete quest [{quant}x]: {(Quest.ItemId != null && Quest.ItemId != "0" ? $"{Quest.Id}^{Quest.ItemId}" : Quest.Id.ToString())} {(InBlank ? "[InBlank]" : "")}";
        }
    }
}