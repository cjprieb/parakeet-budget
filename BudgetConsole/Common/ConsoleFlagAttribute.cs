using System;

namespace BudgetConsole.Common
{
    public class ConsoleFlagAttribute : Attribute
    {
        private string _name = null;

        public string FlagName
        {
            get { return _name; }
            set { _name = value; }
        } 

        public ConsoleFlagAttribute()
        {

        }

        public ConsoleFlagAttribute(string name)
        {
            _name = name;
        }
    }
}
