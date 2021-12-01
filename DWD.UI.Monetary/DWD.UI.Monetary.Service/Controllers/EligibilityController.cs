namespace DWD.UI.Monetary.Service.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Domain.BusinessEntities;
    using Domain.Interfaces;
    using Domain.UseCases;
    using Models;
    using Extensions;
    using Mappers;
    using Microsoft.Extensions.Logging;
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// Provides endpoints for determine the eligibility for benefits.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EligibilityController : ControllerBase
    {
        private readonly IEligibilityBasisGateway eligibilityBasisGateway;
        private readonly ILogger<BasePeriodController> logger;

        public EligibilityController(IEligibilityBasisGateway eligibilityBasisGateway,
            ILogger<BasePeriodController> logger)
        {
            this.eligibilityBasisGateway = eligibilityBasisGateway;
            this.logger = logger;
        }

        /// <summary>
        ///  Determine eligibility for benefits
        /// </summary>
        /// <remarks>
        /// Check for all monetary qualifying requirements the week the claimant is filing for so that the department can determine eligibility for benefits <br /><br />
        /// <b>Eligibility Criteria</b><br />
        /// <para>- Claimant must have been paid wages from covered employment in at least two quarters (BP)</para><br />
        /// <para>- Claimant has wages in their high quarter (BP)to meet the minimum high quarter earnings amount</para><br />
        /// <para>- Wages outside their high quarter that equal to at least 4** times their Weekly Benefit Rate(WBR)</para><br />
        /// <para>- Total base period wages equal to at least 35** times your WBR</para><br /><br />
        /// <note type="tip"><em>WBR is 4** percent of high quarter</em></note><br />
        /// <note type="tip"><em>** - Actual values may vary depending on state law</em></note>
        /// </remarks>
        /// <param name="eligibilityRequestDto"></param>
        [SwaggerResponse((int)HttpStatusCode.OK,
            description: "If eligible then weekly benefit rate is returned. Otherwise reasons for ineligibility")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Initial Claim Date or wagesOfQuarter is empty", typeof(ProblemDetails), "application/problem+json")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal Server Error", typeof(ProblemDetails), "application/problem+json")]
        [Produces("application/json")]
        [HttpPost]
        [Route("VerifyEligibility")]
        public async Task<IActionResult> VerifyEligibilityAsync([FromBody] EligibilityRequestDto eligibilityRequestDto)
        {
            if (eligibilityRequestDto is null)
            {
                return this.Problem("Eligibility DTO was not given", null, 400);
            }

            if (eligibilityRequestDto.WagesOfQuarters is null)
            {
                return this.Problem("Eligibility Wages Of Quarters was not given", null, 400);
            }

            var eligibilityVerificationRequest = new EligibilityVerificationRequest(
                eligibilityRequestDto.WagesOfQuarters.ToCollection(), eligibilityRequestDto.InitialClaimDate,
                eligibilityRequestDto.ClaimantId);
            try
            {
                var verifier = new CheckEligibilityOfMonetaryRequirements(this.eligibilityBasisGateway);
                var result = await verifier.VerifyAsync(eligibilityVerificationRequest).ConfigureAwait(true);
                return result.IsEligible
                    ? this.Ok(EligibilityResultMapper.MapToDto((EligibleResult)result))
                    : this.Ok(EligibilityResultMapper.MapToDto((IneligibleResult)result));
            }
#pragma warning disable CA1031
            catch (Exception exception)
#pragma warning restore CA1031
            {
                this.logger.LogError(exception, "Error determining the eligibility for benefits");
                return this.Problem(exception.Message, null, 400);
            }
        }
    }
}
