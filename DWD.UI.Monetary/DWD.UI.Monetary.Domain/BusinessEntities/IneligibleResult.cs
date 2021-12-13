namespace DWD.UI.Monetary.Domain.BusinessEntities;

using System.Collections.ObjectModel;

/// <summary>
/// This is the concrete class returned when the claimant is ineligible.
/// </summary>
public class IneligibleResult : EligibilityResult
{
    /// <summary>
    /// Gets a collection of numeric codes indicating all of the reasons for the determination of ineligibility.
    /// </summary>
    public Collection<IneligibilityReason> IneligibilityReasons { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IneligibleResult"/> class.
    /// </summary>
    /// <param name="reasons">A collection of numeric codes indicating all of the reasons for the determination of ineligibility.</param>
    public IneligibleResult(Collection<IneligibilityReason> reasons)
    {
        this.IsEligible = false;
        this.IneligibilityReasons = reasons ?? new Collection<IneligibilityReason>();
    }
}
