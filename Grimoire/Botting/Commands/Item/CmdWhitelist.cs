using Grimoire.Game;
using Grimoire.UI;
using System;
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
            string ItemName = (instance.IsVar(this.ItemName) ? Configuration.Tempvariable[instance.GetVar(this.ItemName)] : this.ItemName);
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
            Console.WriteLine($"Drops: {instance.Configuration.Drops.Count}");
        }

        public override string ToString()
        {
            return $"Whitelist {(ItemNameOrNot() ? $"{state}: {ItemName}" : state.ToString())}";
        }
    }
}