#nullable enable
namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;
    using Utilities;

    /// <summary>
    /// Represents an unemployment insurance quarter.
    /// </summary>
    /// <remarks> For unemployment purposes the quarter does not start until the first full week of that month.
    /// Example: October of 2021. The first full week of October was Sunday-Saturday 10/3-10/9 so that is when the quarter 4
    /// would start for unemployment purposes.  The week of 9/26-10/2 would be considered to be apart of Q3.
    /// </remarks>
    internal class UIQuarter : IUIQuarter
    {
        // TODO: Ask Helen if this is needed, and if so what the correct minimum should be.
        /// <summary>
        /// The minimum valid year.
        /// </summary>
        private const int MinimumValidYear = 1900;

        // TODO: Ask Helen if this is needed, and if so what the correct maximum should be.
        /// <summary>
        /// The maximum valid year.
        /// </summary>
        private const int MaximumValidYear = 9999;

        // TODO: Ask Helen if this is needed, and if so what the correct minimum should be.
        /// <summary>
        /// The minimum valid date that can be used to construct a UI Quarter.
        /// </summary>
        private static readonly DateTime MinimumValidDate = new(1900, 1, 1);

        /// <summary>
        /// The quarter's year.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// The quarter number.
        /// </summary>
        public int QuarterNumber { get; private set; }

        public ICalendarQuarter CalendarQuarter { get; }

        /// <summary>
        /// Construct from year and quarter number.
        /// </summary>
        /// <param name="year">The quarter's year.</param>
        /// <param name="quarterNumber">The quarter number.</param>
        public UIQuarter(int year, int quarterNumber)
        {
            // Check if year and quarter number are invalid
            if (YearOrQuarterInvalid(year, quarterNumber, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

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
            // Check if date is invalid
            if (DateInvalid(date, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            this.CalendarQuarter = calendarQuarter;
            this.Year = date.Year;
            this.QuarterNumber = this.CalendarQuarter.CalendarQuarterNumber(date);

            // Find first Sunday of calendar quarter
            var firstSundayOfQuarter = this.CalendarQuarter.FirstDayOfCalendarQuarter(date.Year, this.QuarterNumber);
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
        /// Check for value equality.
        /// </summary>
        /// <param name="other">The other UIQuarter to check for equality.</param>
        /// <returns>True if the values are equal, false otherwise.</returns>
        public bool Equals(IUIQuarter? other) =>
            this.Equals((object)other!);

        /// <summary>
        /// Check if the object for value equality.
        /// </summary>
        /// <param name="obj">The object to check for equality.</param>
        /// <returns>True if the object is a UIQuarter and if its values are equal, false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is UIQuarter quarter)
            {
                return this.Year.Equals(quarter.Year) && this.QuarterNumber.Equals(quarter.QuarterNumber);
            }

            return false;
        }

        /// <summary>
        /// Get a hash code for this UIQuarter instance.
        /// </summary>
        /// <returns>A integer hashcode for this UIQuarter instance.</returns>
        public override int GetHashCode() => HashCode.Combine(this.Year, this.QuarterNumber);

        /// <summary>
        /// Determine if the year and quarter number are invalid.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="quarterNumber">The quarter number.</param>
        /// <param name="errorMessage">The error message (out).</param>
        /// <returns>True if the year or quarter number are invalid, false otherwise.</returns>
        private static bool YearOrQuarterInvalid(int year, int quarterNumber, out string? errorMessage)
        {
            // Check the year is within bounds
            if (year is < MinimumValidYear or > MaximumValidYear)
            {
                errorMessage = $"The supplied year is not valid: year must be between {MinimumValidYear} and {MaximumValidYear} (Year={year}).";
                return true;
            }

            // Check the quarter number is within bounds
            if (quarterNumber is < 1 or > 4)
            {
                errorMessage = $"The supplied quarterNumber is not valid: quarter number must be between 1 and 4. (Quarter Number={quarterNumber}).";
                return true;
            }

            errorMessage = null;
            return false;
        }

        /// <summary>
        /// Determine if the date is invalid.
        /// </summary>
        /// <param name="date">The year.</param>
        /// <param name="errorMessage">The error message (out).</param>
        /// <returns>True if the date is invalid, false otherwise.</returns>
        private static bool DateInvalid(DateTime date, out string? errorMessage)
        {
            // Check the year is within bounds
            if (date < MinimumValidDate)
            {
                errorMessage = $"The supplied date is not valid: Dates before {MinimumValidDate} are not supported when creating UIQuarters (Date={date.Date.ToShortDateString()}).";
                return true;
            }

            errorMessage = null;
            return false;
        }
    }
}
