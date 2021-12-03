namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;

    /// <summary>
    /// Computed benefit year for claimant.
    /// </summary>
    public class BenefitYear
    {
        public BenefitYear(int benefitWeeks,
                            DateTime claimRequestedDate,
                            DateTime benefitBeginDate,
                            DateTime benefitEndDate,
                            YearWeek benefitBeginYearWeek,
                            YearWeek benefitEndYearWeek)
        {
            this.BenefitWeeks = benefitWeeks;
            this.ClaimRequestedDate = claimRequestedDate;
            this.BeginDate = benefitBeginDate;
            this.EndDate = benefitEndDate;
            this.BeginYearWeek = benefitBeginYearWeek;
            this.EndYearWeek = benefitEndYearWeek;
        }

        /// <summary>
        /// The number of weeks in a benefit year.
        ///
        /// The value can be 52 or 53. 53 is an exceptional case when for
        /// when a subsequent year claim's base period overlaps with the prior.
        /// </summary>
        public int BenefitWeeks { get; }

        /// <summary>
        /// The benefit date requested by the claim.
        ///
        /// Usually the claim initiated date but claimant can request benefits begin
        /// in a week after that date.
        /// </summary>
        public DateTime ClaimRequestedDate { get; }

        /// <summary>
        /// The calculated benefit begin date.
        ///
        /// Benefits always begin on a Sunday regardless of the requested date.
        /// </summary>
        public DateTime BeginDate { get; }

        /// <summary>
        /// The calculated benefit end date.
        ///
        /// Benefits always end on a Sunday 52 weeks after the begin date.
        /// In exceptional cases to avoid an overlap in base periods for
        /// subsequent claims, this value may be 53 weeks.
        /// </summary>
        public DateTime EndDate { get; }

        /// <summary>
        /// The benefit begin year and week.
        ///
        /// First week of the year is the week that contains January 1st
        /// even if the beginning of the week is in the prior year.
        /// The week number and year is calculated based on the Saturday
        /// (week ending date) following the BeginDate.
        /// </summary>
        public YearWeek BeginYearWeek { get; }

        /// <summary>
        /// The benefit end year and week.
        ///
        /// First week of the year is the week that contains January 1st
        /// even if the beginning of the week is in the prior year.
        /// The week number and year is calculated based EndDate
        /// which is always a Saturday.
        /// </summary>
        public YearWeek EndYearWeek { get; }
    }
}
