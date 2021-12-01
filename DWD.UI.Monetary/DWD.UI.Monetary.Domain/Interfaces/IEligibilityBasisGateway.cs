namespace DWD.UI.Monetary.Service.Gateways
{
    using System.Threading.Tasks;
    using Domain.BusinessEntities;

    public interface IEligibilityBasisGateway
    {
        Task<EligibilityBasis> GetEligibilityBasisAsync();
    }
}
