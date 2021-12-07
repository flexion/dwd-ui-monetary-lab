namespace DWD.UI.Monetary.Service.Models
{
    using System.Collections.ObjectModel;
    using DWD.UI.Monetary.Domain.BusinessEntities;

    /// <summary>
    /// Data Transfer Object to carry the response when a determination of ineligibility has been made.
    /// </summary>
    public class IneligibleResultDto
    {
        /// <summary>
        /// Gets a value indicating whether the claimant is ineligible based on the base period wages
        /// </summary>
        public bool IsEligible { get; }

        /// <summary>
        /// Gets the collection of reasons for the determination of ineligibility.
        /// </summary>
        public Collection<IneligibilityDisplayReason> IneligibilityReasons { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IneligibleResultDto"/> class.
        /// </summary>
        /// <param name="isEligible">Indicates that the claimant is ineligible based on the base period wages.</param>
        /// <param name="ineligibilityReasons">the collection of reasons for the determination of ineligibility.</param>
        public IneligibleResultDto(bool isEligible, Collection<IneligibilityDisplayReason> ineligibilityReasons)
        {
            this.IsEligible = isEligible;
            this.IneligibilityReasons = ineligibilityReasons;
        }
    }

    /// <summary>
    /// Data Transfer Object to carry the response when a determination of eligibility has been made.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "These classes express a hierarchy and are strongly coupled")]
    public class IneligibilityDisplayReason
    {
        /// <summary>
        /// Gets the ineligibility reason code.
        /// </summary>
        public IneligibilityReason Code { get; }

        /// <summary>
        /// Gets the ineligibility reason description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IneligibilityDisplayReason"/> class.
        /// </summary>
        /// <param name="code">A numeric code representing the ineligibility reason.</param>
        /// <param name="description">The user-friendly description of the reason for ineligibilty</param>
        public IneligibilityDisplayReason(IneligibilityReason code, string description)
        {
            this.Code = code;
            this.Description = description;
        }
    }
}
