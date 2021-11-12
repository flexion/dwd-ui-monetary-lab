#pragma warning disable IDE0009

namespace DWD.UI.Monetary.Service.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using DWD.UI.Monetary.Domain.UseCases;
    using DWD.UI.Monetary.Service.Mappers;

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
        /// <param name="initialClaimDate">The initial claim date.</param>
        /// <param name="useAltBase">Check the alt base period.</param>
        /// <returns>The calculated base period.</returns>
        [HttpGet]
        [Route("GetBasePeriodFromInitialClaimDate")]
        public IActionResult GetBasePeriodFromInitialClaimDate(DateTime initialClaimDate, bool useAltBase)
        {
            try
            {
                // Calculate the base period
                var basePeriod = this.calculateBasePeriod.CalculateBasePeriodFromInitialClaimDate(initialClaimDate, useAltBase);

                // Map from IBasePeriod to BasePeriodDto and return
                var result = BasePeriodMapper.MapToDto(basePeriod);
                return Ok(result);
            }
            catch (ArgumentException argumentException)
            {
                // Log and return http 500
                this.logger.LogError(argumentException, "Error calculating base period from initialClaimDate={0}", initialClaimDate);
                return Problem(argumentException.Message);
            }
        }
    }
}
