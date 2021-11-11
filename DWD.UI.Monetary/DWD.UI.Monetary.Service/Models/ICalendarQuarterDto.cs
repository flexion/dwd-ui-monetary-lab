namespace DWD.UI.Monetary.Service.Models
{
    /// <summary>
    /// Calendar quarter dto interface.
    /// </summary>
    public interface ICalendarQuarterDto
    {
        /// <summary>
        /// The calendar year.
        /// </summary>
        int Year { get; set; }

        /// <summary>
        /// The quarter number.
        /// </summary>
        int QuarterNumber { get; set; }
    }
}