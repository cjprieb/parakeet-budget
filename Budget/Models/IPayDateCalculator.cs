using System;

namespace Willowcat.Budget.Models
{
    public interface IPayDateCalculator
    {
        DateTime GetNextPayDate(DateTime currentDate, bool allowCurrentDate);
        DateTime GetMostRecentPayDate();
    }
}
