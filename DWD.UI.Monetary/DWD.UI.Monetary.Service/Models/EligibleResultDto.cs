namespace DWD.UI.Monetary.Service.Models
{
    /// <summary>
    /// Data Transfer Object to carry the response when a determination of Eligibility has been made.
    /// </summary>
    public class EligibleResultDto
    {
        /// <summary>
        /// Gets a value indicating whether the claimant is ineligible based on the base period wages
        /// </summary>
        public bool IsEligible { get; }

        /// <summary>
        /// Gets the Weekly Benefit Rate.
        /// </summary>
        public decimal WeeklyBenefitRate { get; }

        /// <summary>
        /// Gets the Maximum Benefit Amount.
        /// </summary>
        public decimal MaximumBenefitAmount { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EligibleResultDto"/> class.
        /// </summary>
        /// <param name="isEligible">Indicates that the claimant is ineligible based on the base period wages.</param>
        /// <param name="weeklyBenefitRate">The calulated Weekly Benefit Rate.</param>
        public EligibleResultDto(bool isEligible, decimal weeklyBenefitRate, decimal maximumBenefitAmount)
        {
            this.IsEligible = isEligible;
            this.WeeklyBenefitRate = weeklyBenefitRate;
            this.MaximumBenefitAmount = maximumBenefitAmount;
        }
    }
}
