namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System.Collections.Generic;

    /// <summary>
    /// Simplified representation of the base period.
    /// </summary>
    public interface IBasePeriod
    {
        /// <summary>
        /// Array of quarters making up the base period.
        /// </summary>
        IEnumerable<IUIQuarter> BasePeriodQuarters { get; }
        /// <summary>
        /// Array of alternative quarters making up the base period.
        /// </summary>
        IEnumerable<IUIQuarter> AltBasePeriodQuarters { get; }
    }
}
