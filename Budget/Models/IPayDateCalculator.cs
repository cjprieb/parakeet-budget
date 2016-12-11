using System;

namespace Budget.Models
{
    public interface IPayDateCalculator
    {
        DateTime GetNextPayDate(DateTime currentDate, bool allowCurrentDate);
        DateTime GetMostRecentPayDate();
    }
}
