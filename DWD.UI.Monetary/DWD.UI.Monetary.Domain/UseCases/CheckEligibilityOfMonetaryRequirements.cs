namespace DWD.UI.Monetary.Domain.UseCases
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using DWD.UI.Monetary.Domain.Interfaces;

    /// <summary>
    /// Concrete class for the eligibility determination use case.
    /// </summary>
    public class CheckEligibilityOfMonetaryRequirements : ICheckEligibilityOfMonetaryRequirements
    {
        private readonly IEligibilityBasisGateway eligibilityBasisGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckEligibilityOfMonetaryRequirements"/> class.
        /// </summary>
        /// <param name="eligibilityBasisGateway">The eligibility basis gateway provided by the IOC container.</param>
        public CheckEligibilityOfMonetaryRequirements(IEligibilityBasisGateway eligibilityBasisGateway) =>
            this.eligibilityBasisGateway = eligibilityBasisGateway;

        /// <summary>
        /// check all monetary requirements.
        /// </summary>
        /// <param name="verificationRequest">Claimant information.</param>
        /// <returns>EligibilityResult.</returns>
        /// <exception cref="ArgumentNullException">The verification request was null.</exception>
        public async Task<EligibilityResult> VerifyAsync(EligibilityVerificationRequest verificationRequest)
        {
            var ineligibilityReasons = new Collection<IneligibilityReason>();

            if (verificationRequest is null)
            {
                throw new ArgumentNullException(nameof(verificationRequest));
            }

            // Claimant must have been paid wages from covered employment in at least two quarters (BP)
            if (verificationRequest.WagesOfQuarters.Count(wage => wage > 0) < 2)
            {
                ineligibilityReasons.Add(IneligibilityReason.InsufficientQuartersWithWages);
            }

            var wagesOfHighQuarter = verificationRequest.WagesOfQuarters.Max();
            var eligibilityBasis = await this.eligibilityBasisGateway.GetEligibilityBasisAsync().ConfigureAwait(true);

            // Claimant has wages in their high quarter (BP)to meet the minimum high quarter earnings amount
            if (wagesOfHighQuarter < eligibilityBasis.MinHighQuarterEarnings)
            {
                ineligibilityReasons.Add(IneligibilityReason.InsufficientHighQuarterWage);
            }

            // Weekly benefit rate is 4 percent of high quarter
            var weeklyBenefitRate = wagesOfHighQuarter * eligibilityBasis.PercentWeeklyBenefitRate / 100;
            var totalBasePeriodWages = verificationRequest.WagesOfQuarters.Sum();
            var sumOfWagesOutsideHighQuarter = totalBasePeriodWages - wagesOfHighQuarter;

            // Wages outside their high quarter that equal to at least 4 times their WBR
            if (sumOfWagesOutsideHighQuarter < eligibilityBasis?.WagesOutsideOfHighQuarterFactor * weeklyBenefitRate)
            {
                ineligibilityReasons.Add(IneligibilityReason.InsufficientNonHighQuarterWages);
            }

            // Total base period wages equal to at least 35 times your WBR
            if (totalBasePeriodWages < eligibilityBasis?.BasePeriodWagesFactor * weeklyBenefitRate)
            {
                ineligibilityReasons.Add(IneligibilityReason.InsufficientTotalBasePeriodWages);
            }

            return ineligibilityReasons.Count > 0 ? new IneligibleResult(ineligibilityReasons) : new EligibleResult(weeklyBenefitRate);
        }
    }
}
