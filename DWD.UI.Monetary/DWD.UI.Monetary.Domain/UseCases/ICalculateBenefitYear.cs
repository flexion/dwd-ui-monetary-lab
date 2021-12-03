namespace DWD.UI.Monetary.Domain.UseCases
{
    using System;
    using BusinessEntities;

    /// <summary>
    /// Calculate the claimant's benefit year.
    ///
    /// The yeay is calculated based on the claim date. However, the claimant can request
    /// benefits by delayed by a week. This date preference is handled as a detail for the
    /// caller to figure out.
    /// </summary>
    public interface ICalculateBenefitYear
    {
        /// <summary>
        /// Calculate the benefit period based on requested start date.
        /// </summary>
        /// <param name="requestedClaimStartDate">The requested start date for the UI claim</param>
        /// <returns></returns>
        BenefitYear CalculateBenefitYearFromDate(DateTime requestedClaimStartDate);
    }
}
