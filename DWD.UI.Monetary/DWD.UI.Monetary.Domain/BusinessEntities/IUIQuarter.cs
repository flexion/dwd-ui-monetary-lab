namespace DWD.UI.Monetary.Domain.BusinessEntities;

using System;

/// <summary>
/// Simplified representation of unemployment insurance quarter.
/// </summary>
public interface IUIQuarter : IEquatable<IUIQuarter>, IComparable<IUIQuarter>
{
    /// <summary>
    /// Gets the quarter's year.
    /// </summary>
    int Year { get; }

    /// <summary>
    /// Gets the quarter number.
    /// </summary>
    int QuarterNumber { get; }
}
