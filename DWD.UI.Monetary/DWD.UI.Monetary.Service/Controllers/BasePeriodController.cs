namespace DWD.UI.Monetary.Service.Controllers;

using System;
using System.Globalization;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DWD.UI.Monetary.Domain.UseCases;
using DWD.UI.Monetary.Service.Mappers;
using Microsoft.AspNetCore.Http;
using Models;

/// <summary>
/// Provides endpoints for BasePeriod.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("v{version:apiVersion}")]
public class BasePeriodController : ControllerBase
{
    /// <summary>
    /// Date error logging message.
    /// </summary>
    private static readonly Action<ILogger, string, Exception> DateError =
        LoggerMessage.Define<string>(
                        LogLevel.Information,
                        new EventId(2, nameof(DateTime)),
                        "Error calculating standard base period from initialClaimDate={InitialClaimDate}");

    /// <summary>
    /// Year/week error logging message.
    /// </summary>
    private static readonly Action<ILogger, string, Exception> YearWeekError =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(2, nameof(DateTime)),
            "Error calculating standard base period from Year/Week={YearWeek}");

    /// <summary>
    /// Local logger reference.
    /// </summary>
    private readonly ILogger<BasePeriodController> logger;

    /// <summary>
    /// Local reference to domain logic.
    /// </summary>
    private readonly ICalculateBasePeriod calculateBasePeriod;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="logger">A logger reference.</param>
    /// <param name="calculateBasePeriod">A domain logic reference.</param>
    public BasePeriodController(ILogger<BasePeriodController> logger, ICalculateBasePeriod calculateBasePeriod)
    {
        this.logger = logger;
        this.calculateBasePeriod = calculateBasePeriod;
    }

    /// <summary>
    /// Calculate standard base period from initial claim date.
    /// </summary>
    /// <remarks>
    /// <b>Standard Calculation:</b><br />
    /// The first four of the last five completed calendar quarters before the week a claimant
    /// files an initial claim application for a new benefit year.<br /><br />
    /// Quarters - For unemployment purposes the quarter does not start until the first full week of that month.<br /><br />
    /// Example -  October of 2021.<br />
    /// The first full week of October was Sunday-Saturday 10/3-10/9 so
    /// that is when the quarter 4 would start for unemployment purposes. The week of 9/26-10/2
    /// would be considered to be apart of Q3.<br /><br />
    /// </remarks>
    /// <param name="initialClaimDate">The initial claim date in standard formats (MM/DD/YYYY, MM-DD-YYYY, YYYY-MM-DD, etc.). Default value is 1/1/1.</param>
    /// <returns>The calculated base period.</returns>
    /// <response code="400">Invalid Date.</response>
    [Consumes(MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(IBasePeriodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    [Route("standard-base-period-by-date")]
    public IActionResult GetStandardBasePeriodFromInitialClaimDate(DateTime initialClaimDate)
    {
        try
        {
            // Calculate the base period
            var basePeriod = this.calculateBasePeriod.CalculateBasePeriodFromInitialClaimDate(initialClaimDate);

            // Map from IBasePeriod to BasePeriodDto and return
            var result = BasePeriodMapper.MapToDto(basePeriod.BasePeriodQuarters);
            return this.Ok(result);
        }
        catch (ArgumentException argumentException)
        {
            // Log and return http 400
            DateError(this.logger, initialClaimDate.ToString(CultureInfo.CurrentCulture), argumentException);
            return this.Problem(argumentException.Message, null, 400);
        }
    }

    /// <summary>
    /// Calculate standard base period from Year and Week.
    /// </summary>
    /// <remarks>
    /// <b>Standard Calculation:</b><br />
    /// The first four of the last five completed calendar quarters before the week a claimant
    /// files an initial claim application for a new benefit year.<br /><br />
    /// Quarters - For unemployment purposes the quarter does not start until the first full week of that month.<br /><br />
    /// Example -  October of 2021.<br />
    /// The first full week of October was Sunday-Saturday 10/3-10/9 so
    /// that is when the quarter 4 would start for unemployment purposes. The week of 9/26-10/2
    /// would be considered to be apart of Q3.<br /><br />
    /// </remarks>
    /// <param name="year">Year to calculate the base periods. Default value is 0.</param>
    /// <param name="week">Week number of the year. Must be between 1 and 52 or 53(If first day of the year lands on saturday). Default value is 0.</param>
    /// <returns>The calculated base period.</returns>
    /// <response code="400">Invalid Year/Week.</response>
    [Consumes(MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(IBasePeriodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    [Route("standard-base-period-by-year-week")]
    public IActionResult GetStandardBasePeriodFromYearAndWeek(int year, int week)
    {
        try
        {
            // Calculate the base period
            var basePeriod = this.calculateBasePeriod.CalculateBasePeriodFromYearAndWeek(year, week);

            // Map from IBasePeriod to BasePeriodDto and return
            var result = BasePeriodMapper.MapToDto(basePeriod.BasePeriodQuarters);
            return this.Ok(result);
        }
        catch (ArgumentException argumentException)
        {
            // Log and return http 400
            YearWeekError(this.logger, year + "/" + week, argumentException);
            return this.Problem(argumentException.Message, null, 400);
        }
    }

    /// <summary>
    /// Calculate alternate base period from initial claim date.
    /// </summary>
    /// <remarks>
    /// <b>Alternate Calculation:</b><br />
    /// The alternate base period will be the four most recently completed calendar quarters before the
    /// week you filed your initial claim application for a new benefit year<br /><br />
    /// Quarters - For unemployment purposes the quarter does not start until the first full week of that month.<br /><br />
    /// Example - October of 2021.<br />
    /// The first full week of October was 10/3-10/9 so that is when the quarter 4 would start for unemployment purposes.
    /// The week of 9/26-10/2 would be considered to be apart of Q3.<br /><br />
    /// Example - Claimant files a claim 11/10/21 (quarter 4).<br />
    /// He does not qualify for a standard base period so the system will check for the alternate base period.
    /// </remarks>
    /// <param name="initialClaimDate">The initial claim date in standard formats (MM/DD/YYYY, MM-DD-YYYY, YYYY-MM-DD, etc.). Default value is 1/1/1.</param>
    /// <returns>The calculated base period.</returns>
    /// <response code="400">Invalid Date.</response>
    [Consumes(MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(IBasePeriodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    [Route("alternative-base-period-by-date")]
    public IActionResult GetAlternateBasePeriodFromInitialClaimDate(DateTime initialClaimDate)
    {
        try
        {
            // Calculate the base period
            var basePeriod = this.calculateBasePeriod.CalculateBasePeriodFromInitialClaimDate(initialClaimDate);

            // Map from IBasePeriod to BasePeriodDto and return
            var result = BasePeriodMapper.MapToDto(basePeriod.AltBasePeriodQuarters);
            return this.Ok(result);
        }
        catch (ArgumentException argumentException)
        {
            // Log and return http 400
            DateError(this.logger, initialClaimDate.ToString(CultureInfo.CurrentCulture), argumentException);
            return this.Problem(argumentException.Message, null, 400);
        }
    }

    /// <summary>
    /// Calculate alternate base period from year and week.
    /// </summary>
    /// <remarks>
    /// <b>Alternate Calculation:</b><br />
    /// The alternate base period will be the four most recently completed calendar quarters before the
    /// week you filed your initial claim application for a new benefit year<br /><br />
    /// Quarters - For unemployment purposes the quarter does not start until the first full week of that month.<br /><br />
    /// Example - October of 2021.<br />
    /// The first full week of October was 10/3-10/9 so that is when the quarter 4 would start for unemployment purposes.
    /// The week of 9/26-10/2 would be considered to be apart of Q3.<br /><br />
    /// Example - Claimant files a claim 11/10/21 (quarter 4).<br />
    /// He does not qualify for a standard base period so the system will check for the alternate base period.
    /// </remarks>
    /// <param name="year">Year to calculate the base periods. Default value is 0.</param>
    /// <param name="week">Week number of the year. Must be between 1 and 52 or 53(If first day of the year lands on saturday). Default value is 0.</param>
    /// <returns>The calculated base period.</returns>
    /// <response code="400">Invalid Year/Week.</response>
    [Consumes(MediaTypeNames.Text.Plain)]
    [ProducesResponseType(typeof(IBasePeriodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    [Route("alternative-base-period-by-year-week")]
    public IActionResult GetAlternateBasePeriodFromYearAndWeek(int year, int week)
    {
        try
        {
            // Calculate the base period
            var basePeriod = this.calculateBasePeriod.CalculateBasePeriodFromYearAndWeek(year, week);

            // Map from IBasePeriod to BasePeriodDto and return
            var result = BasePeriodMapper.MapToDto(basePeriod.AltBasePeriodQuarters);
            return this.Ok(result);
        }
        catch (ArgumentException argumentException)
        {
            // Log and return http 400
            YearWeekError(this.logger, year + "/" + week, argumentException);
            return this.Problem(argumentException.Message, null, 400);
        }
    }
}
