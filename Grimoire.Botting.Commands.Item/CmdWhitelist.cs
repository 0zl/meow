using Grimoire.Game;
using Grimoire.UI;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdWhitelist : IBotCommand
    {
        public enum State
        {
            On,
            Off,
            Toggle,
            Add,
            Remove
        }

        public string ItemName
        {
            get;
            set;
        }

        public State state
        {
            get;
            set;
        }

        private bool ItemNameOrNot()
        {
            if (state == State.On || state == State.Off || state == State.Toggle) return false;
            else return true;
        }

        public async Task Execute(IBotEngine instance)
        {
            switch (state)
            {
                case State.On:
                    instance.Configuration.EnablePickup = true;
                    break;
                case State.Off:
                    instance.Configuration.EnablePickup = true;
                    break;
                case State.Toggle:
                    instance.Configuration.EnablePickup = !instance.Configuration.EnablePickup;
                    break;
                case State.Add:
                    instance.Configuration.Drops.Add(ItemName); 
                    break;
                case State.Remove:
                    instance.Configuration.Drops.Remove(ItemName);
                    break;
            }
        }

        public override string ToString()
        {
            return $"Whitelist {(ItemNameOrNot() ? $"{state.ToString()}: {ItemName}" : state.ToString())}";
        }
    }
}