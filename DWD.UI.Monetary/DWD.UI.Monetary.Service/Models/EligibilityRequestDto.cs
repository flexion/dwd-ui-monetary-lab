namespace DWD.UI.Monetary.Service.Models;

using System;
using System.Collections.Generic;

/// <summary>
/// Information about the claimant necessary for determining eligibility for benefits.
/// </summary>
public class EligibilityRequestDto
{
    /// <summary>
    ///  Gets or sets the claimant identifier.
    /// </summary>
    public string ClaimantId { get; set; }

    /// <summary>
    /// Gets or sets the date when the claim first submitted and must be on after 01/01/1900.
    /// </summary>
    public DateTime InitialClaimDate { get; set; }

    /// <summary>
    /// Gets or sets the wages of standard or alternate base periods from all employers.
    /// </summary>
    public IEnumerable<decimal> WagesOfQuarters { get; set; } = new List<decimal>();
}
