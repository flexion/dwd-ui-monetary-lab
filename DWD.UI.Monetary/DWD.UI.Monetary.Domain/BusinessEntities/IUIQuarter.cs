namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    /// <summary>
    /// Simplified representation of unemployment insurance quarter.
    /// </summary>
    public interface IUIQuarter
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
