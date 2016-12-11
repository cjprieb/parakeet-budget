using System;
using System.IO;

namespace Budget
{
    public class BudgetManager
    {
        BudgetTextParser _Parser = new BudgetTextParser();

        public PaycheckPeriodBudget ParseBudgetFile(string filePath)
        {
            return _Parser.ParseFile(filePath);
        }

        public void ParseCsvFile(string exportFile, string outputFile)
        {
            AccountCsvParser csvParser = new AccountCsvParser();
            csvParser.ParseFile(exportFile);
            File.WriteAllLines(outputFile, csvParser.OutputLines);
        }

        public void WriteBudgetFile(PaycheckPeriodBudget budget, string outputFile)
        {
            _Parser.WriteToFile(budget, outputFile);
        }
    }
}
