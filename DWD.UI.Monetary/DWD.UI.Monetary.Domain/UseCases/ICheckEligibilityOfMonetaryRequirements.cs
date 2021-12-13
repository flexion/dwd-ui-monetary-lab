namespace DWD.UI.Monetary.Domain.UseCases;

using System.Threading.Tasks;
using DWD.UI.Monetary.Domain.BusinessEntities;

/// <summary>
/// Interface for the eligibility determination use case.
/// </summary>
public interface ICheckEligibilityOfMonetaryRequirements
{
    /// <summary>
    /// Verify all the monetary requirements.
    /// </summary>
    /// <param name="verificationRequest">The EligibilityVerificationRequest received from the service layer.</param>
    /// <returns>EligibilityResult.</returns>
    public Task<EligibilityResult> VerifyAsync(EligibilityVerificationRequest verificationRequest);
}
