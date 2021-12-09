namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    /// <summary>
    /// Semi static information that derives from state law used to determine eligibility for benefits
    /// </summary>
    /// <param name="MinHighQuarterEarnings">The wages that the claimant must have at least in one quarter</param>
    /// <param name="PercentWeeklyBenefitRate">Used to calculate Weekly Benefit Rate</param>
    /// <param name="MinQuarters">Minimum number of quarters the claimant has to have wages for</param>
    /// <param name="WagesOutsideOfHighQuarterFactor">Weekly Benefit Rate times this determines the minimum total wages outside the high quarter</param>
    /// <param name="BasePeriodWagesFactor">This is used to calculate total base period wages</param>
    /// <param name="NumberOfWeeks">Number of weeks used to determine Maximum Benefit Amount</param>
    /// <param name="PercentOfBasePeriodWages">Percentage of total base period wages used to determine Maximum Benefit Amount</param>
    public record EligibilityBasis(int MinHighQuarterEarnings, int PercentWeeklyBenefitRate, int MinQuarters,
        int WagesOutsideOfHighQuarterFactor, int BasePeriodWagesFactor, int NumberOfWeeks,
        int PercentOfBasePeriodWages);
}
