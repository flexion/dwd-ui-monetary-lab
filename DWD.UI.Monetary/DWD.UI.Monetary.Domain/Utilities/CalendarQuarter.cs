#pragma warning disable CA1052
#pragma warning disable CA1822
namespace DWD.UI.Monetary.Domain.Utilities
{
    using System;

    /// <summary>
    /// Additional methods to be used on any DateTime.
    /// </summary>
    public class CalendarQuarter : ICalendarQuarter
    {
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
                _ => 4
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
                _ => throw new ArgumentOutOfRangeException(message: "Unknown calendar quarter", paramName: nameof(quarterNumber))
            };
    }
}
