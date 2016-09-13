using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Models
{
    public static class BudgetExtensions
    {
        public static decimal GetSumOfCategoryTransactions(this BudgetCategory category, List<BudgetLine> lineItems)
        {
            return lineItems
                .Where(item => item.CategoryAmount.ContainsKey(category.Name))
                .Sum(item => item.CategoryAmount[category.Name]);
        }

        public static decimal GetEndingBalance(this BudgetCategory category, List<BudgetLine> lineItems)
        {
            return category.StartingBalance + category.GetSumOfCategoryTransactions(lineItems);
        }
    }
}
