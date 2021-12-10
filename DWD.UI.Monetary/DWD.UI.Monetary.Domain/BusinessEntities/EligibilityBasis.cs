namespace DWD.UI.Monetary.Domain.BusinessEntities;

/// <summary>
/// EligibilityBasis is an object holding values used in determining eligibility for benefits.
/// </summary>
/// <remarks>
/// These properties are established by State law and are expected to change occasionally, so they are read
/// from a data store.
/// </remarks>
/// <param name="MinHighQuarterEarnings">The minimum wage amount required in the claimant's highest quarter of the base period.</param>
/// <param name="PercentWeeklyBenefitRate">Used to calculate Weekly Benefit Rate.</param>
/// <param name="MinQuarters">The minimum number of quarters of the base period in which the claimant must have non-zero wages.</param>
/// <param name="WagesOutsideOfHighQuarterFactor">Weekly Benefit Rate times this determines the minimum total wages outside the high quarter.</param>
/// <param name="BasePeriodWagesFactor">This is used to calculate total base period wages.</param>
/// <param name="NumberOfWeeks">Number of weeks used to determine Maximum Benefit Amount.</param>
/// <param name="PercentOfBasePeriodWages">Percentage of total base period wages used to determine Maximum Benefit Amount.</param>
public record EligibilityBasis(int MinHighQuarterEarnings, int PercentWeeklyBenefitRate, int MinQuarters,
    int WagesOutsideOfHighQuarterFactor, int BasePeriodWagesFactor, int NumberOfWeeks,
    int PercentOfBasePeriodWages);
