using System.Text.RegularExpressions;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class StatementCommand
    {
        public string Tag
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public string Value1
        {
            get;
            set;
        }

        public string Value2
        {
            get;
            set;
        }

        public string Description1
        {
            get;
            set;
        }

        public string Description2
        {
            get;
            set;
        }

        public bool IsVar(string value)
        {
            return Regex.IsMatch(Value1, @"\[([^\)]*)\]");
        }

        public string GetVar(string value)
        {
            return Regex.Replace(value, @"[\[\]']+", "");
        }
    }
}