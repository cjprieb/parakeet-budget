using System;
using System.Collections.Generic;
using System.Linq;
using Budget;
using Budget.Models;

namespace TestBudget
{
    public class AccountCsvParserUnitTest
    {
        #region ParseAndFormatDebitLine
        public void ParseAndFormatDebitLine()
        {
            string[] columnNames = { "Transaction Number", "Date", "Description", "Memo", "Amount Debit", "Amount Credit", "Balance", "Check Number", "Fees" };
            string line = "\"20160919000000[-6:CST]*-5.15*0**External ,Withdrawal\",09/16/2016,\"Point ,\"\"of sale\",\"s\"\",sss\",-5.15,,\"4444.55\",,";
            string expectedOutput = "09/16\t??\tPoint ,\"of sale s\",sss\t$5.15";

            var parser = new AccountCsvParser();

            var lineValues = parser.ParseLine(columnNames.ToList(), line);
            var failures = 0;
            if (!AssertColumnEquals(lineValues, AccountFields.TransactionNumber, "20160919000000[-6:CST]*-5.15*0**External ,Withdrawal"))
            {
                failures++;
            }
            if (!AssertColumnEquals(lineValues, AccountFields.Date, "09/16/2016"))
            {
                failures++;
            }
            if (!AssertColumnEquals(lineValues, AccountFields.Description, "Point ,\"of sale"))
            {
                failures++;
            }
            if (!AssertColumnEquals(lineValues, AccountFields.Memo, "s\",sss"))
            {
                failures++;
            }
            if (!AssertColumnEquals(lineValues, AccountFields.AmountDebit, "-5.15"))
            {
                failures++;
            }
            if (!AssertColumnEquals(lineValues, AccountFields.AmountCredit, ""))
            {
                failures++;
            }
            if (!AssertColumnEquals(lineValues, AccountFields.Balance, "4444.55"))
            {
                failures++;
            }
            if (!AssertColumnEquals(lineValues, AccountFields.CheckNumber, ""))
            {
                failures++;
            }
            if (!AssertColumnEquals(lineValues, AccountFields.Fees, ""))
            {
                failures++;
            }

            var outputLine = parser.FormatLine(lineValues);
            if (!AssertEquals(expectedOutput, outputLine))
            {
                failures++;
            }


            if (failures == 1)
            {
                Console.WriteLine($"One test failed.");
            }
            else if (failures > 1)
            {
                Console.WriteLine($"{failures} tests failed.");
            }
            else
            {
                Console.WriteLine("All tests passed!");
            }
        }
        #endregion ParseAndFormatDebitLine

        #region AssertColumnEquals
        private bool AssertColumnEquals(Dictionary<string, string> result, string columnName, string expectedValue)
        {
            bool success = false;
            if (result.ContainsKey(columnName))
            {
                if (result[columnName].Equals(expectedValue))
                {
                    success = true;
                    Console.WriteLine($"\tSuccess! {columnName} is '{expectedValue}'");
                }
                else
                {
                    Console.WriteLine($"\tFailed! Value at {columnName} '{result[columnName]}' does not match expected value '{expectedValue}");
                }
            }
            else
            {
                Console.WriteLine($"\tFailed! No {columnName} in result");
            }
            return success;
        }
        #endregion AssertColumnEquals

        #region AssertEquals
        private bool AssertEquals(string expectedValue, string actualValue)
        {
            bool success = false;
            if (expectedValue.Equals(actualValue))
            {
                Console.WriteLine($"\tSuccess! Expected value matched actual value\n\t\t'{expectedValue}'");
                success = true;
            }
            else
            {
                Console.WriteLine($"\tFailed! Expected \n\t\t'{expectedValue}' \n\tbut was \n\t\t'{actualValue}'");
            }
            return success;
        }
        #endregion AssertEquals
    }
}
