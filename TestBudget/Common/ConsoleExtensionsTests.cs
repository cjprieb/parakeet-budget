using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Willowcat.BudgetConsole;
using Willowcat.Common;

namespace Willowcat.BudgetConsole.Common.Tests
{
    [TestClass]
    public class ConsoleExtensionsTests
    {
        [TestMethod]
        public void ConsoleArgumentsTest()
        {
            //Setup
            string args = "-out \"a sample \\\" file\" -in text.txt";

            //Action
            var commandArguments = new ConsoleArguments(args);

            //Assert
            Assert.AreEqual(2, commandArguments.Count(), "size mismatch");
            Assert.AreEqual("out", commandArguments[0].FlagName, "output FlagName mismatch");
            Assert.AreEqual("a sample \" file", commandArguments[0].Value, "output Value mismatch");
            Assert.AreEqual("in", commandArguments[1].FlagName, "input FlagName mismatch");
            Assert.AreEqual("text.txt", commandArguments[1].Value, "input Value mismatch");
        }

        [TestMethod]
        public void ParseArgumentsTest()
        {
            //Setup
            string args = "-out \"a sample \\\" file\" -in text.txt";

            //Action
            var commandArguments = ConsoleExtensions.ParseArguments<CsvFileParseArguments>(args);

            //Assert
            Assert.AreEqual("a sample \" file", commandArguments.OutputFileName, "output file mismatch");
            Assert.AreEqual("text.txt", commandArguments.InputFileName, "input file mismatch");
        }
    }
}