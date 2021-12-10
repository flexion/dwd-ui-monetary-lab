namespace DWD.UI.Monetary.Service.Models
{
    /// <summary>
    /// Calendar quarter dto interface.
    /// </summary>
    public interface ICalendarQuarterDto
    {
        /// <summary>
        /// Gets or sets the calendar year.
        /// </summary>
        int Year { get; set; }

        /// <summary>
        /// Gets or sets the quarter number.
        /// </summary>
        int QuarterNumber { get; set; }
    }
}
