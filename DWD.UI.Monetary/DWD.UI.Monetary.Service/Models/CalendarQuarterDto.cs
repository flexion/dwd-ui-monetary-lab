namespace DWD.UI.Monetary.Service.Models
{
    /// <summary>
    /// A dto for calendar quarter.
    /// </summary>
    public class CalendarQuarterDto : ICalendarQuarterDto
    {
        /// <summary>
        /// The calendar year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// The quarter number.
        /// </summary>
        public int QuarterNumber { get; set; }
    }
}
