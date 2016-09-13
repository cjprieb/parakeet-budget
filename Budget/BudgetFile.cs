using Budget.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Budget
{
    public class BudgetFile
    {
        private static Regex _FileDatePattern = new Regex(@"(\d{4}) (\d{2}) (\d{2})");
        private static Regex _SettingPattern = new Regex(@"#(\w+)=(.+)");
        private static Regex _CategoryPattern = new Regex(@"#category=(\w+)\|(\d+)\|(\d+)");
        private static Regex _BudgetLinePattern = new Regex(@"(\d+)/(\d+)\t(\w+)\t([^\t]+)\t(\+?)\$([\d\.\-]+)");

        private string _filePath = null;

        public BudgetFile(string filePath)
        {
            if (filePath == null)
            {
                throw new NullReferenceException("filePath cannot be null");
            }
            _filePath = filePath;
        }

        #region Public methods
        public PaycheckPeriodBudget ParseFile()
        {            
            var FileDateMatch = _FileDatePattern.Match(_filePath);
            if ( !FileDateMatch.Success )
            {
                throw new ArgumentException($"'{_filePath}' must contain a date in the format 'yyyy mm dd'");
            }

            var Year = int.Parse(FileDateMatch.Groups[1].Value);
            var Month = int.Parse(FileDateMatch.Groups[2].Value);
            var Date = int.Parse(FileDateMatch.Groups[3].Value);
            var Budget = new PaycheckPeriodBudget(new DateTime(Year, Month, Date));

            var LineCount = 0;
            var PreviousMonth = 0;
            var CurrentLine = "";
            BudgetLine CurrentBudgetLine = null;
            foreach ( var line in File.ReadAllLines(_filePath) )
            {
                CurrentLine = line;
                LineCount++;

                if ( line.StartsWith("#") )
                {
                    var CategoryMatch = _CategoryPattern.Match(line);
                    if (CategoryMatch.Success)
                    {
                        try
                        {
                            var CategoryName = CategoryMatch.Groups[1].Value;
                            var BudgetAmountValue = CategoryMatch.Groups[2].Value;
                            decimal BudgetAmount = 0;
                            if ( BudgetAmountValue.Length > 0 )
                            {
                                BudgetAmount = decimal.Parse(BudgetAmountValue);
                            }

                            var BalanceAmountValue = CategoryMatch.Groups[3].Value;
                            decimal BalanceAmount = 0;
                            if (BudgetAmountValue.Length > 0)
                            {
                                BalanceAmount = decimal.Parse(BalanceAmountValue);
                            }

                            if (CategoryName == "all")
                            {
                                Budget.PayPeriodTotal = BudgetAmount;
                                Budget.BalanceTotal = BalanceAmount;
                            }
                            else
                            {
                                Budget.CategoryOrder.Add(CategoryName);
                                Budget.Categories[CategoryName] = new BudgetCategory()
                                {
                                    Name = CategoryName,
                                    PaycheckBudget = BudgetAmount,
                                    StartingBalance = BalanceAmount,
                                    StartingDate = Budget.StartingDate
                                };
                            }
                        }
                        catch (FormatException ex)
                        {
                            throw new BudgetParseException($"Invalid format in {_filePath}: '{line}'", LineCount, ex);
                        }
                    }
                    else
                    {
                        //var SettingMatch = _SettingPattern.Match(line);
                        //if ( SettingMatch.Success )
                        //{
                        //    var Key = SettingMatch.Groups[1].Value;
                        //    var Value = SettingMatch.Groups[2].Value;
                        //}

                        Budget.Settings.Add(line);
                    }
                }
                else
                {
                    var LineItemMatch = _BudgetLinePattern.Match(line);
                    if ( LineItemMatch.Success )
                    {
                        Month = int.Parse(LineItemMatch.Groups[1].Value);
                        Date = int.Parse(LineItemMatch.Groups[2].Value);
                        if (PreviousMonth > Month)
                        {
                            Year = Year + 1;
                        }

                        var CategoryName = LineItemMatch.Groups[3].Value;
                        var Description = LineItemMatch.Groups[4].Value;
                        var isPositive = LineItemMatch.Groups[5].Value == "+";
                        var AmountString = LineItemMatch.Groups[6].Value;
                        decimal Amount = 0;

                        if (!decimal.TryParse(AmountString, out Amount))
                        {
                            var message = $"Amount for { Description } in {_filePath} must be a valid decimal. [{ AmountString }] could not be parsed.";
                            throw new BudgetParseException(message, LineCount);
                        }
                        Amount = (isPositive ? 1 : -1) * Amount;

                        if (CurrentBudgetLine == null
                            || CurrentBudgetLine.Description != Description
                            || CurrentBudgetLine.CategoryAmount.ContainsKey(CategoryName))
                        {
                            CurrentBudgetLine = new BudgetLine()
                            {
                                Date = new DateTime(Year, Month, Date),
                                CategoryAmount = new Dictionary<string, decimal>(),
                                Description = Description
                            };
                            Budget.LineItems.Add(CurrentBudgetLine);
                        }
                        CurrentBudgetLine.CategoryAmount[CategoryName] = Amount;

                        PreviousMonth = Month;
                    }
                    else if ( !string.IsNullOrEmpty(line) ) 
                    {
                        throw new BudgetParseException($"Invalid line in {_filePath}: '{line}'", LineCount);
                    }
                }
            }
            return Budget;
        }

        public void WriteToFile(PaycheckPeriodBudget budget, string filePath = null)
        {
            using (StreamWriter writer = new StreamWriter(filePath ?? _filePath, false))
            {
                writer.WriteLine($"#category=all|{budget.PayPeriodTotal}|{budget.BalanceTotal}");
                foreach ( var categoryName in budget.CategoryOrder )
                {
                    var category = budget.Categories[categoryName];
                    writer.WriteLine($"#category={categoryName}|{category.PaycheckBudget}|{category.StartingBalance}");
                }

                foreach ( var setting in budget.Settings )
                {
                    writer.WriteLine(setting);
                }

                foreach ( var line in budget.LineItems )
                {
                    foreach (var category in line.CategoryAmount.Keys)
                    {
                        var amount = line.CategoryAmount[category];
                        var plusSign = amount > 0 ? "+" : "";
                        amount = amount > 0 ? amount :  - 1 * amount; //negative amounts are stored without the '-'
                        writer.WriteLine($"{line.Date.ToString("MM/dd")}\t{category}\t{line.Description}\t{plusSign}${amount.ToString("0.00")}");
                    }
                }
            }
        }

        public void WriteToCsvFile(PaycheckPeriodBudget budget)
        {
            throw new NotImplementedException();
        }
        #endregion Public methods
    }
}
