namespace DWD.UI.Monetary.Service.Gateways
{
    using System.Collections.ObjectModel;
    using Models.Stubs;

    public interface IClaimantWageRepository
    {
        void AddClaimantWage(ClaimantWage wage);
        void UpdateClaimantWage(ClaimantWage wage);
        void DeleteClaimantWage(long id);
        ClaimantWage GetClaimantWage(long id);
        Collection<ClaimantWage> GetClaimantWages();
    }
}
