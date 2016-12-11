using System.Collections.Generic;
using System.Text;

namespace BudgetConsole.Common
{
    //TODO: move to common library
    public class ConsoleArguments : List<ConsoleArgument>
    {
        public ConsoleArguments(string args)
        {
            Parse(args);
        }

        private void Parse(string args)
        {
            args += " ";
            StringBuilder argument = new StringBuilder();
            ConsoleArgument currentArgument = null;
            bool insideQuote = false;
            bool escapeKeyFound = false;
            foreach (var c in args)
            {
                if (insideQuote)
                {
                    if (escapeKeyFound)
                    {
                        if (c == 't')
                        {
                            argument.Append('\t');
                        }
                        else if (c == 'n')
                        {
                            argument.Append('\n');
                        }
                        else
                        {
                            argument.Append(c);
                        }
                        escapeKeyFound = false;
                    }
                    else if (c == '\\')
                    {
                        escapeKeyFound = true;
                    }
                    else if (c == '"')
                    {
                        insideQuote = false;
                    }
                    else
                    {
                        argument.Append(c);
                    }
                }
                else
                {
                    if (c == '"')
                    {
                        insideQuote = true;
                    }
                    else if (c == ' ')
                    {
                        string str = argument.ToString();
                        argument.Clear();
                        if (str.StartsWith("-") && str.Length > 1)
                        {
                            currentArgument = new ConsoleArgument() { FlagName = str.Substring(1) };
                            Add(currentArgument);
                        }
                        else if (currentArgument != null)
                        {
                            currentArgument.Value = str;
                        }
                        else 
                        {
                            Add(new ConsoleArgument() { FlagName = "", Value = str });
                        }
                    }
                    else
                    {
                        argument.Append(c);
                    }
                }
            }
        }
    }

    public class ConsoleArgument
    {
        public string FlagName { get; set; }

        public string Value { get; set; }
    }
}
