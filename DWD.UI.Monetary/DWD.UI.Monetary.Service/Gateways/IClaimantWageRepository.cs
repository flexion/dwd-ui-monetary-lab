namespace DWD.UI.Monetary.Service.Gateways;

using System.Collections.ObjectModel;
using Calendar;
using DWD.UI.Monetary.Service.Models.Stubs;

/// <summary>
/// Interface for ClaimantWageRepository.
/// </summary>
public interface IClaimantWageRepository
{
    /// <summary>
    /// Add a new wage entry.
    /// </summary>
    /// <param name="wage">wage entry.</param>
    void AddClaimantWage(ClaimantWage wage);

    /// <summary>
    /// Update a wage entry by id.
    /// </summary>
    /// <param name="wage">wage entry.</param>
    void UpdateClaimantWage(ClaimantWage wage);

    /// <summary>
    /// Delete a wage entry by id.
    /// </summary>
    /// <param name="id">wage entry id.</param>
    void DeleteClaimantWage(long id);

    /// <summary>
    /// Get a wage entry by id.
    /// </summary>
    /// <param name="id">wage entry id.</param>
    /// <returns>wage entry.</returns>
    ClaimantWage GetClaimantWage(long id);

    /// <summary>
    /// Get all wages.
    /// </summary>
    /// <returns>all wages.</returns>
    Collection<ClaimantWage> GetClaimantWages();

    /// <summary>
    /// Get all wages for a claimant.
    /// </summary>
    /// <param name="claimantId">Claimant ID.</param>
    /// <returns>wages for claimant.</returns>
    Collection<ClaimantWage> GetClaimantWagesByClaimantId(string claimantId);

    /// <summary>
    /// Get all wages for a claimant for specific quarters.
    /// </summary>
    /// <param name="claimantId">Claimant ID.</param>
    /// <param name="quarters">Quarters.</param>
    /// <returns>wages for claimant.</returns>
    Collection<ClaimantWage> GetClaimantWagesByClaimantIdByQuarters(string claimantId, Quarters quarters);
}
