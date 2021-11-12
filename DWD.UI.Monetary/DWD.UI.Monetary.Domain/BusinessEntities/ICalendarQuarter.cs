namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;

    /// <summary>
    /// Simplified representation of calendar quarter.
    /// </summary>
    public interface ICalendarQuarter : IEquatable<ICalendarQuarter>
    {
        /// <summary>
        /// The calendar year.
        /// </summary>
        int Year { get; }

        /// <summary>
        /// The quarter number.
        /// </summary>
        int QuarterNumber { get; }
    }
}
