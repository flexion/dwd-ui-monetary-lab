namespace DWD.UI.Monetary.Domain.UseCases;

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
    /// <returns>Base period information.</returns>
    public IBasePeriod CalculateBasePeriodFromInitialClaimDate(DateTime initialClaimDate) =>
        new BasePeriod(initialClaimDate);
}