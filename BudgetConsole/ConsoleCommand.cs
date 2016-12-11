using System;

namespace BudgetConsole
{
    public class ConsoleCommand
    {
        public string Selector { get; private set; }
        public string Description { get; private set; }
        public Action<string> ProcessCommand { get; private set; }

        public ConsoleCommand(string command, string description, Action<string> action)
        {
            Selector = command;
            Description = description;
            ProcessCommand = action;
        }
    }
}
