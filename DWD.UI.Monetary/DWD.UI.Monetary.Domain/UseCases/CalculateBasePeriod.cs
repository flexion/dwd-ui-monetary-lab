namespace DWD.UI.Monetary.Domain.UseCases;

using System;
using DWD.UI.Calendar;
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
    /// <returns>Base period information.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The initialClaimDate is not supported.</exception>
    public IBasePeriod CalculateBasePeriodFromInitialClaimDate(DateTime initialClaimDate)
    {
        if (initialClaimDate.Year < Constants.MINBENEFITYEAR)
        {
            throw new ArgumentOutOfRangeException(nameof(initialClaimDate), $"Dates before {Constants.MINBENEFITYEAR} are not supported.");
        }

        return new BasePeriod(initialClaimDate);
    }

    /// <summary>
    /// Calculate the base period from year and week.
    /// </summary>
    /// <param name="year">Year to calculate the base periods.</param>
    /// <param name="week">Week number of the year. Must be between 1 and 52 or 53(If first day of the year lands on saturday).</param>
    /// <returns>Base period information.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The year is not supported.</exception>
    public IBasePeriod CalculateBasePeriodFromYearAndWeek(int year, int week)
    {
        if (year < Constants.MINBENEFITYEAR)
        {
            throw new ArgumentOutOfRangeException(nameof(year), $"Years before {Constants.MINBENEFITYEAR} are not supported.");
        }

        return new BasePeriod(new UIWeek(year, week));
    }
}
