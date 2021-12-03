namespace DWD.UI.Monetary.Domain.UseCases
{
    using System.Threading.Tasks;
    using BusinessEntities;

    public interface ICheckEligibilityOfMonetaryRequirements
    {
        /// <summary>
        /// Verify all the monetary requirements
        /// </summary>
        /// <returns>EligibilityResult</returns>
        public Task<EligibilityResult> VerifyAsync(EligibilityVerificationRequest verificationRequest);
    }
}
