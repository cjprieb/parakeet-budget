using System;
using Willowcat.Budget.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Willowcat.TestBudget
{
    [TestClass]
    public class PayDateCalculatorUnitTest
    {

        [TestMethod]
        public void TestBiMonthly_2016Sept27()
        {
            var calculator = new BiMonthlyPayDate(15, BiMonthlyPayDate.EndOfMonth);
            var sept27 = new DateTime(2016, 9, 27);
            var expectedDate = new DateTime(2016, 9, 30);

            var date = calculator.GetNextPayDate(sept27, true);
            Assert.AreEqual(expectedDate, date, "Expecting Sept 30, 2016 (allow current)");

            date = calculator.GetNextPayDate(sept27, false);
            Assert.AreEqual(expectedDate, date, "Expecting Sept 30, 2016 (don't allow current)");
        }

        [TestMethod]
        public void TestBiMonthly_2016Sept30()
        {
            var calculator = new BiMonthlyPayDate(15, BiMonthlyPayDate.EndOfMonth);
            var sept30 = new DateTime(2016, 9, 30);

            var date = calculator.GetNextPayDate(sept30, true);
            Assert.AreEqual(new DateTime(2016, 9, 30), date, "Expecting Sept 30, 2016 (allow current)");

            date = calculator.GetNextPayDate(sept30, false);
            Assert.AreEqual(new DateTime(2016, 10, 15), date, "Expecting Oct 15, 2016 (don't allow current)");
        }

        [TestMethod]
        public void TestBiMonthly_2016Oct1()
        {
            var calculator = new BiMonthlyPayDate(15, BiMonthlyPayDate.EndOfMonth);
            var oct1 = new DateTime(2016, 10, 1);

            var date = calculator.GetNextPayDate(oct1, true);
            Assert.AreEqual(new DateTime(2016, 10, 15), date, "Expecting Oct 15, 2016 (allow current)");

            date = calculator.GetNextPayDate(oct1, false);
            Assert.AreEqual(new DateTime(2016, 10, 15), date, "Expecting Oct 15, 2016 (don't allow current)");
        }

        [TestMethod]
        public void TestBiMonthly_2016Feb29()
        {
            var calculator = new BiMonthlyPayDate(15, BiMonthlyPayDate.EndOfMonth);
            var feb29 = new DateTime(2016, 2, 29);

            var date = calculator.GetNextPayDate(feb29, true);
            Assert.AreEqual(new DateTime(2016, 2, 29), date, "Expecting Feb 29, 2016 (allow current)");

            date = calculator.GetNextPayDate(feb29, false);
            Assert.AreEqual(new DateTime(2016, 3, 15), date, "Expecting Mar 1, 2016 (don't allow current)");
        }

        [TestMethod]
        public void TestBiMonthly_2016Feb29_BeginOfMonth()
        {
            var calculator = new BiMonthlyPayDate(1, 15);
            var feb29 = new DateTime(2016, 2, 29);

            var date = calculator.GetNextPayDate(feb29, true);
            Assert.AreEqual(new DateTime(2016, 3, 1), date, "Expecting Mar 1, 2016 (allow current)");

            date = calculator.GetNextPayDate(feb29, false);
            Assert.AreEqual(new DateTime(2016, 3, 1), date, "Expecting Mar 1, 2016 (don't allow current)");
        }

        [TestMethod]
        public void TestWeekly_Sept27_Friday()
        {
            var calculator = new WeeklyPayDate(DayOfWeek.Friday);
            var sept27 = new DateTime(2016, 9, 27);

            var date = calculator.GetNextPayDate(sept27, true);
            Assert.AreEqual(new DateTime(2016, 9, 30), date, "Expecting Sept 30, 2016 (allow current)");

            date = calculator.GetNextPayDate(sept27, false);
            Assert.AreEqual(new DateTime(2016, 9, 30), date, "Expecting Sept 30, 2016 (don't allow current)");
        }

        [TestMethod]
        public void TestWeekly_Sept30_Friday()
        {
            var calculator = new WeeklyPayDate(DayOfWeek.Friday);
            var sept30 = new DateTime(2016, 9, 30);

            var date = calculator.GetNextPayDate(sept30, true);
            Assert.AreEqual(new DateTime(2016, 9, 30), date, "Expecting Sept 30, 2016 (allow current)");

            date = calculator.GetNextPayDate(sept30, false);
            Assert.AreEqual(new DateTime(2016, 10, 7), date, "Expecting Oct 7, 2016 (don't allow current)");
        }

        [TestMethod]
        public void TestWeekly_Oct31_Friday()
        {
            var calculator = new WeeklyPayDate(DayOfWeek.Friday);
            var oct31 = new DateTime(2016, 10, 31);

            var date = calculator.GetNextPayDate(oct31, true);
            Assert.AreEqual(new DateTime(2016, 11, 4), date, "Expecting Nov 4, 2016 (allow current)");

            date = calculator.GetNextPayDate(oct31, false);
            Assert.AreEqual(new DateTime(2016, 11, 4), date, "Expecting Nov 4, 2016 (allow current)");
        }

        [TestMethod]
        public void TestWeekly_Oct31_Sunday()
        {
            var calculator = new WeeklyPayDate(DayOfWeek.Sunday);
            var oct31 = new DateTime(2016, 10, 31);

            var date = calculator.GetNextPayDate(oct31, true);
            Assert.AreEqual(new DateTime(2016, 11, 6), date, "Expecting Nov 6, 2016 (allow current)");

            date = calculator.GetNextPayDate(oct31, false);
            Assert.AreEqual(new DateTime(2016, 11, 6), date, "Expecting Nov 6, 2016 (allow current)");
        }

        [TestMethod]
        public void TestWeekly_Sept30_Friday_CurrentPayPeriod()
        {
            var sept30 = new DateTime(2016, 9, 30);
            var calculator = new WeeklyPayDate(DayOfWeek.Friday, sept30);

            var date = calculator.GetMostRecentPayDate();
            Assert.AreEqual(new DateTime(2016, 9, 30), date, "Expecting Sept 30, 2016");
        }

        [TestMethod]
        public void TestWeekly_Sept27_Friday_CurrentPayPeriod()
        {
            var sept27 = new DateTime(2016, 9, 27);
            var calculator = new WeeklyPayDate(DayOfWeek.Friday, sept27);

            var date = calculator.GetMostRecentPayDate();
            Assert.AreEqual(new DateTime(2016, 9, 23), date, "Expecting Sept 23, 2016");
        }

        [TestMethod]
        public void TestBiMonthly_Sept30_Friday_CurrentPayPeriod()
        {
            var sept30 = new DateTime(2016, 9, 30);
            var calculator = new BiMonthlyPayDate(15, BiMonthlyPayDate.EndOfMonth, sept30);

            var date = calculator.GetMostRecentPayDate();
            Assert.AreEqual(new DateTime(2016, 9, 30), date, "Expecting Sept 30, 2016");
        }

        [TestMethod]
        public void TestBiMonthly_Sept27_Friday_CurrentPayPeriod()
        {
            var sept27 = new DateTime(2016, 9, 27);
            var calculator = new BiMonthlyPayDate(15, BiMonthlyPayDate.EndOfMonth, sept27);

            var date = calculator.GetMostRecentPayDate();
            Assert.AreEqual(new DateTime(2016, 9, 15), date, "Expecting Sept 23, 2016");
        }
    }
}
