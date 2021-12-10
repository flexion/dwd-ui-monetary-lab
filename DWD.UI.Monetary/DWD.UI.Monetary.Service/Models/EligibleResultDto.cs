namespace DWD.UI.Monetary.Service.Models
{
    public class EligibleResultDto
    {
        public bool IsEligible { get; }
        public decimal WeeklyBenefitRate { get; }

        public decimal MaximumBenefitAmount { get; }

        public EligibleResultDto(bool isEligible, decimal weeklyBenefitRate, decimal maximumBenefitAmount)
        {
            this.IsEligible = isEligible;
            this.WeeklyBenefitRate = weeklyBenefitRate;
            this.MaximumBenefitAmount = maximumBenefitAmount;
        }
    }
}
