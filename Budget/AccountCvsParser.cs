using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Willowcat.Budget.Models;

namespace Willowcat.Budget
{
    public class AccountCsvParser
    {
        #region Member Variables...
        private List<string> _outputLines = new List<string>();
        #endregion Member Variables...

        #region Properties...
        public List<string> OutputLines
        {
            get { return _outputLines; }
        }
        #endregion Properties...

        #region FormatLine
        public string FormatLine(Dictionary<string, string> dictionary)
        {
            StringBuilder outputLine = new StringBuilder();
            //TODO: add try/catch
            DateTime date = DateTime.ParseExact(dictionary[AccountFields.Date], "MM/dd/yyyy", null);

            decimal credit = 0m;
            if (!string.IsNullOrEmpty(dictionary[AccountFields.AmountCredit]))
            {
                credit = decimal.Parse(dictionary[AccountFields.AmountCredit]);
            }

            decimal debit = 0m;
            if (!string.IsNullOrEmpty(dictionary[AccountFields.AmountDebit]))
            {
                debit = decimal.Parse(dictionary[AccountFields.AmountDebit]);
            };

            string description = dictionary[AccountFields.Description] + " " + dictionary[AccountFields.Memo];
            description = description.Replace("External Withdrawal ", "").Replace("Point Of Sale Withdrawal", "");
            //TODO: parse description

            outputLine.Append(date.ToString("MM/dd")).Append("\t");
            outputLine.Append(credit > 0 ? "INCOME" : "??").Append("\t");
            outputLine.Append(description).Append("\t");
            if (credit > 0)
            {
                outputLine.Append("+$").Append(credit);
            }
            else if (debit < 0)
            {
                outputLine.Append("$").Append(-1 * debit);
            }

            return outputLine.ToString();
        }
        #endregion FormatLine

        #region ParseFile
        public void ParseFile(string inputFilePath)
        {
            List<string> columnNames = null;
            _outputLines.Clear();

            foreach (var line in File.ReadAllLines(inputFilePath))
            {
                if (columnNames == null )
                {
                    if (line.StartsWith("\""))
                    {
                        //The bank files have some additional lines at the beginning 
                        //before the column definitions begin. So we're skipping those.
                        continue;
                    }
                    else
                    {
                        columnNames = line.Split(',').ToList();
                    }
                }
                else
                {
                    var hash = ParseLine(columnNames, line);
                    _outputLines.Add(FormatLine(hash));
                }
            }

            _outputLines.Reverse();
        }
        #endregion ParseFile

        #region ParseLine
        public Dictionary<string, string> ParseLine(List<string> columnNames, string line)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            StringBuilder builder = new StringBuilder();
            int quoteStartIndex = -1;
            int quoteEndIndex = -1;
            int columnIndex = 0;
            for (int index = 0; index < line.Length; index++)
            {
                if (columnIndex >= columnNames.Count)
                {
                    break;
                }

                char c = line[index];
                bool addNewField = false;
                if (quoteStartIndex < 0)
                {
                    if (c == '"')
                    {
                        quoteStartIndex = index;
                    }
                    else if (c == ',')
                    {
                        addNewField = true;
                    }
                    else
                    {
                        builder.Append(c);
                    }
                }
                else
                {
                    if (c == '"' && quoteEndIndex < 0)
                    {
                        quoteEndIndex = index;
                    }
                    else if (c == ',' && quoteEndIndex >= 0)
                    {
                        addNewField = true;
                    }
                    else
                    {
                        builder.Append(c);
                        quoteEndIndex = -1;
                    }
                }

                if (addNewField)
                {
                    dictionary[columnNames[columnIndex]] = builder.ToString();

                    //Reset and advance column index
                    quoteStartIndex = -1;
                    quoteEndIndex = -1;
                    builder.Clear();
                    columnIndex++;
                }
            }


            if (columnIndex + 1 == columnNames.Count)
            {
                dictionary[columnNames[columnIndex]] = builder.ToString();
            }
            return dictionary;
        }
        #endregion ParseLine
    }
}
