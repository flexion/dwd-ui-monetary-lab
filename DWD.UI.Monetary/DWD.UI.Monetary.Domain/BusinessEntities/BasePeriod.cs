namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    // TODO: Add a method to get base period by year and week i.e. 2021 41, meaning 41st week of 2021.

    /// <summary>
    /// The base period used within our eligibility logic.
    /// </summary>
    internal class BasePeriod : IBasePeriod, IEnumerable<CalendarQuarter>
    {
        // TODO: Ask Helen if this is needed, and if so what the correct minimum should be.
        /// <summary>
        /// The minimum valid initial base claim date that we will calculate from.
        /// </summary>
        private static readonly DateTime MinimumValidInitialBaseClaimDate = new(1900, 1, 1);

        /// <summary>
        /// Local storage for quarters.
        /// </summary>
        private readonly CalendarQuarter[] quarters = new CalendarQuarter[4];

        /// <summary>
        /// Hidden default constructor.
        /// </summary>
        private BasePeriod() { }

        /// <summary>
        /// Construct instance using initial claim date as input.
        /// </summary>
        /// <param name="initialClaimDate">The initial claim date.</param>
        /// <param name="useAltBasePeriod">Calculate alternative.</param>
        /// <exception cref="ArgumentException">Throws a ArgumentException if the supplied initial claim date is not valid.</exception>
        public BasePeriod(DateTime initialClaimDate, bool useAltBasePeriod)
        {
            // Check if claim date is invalid
            if (ClaimDateInvalid(initialClaimDate, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            // Populate basePeriod with last 4 complete quarters, skipping most recent complete quarter
            var currentQuarter = new CalendarQuarter(initialClaimDate);
            var tempQuarter = --currentQuarter;

            for (var i = 3; i >= 0; i--)
            {
                tempQuarter = --tempQuarter;
                this[i] = new CalendarQuarter(tempQuarter.Year, tempQuarter.QuarterNumber);
            }
        }

        /// <summary>
        /// Get base period quarters as IEnumerable of ICalendarQuarter.
        /// </summary>
        public IEnumerable<ICalendarQuarter> BasePeriodQuarters => this;

        /// <summary>
        /// Indexer into base period quarters.
        /// </summary>
        /// <param name="index">The zero based quarter index to return.</param>
        public CalendarQuarter this[int index]
        {
            get => this.quarters[index];
            private init => this.quarters[index] = value;
        }

        /// <summary>
        /// Friendly getter for first quarter.
        /// </summary>
        public CalendarQuarter FirstQuarter => this[0];

        /// <summary>
        /// Friendly getter for second quarter.
        /// </summary>
        public CalendarQuarter SecondQuarter => this[1];

        /// <summary>
        /// Friendly getter for third quarter.
        /// </summary>
        public CalendarQuarter ThirdQuarter => this[2];

        /// <summary>
        /// Friendly getter for fourth quarter.
        /// </summary>
        public CalendarQuarter FourthQuarter => this[3];

        /// <summary>
        /// Get an enumerator so clients can use foreach and similar interactions.
        /// </summary>
        /// <returns>An IEnumerator of type CalendarQuarter.</returns>
        public IEnumerator<CalendarQuarter> GetEnumerator()
        {
            for (var i = 0; i < 4; i++)
            {
                yield return this.quarters[i];
            }
        }

        /// <summary>
        /// Get an enumerator.
        /// </summary>
        /// <returns>An IEnumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Determine if the claim date is invalid.
        /// </summary>
        /// <param name="initialClaimDate">The initial claim date.</param>
        /// <param name="errorMessage">The error message (out).</param>
        /// <returns>True if the claim date is invalid, false otherwise.</returns>
        private static bool ClaimDateInvalid(DateTime initialClaimDate, out string errorMessage)
        {
            // Check the claim date is not prior to our minimum
            if (initialClaimDate.Date < MinimumValidInitialBaseClaimDate)
            {
                errorMessage = $"The supplied initial claim date is not valid: Dates before {MinimumValidInitialBaseClaimDate} are not supported (initialClaimDate={initialClaimDate.Date.ToShortDateString()}).";
                return true;
            }

            // Check the claim date is not a future date
            if (initialClaimDate.Date > DateTime.Today)
            {
                errorMessage = $"The supplied initial claim date is not valid: Future date's are not supported (initialClaimDate={initialClaimDate.Date.ToShortDateString()}).";
                return true;
            }

            errorMessage = null;
            return false;
        }
    }
}
