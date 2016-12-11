using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Willowcat.Budget.Models
{
    public class BiMonthlyPayDate : IPayDateCalculator
    {
        #region Static Variables...
        public static readonly int EndOfMonth = 100;
        private static readonly string InvalidDateErrorMessage = "Date must be a valid day of the month (1-31) or BiMonthlyPayDate.EndOfMonth)";
        #endregion Static Variables...

        #region Member Variables...

        private DateTime? _CurrentDate;
        private int _FirstPayDate = 0;
        private int _SecondPayDate = 0;

        #endregion Member Variables...

        #region Constructors...

        public BiMonthlyPayDate(int firstPayDate, int secondPayDate)
        {
            if (firstPayDate < 1 || (firstPayDate > 31 && firstPayDate != EndOfMonth))
            {
                throw new ArgumentOutOfRangeException("firstPayDate", firstPayDate, InvalidDateErrorMessage);
            }

            if (secondPayDate < 1 || (secondPayDate > 31 && secondPayDate != EndOfMonth))
            {
                throw new ArgumentOutOfRangeException("secondPayDate", secondPayDate, InvalidDateErrorMessage);
            }

            _FirstPayDate = firstPayDate;
            _SecondPayDate = secondPayDate;
        }

        public BiMonthlyPayDate(int firstPayDate, int secondPayDate, DateTime currentDate) : this(firstPayDate, secondPayDate)
        {
            _CurrentDate = currentDate;
        }

        #endregion Constructors...

        #region Methods...

        public DateTime GetMostRecentPayDate()
        {
            DateTime newDate = _CurrentDate ?? DateTime.Today;
            DateTime tomorrow = newDate.AddDays(1);
            while (!IsPaycheckDay(_FirstPayDate, newDate.Day, tomorrow.Day) && !IsPaycheckDay(_SecondPayDate, newDate.Day, tomorrow.Day))
            {
                tomorrow = newDate;
                newDate = newDate.AddDays(-1);
            }
            return newDate.Date;
        }

        public DateTime GetNextPayDate(DateTime currentDate, bool allowCurrentDate)
        {
            DateTime newDate = allowCurrentDate ? currentDate : currentDate.AddDays(1);
            DateTime tomorrow = newDate.AddDays(1);
            while (!IsPaycheckDay(_FirstPayDate, newDate.Day, tomorrow.Day) && !IsPaycheckDay(_SecondPayDate, newDate.Day, tomorrow.Day))
            {
                newDate = tomorrow;
                tomorrow = newDate.AddDays(1);
            }
            return newDate.Date;
        }

        private bool IsPaycheckDay(int payDate, int currDate, int dayAfter)
        {
            if (payDate == EndOfMonth)
            {
                return dayAfter == 1;
            }
            else
            {
                return currDate == payDate;
            }
        }

        #endregion Methods...
    }
}
