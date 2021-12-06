namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    public record EligibilityBasis(int MinHighQuarterEarnings, int PercentWeeklyBenefitRate, int MinQuarters,
        int WagesOutsideOfHighQuarterFactor, int BasePeriodWagesFactor, int NumberOfWeeks,
        int PercentOfBasePeriodWages);
}
