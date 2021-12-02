namespace DWD.UI.Monetary.Domain.UseCases
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using BusinessEntities;

    /// <summary>
    ///
    /// </summary>
    public class CalculateBenefitPeriod : ICalculateBenefitPeriod
    {
        private readonly ICalculateBasePeriod calculateBasePeriod;

        /// <summary>
        /// Public constructor to include DI.
        /// </summary>
        /// <param name="aCalculateBasePeriod">Use Case needs a base period.</param>
        public CalculateBenefitPeriod(ICalculateBasePeriod aCalculateBasePeriod)
                                => this.calculateBasePeriod = aCalculateBasePeriod;

        /// <summary>
        /// Calculate the benefit period based on requested start date.
        /// </summary>
        /// <param name="requestedClaimStartDate">The requested start date for the UI claim</param>
        /// <param name="nextRequestedClaimStartDate">The requested start date for the next UI claim (nullable)</param>
        /// <returns></returns>
        public BenefitYear CalculateBenefitPeriodFromDate(DateTime requestedClaimStartDate, bool? useAlternateBasePeriodRequested, DateTime? nextRequestedClaimStartDate, bool? useAlternateBasePeriodNext)
        {
            var calculatedBenefitWeeks = this.CalculateBenefitWeeks(requestedClaimStartDate, useAlternateBasePeriodRequested, nextRequestedClaimStartDate, useAlternateBasePeriodNext);
            var claimRequestedDate = requestedClaimStartDate;
            // Move the Begin Date to the Sunday of the first benefit week
            var claimBeginDate = claimRequestedDate.AddDays(-1 * (claimRequestedDate.DayOfWeek - DayOfWeek.Sunday));
            // Move the Begin Date to the Saturday of the last benefit week
            var claimEndDate = claimBeginDate.AddDays((7 * calculatedBenefitWeeks) - 1);
            // The first week of the year is the first full week
            // so use the last day of the week (Saturday) to compute
            var claimBeginYearWeek = GetYearWeekFromDate(claimBeginDate.AddDays(DayOfWeek.Saturday - claimBeginDate.DayOfWeek));
            var claimEndYearWeek = GetYearWeekFromDate(claimEndDate);

            return new BenefitYear(calculatedBenefitWeeks, claimRequestedDate, claimBeginDate, claimEndDate, claimBeginYearWeek, claimEndYearWeek);
        }

        /// <summary>
        /// Calculate the week for a date.
        ///
        /// The first week of a year begins when any part of that week
        /// is contains January 1st. The end of the calendar week is a Saturday.
        /// Therefore, if the week begins (Sunday) on 12/25 and ends (Saturday) on 1/1
        /// then that is the first week of the year.
        /// </summary>
        /// <param name="inDate"></param>
        /// <returns>Year week</returns>
        private static YearWeek GetYearWeekFromDate(DateTime inDate)
        {
            var dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
            Debug.Assert(dateTimeFormatInfo != null, nameof(dateTimeFormatInfo) + " != null");
            var week = dateTimeFormatInfo.Calendar.GetWeekOfYear(inDate,
                CalendarWeekRule.FirstFullWeek,
                DayOfWeek.Saturday);
            return new YearWeek(inDate.Year, week);
        }

        /// <summary>
        /// Calculates the number of benefit weeks for a begin date.
        /// </summary>
        /// <param name="requestedClaimStartDate"></param>
        /// <param name="useAlternateBasePeriodRequested"></param>
        /// <param name="nextRequestedClaimStartDate"></param>
        /// <param name="useAlternateBasePeriodNext"></param>
        /// <returns>weeks</returns>
        public virtual int CalculateBenefitWeeks(DateTime requestedClaimStartDate, bool? useAlternateBasePeriodRequested, DateTime? nextRequestedClaimStartDate, bool? useAlternateBasePeriodNext)
        {
            var result = 52;
            if (nextRequestedClaimStartDate.HasValue)
            {
                var requestedClaimStartDateBasePeriod = this.calculateBasePeriod.CalculateBasePeriodFromInitialClaimDate(requestedClaimStartDate);
                var nextRequestedClaimStartDateBasePeriod = this.calculateBasePeriod.CalculateBasePeriodFromInitialClaimDate(nextRequestedClaimStartDate.Value);

                var firstPeriodLastQuarter = useAlternateBasePeriodRequested.HasValue && useAlternateBasePeriodRequested.Value
                                                                            ? requestedClaimStartDateBasePeriod.AltBasePeriodQuarters.Last()
                                                                                : requestedClaimStartDateBasePeriod.BasePeriodQuarters.Last();
                var nextPeriodFirstQuarter = useAlternateBasePeriodNext.HasValue && useAlternateBasePeriodNext.Value
                                                                            ? nextRequestedClaimStartDateBasePeriod.AltBasePeriodQuarters.First()
                                                                                : nextRequestedClaimStartDateBasePeriod.BasePeriodQuarters.First();

                if (firstPeriodLastQuarter.CompareTo(nextPeriodFirstQuarter) >= 0)
                {
                    result = 53;
                }
                else
                {
                    result = 52;
                }

            }

            return result;
        }
    }
}
