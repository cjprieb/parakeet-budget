using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Willowcat.Budget
{
    public class BudgetParseException : Exception
    {
        private int _LineCount = 0;

        public int LineCount
        {
            get
            {
                return _LineCount;
            }
        }

        public BudgetParseException(string message, int lineCount) : base(message)
        {
            _LineCount = lineCount;
        }

        public BudgetParseException(string message, int lineCount, Exception innerException) : base(message, innerException)
        {
            _LineCount = lineCount;
        }
    }
}
