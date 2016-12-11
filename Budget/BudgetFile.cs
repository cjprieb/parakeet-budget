using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Budget.Models;

namespace Budget
{
    public class BudgetTextParser
    {
        private static Regex _FileDatePattern = new Regex(@"(\d{4}) (\d{2}) (\d{2})");
        private static Regex _SettingPattern = new Regex(@"#(\w+)=(.+)");
        private static Regex _CategoryPattern = new Regex(@"#category=(\w+)\|(\d+)\|([\-\d]+)");
        private static Regex _BudgetLinePattern = new Regex(@"(\d+)/(\d+)\t([\w\?]+)\t([^\t]+)\t(\+?)\$?([\d\.\-]+)");

        //private string _filePath = null;
        private List<string> _categoryFieldNamesForCsv = new List<string>();
        private int _fieldsAdded = 0;

        #region Public methods
        public PaycheckPeriodBudget ParseFile(string filePath)
        {
            if (filePath == null)
            {
                throw new NullReferenceException("filePath cannot be null");
            }
            var FileDateMatch = _FileDatePattern.Match(filePath);
            if ( !FileDateMatch.Success )
            {
                throw new ArgumentException($"'{filePath}' must contain a date in the format 'yyyy MM dd'");
            }

            var Year = int.Parse(FileDateMatch.Groups[1].Value);
            var Month = int.Parse(FileDateMatch.Groups[2].Value);
            var Date = int.Parse(FileDateMatch.Groups[3].Value);
            var Budget = new PaycheckPeriodBudget(new DateTime(Year, Month, Date));

            var LineCount = 0;
            var PreviousMonth = 0;
            var CurrentLine = "";
            BudgetLine CurrentBudgetLine = null;
            foreach ( var line in File.ReadAllLines(filePath) )
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
                            throw new BudgetParseException($"Invalid format in {filePath}: '{line}'", LineCount, ex);
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
                            var message = $"Amount for { Description } in {filePath} must be a valid decimal. [{ AmountString }] could not be parsed.";
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
                        throw new BudgetParseException($"Invalid line in {filePath}: '{line}'", LineCount);
                    }
                }
            }
            if ( !Budget.CategoryOrder.Contains("??") )
            {
                Budget.CategoryOrder.Add("??");
            }
            return Budget;
        }

        public void WriteToFile(PaycheckPeriodBudget budget, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine($"#category=all|{budget.PayPeriodTotal}|{budget.BalanceTotal}");
                foreach ( var categoryName in budget.CategoryOrder )
                {
                    if (budget.Categories.ContainsKey(categoryName))
                    {
                        var category = budget.Categories[categoryName];
                        writer.WriteLine($"#category={categoryName}|{category.PaycheckBudget}|{category.StartingBalance}");
                    }
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
        
        public void WriteToCsvFile(PaycheckPeriodBudget budget, string filePath, bool append)
        {
            using (StreamWriter writer = new StreamWriter(filePath, append))
            {
                List<string> values = new List<string>();
                if (!append)
                {
                    _fieldsAdded = 0;
                    _categoryFieldNamesForCsv.Clear();
                    var fieldNames = new List<string>();
                    fieldNames.Add("Date");
                    fieldNames.Add("Category");
                    fieldNames.Add("Description");
                    fieldNames.Add("Amount");
                    WriteCsvLine(writer, fieldNames);
                }
                
                budget.LineItems.Sort((a, b) => a.Date.CompareTo(b.Date));
                foreach (var line in budget.LineItems)
                {
                    foreach (var categoryName in line.CategoryAmount.Keys)
                    {
                        values.Clear();
                        values.Add(line.Date.ToShortDateString());
                        values.Add(categoryName);
                        values.Add(line.Description);
                        values.Add(line.CategoryAmount[categoryName].ToString());
                    }
                    WriteCsvLine(writer, values);
                }
            }
        }

        public void WriteToCsvFile_ByCategory(PaycheckPeriodBudget budget, string filePath, bool append)
        {
            using (StreamWriter writer = new StreamWriter(filePath, append))
            {
                List<string> values = new List<string>();
                if (!append)
                {
                    _fieldsAdded = 0;
                    _categoryFieldNamesForCsv.Clear();
                    var fieldNames = new List<string>();
                    fieldNames.Add("Date");
                    fieldNames.Add("Description");
                    foreach (var categoryName in budget.CategoryOrder)
                    {
                        fieldNames.Add(categoryName);
                        _categoryFieldNamesForCsv.Add(categoryName);
                    }
                    WriteCsvLine(writer, fieldNames);
                    
                    values.Add(budget.StartingDate.ToShortDateString());
                    values.Add("Starting Budget");
                    foreach (var categoryName in budget.CategoryOrder)
                    {
                        if (budget.Categories.ContainsKey(categoryName))
                        {
                            values.Add(budget.Categories[categoryName].StartingBalance.ToString());
                        }
                        else
                        {
                            values.Add("");
                        }
                    }
                }

                DateTime? maxDate = null;
                budget.LineItems.Sort((a, b) => a.Date.CompareTo(b.Date));
                foreach (var line in budget.LineItems)
                {
                    if (maxDate == null || maxDate.Value.CompareTo(line.Date) < 0)
                    {
                        maxDate = line.Date;
                    }
                    values.Clear();
                    values.Add(line.Date.ToShortDateString());
                    values.Add(line.Description);
                    foreach (var categoryName in _categoryFieldNamesForCsv)
                    {
                        if (line.CategoryAmount.ContainsKey(categoryName))
                        {
                            values.Add(line.CategoryAmount[categoryName].ToString());
                        }
                        else
                        {
                            values.Add("");
                        }
                    }

                    foreach (var categoryName in line.CategoryAmount.Keys)
                    {
                        if (!_categoryFieldNamesForCsv.Contains(categoryName))
                        {
                            _categoryFieldNamesForCsv.Add(categoryName);
                            var categoryAmount = line.CategoryAmount[categoryName];
                            values.Add(categoryAmount.ToString());
                        }
                    }
                    WriteCsvLine(writer, values);
                }

                if (maxDate.HasValue)
                {
                    values.Clear();
                    values.Add(maxDate.Value.ToShortDateString()); //TODO: get paycheck date more intellegently
                    values.Add("Paycheck");
                    foreach (var categoryName in _categoryFieldNamesForCsv)
                    {
                        if (budget.Categories.ContainsKey(categoryName))
                        {
                            values.Add(budget.Categories[categoryName].PaycheckBudget.ToString());
                        }
                        else
                        {
                            values.Add("");
                        }
                    }

                    foreach (var categoryName in budget.Categories.Keys)
                    {
                        if (!_categoryFieldNamesForCsv.Contains(categoryName))
                        {
                            _categoryFieldNamesForCsv.Add(categoryName);
                            var categoryAmount = budget.Categories[categoryName];
                            values.Add(categoryAmount.ToString());
                        }
                    }
                    WriteCsvLine(writer, values);
                }
            }
        }
        #endregion Public methods

        #region Unorganized methods
        private void WriteCsvLine(StreamWriter writer, List<string> values)
        {
            var isFirst = true;
            foreach (var value in values)
            {
                if (!isFirst)
                {
                    writer.Write(",");
                }
                if (value.Contains(",") || value.Contains("\""))
                {
                    // surround with double-quotes
                    writer.Write("\"");
                    writer.Write(value.Replace("\"", "\"\"")); // duplicate double-quotes
                    writer.Write("\"");
                }
                else
                {
                    writer.Write(value);
                }
                isFirst = false;
            }
            writer.Write(Environment.NewLine);
        }
        #endregion Unorganized methods
    }
}
