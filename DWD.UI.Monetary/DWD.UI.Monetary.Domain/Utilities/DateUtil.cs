namespace DWD.UI.Monetary.Domain.Utilities
{
    using System;
    /// <summary>
    /// Additional methods to be used on any DateTime.
    /// </summary>
    public static class DateUtil
    {
        /// <summary>
        /// Return the calendar quarter number for the supplied date.
        /// </summary>
        /// <param name="date">date.</param>
        /// <returns>The calendar quarter number.</returns>
        public static int CalendarQuarterNumber(DateTime date) =>
            date.Month switch
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
        public static DateTime FirstDayOfCalendarQuarter(int year, int quarterNumber) =>
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
