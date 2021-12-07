namespace DWD.UI.Monetary.Service.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information about the claimant necessary for determining eligibility for benefits
    /// </summary>
    public class EligibilityRequestDto
    {
        /// <summary>
        /// ID for the claimant.
        /// </summary>
        public string ClaimantId { get; set; }

        /// <summary>
        /// The VNC initial claim date.
        /// </summary>
        public DateTime InitialClaimDate { get; set; }

        /// <summary>
        /// Quarterly wages.s
        /// </summary>
        public IEnumerable<decimal> WagesOfQuarters { get; set; } = new List<decimal>();
    }
}
