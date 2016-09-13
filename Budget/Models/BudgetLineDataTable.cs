using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Models
{
    public class BudgetLineDataTable : IListSource
    {
        private PaycheckPeriodBudget _parentBudget;
        private PropertyDescriptorCollection _properties;
        private BudgetLineRowCollection _dataRows;

        public PropertyDescriptorCollection Columns
        {
            get
            {
                return _properties;
            }
        }

        public BudgetLineRowCollection Rows
        {
            get
            {
                return _dataRows;
            }
        }

        public BudgetLineDataTable(PaycheckPeriodBudget parent, PropertyDescriptorCollection columns)
        {
            _parentBudget = parent;
            _properties = columns;
            _dataRows = new BudgetLineRowCollection(this);
        }

        public bool ContainsListCollection
        {
            get
            {
                return true;
            }
        }

        public IList GetList()
        {
            return _dataRows;
        }
    }
}
