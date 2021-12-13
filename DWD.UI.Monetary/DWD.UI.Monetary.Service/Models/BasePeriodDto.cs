namespace DWD.UI.Monetary.Service.Models;

using System.Collections.Generic;

/// <summary>
/// A dto for base period.
/// </summary>
public class BasePeriodDto : IBasePeriodDto
{
    /// <summary>
    /// An array of quarters making up the base period.
    /// </summary>
    public IEnumerable<ICalendarQuarterDto> Quarters { get; set; }
}
