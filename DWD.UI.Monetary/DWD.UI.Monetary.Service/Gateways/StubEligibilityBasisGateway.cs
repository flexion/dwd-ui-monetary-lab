#pragma warning disable 1998  // suppress lack of await in sync stub; will not be needed with real gateway
namespace DWD.UI.Monetary.Service.Gateways;

using System.Threading.Tasks;
using DWD.UI.Monetary.Domain.BusinessEntities;
using DWD.UI.Monetary.Domain.Interfaces;

/// <summary>
/// A new EligibilityBasisGateway will replace this one and actually call the appropriate data store for the eligibility information.
/// </summary>
public class StubEligibilityBasisGateway : IEligibilityBasisGateway
{
    /// <summary>
    /// Provides temporarary values used in determination of eligibility - to be replaced with a data store TBD.
    /// </summary>
    /// <returns>An object holding several values required to determine eligibility and to calculate WBR and MBA.</returns>
    public async Task<EligibilityBasis> GetEligibilityBasisAsync() => new EligibilityBasis(1350, 4, 2, 4, 35, 26, 40);
}
