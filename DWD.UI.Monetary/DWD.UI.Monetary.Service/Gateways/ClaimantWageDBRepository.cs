namespace DWD.UI.Monetary.Service.Gateways;

using System.Collections.ObjectModel;
using System.Linq;
using DWD.UI.Monetary.Service.Frameworks;
using DWD.UI.Monetary.Service.Models.Stubs;

/// <summary>
/// Concrete ClaimantWageDbRepository
/// </summary>
public class ClaimantWageDbRepository : IClaimantWageRepository
{
    /// <summary>
    /// DB context.
    /// </summary>
    private readonly ClaimantWageContext context;

    /// <summary>
    /// Create the new repository with a DB context.
    /// </summary>
    /// <param name="theClaimantWageContext">DB context</param>
    public ClaimantWageDbRepository(ClaimantWageContext theClaimantWageContext) => this.context = theClaimantWageContext;

    /// <summary>
    /// Add a new wage entry.
    /// </summary>
    /// <param name="wage">wage entry</param>
    public void AddClaimantWage(ClaimantWage wage)
    {
        _ = this.context.ClaimantWages.Add(wage);
        _ = this.context.SaveChanges();
    }

    /// <summary>
    /// Update a wage entry by id.
    /// </summary>
    /// <param name="wage">wage entry</param>
    public void UpdateClaimantWage(ClaimantWage wage)
    {
        _ = this.context.ClaimantWages.Update(wage);
        _ = this.context.SaveChanges();
    }

    /// <summary>
    /// Delete a wage entry by id.
    /// </summary>
    /// <param name="id">wage entry id</param>
    public void DeleteClaimantWage(long id)
    {
        var entity = this.context.ClaimantWages.Find(id);
        _ = this.context.ClaimantWages.Remove(entity);
        _ = this.context.SaveChanges();
    }

    /// <summary>
    /// Get a wage entry by id.
    /// </summary>
    /// <param name="id">wage entry id</param>
    /// <returns>wage entry</returns>
    public ClaimantWage GetClaimantWage(long id) => this.context.ClaimantWages.Find(id);

    /// <summary>
    /// Get all wages.
    /// </summary>
    /// <returns>all wages</returns>
    public Collection<ClaimantWage> GetClaimantWages() => new(this.context.ClaimantWages.ToList());

    /// <summary>
    /// Get all wages for a claimant.
    /// </summary>
    /// <param name="claimantId">The claimant ID</param>
    /// <returns>wages for claimant</returns>
    public Collection<ClaimantWage> GetClaimantWagesByClaimantId(string claimantId) =>
        new(this.context.ClaimantWages.Where(c => c.ClaimantId == claimantId).ToList());
}
