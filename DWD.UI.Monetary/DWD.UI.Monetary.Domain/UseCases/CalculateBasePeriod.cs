namespace DWD.UI.Monetary.Domain.UseCases
{
    using System;
    using DWD.UI.Monetary.Domain.BusinessEntities;

    /// <summary>
    /// Provides public access to base period calculations.
    /// </summary>
    public class CalculateBasePeriod : ICalculateBasePeriod
    {
        /// <summary>
        /// Calculate the base period from the initial claim date.
        /// </summary>
        /// <param name="initialClaimDate">The initial claim date.  Not required, but per UI rules the initial claim date should fall on a Sunday as it is the first day of the week for claim purposes.</param>
        /// <param name="year">Year to calculate the base periods</param>
        /// <param name="weekOfYear">Week number of the year. Must be between 1 and 52 or 53(If first day of the year lands on saturday)</param>
        /// <returns>Base period information.</returns>
        public IBasePeriod CalculateBasePeriodFromInitialClaimDate(DateTime initialClaimDate, int year, int weekOfYear) =>
                                   initialClaimDate.Year == 1 ? new BasePeriod(year, weekOfYear) : new BasePeriod(initialClaimDate);
    }
}
