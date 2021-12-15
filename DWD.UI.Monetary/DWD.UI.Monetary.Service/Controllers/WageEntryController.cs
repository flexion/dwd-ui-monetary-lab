#pragma warning disable IDE0009

namespace DWD.UI.Monetary.Service.Controllers;

using Microsoft.AspNetCore.Mvc;
using DWD.UI.Monetary.Service.Gateways;
using DWD.UI.Monetary.Service.Models.Stubs;

/// <summary>
/// Provides endpoints for entering wage data.
/// </summary>
[ApiController]
[Route("[controller]")]
public class WageEntryController : ControllerBase
{
    /// <summary>
    /// Claimant Wages.
    /// </summary>
    private readonly IClaimantWageRepository claimantWageRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="WageEntryController"/> class.
    /// </summary>
    /// <param name="theClaimantWageRepository">Wages.</param>
    public WageEntryController(IClaimantWageRepository theClaimantWageRepository) =>
        this.claimantWageRepository = theClaimantWageRepository;

    /// <summary>
    /// Get claimant quarterly wage entry by entry id.
    /// </summary>
    /// <param name="id">entry id.</param>
    /// <returns>wage entry.</returns>
    [HttpGet]
    [Route("GetClaimantWage/{id}")]
    public IActionResult GetClaimantWage([FromRoute] long id)
    {
        var claimantWage = this.claimantWageRepository.GetClaimantWage(id);
        return this.Ok(claimantWage);
    }

    /// <summary>
    /// Gets all claimant quarterly wage entries.
    /// </summary>
    /// <returns>All wage entries.</returns>
    [HttpGet]
    [Route("GetAllClaimantWages")]
    public IActionResult GetAllClaimantWages()
    {
        var claimantWages = this.claimantWageRepository.GetClaimantWages();
        return this.Ok(claimantWages);
    }

    /// <summary>
    /// Gets all claimant quarterly wage entries for claimant.
    /// </summary>
    /// <param name="claimantId">claimant identifier.</param>
    /// <returns>All wage entries for claimant.</returns>
    [HttpGet]
    [Route("GetAllClaimantWagesForClaimant/{claimantId}")]
    public IActionResult GetAllClaimantWagesForClaimant([FromRoute] string claimantId)
    {
        var claimantWages = this.claimantWageRepository.GetClaimantWagesByClaimantId(claimantId);
        return this.Ok(claimantWages);
    }

    /// <summary>
    /// Updates a single wage entry by wage entry id.
    /// </summary>
    /// <param name="id">wage entry id.</param>
    /// <param name="year">quarterly wages year.</param>
    /// <param name="quarter">quarterly wages quarter.</param>
    /// <param name="wages">quarterly wages.</param>
    /// <returns>All wage entries.</returns>
    [HttpPut]
    [Route("UpdateClaimantWage/{id}")]
    public IActionResult UpdateClaimantWage([FromRoute] long id, short? year, short? quarter, decimal wages)
    {
        var claimantWage = this.claimantWageRepository.GetClaimantWage(id);
        claimantWage.WageYear = year;
        claimantWage.WageQuarter = quarter;
        claimantWage.TotalWages = wages;

        this.claimantWageRepository.UpdateClaimantWage(claimantWage);
        var claimantWages = this.claimantWageRepository.GetClaimantWages();
        return this.Ok(claimantWages);
    }

    /// <summary>
    /// Deletes a wage entry by wage entry id.
    /// </summary>
    /// <param name="id">wage id.</param>
    /// <returns>All wage entries.</returns>
    [HttpDelete]
    [Route("DeleteClaimantWage/{id}")]
    public IActionResult DeleteClaimantWage([FromRoute] long id)
    {
        this.claimantWageRepository.DeleteClaimantWage(id);
        var claimantWages = this.claimantWageRepository.GetClaimantWages();
        return this.Ok(claimantWages);
    }

    /// <summary>
    /// Create a quarterly wages entry.
    /// </summary>
    /// <param name="claimantId">claimant identifier.</param>
    /// <param name="year">quarterly wages year.</param>
    /// <param name="quarter">quarterly wages quarter.</param>
    /// <param name="wages">quarterly wages.</param>
    /// <returns>All wage entries.</returns>
    [HttpPost]
    [Route("CreateClaimantWage")]
    public IActionResult CreateClaimantWage(string claimantId, short? year, short? quarter, decimal wages)
    {
        var wage = new ClaimantWage
        {
            ClaimantId = claimantId,
            WageYear = year,
            WageQuarter = quarter,
            TotalWages = wages
        };

        this.claimantWageRepository.AddClaimantWage(wage);
        var claimantWages = this.claimantWageRepository.GetClaimantWages();
        return this.Ok(claimantWages);
    }
}
