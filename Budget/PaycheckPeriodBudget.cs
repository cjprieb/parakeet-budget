﻿using Budget.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    public class PaycheckPeriodBudget
    {
        #region Private fields
        private Dictionary<string, BudgetCategory> _Categories = null;
        private List<string> _CategoryOrder = null;
        private List<BudgetLine> _LineItems = null;
        private List<string> _Settings = null;
        #endregion

        #region Public properties
        public List<BudgetLine> AllItems
        {
            get
            {
                var list = new List<BudgetLine>();
                list.Add(StartingBudget);
                list.AddRange(_LineItems);
                var total = TotalBudget;
                var budget = PaycheckBudget;
                list.Add(total);
                list.Add(budget);
                var nextPaycheck = new BudgetLine()
                {
                    Date = total.Date,
                    Description = "NEXT PAYCHECK BALANCE",
                    CategoryAmount = new Dictionary<string, decimal>()
                };
                foreach ( var category in CategoryOrder )
                {
                    nextPaycheck.CategoryAmount[category] = total.CategoryAmount[category] + budget.CategoryAmount[category];
                }
                list.Add(nextPaycheck);

                return list;
            }
        }

        public decimal BalanceTotal { get; set; }
        public Dictionary<string, BudgetCategory> Categories
        {
            get
            {
                if (_Categories == null)
                {
                    _Categories = new Dictionary<string, BudgetCategory>();
                }
                return _Categories;
            }
        }

        public List<string> CategoryOrder
        {
            get
            {
                if (_CategoryOrder == null)
                {
                    _CategoryOrder = new List<string>();
                }
                return _CategoryOrder;
            }
        }

        public BudgetLine EndingBudget
        {
            get
            {
                var line = new BudgetLine()
                {
                    Date = LineItems.Max(item => item.Date),
                    Description = "NEXT PAYCHECK BALANCE",
                    CategoryAmount = new Dictionary<string, decimal>(),
                    ReadOnly = true
                };
                foreach (var categoryName in CategoryOrder)
                {
                    var category = Categories[categoryName];
                    line.CategoryAmount[categoryName] = category.StartingBalance + category.GetSumOfCategoryTransactions(LineItems) + category.PaycheckBudget;
                }
                return line;
            }
        }

        public List<BudgetLine> LineItems
        {
            get
            {
                if (_LineItems == null)
                {
                    _LineItems = new List<BudgetLine>();
                }
                return _LineItems;
            }
        }

        public BudgetLine PaycheckBudget
        {
            get
            {
                var line = new BudgetLine()
                {
                    Date = LineItems.Max(item => item.Date),
                    Description = "PAYCHECK BUDGET",
                    CategoryAmount = new Dictionary<string, decimal>(),
                    ReadOnly = false
                };

                foreach (var category in CategoryOrder)
                {
                    line.CategoryAmount[category] = Categories[category].PaycheckBudget;
                }
                return line;
            }
        }

        public decimal PayPeriodTotal { get; set; }

        public List<string> Settings
        {
            get
            {
                if (_Settings == null)
                {
                    _Settings = new List<string>();
                }
                return _Settings;
            }
        }

        public DateTime StartingDate { get; protected set; }

        public BudgetLine StartingBudget
        {
            get
            {
                var line = new BudgetLine()
                {
                    Date = StartingDate,
                    Description = "BUDGET START",
                    CategoryAmount = new Dictionary<string, decimal>(),
                    ReadOnly = true
                };
                foreach ( var category in CategoryOrder )
                {
                    line.CategoryAmount[category] = Categories[category].StartingBalance;
                }
                return line;
            }
        }

        public BudgetLine TotalBudget
        {
            get
            {
                var line = new BudgetLine()
                {
                    Date = LineItems.Max(item => item.Date),
                    Description = "TOTAL",
                    CategoryAmount = new Dictionary<string, decimal>(),
                    ReadOnly = true
                };

                foreach (var categoryName in CategoryOrder)
                {
                    var category = Categories[categoryName];
                    line.CategoryAmount[categoryName] = category.StartingBalance + category.GetSumOfCategoryTransactions(LineItems);
                }
                return line;
            }
        }
        #endregion

        #region Constructors
        public PaycheckPeriodBudget()
        {

        }

        public PaycheckPeriodBudget(DateTime startingDate)
        {
            StartingDate = startingDate;
        }
        #endregion 

        #region Protected methods
        protected void Clear()
        {
            Categories.Clear();
            LineItems.Clear();
            Settings.Clear();
        }
        #endregion

        #region Public methods
        public PaycheckPeriodBudget GenerateNextPayPeriod(DateTime newStartDate)
        {
            var budget = new PaycheckPeriodBudget(newStartDate);
            budget.PayPeriodTotal = PayPeriodTotal;
            budget.BalanceTotal = Math.Round(budget.GetEndingBalance());
            foreach (var categoryName in CategoryOrder)
            {
                var category = Categories[categoryName];
                var endingBalance = category.GetEndingBalance(LineItems);
                budget.Categories.Add(categoryName, new BudgetCategory()
                {
                    Name = categoryName,
                    PaycheckBudget = category.PaycheckBudget,
                    StartingDate = newStartDate,
                    StartingBalance = Math.Round(endingBalance)
                });
            }

            return budget;
        }

        public decimal GetEndingBalance()
        {
            return BalanceTotal + LineItems.Sum(item => item.TotalAmount);
        }
        #endregion
    }
}