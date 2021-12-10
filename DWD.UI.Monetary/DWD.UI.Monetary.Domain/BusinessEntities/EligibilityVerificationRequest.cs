namespace DWD.UI.Monetary.Domain.BusinessEntities;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The object used to request a determination of eligibility.
/// </summary>
public class EligibilityVerificationRequest
{
    /// <summary>
    /// Gets a collection of the wages in the base period.
    /// </summary>
    [Required]
    public Collection<decimal> WagesOfQuarters { get; }

    /// <summary>
    /// Gets the initial Claim date.
    /// </summary>
    public DateTime InitialClaimDate { get; }

    /// <summary>
    /// Gets the claimant ID.
    /// </summary>
    public string ClaimantId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EligibilityVerificationRequest"/> class.
    /// </summary>
    /// <param name="wagesOfQuarters">A collection of the wages in the base period.</param>
    /// <param name="initialClaimDate">The initial Claim date.</param>
    /// <param name="claimantId">The claimant ID.</param>
    public EligibilityVerificationRequest(Collection<decimal> wagesOfQuarters, DateTime initialClaimDate, string claimantId)
    {
        this.WagesOfQuarters = wagesOfQuarters;
        this.InitialClaimDate = initialClaimDate;
        this.ClaimantId = claimantId;
    }
}
