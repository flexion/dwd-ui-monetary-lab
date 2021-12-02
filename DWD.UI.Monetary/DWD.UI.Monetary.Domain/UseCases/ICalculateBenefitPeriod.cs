namespace DWD.UI.Monetary.Domain.UseCases
{
    using System;
    using BusinessEntities;

    public interface ICalculateBenefitPeriod
    {
        /// <summary>
        /// Calculate the benefit period based on requested start date.
        /// </summary>
        /// <param name="requestedClaimStartDate">The requested start date for the UI claim</param>
        /// <param name="nextRequestedClaimStartDate">The requested start date for the next UI claim (nullable)</param>
        /// <returns></returns>
        BenefitYear CalculateBenefitPeriodFromDate(DateTime requestedClaimStartDate, bool? useAlternateBasePeriodRequested, DateTime? nextRequestedClaimStartDate, bool? useAlternateBasePeriodNext);

        int CalculateBenefitWeeks(DateTime requestedClaimStartDate, bool? useAlternateBasePeriodRequested, DateTime? nextRequestedClaimStartDate, bool? useAlternateBasePeriodNext);
    }
}
