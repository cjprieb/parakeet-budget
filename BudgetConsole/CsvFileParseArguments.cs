using Willowcat.Common;

namespace Willowcat.BudgetConsole
{
    public class CsvFileParseArguments
    {
        [ConsoleFlag("out")]
        public string OutputFileName { get; set; }

        [ConsoleFlag("in")]
        public string InputFileName { get; set; }
    }
}
