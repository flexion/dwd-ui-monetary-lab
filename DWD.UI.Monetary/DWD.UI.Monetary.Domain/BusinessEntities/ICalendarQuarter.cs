namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    /// <summary>
    /// Simplified representation of calendar quarter.
    /// </summary>
    public interface ICalendarQuarter
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