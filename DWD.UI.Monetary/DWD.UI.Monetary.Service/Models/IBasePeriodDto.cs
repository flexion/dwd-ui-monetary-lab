namespace DWD.UI.Monetary.Service.Models;

using System.Collections.Generic;

/// <summary>
/// Base period dto interface.
/// </summary>
public interface IBasePeriodDto
{
    /// <summary>
    /// Gets or sets an array of quarters making up the base period.
    /// </summary>
    IEnumerable<ICalendarQuarterDto> Quarters { get; set; }
}
