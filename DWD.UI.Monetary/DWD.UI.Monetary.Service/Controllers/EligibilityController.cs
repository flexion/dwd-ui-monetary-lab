namespace DWD.UI.Monetary.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using DWD.UI.Monetary.Domain.UseCases;
    using DWD.UI.Monetary.Service.Models;
    using DWD.UI.Monetary.Service.Extensions;

    /// <summary>
    /// Provides endpoints for entering wage data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EligibilityController : ControllerBase
    {
        [HttpPost]
        [Route("VerifyElegibility")]
        public IActionResult VerifyElegibility([FromBody] ElegibilityDto elegibilityDto)
        {
            if (elegibilityDto is null)
            {
                var problem = this.Problem("Elegibility DTO was not given", null, 400);
                return problem;
            }

            if (elegibilityDto.WagesOfQuarters is null)
            {
                var problem = this.Problem("Elegibility Wages Of Quarters was not given", null, 400);
                return problem;
            }

            var eligibilityVerificationRequest = new EligibilityVerificationRequest(
                elegibilityDto.WagesOfQuarters.ToCollection<decimal>(), elegibilityDto.InitialClaimDate, elegibilityDto.ClaimantId);
            var eligibilityBasis = new EligibilityBasis(elegibilityDto.MinHighQuarterEarnings, elegibilityDto.PercentWeeklyBenefitRate,
                    elegibilityDto.MinQuarters, elegibilityDto.WagesOutsideOfHighQuarterFactor, elegibilityDto.BasePeriodWagesFactor,
                    elegibilityDto.NumberOfWeeks, elegibilityDto.PercentOfBasePeriodWages);
            var verifier = new CheckEligibilityOfMonetaryRequirements();
            var result = verifier.Verify(eligibilityVerificationRequest, eligibilityBasis);
            return this.Ok(result);
        }
    }
}