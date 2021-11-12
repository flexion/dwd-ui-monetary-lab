namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;

    /// <summary>
    /// Represents a calendar quarter.
    /// </summary>
    internal class CalendarQuarter : ICalendarQuarter
    {
        /// <summary>
        /// The quarter's year.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// The quarter number.
        /// </summary>
        public int QuarterNumber { get; private set; }

        /// <summary>
        /// Construct from year and quarter number.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="quarterNumber"></param>
        public CalendarQuarter(int year, int quarterNumber)
        {
            this.Year = year;
            this.QuarterNumber = quarterNumber;
        }

        /// <summary>
        /// Construct from calendar date.
        /// </summary>
        /// <param name="date">date.</param>
        public CalendarQuarter(DateTime date)
        {
            this.Year = date.Year;
            this.QuarterNumber = GetQuarter(date);
        }

        /// <summary>
        /// Return the quarter number for the supplied date.
        /// </summary>
        /// <param name="date">date.</param>
        /// <returns>The quarter number.</returns>
        private static int GetQuarter(DateTime date) =>
            date.Month switch
            {
                < 4 => 1,
                < 7 => 2,
                < 10 => 3,
                _ => 4
            };

        /// <summary>
        /// Overload -- operator so we can decrement calendar quarters easily.
        /// </summary>
        /// <param name="quarter">The calendar quarter to decrement.</param>
        /// <returns>The previous calendar quarter.</returns>
        public static CalendarQuarter operator --(CalendarQuarter quarter)
        {
            if (quarter == null)
            {
                throw new ArgumentNullException(nameof(quarter));
            }

            quarter.QuarterNumber--;
            if (quarter.QuarterNumber == 0)
            {
                quarter.QuarterNumber = 4;
                quarter.Year--;
            }

            return quarter;
        }

        /// <summary>
        /// Decrement the calendar quarter.
        /// </summary>
        /// <param name="item">The calendar quarter to decrement.</param>
        /// <returns>The previous calendar quarter.</returns>
        public static CalendarQuarter Decrement(CalendarQuarter item) => --item;

        public bool Equals(ICalendarQuarter oth)
        {
            var result = false;

            if (null != oth)
            {
                result = this.Year == oth.Year && this.QuarterNumber == oth.QuarterNumber;
            }

            return result;
        }
    }
}
