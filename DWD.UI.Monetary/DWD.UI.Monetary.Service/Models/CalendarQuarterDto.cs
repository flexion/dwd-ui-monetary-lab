namespace DWD.UI.Monetary.Service.Models;

/// <summary>
/// A Data Transfer Object for calendar quarter.
/// </summary>
public class CalendarQuarterDto
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
