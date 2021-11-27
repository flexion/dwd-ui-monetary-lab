namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;
    using System.Collections.ObjectModel;

    public class EligibilityVerificationRequest
    {
        public Collection<decimal> WagesOfQuarters { get; }
        public DateTime InitialClaimDate { get; }
        public string ClaimantId { get; }

        public EligibilityVerificationRequest(Collection<decimal> wagesOfQuarters, DateTime initialClaimDate, string claimantId)
        {
            this.WagesOfQuarters = wagesOfQuarters;
            this.InitialClaimDate = initialClaimDate;
            this.ClaimantId = claimantId;
        }
    }
}
