#pragma warning disable 1998  // suppress lack of await in sync stub; will not be needed with real gateway
namespace DWD.UI.Monetary.Service.Gateways;

using System.Threading.Tasks;
using Domain.BusinessEntities;
using Domain.Interfaces;

/// <summary>
/// A new EligibilityBasisGateway will replace this one and actually call the appropriate data store for the eligibility information.
/// </summary>
public class StubEligibilityBasisGateway : IEligibilityBasisGateway
{
    public async Task<EligibilityBasis> GetEligibilityBasisAsync() => new EligibilityBasis(1350, 4, 2, 4, 35, 26, 40);
}