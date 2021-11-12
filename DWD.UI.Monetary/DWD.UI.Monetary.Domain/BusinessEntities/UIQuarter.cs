#nullable enable
namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;
    using Extensions;

    /// <summary>
    /// Represents an unemployment insurance quarter.
    /// </summary>
    /// <remarks> For unemployment purposes the quarter does not start until the first full week of that month.
    /// Example: October of 2021. The first full week of October was Sunday-Saturday 10/3-10/9 so that is when the quarter 4
    /// would start for unemployment purposes.  The week of 9/26-10/2 would be considered to be apart of Q3.
    /// </remarks>
    internal class UIQuarter : IUIQuarter, IEquatable<UIQuarter>
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
        public UIQuarter(DateTime date)
        {
            this.Year = date.Year;
            this.QuarterNumber = date.UIQuarterNumber();
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

        public bool Equals(UIQuarter? other) =>
            this.Equals((object)other!);

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as UIQuarter;
            return null != other && this.Year.Equals(other.Year) && this.QuarterNumber.Equals(other.QuarterNumber);
        }

        public override int GetHashCode() => HashCode.Combine(this.Year, this.QuarterNumber);
    }
}
