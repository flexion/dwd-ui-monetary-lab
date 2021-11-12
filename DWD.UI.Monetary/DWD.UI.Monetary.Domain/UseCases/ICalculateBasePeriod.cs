namespace DWD.UI.Monetary.Domain.UseCases
{
    using System;
    using DWD.UI.Monetary.Domain.BusinessEntities;

    /// <summary>
    /// Interface defining our base period calculations.
    /// </summary>
    public interface ICalculateBasePeriod
    {
        /// <summary>
        /// Calculate base period from initial claim date.
        /// </summary>
        /// <param name="initialClaimDate">The initial claim date.</param>
        /// <param name="useAltBasePeriod">Use alternative base period calculation.</param>
        /// <returns>The calculated base period.</returns>
        IBasePeriod CalculateBasePeriodFromInitialClaimDate(DateTime initialClaimDate, bool useAltBasePeriod);
    }
}
