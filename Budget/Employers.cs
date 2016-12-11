using Willowcat.Budget.Models;

namespace Willowcat.Budget
{
    public class Employers
    {
        private static IPayDateCalculator _CurrentCalculator = null;

        public static IPayDateCalculator GetCurrentPayDateCalculator()
        {
            if (_CurrentCalculator == null)
            {
                _CurrentCalculator = new BiMonthlyPayDate(15, BiMonthlyPayDate.EndOfMonth);
            }
            return _CurrentCalculator;
        }
    }
}
