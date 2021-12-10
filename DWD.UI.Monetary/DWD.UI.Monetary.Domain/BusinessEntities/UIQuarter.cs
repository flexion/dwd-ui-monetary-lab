#nullable enable
namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;
    using DWD.UI.Monetary.Domain.Utilities;

    /// <summary>
    /// Represents an unemployment insurance quarter.
    /// </summary>
    /// <remarks> For unemployment purposes the quarter does not start until the first full week of that month.
    /// Example: October of 2021. The first full week of October was Sunday-Saturday 10/3-10/9 so that is when the quarter 4
    /// would start for unemployment purposes.  The week of 9/26-10/2 would be considered to be apart of Q3.
    /// </remarks>
    internal class UIQuarter : IUIQuarter
    {
        /// <summary>
        /// The quarter's year.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// The quarter number.
        /// </summary>
        /// <returns>quarter number</returns>
        public int QuarterNumber { get; private set; }

        /// <summary>
        /// Construct from year and quarter number.
        /// </summary>
        /// <param name="year">The quarter's year.</param>
        /// <param name="quarterNumber">The quarter number.</param>
        public UIQuarter(int year, int quarterNumber)
        {
            this.Year = year;
            this.QuarterNumber = quarterNumber;
        }

        /// <summary>
        /// Construct from calendar date.
        /// </summary>
        /// <param name="date">date.</param>
        /// <param name="calendarQuarter">calendarQuarter.</param>
        public UIQuarter(DateTime date, ICalendarQuarter calendarQuarter)
        {
            this.Year = date.Year;
            this.QuarterNumber = calendarQuarter.CalendarQuarterNumber(date);

            // Find first Sunday of calendar quarter
            var firstSundayOfQuarter = calendarQuarter.FirstDayOfCalendarQuarter(date.Year, this.QuarterNumber);
            while (firstSundayOfQuarter.DayOfWeek != DayOfWeek.Sunday)
            {
                firstSundayOfQuarter = firstSundayOfQuarter.AddDays(1);
            }

            // If required, decrement calendar quarter to UI quarter
            if (date < firstSundayOfQuarter)
            {
                this.QuarterNumber--;
                if (this.QuarterNumber == 0)
                {
                    this.QuarterNumber = 4;
                    this.Year--;
                }
            }
        }

        /// <summary>
        /// Overload -- operator so we can decrement ui quarters easily.
        /// </summary>
        /// <param name="quarter">The ui quarter to decrement.</param>
        /// <returns>The previous ui quarter.</returns>
        public static UIQuarter operator --(UIQuarter quarter)
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
        /// Decrement the ui quarter.
        /// </summary>
        /// <param name="item">The ui quarter to decrement.</param>
        /// <returns>The previous ui quarter.</returns>
        public static UIQuarter Decrement(UIQuarter item) => --item;

        /// <summary>
        /// Compare two UIQuarter objects.
        /// </summary>
        /// <param name="other">Another UIQuarter</param>
        /// <returns>comparison</returns>
        public int CompareTo(IUIQuarter? other)
        {
            var result = -1;
            if (other is UIQuarter quarter)
            {
                result = this.Year.CompareTo(quarter.Year);

                if (0 == result)
                {
                    result = this.QuarterNumber.CompareTo(quarter.QuarterNumber);
                }
            }

            return result;
        }

        /// <summary>
        /// Generics Equals.
        /// </summary>
        /// <param name="other">the other</param>
        /// <returns>true/false</returns>
        public bool Equals(IUIQuarter? other) =>
            this.Equals((object)other!);

        /// <summary>
        /// Override base equals.
        /// </summary>
        /// <param name="obj">other obj</param>
        /// <returns>true/false</returns>
        public override bool Equals(object? obj)
        {
            if (obj is UIQuarter quarter)
            {
                return this.Year.Equals(quarter.Year) && this.QuarterNumber.Equals(quarter.QuarterNumber);
            }

            return false;
        }

        /// <summary>
        /// Hash code override.
        /// </summary>
        /// <returns>hash</returns>
        public override int GetHashCode() => HashCode.Combine(this.Year, this.QuarterNumber);
    }
}
