namespace DWD.UI.Monetary.Service.Models
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// API presentation result for ineligible reasons.
    /// </summary>
    public class IneligibleResultDto
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isEligible">boolean</param>
        /// <param name="ineligibilityReasons">reasons collection</param>
        public IneligibleResultDto(bool isEligible, Collection<IneligibilityDisplayReason> ineligibilityReasons)
        {
            this.IsEligible = isEligible;
            this.IneligibilityReasons = ineligibilityReasons;
        }

        /// <summary>
        /// Is claimant eligible.
        /// </summary>
        public bool IsEligible { get; }

        /// <summary>
        /// The reasons for ineligibility.
        /// </summary>
        /// <returns>collection of reasons</returns>
        public Collection<IneligibilityDisplayReason> IneligibilityReasons { get; }
    }
}
