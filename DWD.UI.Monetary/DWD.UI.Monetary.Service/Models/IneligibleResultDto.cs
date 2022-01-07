namespace DWD.UI.Monetary.Service.Models;

using System.Collections.ObjectModel;

/// <summary>
/// API presentation result for ineligible reasons.
/// </summary>
public class IneligibleResultDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IneligibleResultDto"/> class.
    /// </summary>
    /// <param name="isEligible">boolean.</param>
    /// <param name="ineligibilityReasons">reasons collection.</param>
    public IneligibleResultDto(bool isEligible, Collection<IneligibilityDisplayReason> ineligibilityReasons)
    {
        this.IsEligible = isEligible;
        this.IneligibilityReasons = ineligibilityReasons;
    }

    /// <summary>
    /// Gets a value indicating whether is claimant eligible.
    /// </summary>
    public bool IsEligible { get; }

    /// <summary>
    /// Gets the reasons for ineligibility.
    /// </summary>
    /// <returns>collection of reasons.</returns>
    public Collection<IneligibilityDisplayReason> IneligibilityReasons { get; }
}
