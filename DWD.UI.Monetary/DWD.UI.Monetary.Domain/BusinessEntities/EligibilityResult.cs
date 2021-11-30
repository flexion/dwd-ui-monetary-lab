namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System.Collections.ObjectModel;

    public class EligibilityResult
    {
        public bool IsEligible { get; }
        public decimal? WeeklyBenefitRate { get; }
        public decimal? MaxBenefitAmount { get; }
        public Collection<IneligibilityReason> IneligibilityReasons { get; } = new();

        public EligibilityResult(decimal? weeklyBenefitRate)
        {
            this.IsEligible = true;
            this.WeeklyBenefitRate = weeklyBenefitRate;
            this.MaxBenefitAmount = null;
        }

        public EligibilityResult(Collection<IneligibilityReason> reasons)
        {
            this.IsEligible = false;
            this.IneligibilityReasons = reasons ?? new Collection<IneligibilityReason>();
        }
    };
}
