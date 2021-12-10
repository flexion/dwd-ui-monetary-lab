namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    /// <summary>
    /// This is the concrete class returned when the claimant is eligible.
    /// Max benefit rate will be added in a future story.
    /// </summary>
    public class EligibleResult : EligibilityResult
    {
        /// <summary>
        /// Gets the claimant's Weekly Benefit Rate.
        /// </summary>
        public decimal WeeklyBenefitRate { get; }

        /// <summary>
        /// Gets the claimant's Maximum Annual Benefit Amount.
        /// </summary>
        public decimal MaximumBenefitAmount { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EligibleResult"/> class.
        /// </summary>
        /// <param name="weeklyBenefitRate">The claimant's Weekly Benefit Rate amount computed in accordance with s. 108.05.</param>
        /// <param name="maximumBenefitAmount">The claimant's Maximum Annual Benefit Amount.</param>
        public EligibleResult(decimal weeklyBenefitRate, decimal maximumBenefitAmount)
        {
            this.IsEligible = true;
            this.WeeklyBenefitRate = weeklyBenefitRate;
            this.MaximumBenefitAmount = maximumBenefitAmount;
        }
    }
}
