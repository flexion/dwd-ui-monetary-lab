namespace DWD.UI.Monetary.Service.Models;

using System.Collections.Generic;

/// <summary>
/// A dto for base period.
/// </summary>
public class BasePeriodDto
{
    /// <summary>
    /// Gets or sets an ordered collection of quarters making up the base period.
    /// </summary>
    public IEnumerable<CalendarQuarterDto> Quarters { get; set; }
}
