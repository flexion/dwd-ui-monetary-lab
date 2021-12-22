namespace DWD.UI.Monetary.Service.Controllers;

using AutoMapper;
using DWD.UI.Calendar;
using Microsoft.AspNetCore.Mvc;
using DWD.UI.Monetary.Service.Gateways;
using DWD.UI.Monetary.Service.Models.Stubs;
using DWD.UI.Monetary.Service.Models;
using System.Collections.ObjectModel;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Linq;

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
    /// Gets wage entries for a claimant in any of a collection of calendar quarters.
    /// </summary>
    /// <param name="claimantId">The claimant identifier.</param>
    /// <param name="calendarQuarters">A list of quarters from which to return wages.</param>
    /// <returns>All wage entries for claimant during the specified quarters.</returns>
    [SwaggerResponse((int)HttpStatusCode.OK, description: "The request was successfule, and the wages for the requested quarters are returned.")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "The parameters supplied were invalid", typeof(ProblemDetails), "application/problem+json")]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal Server Error", typeof(ProblemDetails), "application/problem+json")]
    [Produces("application/json")]
    [HttpPost]
    [Route("GetWagesForClaimantByQuarters/{claimantId}")]
    public IActionResult GetAllWagesForClaimantByQuarters([FromRoute] string claimantId, [FromBody] Collection<CalendarQuarterDto> calendarQuarters)
    {
        if (calendarQuarters == null)
        {
            return this.Problem("The quarters from which to fetch wages were not given.", null, (int)HttpStatusCode.BadRequest);
        }

        var quarters = new Quarters();
        try
        {
            quarters.AddRange(calendarQuarters.Select(q => this.mapper.Map<Quarter>(q)));

            var claimantWages = this.claimantWageRepository.GetClaimantWagesByClaimantIdByQuarters(claimantId, quarters);
            return this.Ok(claimantWages);
        }
        catch (System.ArgumentOutOfRangeException ex)
        {
            return this.Problem(ex.Message, null, (int)HttpStatusCode.BadRequest);
        }
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
    public IActionResult UpdateClaimantWage([FromRoute] long id, int? year, short? quarter, decimal wages)
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
    public IActionResult CreateClaimantWage(string claimantId, int? year, short? quarter, decimal wages)
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
