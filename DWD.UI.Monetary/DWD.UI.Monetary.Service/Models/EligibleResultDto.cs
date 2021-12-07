namespace DWD.UI.Monetary.Service.Models
{
    /// <summary>
    /// Result when claimant is eligible.
    /// </summary>
    public class EligibleResultDto
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isEligible">boolean</param>
        /// <param name="weeklyBenefitRate">benefit rate</param>
        public EligibleResultDto(bool isEligible, decimal weeklyBenefitRate)
        {
            this.IsEligible = isEligible;
            this.WeeklyBenefitRate = weeklyBenefitRate;
        }

        /// <summary>
        /// Is eligible?
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsEligible { get; }

        /// <summary>
        /// Gets the benefit rate for eligible claimant.
        /// </summary>
        /// <returns>benefit rate</returns>
        public decimal WeeklyBenefitRate { get; }
    }
}
