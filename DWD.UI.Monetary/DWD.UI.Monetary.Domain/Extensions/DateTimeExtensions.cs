namespace DWD.UI.Monetary.Domain.Extensions
{
    using System;

    // TODO: Refactor to shared library.

    /// <summary>
    /// Additional methods to be used on any DateTime.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Return the quarter number for the supplied date.
        /// </summary>
        /// <param name="date">date.</param>
        /// <returns>The quarter number.</returns>
        public static int QuarterNumber(this DateTime date) =>
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
            date.QuarterNumber() switch
            {
                1 => new DateTime(date.Year, 1, 1),
                2 => new DateTime(date.Year, 4, 1),
                3 => new DateTime(date.Year, 7, 1),
                4 => new DateTime(date.Year, 10, 1),
                _ => throw new ArgumentOutOfRangeException(message: "Unknown calendar quarter", paramName: nameof(date))
            };

        /// <summary>
        /// Return the UI quarter number.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>The UI quarter number.</returns>
        /// <remarks> For unemployment purposes the quarter does not start until the first full week of that month.
        /// Example: October of 2021. The first full week of October was Sunday-Saturday 10/3-10/9 so that is when the quarter 4
        /// would start for unemployment purposes.  The week of 9/26-10/2 would be considered to be apart of Q3.
        /// </remarks>
        public static int UIQuarterNumber(this DateTime date)
        {
            var calendarQuarter = date.QuarterNumber();

            // Find first Sunday of calendar quarter
            var firstSundayOfQuarter = date.FirstDayOfCalendarQuarter();
            while (firstSundayOfQuarter.DayOfWeek != DayOfWeek.Sunday)
            {
                firstSundayOfQuarter = firstSundayOfQuarter.AddDays(1);
            }

            if (date < firstSundayOfQuarter)
            {
                return --calendarQuarter;
            }

            return calendarQuarter;
        }
    }
}
