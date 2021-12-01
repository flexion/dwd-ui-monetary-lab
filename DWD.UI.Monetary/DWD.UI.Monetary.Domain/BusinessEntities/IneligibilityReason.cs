namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System.ComponentModel;

    public enum IneligibilityReason
    {
        [Description("Claimant does not have at least two quarters' wages")]
        InsufficientQuartersWithWages,

        [Description("Claimant's high quarter wage is not at least minimum High Quarter Earnings")]
        InsufficientHighQuarterWage,

        [Description("Claimant's outside High Quarter wages are too low relative to Weekly Benefit Rate")]
        InsufficientNonHighQuarterWages,

        [Description("Claimant's Total Base Period Wages are too low relative to Weekly Benefit Rate")]
        InsufficientTotalBasePeriodWages,
    }
}
