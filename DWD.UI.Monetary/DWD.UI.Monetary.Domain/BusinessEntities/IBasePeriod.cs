namespace DWD.UI.Monetary.Domain.BusinessEntities;

using System.Collections.Generic;
using DWD.UI.Calendar;

/// <summary>
/// Simplified representation of the base period.
/// </summary>
public interface IBasePeriod
{
    /// <summary>
    /// Gets the collection of standard quarters making up the base period.
    /// </summary>
    IReadOnlyList<Quarter> StandardQuarters { get; }

    /// <summary>
    /// Gets the collection of alternate quarters making up the base period.
    /// </summary>
    IReadOnlyList<Quarter> AlternateQuarters { get; }
}
