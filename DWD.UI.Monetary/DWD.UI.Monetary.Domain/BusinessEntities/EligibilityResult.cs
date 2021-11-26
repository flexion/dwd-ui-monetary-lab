namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    public record EligibilityResult(bool IsEligible, decimal? WeeklyBenefitRate, decimal? MaxBenefitAmount);
}
