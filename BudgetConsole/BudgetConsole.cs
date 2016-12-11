using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Budget;
using Budget.Models;
using BudgetConsole.Common;

namespace BudgetConsole
{ 
    public enum RepeatType
    {
        NoRepeat, RepeatCommand, RepeatOptions
    }

    public class BudgetConsole
    {
        private IPayDateCalculator _PayDateCalculator = new BiMonthlyPayDate(15, BiMonthlyPayDate.EndOfMonth);
        private BudgetManager _Manager = null;
        private ConsoleCommand[] _Commands = null;
        private ConsoleCommand _LastCommand = null;
        private RepeatType _Repeat = RepeatType.NoRepeat;

        protected string CurrentDirectory
        {
            get
            {
                return Properties.Settings.Default.CurrentDirectory;
            }

            set
            {
                Properties.Settings.Default.CurrentDirectory = value;
            }
        }

        public BudgetConsole()
        {
            _Commands = new ConsoleCommand[]
            {
                new ConsoleCommand("1", "View budget file", ViewBudgetFile),
                //new ConsoleCommand("2", "Calculate weekly budget", CalculateWeeklyBudget),
                new ConsoleCommand("3", "Calculate next budget file", CalculateNextBudgetFile),
                new ConsoleCommand("4", "Search budget files", SearchBudgetFiles),
                new ConsoleCommand("5", "Parse CSV file [-out <path>] [-in <path>]", ParseCsvFile),
                new ConsoleCommand("cd", "Change directory [<path>]", ChangeDirectory),
                new ConsoleCommand("li", "List directories and files in current directory", ListDirectoryContents),
                new ConsoleCommand("q", "Quit", QuitAction)
            };
            _Manager = new BudgetManager();
            if ( string.IsNullOrEmpty(CurrentDirectory) )
            {
                CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            }
        }

        //TODO: Write help text for arg use
        private void ParseCsvFile(string arguments)
        {
            var csvArgs = ConsoleExtensions.ParseArguments<CsvFileParseArguments>(arguments);
            string inputFileName = !string.IsNullOrEmpty(csvArgs.InputFileName) ? csvArgs.InputFileName : "Export.csv";
            string inputFile = Path.Combine(CurrentDirectory, inputFileName);
            if (File.Exists(inputFile))
            {
                string outputFileName = !string.IsNullOrEmpty(csvArgs.OutputFileName) ? csvArgs.OutputFileName : "output.txt";
                string outputFile = Path.Combine(CurrentDirectory, outputFileName);
                _Manager.ParseCsvFile(inputFile, outputFile);
                Console.WriteLine($"{inputFile} parsed and saved as {outputFile}");
            }
            else
            {
                Console.WriteLine($"Error! {inputFile} doesn't exist");
            }
        }

        private void QuitAction(string arguments)
        {
            Properties.Settings.Default.Save();
            _Repeat = RepeatType.NoRepeat;
        }

        public RepeatType Run(RepeatType lastRepeat)
        {
            _Repeat = RepeatType.RepeatOptions;
            ConsoleCommand selectedCommand = null;
            string userInput = null;

            if (lastRepeat == RepeatType.RepeatCommand)
            {
                selectedCommand = _LastCommand;
            }
            else if (lastRepeat == RepeatType.RepeatOptions)
            {
                if (_LastCommand == null)
                {
                }

                Console.WriteLine("Current directory: " + CurrentDirectory);
                foreach (var command in _Commands)
                {
                    var selector = $"[{command.Selector}]";
                    Console.WriteLine($"  {selector,-6} {command.Description}");
                }
                Console.Write("What do you want to do? ");
                userInput = Console.ReadLine().Trim();
                selectedCommand = _Commands.FirstOrDefault(cmd => userInput.StartsWith(cmd.Selector, StringComparison.CurrentCultureIgnoreCase));

                //TODO: Only clear console for certain commands
                Console.Clear();
            }

            if (selectedCommand != null)
            {
                _LastCommand = selectedCommand;
                string arguments = null;
                if (!string.IsNullOrEmpty(userInput))
                {
                    arguments = userInput.Substring(selectedCommand.Selector.Length).Trim();
                }
                selectedCommand.ProcessCommand(arguments);
            }
            else
            {
                Console.WriteLine($"'{userInput}' is an unknown command. Please try another option.");
            }

            return _Repeat;
        }

