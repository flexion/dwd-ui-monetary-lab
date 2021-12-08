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
        /// Initializes a new instance of the <see cref="EligibleResult"/> class.
        /// </summary>
        /// <param name="weeklyBenefitRate">The claimant's Weekly Benefit Rate amount computed in accordance with s. 108.05.</param>
        public EligibleResult(decimal weeklyBenefitRate)
        {
            this.IsEligible = true;
            this.WeeklyBenefitRate = weeklyBenefitRate;
        }
    }
}
