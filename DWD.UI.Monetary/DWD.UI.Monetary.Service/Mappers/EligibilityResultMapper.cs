namespace DWD.UI.Monetary.Service.Mappers
{
    using System;
    using System.Collections.ObjectModel;
    using Domain.BusinessEntities;
    using Extensions;
    using Models;

    /// <summary>
    /// Maps EligibleResult to EligibleResultDto and IneligibleResult to IneligibleResultDto
    /// </summary>
    public static class EligibilityResultMapper
    {
        public static IneligibleResultDto MapToDto(IneligibleResult ineligibleResult)
        {
            if (ineligibleResult == null)
            {
                throw new ArgumentNullException(nameof(ineligibleResult));
            }

            var ineligibleDisplayReasons = new Collection<IneligibilityDisplayReason>();
            foreach (var reason in ineligibleResult.IneligibilityReasons)
            {
                ineligibleDisplayReasons.Add(new IneligibilityDisplayReason(reason, reason.GetDescription()));
            }

            return new IneligibleResultDto(ineligibleResult.IsEligible, ineligibleDisplayReasons);
        }

        public static EligibleResultDto MapToDto(EligibleResult eligibleResult)
        {
            if (eligibleResult == null)
            {
                throw new ArgumentNullException(nameof(eligibleResult));
            }

            return new EligibleResultDto(eligibleResult.IsEligible, eligibleResult.WeeklyBenefitRate, eligibleResult.MaximumBenefitAmount);
        }
    }
}