        #region Commands...
        private void CalculateWeeklyBudget(string argument)
        {
        }

        private void CalculateNextBudgetFile(string argument)
        {
            Console.WriteLine("** Calculate Next Budget File **");
            Console.WriteLine("Enter date of file to open");
            Console.WriteLine(" or [current] for the most recent file");
            Console.WriteLine(" or [b] to go back");
            Console.Write(": ");
            string dateString = Console.ReadLine().Trim();

            if (dateString.StartsWith("b"))
            {
                _Repeat = RepeatType.RepeatOptions;
                Console.Clear();
            }
            else
            {
                string filePath = BuildFilePathFromInput(dateString);
                if (!string.IsNullOrEmpty(filePath))
                {
                    PaycheckPeriodBudget accountData = _Manager.ParseBudgetFile(filePath);
                    DateTime nextDate = _PayDateCalculator.GetNextPayDate(accountData.StartingDate, false);
                    PaycheckPeriodBudget newAccountData = accountData.GenerateNextPayPeriod(nextDate);
                    string outputPath = Path.Combine(CurrentDirectory, $"budget {nextDate.ToString("yyyy MM dd")}.txt");
                    Console.WriteLine($"Writing budget file for {nextDate.ToShortDateString()} to {outputPath}");
                    _Manager.WriteBudgetFile(newAccountData, outputPath);
                }
            }
        }

