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

        public async Task Execute(IBotEngine instance)
        {
            switch (state)
            {
                case State.On:
                    break;
                case State.Off:
                    break;
                case State.Toggle:
                    break;
                case State.Add:
                    break;
                case State.Remove:
                    break;
            }
        }

        public override string ToString()
        {
            return "Bank swap {" + state + "}";
        }
    }
}