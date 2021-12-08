namespace DWD.UI.Monetary.Service.Models;

public class EligibleResultDto
{
    public bool IsEligible { get; }
    public decimal WeeklyBenefitRate { get; }

    public EligibleResultDto(bool isEligible, decimal weeklyBenefitRate)
    {
        this.IsEligible = isEligible;
        this.WeeklyBenefitRate = weeklyBenefitRate;
    }
}