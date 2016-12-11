using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Budget;

namespace TestBudget
{
    [TestClass]
    public class BudgetFileUnitTest
    {
        [TestMethod]
        public void TestParseOfExistingFile()
        {
            var filePath = @"C:\Users\Crystal\Documents\budget\budget 2016 01 01.txt";
            var budgetFile = new BudgetTextParser();
            var budget = budgetFile.ParseFile(filePath);
            
            Assert.AreEqual(budget.BalanceTotal, 31670);
            Assert.AreEqual(budget.PayPeriodTotal, 614);

            var category = budget.Categories["food"];
            Assert.AreEqual(category.StartingBalance, 58);
            Assert.AreEqual(category.PaycheckBudget, 38);

            category = budget.Categories["cats"];
            Assert.AreEqual(category.StartingBalance, 120);
            Assert.AreEqual(category.PaycheckBudget, 13);

            category = budget.Categories["nowork"];
            Assert.AreEqual(category.StartingBalance, 13898);
            Assert.AreEqual(category.PaycheckBudget, 150);



        }
    }
}
