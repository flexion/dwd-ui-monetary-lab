namespace DWD.UI.Monetary.Service.Mappers
{
    using System;
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
        /// <param name="basePeriod">The base period input.</param>
        /// <returns>An IBasePeriodDto instance.</returns>
        public static IBasePeriodDto MapToDto(IBasePeriod basePeriod)
        {
            if (basePeriod == null)
            {
                throw new ArgumentNullException(nameof(basePeriod));
            }

            return new BasePeriodDto
            {
                Quarters = basePeriod.BasePeriodQuarters
                    .Select(x =>
                        new CalendarQuarterDto
                        {
                            Year = x.Year,
                            QuarterNumber = x.QuarterNumber
                        }).ToArray()
            };
        }
    }
}
