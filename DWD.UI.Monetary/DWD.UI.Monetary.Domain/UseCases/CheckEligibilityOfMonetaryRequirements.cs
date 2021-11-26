namespace DWD.UI.Monetary.Domain.UseCases
{
    using System.Linq;
    using BusinessEntities;

    public class CheckEligibilityOfMonetaryRequirements : ICheckEligibilityOfMonetaryRequirements
    {
        public EligibilityResult Verify(EligibilityVerificationRequest verificationRequest,
            EligibilityBasis eligibilityBasis)
        {
            var eligibilityResult = new EligibilityResult(false, null, null);
            if (verificationRequest?.WagesOfQuarters == null || verificationRequest.WagesOfQuarters.Count(wage => wage > 0) < 2)
            {
                return eligibilityResult;
            }

            var wagesOfHighQuarter = verificationRequest.WagesOfQuarters.Max();
            if (wagesOfHighQuarter < eligibilityBasis?.MinHighQuarterEarnings)
            {
                return eligibilityResult;
            }

            var weeklyBenefitRate = wagesOfHighQuarter * eligibilityBasis?.PercentWeeklyBenefitRate / 100;
            var totalBasePeriodWages = verificationRequest.WagesOfQuarters.Sum();
            var sumOfWagesOutsideHighQuarter = totalBasePeriodWages - wagesOfHighQuarter;
            if (sumOfWagesOutsideHighQuarter < eligibilityBasis?.WagesOutsideOfHighQuarterFactor * weeklyBenefitRate)
            {
                return eligibilityResult;
            }

            if (totalBasePeriodWages < eligibilityBasis?.BasePeriodWagesFactor * weeklyBenefitRate)
            {
                return eligibilityResult;
            }

            decimal maxBenefitAmount = 321;
            return new EligibilityResult(true, weeklyBenefitRate, maxBenefitAmount);
        }
    }
}
