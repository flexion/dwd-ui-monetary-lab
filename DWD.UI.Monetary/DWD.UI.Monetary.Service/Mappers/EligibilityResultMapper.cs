namespace DWD.UI.Monetary.Service.Mappers
{
    using System;
    using System.Collections.ObjectModel;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using DWD.UI.Monetary.Service.Extensions;
    using DWD.UI.Monetary.Service.Models;

    /// <summary>
    /// Maps EligibleResult to EligibleResultDto and IneligibleResult to IneligibleResultDto
    /// </summary>
    public static class EligibilityResultMapper
    {
        /// <summary>
        /// Presentation for the ineligible reason.
        /// </summary>
        /// <param name="ineligibleResult">in from the domain layer</param>
        /// <returns>the presentation object</returns>
        /// <exception cref="ArgumentNullException">When the arg is null</exception>
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
        /// Maps the object to the presentation.
        /// </summary>
        /// <param name="eligibleResult">in from the domain layer</param>
        /// <returns>presentation objects</returns>
        /// <exception cref="ArgumentNullException">should not be null</exception>
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
