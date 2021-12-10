namespace DWD.UI.Monetary.Domain.Utilities;

using System;

/// <summary>
/// Quarter information for the given date..
/// </summary>
public interface ICalendarQuarter
{
    /// <summary>
    /// Quarter for date.
    /// </summary>
    /// <param name="dateTime">date</param>
    /// <returns>quarter number</returns>
    public int CalendarQuarterNumber(DateTime dateTime);

    /// <summary>
    /// First date of the quarter
    /// </summary>
    /// <param name="year">year</param>
    /// <param name="quarterNumber">quarter</param>
    /// <returns>calendar date</returns>
    public DateTime FirstDayOfCalendarQuarter(int year, int quarterNumber);
}
