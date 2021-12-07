namespace DWD.UI.Monetary.Service.Mappers
{
    using System;
    using System.Collections.ObjectModel;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using DWD.UI.Monetary.Service.Extensions;
    using DWD.UI.Monetary.Service.Models;

    /// <summary>
    /// Maps EligibleResult to EligibleResultDto and IneligibleResult to IneligibleResultDto.
    /// </summary>
    public static class EligibilityResultMapper
    {
        /// <summary>
        /// Maps the IneligibleResult to the IneligibleResultDto.
        /// </summary>
        /// <param name="ineligibleResult">The ineligible result object.</param>
        /// <returns>The IneligibleResult Data Transfer Object.</returns>
        /// <exception cref="ArgumentNullException">The ineligibleResult was null.</exception>
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

        /// <summary>
        /// Maps the IneligibleResult to the IneligibleResultDto.
        /// </summary>
        /// <param name="eligibleResult">The eligible result object.</param>
        /// <returns>The EligibleResult Data Transfer Object.</returns>
        /// <exception cref="ArgumentNullException">The eligibleResult was null.</exception>
        public static EligibleResultDto MapToDto(EligibleResult eligibleResult)
        {
            if (eligibleResult == null)
            {
                throw new ArgumentNullException(nameof(eligibleResult));
            }

            return new EligibleResultDto(eligibleResult.IsEligible, eligibleResult.WeeklyBenefitRate);
        }
    }
}