        private void ChangeDirectory(string argument)
        {
            if (!string.IsNullOrEmpty(argument))
            {
                string newDirectory = Path.Combine(CurrentDirectory, argument);
                try
                {
                    newDirectory = Path.GetFullPath(newDirectory);
                    if (Directory.Exists(newDirectory))
                    {
                        CurrentDirectory = newDirectory;
                    }
                    else
                    {
                        Console.WriteLine($"Directory {newDirectory} does not exist. Not changing directories.");
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine($"Unable to switch to {newDirectory}: {e.Message}. Not changing directories.");
                }
            }
        }
        private void ListDirectoryContents(string argument)
        {
            Console.WriteLine($"Contents of {CurrentDirectory}");
            foreach ( var directory in Directory.EnumerateDirectories(CurrentDirectory) )
            {
                Console.WriteLine("  " + directory);
            }
                
            foreach (var file in Directory.EnumerateFiles(CurrentDirectory, "*.txt"))
            {
                Console.WriteLine("  " + Path.GetFileName(file));
            }

            Console.WriteLine();
        }

        private void SearchBudgetFiles(string argument)
        {
            Console.Write("Type search term: ");
            string searchTerm = Console.ReadLine();
            string searchTermLowerCase = searchTerm.ToLower();
            Console.WriteLine();

            Console.WriteLine($"Searching for matches in {CurrentDirectory}");
            foreach (string directory in Directory.EnumerateDirectories(CurrentDirectory))
            {
                SearchDirectory(directory, searchTermLowerCase);
            }

            SearchDirectory(CurrentDirectory, searchTermLowerCase);
        }

        private void ViewBudgetFile(string argument)
        {
            Console.WriteLine("** Check Budget **");
            Console.WriteLine("Enter date of file to open");
            Console.WriteLine(" or [current] for the most recent file");
            Console.WriteLine(" or [b] to go back");
            Console.Write(": ");
            string dateString = Console.ReadLine().Trim();

            if (dateString.StartsWith("b"))
            {
                _Repeat = RepeatType.RepeatOptions;
                Console.Clear();
            }
            else
            {
                string filePath = BuildFilePathFromInput(dateString);
                if (!string.IsNullOrEmpty(filePath))
                {
                    PaycheckPeriodBudget accountData = _Manager.ParseBudgetFile(filePath);
                    Console.WriteLine("Current Categories");
                    decimal totalSpent = 0;
                    decimal paycheckTotal = 0;
                    foreach ( var categoryName in accountData.CategoryOrder )
                    {
                        if (accountData.Categories.ContainsKey(categoryName))
                        {
                            var category = accountData.Categories[categoryName];
                            decimal totalSpentInCategory = category.GetSumOfCategoryTransactions(accountData.LineItems);
                            decimal newCategoryValue = category.StartingBalance + totalSpentInCategory;
                            totalSpent += totalSpentInCategory;
                            paycheckTotal += category.PaycheckBudget;
                            if (totalSpentInCategory != 0)
                            {
                                Console.WriteLine($"{categoryName,-10}\t{totalSpentInCategory.ToString("F2"),8}\t{category.PaycheckBudget.ToString("F2"),8}\t{newCategoryValue.ToString("F2"),8}");
                            }
                            else
                            {

                                Console.WriteLine($"{categoryName,-10}\t{string.Empty,8}\t{category.PaycheckBudget.ToString("F2"),8}\t{newCategoryValue.ToString("F2"),8}");
                            }
                        }
                        else
                        {
                            decimal totalSpentInCategory = accountData.LineItems.Sum(item => item.CategoryAmount.ContainsKey(categoryName) ? item.CategoryAmount[categoryName] : 0);
                            Console.WriteLine($"{categoryName,-10}\t{string.Empty,8}\t{string.Empty,8}\t{totalSpentInCategory.ToString("F2"),8}");
                        }
                    }
                    Console.WriteLine(new string('-', 58));
                    Console.WriteLine($"{("Total"),-10}\t{totalSpent.ToString("F2"),8}\t{paycheckTotal.ToString("F2"),8}");
                }
            }
        }

        #endregion Commands...

        #region BuildFilePathFromInput
        private string BuildFilePathFromInput(string dateString)
        {
            string fileName = null;
            string filePath = null;
            if (dateString.Equals("current", StringComparison.CurrentCultureIgnoreCase))
            {
                DateTime current = _PayDateCalculator.GetMostRecentPayDate();
                fileName = $"budget {current.ToString("yyyy MM dd")}.txt";
                //TODO: get earlier date if selected one doesn't exist.
            }
            else
            {
                DateTime date = DateTime.Parse(dateString, CultureInfo.CurrentCulture);
                fileName = $"budget {date.ToString("yyyy MM dd")}.txt";
            }

            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine($"Unable to generate fileName from input '{dateString}'.\n");
                _Repeat = RepeatType.RepeatCommand;
            }
            else
            {
                filePath = Path.Combine(CurrentDirectory, fileName);
                if (File.Exists(filePath))
                {
                    Console.WriteLine($"Opening {filePath}...");
                    _Repeat = RepeatType.RepeatOptions;
                }
                else
                {
                    Console.WriteLine($"{filePath} does not exist.\n");
                    _Repeat = RepeatType.RepeatCommand;
                    filePath = null;
                }
            }
            return filePath;
        }
        #endregion BuildFilePathFromInput

        #region SearchDirectory
        private void SearchDirectory(string directory, string searchTerm)
        {
            Regex regex = null;
            try
            {
                regex = new Regex(searchTerm);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Unable to parse {searchTerm} as a regex: " + ex.Message);
                return;
            }

            foreach (var filePath in Directory.EnumerateFiles(directory))
                {
                    if (Regex.IsMatch(filePath, @"budget \d{4} \d{2} \d{2}.txt$"))
                    {
                        bool fileNamePrinted = false;
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            string line = reader.ReadLine();
                            while (line != null)
                            {
                                string text = line.ToLower();
                                if (!text.StartsWith("#category=") && regex.IsMatch(text))
                                {
                                    if (!fileNamePrinted)
                                    {
                                        Console.WriteLine(filePath);
                                        fileNamePrinted = true;
                                    }
                                    Console.WriteLine("\t" + line);
                                }
                                line = reader.ReadLine();
                            }
                        }
                    }
                }
        }
        #endregion SearchDirectory
    }
}
