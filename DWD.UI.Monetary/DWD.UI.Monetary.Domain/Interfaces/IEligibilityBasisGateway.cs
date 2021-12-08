namespace DWD.UI.Monetary.Domain.Interfaces;

using System.Threading.Tasks;
using Domain.BusinessEntities;

public interface IEligibilityBasisGateway
{
    Task<EligibilityBasis> GetEligibilityBasisAsync();
}