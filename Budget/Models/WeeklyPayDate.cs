using System;

namespace Budget.Models
{
    public class WeeklyPayDate : IPayDateCalculator
    {
        #region Member Variables...

        private DayOfWeek _DayOfWeek;
        private DateTime? _CurrentDate;

        #endregion Member Variables...

        #region Constructors...

        public WeeklyPayDate(DayOfWeek dayOfWeek)
        {
            _DayOfWeek = dayOfWeek;
        }

        public WeeklyPayDate(DayOfWeek dayOfWeek, DateTime currentDate)
        {
            _DayOfWeek = dayOfWeek;
            _CurrentDate = currentDate;
        }

        #endregion Constructors...

        #region Methods...

        public DateTime GetMostRecentPayDate()
        {
            DateTime newDate = _CurrentDate ?? DateTime.Today;

            if (newDate.DayOfWeek != _DayOfWeek)
            {
                int daysToAdd = 0 - (newDate.DayOfWeek - _DayOfWeek) % 7;
                if ( daysToAdd > 0 )
                {
                    daysToAdd = daysToAdd - 7;
                }
                newDate = newDate.AddDays(daysToAdd);
            }

            return newDate.Date;
        }

        public DateTime GetNextPayDate(DateTime currentDate, bool allowCurrentDate)
        {
            DateTime newDate = allowCurrentDate ? currentDate : currentDate.AddDays(1);

            if ( newDate.DayOfWeek != _DayOfWeek )
            {
                int daysToAdd = (_DayOfWeek - newDate.DayOfWeek) % 7;
                if (daysToAdd < 0 )
                {
                    daysToAdd = daysToAdd + 7;
                }
                newDate = newDate.AddDays(daysToAdd);
            }

            return newDate.Date;
        }

        #endregion Methods...
    }
}
