namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System.ComponentModel;

    /// <summary>
    /// This enum defines the reasons for a determination of ineligibilty and corresponding human-readable descriptions.
    /// </summary>
    public enum IneligibilityReason
    {
        /// <summary>
        /// Claimant does not have at least two quarters' wages.
        /// </summary>
        [Description("Claimant does not have at least two quarters' wages")]
        InsufficientQuartersWithWages = 0,

        /// <summary>
        /// Claimant's high quarter wage is not at least minimum High Quarter Earnings.
        /// </summary>
        [Description("Claimant's high quarter wage is not at least minimum High Quarter Earnings")]
        InsufficientHighQuarterWage = 1,

        /// <summary>
        /// Claimant's outside High Quarter wages are too low relative to Weekly Benefit Rate.
        /// </summary>
        [Description("Claimant's outside High Quarter wages are too low relative to Weekly Benefit Rate")]
        InsufficientNonHighQuarterWages = 2,

        /// <summary>
        /// Claimant's Total Base Period Wages are too low relative to Weekly Benefit Rate.
        /// </summary>
        [Description("Claimant's Total Base Period Wages are too low relative to Weekly Benefit Rate")]
        InsufficientTotalBasePeriodWages = 3,
    }
}
