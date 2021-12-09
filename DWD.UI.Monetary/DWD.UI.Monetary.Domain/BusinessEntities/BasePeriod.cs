namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;
    using System.Collections.Generic;
    using Utilities;
    using System.Linq;

    // TODO: Add a method to get base period by year and week i.e. 2021 41, meaning 41st week of 2021.

    /// <summary>
    /// The base period used within our eligibility logic.
    /// </summary>
    /// <remarks> For unemployment purposes the quarter does not start until the first full week of that month.
    /// Example: October of 2021. The first full week of October was Sunday-Saturday 10/3-10/9 so that is when the quarter 4
    /// would start for unemployment purposes.  The week of 9/26-10/2 would be considered to be apart of Q3.
    /// </remarks>
    internal class BasePeriod : IBasePeriod
    {
        // TODO: Ask Helen if this is needed, and if so what the correct minimum should be.
        /// <summary>
        /// The minimum valid initial base claim date that we will calculate from.
        /// </summary>
        private static readonly DateTime MinimumValidInitialBaseClaimDate = new(Constants.MIN_BENEFIT_YEAR, 1, 1);

        /// <summary>
        /// Local storage for quarters.
        /// </summary>
        private UIQuarter[] standardQuarters = new UIQuarter[4];

        /// <summary>
        /// Local storage for quarters.
        /// </summary>
        private UIQuarter[] alternateQuarters = new UIQuarter[4];

        /// <summary>
        /// Hidden default constructor.
        /// </summary>
        private BasePeriod() { }

        /// <summary>
        /// Construct instance using initial claim date as input.
        /// </summary>
        /// <param name="initialClaimDate">The initial claim date.</param>
        /// <exception cref="ArgumentException">Throws a ArgumentException if the supplied initial claim date is not valid.</exception>
        public BasePeriod(DateTime initialClaimDate) =>
            this.PopulateBasePeriods(initialClaimDate);

        /// <summary>
        ///  Populate standard and alternate base periods
        /// </summary>
        /// <param name="initialClaimDate">The initial claim date.</param>
        /// <exception cref="ArgumentException">Throws a ArgumentException if the supplied initial claim date is not valid.</exception>
        private void PopulateBasePeriods(DateTime initialClaimDate)
        {
            // Check if claim date is invalid
            if (initialClaimDate.Date < MinimumValidInitialBaseClaimDate)
            {
                throw new ArgumentException($"The supplied initial claim date is not valid: Dates before {MinimumValidInitialBaseClaimDate} are not supported (initialClaimDate={initialClaimDate.Date.ToShortDateString()}).");
            }

            // Populate basePeriod with last 5 complete quarters, skipping most recent complete quarter
            var previous5Quarters = new UIQuarter[5];
            var currentQuarter = new UIQuarter(initialClaimDate, new CalendarQuarter());

            for (var i = 4; i >= 0; i--)
            {
                currentQuarter = --currentQuarter;
                previous5Quarters[i] = new UIQuarter(currentQuarter.Year, currentQuarter.QuarterNumber);
            }

            this.standardQuarters = previous5Quarters.Take(4).ToArray();
            this.alternateQuarters = previous5Quarters.Skip(1).ToArray();
        }

        public BasePeriod(int year, int weekOfYear)
        {
            var initialClaimDate = new CalendarQuarter().GetDateTimeFromYearAndWeek(year, weekOfYear);
            this.PopulateBasePeriods(initialClaimDate);
        }

        /// <summary>
        /// Get base period quarters as IEnumerable of IUIQuarter.
        /// </summary>
        public IEnumerable<IUIQuarter> BasePeriodQuarters => new List<IUIQuarter>(this.standardQuarters);

        /// <summary>
        /// Get base period quarters as IEnumerable of IUIQuarter.
        /// </summary>
        public IEnumerable<IUIQuarter> AltBasePeriodQuarters => new List<IUIQuarter>(this.alternateQuarters);
    }
}
