using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Models
{
    public class BudgetCategory
    {
        //private List<BudgetLine> _LineItems;

        //public decimal EndingBalance
        //{
        //    get
        //    {
        //        return StartingBalance + LineItems.Select(item => item.Amount).Sum();
        //    }
        //}

        //public DateTime EndingDate { get; set; }

        //public List<BudgetLine> LineItems {
        //    get
        //    {
        //        if ( _LineItems == null )
        //        {
        //            _LineItems = new List<BudgetLine>();
        //        }
        //        return _LineItems;
        //    }
        //}

        public string Name { get; set; }

        public decimal PaycheckBudget { get; set; }

        public decimal StartingBalance { get; set; }

        public DateTime StartingDate { get; set; }
    }
}
