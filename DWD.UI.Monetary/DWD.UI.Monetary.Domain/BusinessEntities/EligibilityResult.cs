namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System.Collections.ObjectModel;

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

    /// <summary>
    /// This is the concrete class returned when the claimant is eligible.
    /// Max benefit rate will be added in a future story.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "<This is a cohesive set of an abstract base class and its subclasses.>")]
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

    /// <summary>
    /// This is the concrete class returned when the claimant is ineligible.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "<This is a cohesive set of an abstract base class and its subclasses.>")]
    public class IneligibleResult : EligibilityResult
    {
        /// <summary>
        /// Gets a collection of numeric codes indicating all of the reasons for the determination of ineligbility.
        /// </summary>
        public Collection<IneligibilityReason> IneligibilityReasons { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IneligibleResult"/> class.
        /// </summary>
        /// <param name="reasons">A collection of numeric codes indicating all of the reasons for the determination of ineligbility.</param>
        public IneligibleResult(Collection<IneligibilityReason> reasons)
        {
            this.IsEligible = false;
            this.IneligibilityReasons = reasons ?? new Collection<IneligibilityReason>();
        }
    }
}
