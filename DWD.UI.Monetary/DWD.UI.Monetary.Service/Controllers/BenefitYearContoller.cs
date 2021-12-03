namespace DWD.UI.Monetary.Service.Controllers
{
    using System;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Provides endpoints for Benefit Year.
    /// </summary>
    [ApiController]
    [Route("benefityear")]
    public class BenefitYearContoller : ControllerBase
    {
        /// <summary>
        /// Local logger reference.
        /// </summary>
        private readonly ILogger<BenefitYearContoller> logger;

        /// <summary>
        /// Local reference to domain logic.
        /// </summary>
        private readonly ICalculateBenefitYear calculateBenefitYear;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">A logger reference.</param>
        /// <param name="aCalculateBenefitYear">A domain logic reference.</param>
        public BenefitYearContoller(ILogger<BenefitYearContoller> logger, ICalculateBenefitYear aCalculateBenefitYear)
        {
            this.logger = logger;
            this.calculateBenefitYear = aCalculateBenefitYear;
        }

        /// <summary>
        /// Calculate benefit year for requested date.
        /// </summary>
        /// <remarks>
        /// <b>Standard Calculation:</b><br />
        /// </remarks>
        /// <param name="requestedDate">The requested benefit date in static route format YYYY-MM-DD.</param>
        /// <returns>The calculated benefit year.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{requestedDate}")]
        public ActionResult<BenefitYear> GetBenefitYearForRequestedDate(DateTime requestedDate)
        {
            return this.Ok(this.calculateBenefitYear.CalculateBenefitYearFromDate(requestedDate));
        }
    }
}
