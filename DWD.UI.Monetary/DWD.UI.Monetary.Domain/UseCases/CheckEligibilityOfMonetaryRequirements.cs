namespace DWD.UI.Monetary.Domain.UseCases
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using BusinessEntities;

    public class CheckEligibilityOfMonetaryRequirements : ICheckEligibilityOfMonetaryRequirements
    {
        /// <summary>
        /// check all monetary requirements
        /// </summary>
        /// <param name="verificationRequest"></param>
        /// <param name="eligibilityBasis"></param>
        /// <returns>EligibilityResult</returns>
        public EligibilityResult Verify(EligibilityVerificationRequest verificationRequest,
            EligibilityBasis eligibilityBasis)
        {
            var ineligibilityReasons = new Collection<IneligibilityReason>();

            if (verificationRequest is null)
            {
                throw new ArgumentNullException(nameof(verificationRequest));
            }

            //claimant must have been paid wages from covered employment in at least two quarters (BP)
            if (verificationRequest.WagesOfQuarters.Count(wage => wage > 0) < 2)
            {
                ineligibilityReasons.Add(IneligibilityReason.InsufficientQuartersWithWages);
            }

            var wagesOfHighQuarter = verificationRequest.WagesOfQuarters.Max();
            //Claimant has wages in their high quarter (BP)to meet the minimum high quarter earnings amount
            if (wagesOfHighQuarter < eligibilityBasis?.MinHighQuarterEarnings)
            {
                ineligibilityReasons.Add(IneligibilityReason.InsufficientHighQuarterWage);
            }

            //Weekly benefit rate is 4 percent of high quarter
            var weeklyBenefitRate = wagesOfHighQuarter * eligibilityBasis?.PercentWeeklyBenefitRate / 100;
            var totalBasePeriodWages = verificationRequest.WagesOfQuarters.Sum();
            var sumOfWagesOutsideHighQuarter = totalBasePeriodWages - wagesOfHighQuarter;
            //wages outside their high quarter that equal to at least 4 times their WBR
            if (sumOfWagesOutsideHighQuarter < eligibilityBasis?.WagesOutsideOfHighQuarterFactor * weeklyBenefitRate)
            {
                ineligibilityReasons.Add(IneligibilityReason.InsufficientNonHighQuarterWages);
            }

            //Total base period wages equal to at least 35 times your WBR
            if (totalBasePeriodWages < eligibilityBasis?.BasePeriodWagesFactor * weeklyBenefitRate)
            {
                ineligibilityReasons.Add(IneligibilityReason.InsufficientTotalBasePeriodWages);
            }

            return ineligibilityReasons.Any() ? new EligibilityResult(ineligibilityReasons) : new EligibilityResult(weeklyBenefitRate);
        }
    }
}
