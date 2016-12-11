using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    public enum BudgetType
    {
        Credit, Debit
    }

    public class BudgetLine
    {
        public decimal Amount { get; set; }

        public BudgetType BudgetType { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }

        public decimal PaycheckAmount { get; set; }

        public decimal StartingAmount { get; set; }
    }

    public class AccountData
    {
        #region Member Variables..
        private List<BudgetLine> _BudgetLines = new List<BudgetLine>();
        private Dictionary<string, Category> _Categories = new Dictionary<string, Category>();
        private List<string> _CategoryOrder = new List<string>();
        private List<string> _Notes = new List<string>();
        #endregion Member Variables..

        #region Properties...
        public List<BudgetLine> BudgetLines { get { return _BudgetLines; } }

        public Dictionary<string, Category> Categories { get { return _Categories; } }

        public List<string> CategoryOrder { get { return _CategoryOrder; } }

        public List<string> Notes { get { return _Notes; } }

        public DateTime StartingDate { get; set; }

        public decimal TotalBudgetAmount { get; set; }

        public decimal TotalStartingAmount { get; set; }
        #endregion Properties...
    }
}
