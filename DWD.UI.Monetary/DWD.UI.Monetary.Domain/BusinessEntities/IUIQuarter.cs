namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;

    /// <summary>
    /// Simplified representation of unemployment insurance quarter.
    /// </summary>
    public interface IUIQuarter : IEquatable<IUIQuarter>, IComparable<IUIQuarter>
    {
        /// <summary>
        /// The quarter's year.
        /// </summary>
        int Year { get; }

        /// <summary>
        /// The quarter number.
        /// </summary>
        int QuarterNumber { get; }
    }
}
