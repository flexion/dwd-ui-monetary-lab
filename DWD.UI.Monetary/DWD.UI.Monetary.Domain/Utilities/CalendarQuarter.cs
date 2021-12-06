#pragma warning disable CA1052
#pragma warning disable CA1822
namespace DWD.UI.Monetary.Domain.Utilities
{
    using System;
    using System.Globalization;

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

        /// <summary>
        /// Determine date from year and week number
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <returns>DateTime - Start Date of the week</returns>
        /// <exception cref="ArgumentException"></exception>
        public DateTime GetDateTimeFromYearAndWeek(int year, int weekOfYear)
        {
            var minYear = 1900;
            if (year < minYear)
            {
                throw new ArgumentException($"Year before {minYear} not supported");
            }

            var firstDayInWeek = new DateTime(year, 1, 1);

            var totalWeeksOfYear = 52;

            //There will be 53 weeks in a year if the first day of the year is Saturday
            if (firstDayInWeek.DayOfWeek == DayOfWeek.Saturday)
            {
                totalWeeksOfYear = 53;
            }

            if (weekOfYear < 1 || weekOfYear > totalWeeksOfYear)
            {
                throw new ArgumentException($"Week number must be between 1 and {totalWeeksOfYear}!");
            }

            //Set the firstDayInWeek to previous Sunday if it is not sunday.
            if (firstDayInWeek.DayOfWeek != DayOfWeek.Sunday)
            {
                var offset = firstDayInWeek.DayOfWeek - DayOfWeek.Sunday;
                firstDayInWeek = firstDayInWeek.AddDays(-1 * offset);
            }

            return firstDayInWeek.AddDays((weekOfYear - 1) * 7);
        }
    }
}
