namespace DWD.UI.Monetary.Domain.UseCases
{
    using BusinessEntities;

    public interface ICheckEligibilityOfMonetaryRequirements
    {
        /// <summary>
        /// Verify all the monetary requirements
        /// </summary>
        /// <returns>EligibilityResult</returns>
        public EligibilityResult Verify(EligibilityVerificationRequest verificationRequest, EligibilityBasis eligibilityBasis);
    }
}
