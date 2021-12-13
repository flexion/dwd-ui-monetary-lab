namespace DWD.UI.Monetary.Domain.Utilities;

using System;
using BusinessEntities;

/// <summary>
/// Additional methods to be used on any DateTime.
/// </summary>
public class CalendarQuarter : ICalendarQuarter
{
    /// <summary>
    /// Determine date from year and week number.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="weekOfYear">The week of year.</param>
    /// <returns>DateTime - Start Date of the week.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The benefit year was out of the acceptable range.</exception>
    public static DateTime GetDateTimeFromYearAndWeek(int year, int weekOfYear)
    {
        if (year < Constants.MINBENEFITYEAR)
        {
            throw new ArgumentOutOfRangeException($"Year before {Constants.MINBENEFITYEAR} not supported");
        }

        var firstDayOfYear = new DateTime(year, 1, 1);

        // There will be 53 weeks in a year if the first day of the year is Saturday
        var totalWeeksOfYear = firstDayOfYear.DayOfWeek == DayOfWeek.Saturday ? 53 : 52;

        if (weekOfYear < 1 || weekOfYear > totalWeeksOfYear)
        {
            throw new ArgumentOutOfRangeException($"Week number must be between 1 and {totalWeeksOfYear}!");
        }

        // Set the firstDayOfYear to previous Sunday if it is not sunday.
        if (firstDayOfYear.DayOfWeek != DayOfWeek.Sunday)
        {
            var offset = firstDayOfYear.DayOfWeek - DayOfWeek.Sunday;
            firstDayOfYear = firstDayOfYear.AddDays(-1 * offset);
        }

        return firstDayOfYear.AddDays((weekOfYear - 1) * 7);
    }

    /// <summary>
    /// Return the calendar quarter number for the supplied date.
    /// </summary>
    /// <param name="dateTime">dateTime.</param>
    /// <returns>The calendar quarter number.</returns>
    public int CalendarQuarterNumber(DateTime dateTime) =>
        dateTime.Month switch
        {
            < 4 => 1,
            < 7 => 2,
            < 10 => 3,
            _ => 4,
        };

    /// <summary>
    /// Return the first day of the calendar quarter.
    /// </summary>
    /// <param name="year">year.</param>
    /// <param name="quarterNumber">quarterNumber.</param>
    /// <returns>The first day of the calendar quarter.</returns>
    public DateTime FirstDayOfCalendarQuarter(int year, int quarterNumber) =>
        quarterNumber switch
        {
            1 => new DateTime(year, 1, 1),
            2 => new DateTime(year, 4, 1),
            3 => new DateTime(year, 7, 1),
            4 => new DateTime(year, 10, 1),
            _ => throw new ArgumentOutOfRangeException(message: "Unknown calendar quarter", paramName: nameof(quarterNumber)),
        };
}
