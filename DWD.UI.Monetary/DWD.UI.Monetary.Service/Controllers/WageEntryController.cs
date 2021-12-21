namespace DWD.UI.Monetary.Service.Controllers;

using AutoMapper;
using DWD.UI.Calendar;
using Microsoft.AspNetCore.Mvc;
using DWD.UI.Monetary.Service.Gateways;
using DWD.UI.Monetary.Service.Models.Stubs;
using DWD.UI.Monetary.Service.Models;
using System.Collections.ObjectModel;
using System;

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
    /// Automapper injection.
    /// </summary>
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="WageEntryController"/> class.
    /// </summary>
    /// <param name="theClaimantWageRepository">Wages.</param>
    /// <param name="mapper">Mapper.</param>
    public WageEntryController(IClaimantWageRepository theClaimantWageRepository, IMapper mapper)
    {
        this.claimantWageRepository = theClaimantWageRepository;
        this.mapper = mapper;
    }

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
    /// Gets claimant quarterly wage entries for claimant by quarters.
    /// </summary>
    /// <param name="claimantId">claimant identifier.</param>
    /// <param name="calendarQuarters">list of quarters from which to return wages.</param>
    /// <returns>All wage entries for claimant.</returns>
    [HttpGet]
    [Route("GetAllWagesForClaimantByQuarters/{claimantId}")]
    public IActionResult GetAllWagesForClaimantByQuarters([FromBody] string claimantId, [FromBody] Collection<CalendarQuarterDto> calendarQuarters)
    {
        if (calendarQuarters == null)
        {
            throw new ArgumentNullException(nameof(calendarQuarters));
        }

        var quarters = new Quarters();
        foreach (var quarter in calendarQuarters)
        {
            quarters.Add(this.mapper.Map<Quarter>(quarter));
        }

        var claimantWages = this.claimantWageRepository.GetClaimantWagesByClaimantIdByQuarters(claimantId, quarters);
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
            TotalWages = wages,
        };

        this.claimantWageRepository.AddClaimantWage(wage);
        var claimantWages = this.claimantWageRepository.GetClaimantWages();
        return this.Ok(claimantWages);
    }
}
