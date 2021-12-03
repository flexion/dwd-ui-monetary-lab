namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System.Collections.ObjectModel;

    public abstract class EligibilityResult
    {
        public bool IsEligible { get; protected set; }
    }

    /// <summary>
    /// This is the concrete class returned when the claimant is eligible.
    /// Max benefit rate will be added in a future story.
    /// </summary>
    public class EligibleResult : EligibilityResult
    {
        public decimal WeeklyBenefitRate { get; }
        //public decimal MaxBenefitAmount { get; }

        public EligibleResult(decimal weeklyBenefitRate)
        {
            this.IsEligible = true;
            this.WeeklyBenefitRate = weeklyBenefitRate;
            //this.MaxBenefitAmount = null;
        }
    }

    /// <summary>
    /// This is the concrete class returned when the claimant is ineligible.
    /// </summary>
    public class IneligibleResult : EligibilityResult
    {
        public Collection<IneligibilityReason> IneligibilityReasons { get; }

        public IneligibleResult(Collection<IneligibilityReason> reasons)
        {
            this.IsEligible = false;
            this.IneligibilityReasons = reasons ?? new Collection<IneligibilityReason>();
        }
    }
}
