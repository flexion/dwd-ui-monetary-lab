namespace DWD.UI.Monetary.Domain.Interfaces;

using System.Threading.Tasks;
using DWD.UI.Monetary.Domain.BusinessEntities;

/// <summary>
/// Interface to a gateway that provides configurable factors used in calculating eligibility.
/// </summary>
public interface IEligibilityBasisGateway
{
    /// <summary>
    /// Get the eligibility basis settings from a gateway.
    /// </summary>
    /// <returns>A Task that provides an EligibilityBasis object.</returns>
    Task<EligibilityBasis> GetEligibilityBasisAsync();
}
