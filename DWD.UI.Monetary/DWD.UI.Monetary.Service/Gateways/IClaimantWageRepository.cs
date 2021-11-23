namespace DWD.UI.Monetary.Service.Gateways
{
    using System.Collections.ObjectModel;
    using Models.Stubs;

    public interface IClaimantWageRepository
    {
        /// <summary>
        /// Add a new wage entry.
        /// </summary>
        /// <param name="wage">wage entry</param>
        void AddClaimantWage(ClaimantWage wage);
        /// <summary>
        /// Update a wage entry by id.
        /// </summary>
        /// <param name="wage">wage entry</param>
        void UpdateClaimantWage(ClaimantWage wage);
        /// <summary>
        /// Delete a wage entry by id.
        /// </summary>
        /// <param name="id">wage entry id</param>
        void DeleteClaimantWage(long id);
        /// <summary>
        /// Get a wage entry by id.
        /// </summary>
        /// <param name="id">wage entry id</param>
        /// <returns>wage entry</returns>
        ClaimantWage GetClaimantWage(long id);
        /// <summary>
        /// Get all wages.
        /// </summary>
        /// <returns>all wages</returns>
        Collection<ClaimantWage> GetClaimantWages();
        /// <summary>
        /// Get all wages for a claimant.
        /// </summary>
        /// <param name="claimantId"></param>
        /// <returns>wages for claimant</returns>
        Collection<ClaimantWage> GetClaimantWagesByClaimantId(string claimantId);
    }
}