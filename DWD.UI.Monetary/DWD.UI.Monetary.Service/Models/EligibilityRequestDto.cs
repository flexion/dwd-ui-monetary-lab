namespace DWD.UI.Monetary.Service.Models

{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information about the claimant necessary for determining eligibility for benefits
    /// </summary>
    public class EligibilityRequestDto
    {
        public string ClaimantId { get; set; }
        public DateTime InitialClaimDate { get; set; }
        public IEnumerable<decimal> WagesOfQuarters { get; set; } = new List<decimal>();
    }
}