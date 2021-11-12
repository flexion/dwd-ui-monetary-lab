namespace DWD.UI.Monetary.Domain.Extensions
{
    using System;

    // TODO: Refactor methods out of extensions class.

    /// <summary>
    /// Additional methods to be used on any DateTime.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Return the calendar quarter number for the supplied date.
        /// </summary>
        /// <param name="date">date.</param>
        /// <returns>The calendar quarter number.</returns>
        public static int CalendarQuarterNumber(this DateTime date) =>
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
        /// <param name="date">date.</param>
        /// <returns>The first day of the calendar quarter.</returns>
        public static DateTime FirstDayOfCalendarQuarter(this DateTime date) =>
            date.CalendarQuarterNumber() switch
            {
                1 => new DateTime(date.Year, 1, 1),
                2 => new DateTime(date.Year, 4, 1),
                3 => new DateTime(date.Year, 7, 1),
                4 => new DateTime(date.Year, 10, 1),
                _ => throw new ArgumentOutOfRangeException(message: "Unknown calendar quarter", paramName: nameof(date))
            };
    }
}
