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
        [Route("VerifyEligibility")]
        public IActionResult VerifyEligibility([FromBody] EligibilityDto eligibilityDto)
        {
            if (eligibilityDto is null)
            {
                var problem = this.Problem("Eligibility DTO was not given", null, 400);
                return problem;
            }

            if (eligibilityDto.WagesOfQuarters is null)
            {
                var problem = this.Problem("Eligibility Wages Of Quarters was not given", null, 400);
                return problem;
            }

            var eligibilityVerificationRequest = new EligibilityVerificationRequest(
                eligibilityDto.WagesOfQuarters.ToCollection<decimal>(), eligibilityDto.InitialClaimDate, eligibilityDto.ClaimantId);
            var eligibilityBasis = new EligibilityBasis(eligibilityDto.MinHighQuarterEarnings, eligibilityDto.PercentWeeklyBenefitRate,
                    eligibilityDto.MinQuarters, eligibilityDto.WagesOutsideOfHighQuarterFactor, eligibilityDto.BasePeriodWagesFactor,
                    eligibilityDto.NumberOfWeeks, eligibilityDto.PercentOfBasePeriodWages);
            var verifier = new CheckEligibilityOfMonetaryRequirements();
            var result = verifier.Verify(eligibilityVerificationRequest, eligibilityBasis);
            return this.Ok(result);
        }
    }
}