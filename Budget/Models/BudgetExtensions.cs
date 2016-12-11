using System.Collections.Generic;
using System.Linq;

namespace Willowcat.Budget.Models
{
    public static class BudgetExtensions
    {
        public static decimal GetSumOfCategoryTransactions(this BudgetCategory category, List<BudgetLine> lineItems, FilterType type = FilterType.All)
        {
            return lineItems
                .Where(item =>
                {
                    bool include = item.CategoryAmount.ContainsKey(category.Name);
                    if (include)
                    {
                        switch (type)
                        {
                            case FilterType.Expense:
                                include = item.CategoryAmount[category.Name] < 0;
                                break;

                            case FilterType.Income:
                                include = item.CategoryAmount[category.Name] > 0;
                                break;

                            case FilterType.All:
                            default:
                                //include all
                                break;
                        }
                    }
                    return include;
                })
                .Sum(item => item.CategoryAmount[category.Name]);
        }

        public static decimal GetEndingBalance(this BudgetCategory category, List<BudgetLine> lineItems)
        {
            return category.StartingBalance + category.GetSumOfCategoryTransactions(lineItems);
        }
    }
}
