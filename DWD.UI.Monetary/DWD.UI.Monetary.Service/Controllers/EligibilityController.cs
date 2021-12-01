namespace DWD.UI.Monetary.Service.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using DWD.UI.Monetary.Domain.UseCases;
    using DWD.UI.Monetary.Service.Models;
    using DWD.UI.Monetary.Service.Extensions;
    using Gateways;

    /// <summary>
    /// Provides endpoints for entering wage data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EligibilityController : ControllerBase
    {
        private readonly IEligibilityBasisGateway eligibilityBasisGateway;
        public EligibilityController(IEligibilityBasisGateway eligibilityBasisGateway) => this.eligibilityBasisGateway = eligibilityBasisGateway;

        [HttpPost]
        [Route("VerifyEligibility")]
        public async Task<IActionResult> VerifyEligibilityAsync([FromBody] EligibilityRequestDto eligibilityRequestDto)
        {
            if (eligibilityRequestDto is null)
            {
                var problem = this.Problem("Eligibility DTO was not given", null, 400);
                return problem;
            }

            if (eligibilityRequestDto.WagesOfQuarters is null)
            {
                var problem = this.Problem("Eligibility Wages Of Quarters was not given", null, 400);
                return problem;
            }

            var eligibilityVerificationRequest = new EligibilityVerificationRequest(
                eligibilityRequestDto.WagesOfQuarters.ToCollection<decimal>(), eligibilityRequestDto.InitialClaimDate, eligibilityRequestDto.ClaimantId);
            var verifier = new CheckEligibilityOfMonetaryRequirements(this.eligibilityBasisGateway);
            var result = await verifier.VerifyAsync(eligibilityVerificationRequest);
            return this.Ok(result);
        }
    }
}
