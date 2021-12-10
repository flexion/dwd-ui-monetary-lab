namespace DWD.UI.Monetary.Domain.UseCases
{
    using System;
    using DWD.UI.Monetary.Domain.BusinessEntities;

    /// <summary>
    /// Calculate the claimant's benefit year.
    ///
    /// The year is calculated based on the VNC date. However, the claimant can request
    /// benefits by delayed by a week. This date preference is handled as a detail for the
    /// caller to figure out.
    /// </summary>
    public interface ICalculateBenefitYear
    {
        /// <summary>
        /// Calculate the benefit period based on requested start date.
        /// </summary>
        /// <param name="requestedClaimStartDate">The requested start date for the UI claim</param>
        /// <returns>benefit year</returns>
        BenefitYear CalculateBenefitYearFromDate(DateTime requestedClaimStartDate);
    }
}
