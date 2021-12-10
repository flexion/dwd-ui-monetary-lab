namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    /// <summary>
    /// This is an abstract base class for eligible and ineligible results, which need different properties.
    /// </summary>
    public abstract class EligibilityResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the claimant is eligible for benefits.
        /// </summary>
        public bool IsEligible { get; protected set; }
    }
}
