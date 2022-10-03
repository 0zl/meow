using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdIndex : IBotCommand
    {
        public enum IndexCommand
        {
            Up,
            Down,
            Goto
        }

        public IndexCommand Type
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public string IndexString
        {
            get;
            set;
        } = "";

        public Task Execute(IBotEngine instance)
        {
            int Index = this.Index;
            if (IndexString.Length > 0) {
                string IndexString = (instance.IsVar(this.IndexString) ? Configuration.Tempvariable[instance.GetVar(this.IndexString)] : this.IndexString);
                if (int.TryParse(IndexString, out int i))
                {
                    Index = i;
                }
            }

            switch (Type)
            {
                case IndexCommand.Down:
                    {
                        int num3 = Index - 1;
                        if (num3 > 0)
                        {
                            int num4 = instance.Index += num3;
                            if (num4 < instance.Configuration.Commands.Count)
                            {
                                instance.Index = num4;
                            }
                        }
                        break;
                    }
                case IndexCommand.Up:
                    {
                        int num = Index + 1;
                        if (num > 0)
                        {
                            int num2 = instance.Index -= num;
                            if (num2 > -1)
                            {
                                instance.Index = num2;
                            }
                        }
                        break;
                    }
                case IndexCommand.Goto:
                    {
                        int num = Index - 1;
                        instance.Index = num;
                        break;
                    }
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            switch (Type)
            {
                case IndexCommand.Down:
                    return $"Index down: {Index}";

                case IndexCommand.Up:
                    return $"Index up: {Index}";

                default:
                    return "Goto index: " + (IndexString.Length > 0 ? IndexString : Index.ToString());
            }
        }
    }
}