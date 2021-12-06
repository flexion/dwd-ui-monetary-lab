namespace DWD.UI.Monetary.Service.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using DWD.UI.Monetary.Service.Models;

    /// <summary>
    /// Map base period between domain interfaces and dto interfaces.
    /// </summary>
    public static class BasePeriodMapper
    {
        /// <summary>
        /// Return IBasePeriodDto instance from IBasePeriod instance.
        /// </summary>
        /// <param name="quarters">The base period quarters.</param>
        /// <returns>An IBasePeriodDto instance.</returns>
        /// <exception cref="ArgumentNullException">If the quarters collection is null</exception>
        public static IBasePeriodDto MapToDto(IEnumerable<IUIQuarter> quarters)
        {
            if (quarters == null)
            {
                throw new ArgumentNullException(nameof(quarters));
            }

            return new BasePeriodDto
            {
                Quarters = quarters.Select(x =>
                        new CalendarQuarterDto
                        {
                            Year = x.Year,
                            QuarterNumber = x.QuarterNumber
                        }).ToArray()
            };
        }
    }
}
