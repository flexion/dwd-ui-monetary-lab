namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// This is an abstract base class for eligible and ineligible results, which need different properties
    /// </summary>
    public abstract class EligibilityResult
    {
        /// <summary>
        /// Indicates that the claimant is eligible for benefits
        /// </summary>
        public bool IsEligible { get; protected set; }
    }

    /// <summary>
    /// This is the concrete class returned when the claimant is eligible.
    /// </summary>
    public class EligibleResult : EligibilityResult
    {
        /// <summary>
        /// The claimant's Weekly Benefit Rate amount computed in accordance with s. 108.05
        /// </summary>
        public decimal WeeklyBenefitRate { get; }

        /// <summary>
        /// The total amount of benefits payable to the claimant in a benefit year.
        /// </summary>
        public decimal MaximumBenefitAmount { get; }

        /// <summary>
        /// Instantiates a new EligibleReason to report values associated with the determination
        /// </summary>
        /// <param name="weeklyBenefitRate"></param>
        /// <param name="maximumBenefitAmount"></param>
        public EligibleResult(decimal weeklyBenefitRate, decimal maximumBenefitAmount)
        {
            this.IsEligible = true;
            this.WeeklyBenefitRate = weeklyBenefitRate;
            this.MaximumBenefitAmount = maximumBenefitAmount;
        }
    }

    /// <summary>
    /// This is the concrete class returned when the claimant is ineligible.
    /// </summary>
    public class IneligibleResult : EligibilityResult
    {
        /// <summary>
        /// A collection of numeric codes indicating all of the reasons for the determination of ineligbility
        /// </summary>
        public Collection<IneligibilityReason> IneligibilityReasons { get; }

        /// <summary>
        /// Instantiates a new IneligibleReason to carry the reasons for the determination
        /// </summary>
        /// <param name="reasons"></param>
        public IneligibleResult(Collection<IneligibilityReason> reasons)
        {
            this.IsEligible = false;
            this.IneligibilityReasons = reasons ?? new Collection<IneligibilityReason>();
        }
    }
}
