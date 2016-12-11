namespace Willowcat.BudgetConsole
{
    //public class AccountTxtParser
    //{
    //    #region ParseFile
    //    public AccountData ParseFile(string inputFilePath)
    //    {
    //        Match fileDateMatch = Regex.Match(inputFilePath, @"budget (\d{2} \d{2} \d{4}).txt");
    //        if (!fileDateMatch.Success)
    //        {
    //            throw new ArgumentException($"'{inputFilePath}' does not contain a date of the format 'MM dd yyyy'");
    //        }

    //        DateTime budgetStartDate = DateTime.ParseExact(fileDateMatch.Groups[1].Value, "MM dd yyyy", CultureInfo.InvariantCulture);

    //        AccountData data = new AccountData();
    //        data.StartingDate = budgetStartDate;
    //        int lineIndex = 0;

    //        foreach (var line in File.ReadAllLines(inputFilePath, Encoding.UTF8))
    //        {
    //            lineIndex++;
    //            if (line.StartsWith("#category"))
    //            {
    //                Match categoryMatch = Regex.Match(line, @"#category=([\w\ ])|(-?[\d]+)|(-?[\d]+)");
    //                if (!categoryMatch.Success)
    //                {
    //                    throw new Exception($"'{line}' does not match expected category pattern ({inputFilePath}:{lineIndex}).");
    //                }
    //                string categoryName = categoryMatch.Groups[1].Value;
    //                decimal budgetAmount = decimal.Parse(categoryMatch.Groups[2].Value);
    //                decimal startingAmount = decimal.Parse(categoryMatch.Groups[3].Value);

    //                if (categoryName.Equals("all", StringComparison.CurrentCultureIgnoreCase))
    //                {
    //                    data.TotalStartingAmount = startingAmount;
    //                    data.TotalBudgetAmount = budgetAmount;
    //                }
    //                else
    //                {
    //                    data.CategoryOrder.Add(categoryName);
    //                    data.Categories[categoryName] = new Category()
    //                    {
    //                        PaycheckAmount = budgetAmount,
    //                        Name = categoryName,
    //                        StartingAmount = startingAmount,
    //                    }; ;
    //                }
    //            }
    //            else if (line.StartsWith("#"))
    //            {
    //                data.Notes.Add(line);
    //            }
    //            else
    //            {
    //                string[] parts = line.Split('\t');
    //                if ( parts.Length != 4 )
    //                {
    //                    throw new Exception($"Expected 4 fields in '{line}', but found {parts.Length} ({inputFilePath}:{lineIndex}).");
    //                }

    //                string dateString = parts[0];
    //                string categoryName = parts[1];
    //                string description = parts[2];
    //                string amountString = parts[3];

    //                //Parse Date
    //                Match dateMatch = Regex.Match(dateString, @"(\d+)\\(\d+)");
    //                if (!dateMatch.Success)
    //                {
    //                    throw new Exception($"'{dateString}' does not match expected date pattern ({inputFilePath}:{lineIndex}).");
    //                }
    //                DateTime date = ParseDate(dateMatch.Groups[1].Value, dateMatch.Groups[2].Value, budgetStartDate);

    //                //Parse Amount
    //                Match amountMatch = Regex.Match(amountString, @"(\+)?$(\d*\.\d*)");
    //                if (!amountMatch.Success)
    //                {
    //                    throw new Exception($"'{amountString}' does not match expected amount pattern ({inputFilePath}:{lineIndex}).");
    //                }
    //                bool isCredit = !string.IsNullOrEmpty(amountMatch.Groups[1].Value);
    //                decimal amount = decimal.Parse(amountMatch.Groups[2].Value);

    //                if (!data.Categories.ContainsKey(categoryName))
    //                {
    //                    data.CategoryOrder.Add(categoryName);
    //                    data.Categories[categoryName] = new Category()
    //                    {
    //                        Name = categoryName
    //                    };
    //                }
    //                data.BudgetLines.Add(new BudgetLine()
    //                {
    //                    Amount = amount,
    //                    BudgetType = isCredit ? BudgetType.Credit : BudgetType.Debit,
    //                    Category = categoryName,
    //                    Date = date,
    //                    Description = description
    //                });
    //            }
    //        }

    //        return data;
    //    }

    //    private DateTime ParseDate(string monthString, string dayString, DateTime budgetStartDate)
    //    {
    //        int month = int.Parse(monthString);
    //        int day = int.Parse(dayString);
    //        DateTime date;
    //        if ( month < budgetStartDate.Month )
    //        {
    //            date = new DateTime(budgetStartDate.Year + 1, month, day);
    //        }
    //        else
    //        {
    //            date = new DateTime(budgetStartDate.Year, month, day);
    //        }
    //        return date;
    //    }
    //    #endregion

    //    #region WriteFile
    //    public void WriteFile(AccountData data, string outputFilePath)
    //    {
    //        using(StreamWriter writer = new StreamWriter(outputFilePath, false, Encoding.UTF8))
    //        {
    //            //Write Categories
    //            writer.WriteLine(GetCategoryLine("all", data.TotalBudgetAmount, data.TotalStartingAmount));
    //            foreach (var categoryName in data.CategoryOrder)
    //            {
    //                var category = data.Categories[categoryName];
    //                writer.WriteLine(GetCategoryLine(categoryName, category.PaycheckAmount, category.StartingAmount));
    //            }

    //            //Write Notes
    //            foreach (var note in data.Notes)
    //            {
    //                writer.WriteLine(note);
    //            }

    //            //Write Budget Lines
    //            foreach (var budgetLine in data.BudgetLines)
    //            {
    //                writer.WriteLine(GetBudgetLine(budgetLine));
    //            }
    //        }
    //    }
    //    #endregion

    //    #region GetBudgetLine
    //    private string GetBudgetLine(BudgetLine budget)
    //    {
    //        StringBuilder builder = new StringBuilder();
    //        builder.Append(budget.Date.ToString("MM/dd")).Append("\t");
    //        builder.Append(budget.Category).Append("\t");
    //        builder.Append(budget.Description).Append("\t");
    //        if (budget.BudgetType == BudgetType.Credit)
    //        {
    //            builder.Append("+");
    //        }
    //        builder.Append("$").Append(budget.Amount.ToString("0.00"));
    //        return builder.ToString();
    //    }
    //    #endregion

    //    #region GetCategoryLine
    //    private string GetCategoryLine(string categoryName, decimal budgetAmount, decimal startingAmount)
    //    {
    //        return $"#category{categoryName}|{budgetAmount.ToString("F0")}|{startingAmount.ToString("F0")}";
    //    }
    //    #endregion
    //}

    class Program
    {
        static void Main(string[] args)
        {
            //var tester = new TestAccountCsvParser();
            //tester.ParseAndFormatDebitLine();

            //string inputFilePath = "Export.csv";
            //string outputFilePath = "output.txt";
            //var parser = new AccountCsvParser();
            //parser.ParseFile(inputFilePath);
            //File.WriteAllLines(outputFilePath, parser.OutputLines);

            var budgetConsole = new BudgetConsole();

            RepeatType repeat = RepeatType.RepeatOptions;
            while(repeat != RepeatType.NoRepeat)
            {
                repeat = budgetConsole.Run(repeat);
            }

            //Console.Write("Press any key to quit...");
            //Console.Read();
        }
    }
}
