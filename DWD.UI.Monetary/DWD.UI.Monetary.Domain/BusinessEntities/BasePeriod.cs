namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;
    using System.Collections.Generic;
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
        private static readonly DateTime MinimumValidInitialBaseClaimDate = new(1900, 1, 1);

        /// <summary>
        /// Local storage for quarters.
        /// </summary>
        private readonly UIQuarter[] standardQuarters = new UIQuarter[4];


        /// <summary>
        /// Local storage for quarters.
        /// </summary>
        private readonly UIQuarter[] alternateQuarters = new UIQuarter[4];

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
        public BasePeriod(DateTime initialClaimDate)
        {
            // Check if claim date is invalid
            if (ClaimDateInvalid(initialClaimDate, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            // Populate basePeriod with last 5 complete quarters, skipping most recent complete quarter
            var previous5Quarters = new UIQuarter[5];
            var currentQuarter = new UIQuarter(initialClaimDate);

            for (var i = 4; i >= 0; i--)
            {
                currentQuarter = --currentQuarter;
                previous5Quarters[i] = new UIQuarter(currentQuarter.Year, currentQuarter.QuarterNumber);
            }

            this.standardQuarters = previous5Quarters.Take(4).ToArray();
            this.alternateQuarters = previous5Quarters.Skip(1).ToArray();
        }

        /// <summary>
        /// Get base period quarters as IEnumerable of IUIQuarter.
        /// </summary>
        public IEnumerable<IUIQuarter> BasePeriodQuarters => new List<IUIQuarter>(this.standardQuarters);


        /// <summary>
        /// Get base period quarters as IEnumerable of IUIQuarter.
        /// </summary>
        public IEnumerable<IUIQuarter> AltBasePeriodQuarters => new List<IUIQuarter>(this.alternateQuarters);

        /// <summary>
        /// Friendly getter for first quarter.
        /// </summary>
        public UIQuarter FirstQuarter => this.standardQuarters[0];

        /// <summary>
        /// Friendly getter for second quarter.
        /// </summary>
        public UIQuarter SecondQuarter => this.standardQuarters[1];

        /// <summary>
        /// Friendly getter for third quarter.
        /// </summary>
        public UIQuarter ThirdQuarter => this.standardQuarters[2];

        /// <summary>
        /// Friendly getter for fourth quarter.
        /// </summary>
        public UIQuarter FourthQuarter => this.standardQuarters[3];

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

            errorMessage = null;
            return false;
        }
    }
}
