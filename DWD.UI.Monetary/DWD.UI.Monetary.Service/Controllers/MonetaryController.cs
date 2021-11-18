#pragma warning disable IDE0009

namespace DWD.UI.Monetary.Service.Controllers
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Domain.UseCases;
    using Mappers;
    using Models;
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// Provides endpoints for monetary.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MonetaryController : ControllerBase
    {
        /// <summary>
        /// Local logger reference.
        /// </summary>
        private readonly ILogger<MonetaryController> logger;

        /// <summary>
        /// Local reference to domain logic.
        /// </summary>
        private readonly ICalculateBasePeriod calculateBasePeriod;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">A logger reference.</param>
        /// <param name="calculateBasePeriod">A domain logic reference.</param>
        public MonetaryController(ILogger<MonetaryController> logger, ICalculateBasePeriod calculateBasePeriod)
        {
            this.logger = logger;
            this.calculateBasePeriod = calculateBasePeriod;
        }

        /// <summary>
        /// Calculate base period from initial claim date.
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
        /// <param name="initialClaimDate">The initial claim date in standard formats (MM/DD/YYYY, MM-DD-YYYY, YYYY-MM-DD, etc.).</param>
        /// <param name="useAltBase">Calculate the alternate base period?</param>
        /// <returns>The calculated base period.</returns>
        [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(IBasePeriodDto), "application/json")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Initial Claim Date", typeof(ProblemDetails), "application/problem+json")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal Server Error", typeof(ProblemDetails), "application/problem+json")]
        [Produces("application/json")]
        [HttpGet]
        [Route("GetBasePeriodFromInitialClaimDate")]
        public IActionResult GetBasePeriodFromInitialClaimDate(DateTime initialClaimDate, bool useAltBase)
        {
            try
            {
                // Calculate the base period
                var basePeriod = this.calculateBasePeriod.CalculateBasePeriodFromInitialClaimDate(initialClaimDate);

                // Map from IBasePeriod to BasePeriodDto and return
                var result = BasePeriodMapper.MapToDto(useAltBase ? basePeriod.AltBasePeriodQuarters : basePeriod.BasePeriodQuarters);
                return this.Ok(result);
            }
            catch (ArgumentException argumentException)
            {
                // Log and return http 400
                this.logger.LogError(argumentException, "Error calculating base period from initialClaimDate={0}", initialClaimDate);
                var problem = this.Problem(argumentException.Message, null, 400);
                return problem;
            }
        }
    }
}
