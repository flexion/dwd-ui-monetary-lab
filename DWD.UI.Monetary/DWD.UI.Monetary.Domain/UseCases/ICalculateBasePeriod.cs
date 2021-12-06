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
        /// <param name="year">Year to calculate the base periods</param>
        /// <param name="weekOfYear">Week number of the year. Must be between 1 and 52 or 53(If first day of the year lands on saturday)</param>
        /// <returns>The calculated base period.</returns>
        IBasePeriod CalculateBasePeriodFromInitialClaimDate(DateTime initialClaimDate, int year, int weekOfYear);
    }
}
