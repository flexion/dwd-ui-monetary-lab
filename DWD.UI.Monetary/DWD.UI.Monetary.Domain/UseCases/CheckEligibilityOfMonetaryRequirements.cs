namespace DWD.UI.Monetary.Domain.UseCases
{
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
            var eligibilityResult = new EligibilityResult(false, null, null);
            //claimant must have been paid wages from covered employment in at least two quarters (BP)
            if (verificationRequest?.WagesOfQuarters == null || verificationRequest.WagesOfQuarters.Count(wage => wage > 0) < 2)
            {
                return eligibilityResult;
            }

            var wagesOfHighQuarter = verificationRequest.WagesOfQuarters.Max();
            //Claimant has wages in their high quarter (BP)to meet the minimum high quarter earnings amount
            if (wagesOfHighQuarter < eligibilityBasis?.MinHighQuarterEarnings)
            {
                return eligibilityResult;
            }

            //Weekly benefit rate is 4 percent of high quarter
            var weeklyBenefitRate = wagesOfHighQuarter * eligibilityBasis?.PercentWeeklyBenefitRate / 100;
            var totalBasePeriodWages = verificationRequest.WagesOfQuarters.Sum();
            var sumOfWagesOutsideHighQuarter = totalBasePeriodWages - wagesOfHighQuarter;
            //wages outside their high quarter that equal to at least 4 times their WBR
            if (sumOfWagesOutsideHighQuarter < eligibilityBasis?.WagesOutsideOfHighQuarterFactor * weeklyBenefitRate)
            {
                return eligibilityResult;
            }

            //Total base period wages equal to at least 35 times your WBR
            if (totalBasePeriodWages < eligibilityBasis?.BasePeriodWagesFactor * weeklyBenefitRate)
            {
                return eligibilityResult;
            }

            //TOD0 this needs to be updated with actual calculation
            decimal maxBenefitAmount = 321;
            return new EligibilityResult(true, weeklyBenefitRate, maxBenefitAmount);
        }
    }
}
