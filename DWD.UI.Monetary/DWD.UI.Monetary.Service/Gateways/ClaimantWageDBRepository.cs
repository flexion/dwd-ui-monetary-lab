namespace DWD.UI.Monetary.Service.Gateways
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Frameworks;
    using Models.Stubs;

    public class ClaimantWageDbRepository : IClaimantWageRepository
    {
        private readonly ClaimantWageContext context;

        public ClaimantWageDbRepository(ClaimantWageContext theClaimantWageContext) => this.context = theClaimantWageContext;

        public void AddClaimantWage(ClaimantWage wage)
        {
            _ = this.context.ClaimantWages.Add(wage);
            _ = this.context.SaveChanges();
        }

        public void UpdateClaimantWage(ClaimantWage wage)
        {
            _ = this.context.ClaimantWages.Update(wage);
            _ = this.context.SaveChanges();
        }

        public void DeleteClaimantWage(long id)
        {
            var entity = this.context.ClaimantWages.Find(id);
            _ = this.context.ClaimantWages.Remove(entity);
            _ = this.context.SaveChanges();
        }

        public ClaimantWage GetClaimantWage(long id) => this.context.ClaimantWages.Find(id);

        public Collection<ClaimantWage> GetClaimantWages() => new(this.context.ClaimantWages.ToList());
    }
}
