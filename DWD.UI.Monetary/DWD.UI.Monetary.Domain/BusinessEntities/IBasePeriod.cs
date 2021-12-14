namespace DWD.UI.Monetary.Domain.BusinessEntities;

using System.Collections.Generic;

/// <summary>
/// Simplified representation of the base period.
/// </summary>
public interface IBasePeriod
{
    /// <summary>
    /// Gets array of quarters making up the base period.
    /// </summary>
    IEnumerable<IUIQuarter> BasePeriodQuarters { get; }

    /// <summary>
    /// Gets array of alternative quarters making up the base period.
    /// </summary>
    IEnumerable<IUIQuarter> AltBasePeriodQuarters { get; }
}
