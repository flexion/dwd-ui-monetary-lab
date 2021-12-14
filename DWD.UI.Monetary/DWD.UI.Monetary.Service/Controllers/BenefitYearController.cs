namespace DWD.UI.Monetary.Service.Controllers;

using System;
using System.Net.Mime;
using DWD.UI.Monetary.Domain.BusinessEntities;
using DWD.UI.Monetary.Domain.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Provides endpoints for Benefit Year.
/// </summary>
[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("benefityear")]
public class BenefitYearController : ControllerBase
{
    /// <summary>
    /// Local reference to domain logic.
    /// </summary>
    private readonly ICalculateBenefitYear calculateBenefitYear;

    /// <summary>
    /// Initializes a new instance of the <see cref="BenefitYearController"/> class.
    /// </summary>
    /// <param name="calculateBenefitYear">A domain logic reference.</param>
    public BenefitYearController(ICalculateBenefitYear calculateBenefitYear) => this.calculateBenefitYear = calculateBenefitYear;

    /// <summary>
    /// Calculate benefit year for requested date.
    /// </summary>
    /// <remarks>
    /// As a UI worker, the system needs to identify the Benefit year when a claimant files an
    /// initial claim so the department can determine eligibility for benefits.
    ///
    /// <u><b>Requested Date</b></u><br />
    /// The benefit year is calculated based on the VNC date. However, the claimant can request
    /// benefits by delayed by a week. This date preference is handled as a detail for the
    /// consumer to figure out and pass in the date that corresponds to the requested benefit start.
    ///
    /// <u><b>Benefit Year</b></u>
    /// <br /><br />
    /// <em>BenefitWeeks - The number of weeks in a benefit year.</em>
    /// <br /><br />
    /// The value can be 52 or 53. 53 is an exceptional case when for
    /// when a subsequent year claim's base period overlaps with the prior.
    /// <br /><br />
    /// <em>ClaimRequestedDate - The benefit date requested by the VNC (Valid New Claim).</em>
    /// <br /><br />
    /// Usually the claim initiated date but claimant can request benefits begin
    /// in a week after that date.
    /// <br /><br />
    /// <em>BeginDate - The calculated benefit begin date.</em>
    /// <br /><br />
    /// Benefits always begin on a Sunday regardless of the requested date.
    /// <br /><br />
    /// <em>EndDate - The calculated benefit end date.</em>
    /// <br /><br />
    /// Benefits always end on a Sunday 52 weeks after the begin date.
    ///     In exceptional cases to avoid an overlap in base periods for
    /// <br /><br />
    /// <em>BeginYearWeek - The benefit begin year and week.</em>
    /// <br /><br />
    /// First week of the year is the week that contains January 1st
    ///     even if the beginning of the week is in the prior year.
    ///     The week number and year is calculated based on the Saturday
    ///     (week ending date) following the BeginDate.
    /// <br /><br />
    /// <em>EndYearWeek - The benefit end year and week.</em>
    /// <br /><br />
    /// First week of the year is the week that contains January 1st
    ///     even if the beginning of the week is in the prior year.
    ///     The week number and year is calculated based EndDate
    ///     which is always a Saturday.
    ///
    /// </remarks>
    /// <param name="requestedDate">The requested benefit date in standard formats (MM/DD/YYYY, MM-DD-YYYY, YYYY-MM-DD, etc.).</param>
    /// <returns>The calculated benefit year.</returns>
    [Consumes(MediaTypeNames.Text.Plain)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    [Route("lookup-by-date")]
    public ActionResult<BenefitYear> GetBenefitYearForRequestedDate(DateTime requestedDate) =>
        this.Ok(this.calculateBenefitYear.CalculateBenefitYearFromDate(requestedDate));
}
