namespace DWD.UI.Monetary.Service.Models
{
    /// <summary>
    /// A dto for calendar quarter.
    /// </summary>
    public class CalendarQuarterDto : ICalendarQuarterDto
    {
        /// <summary>
        /// Gets or sets the calendar year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the quarter number.
        /// </summary>
        public int QuarterNumber { get; set; }
    }
}
