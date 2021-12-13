namespace DWD.UI.Monetary.Domain.UseCases;

using System;
using System.Globalization;
using DWD.UI.Monetary.Domain.BusinessEntities;

/// <summary>
/// Benefit year calculations.
/// </summary>
public class CalculateBenefitYear : ICalculateBenefitYear
{
    /// <summary>
    /// Preferred culture for Calendar
    /// </summary>
    public const string EnglishUs = "en-US";

    /// <summary>
    /// The usual number of week in a benefit period.
    /// </summary>
    public const int StandardBenefitWeeks = 52;

    /// <summary>
    /// Calculate the benefit period based on requested start date.
    /// </summary>
    /// <param name="requestedClaimStartDate">The requested start date for the UI claim</param>
    /// <returns>benefit year</returns>
    public BenefitYear CalculateBenefitYearFromDate(DateTime requestedClaimStartDate)
    {
        var calculatedBenefitWeeks = StandardBenefitWeeks;
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
    /// <param name="inDate">the date</param>
    /// <returns>Year week</returns>
    private static YearWeek GetYearWeekFromDate(DateTime inDate)
    {
        var dateTimeFormatInfo = DateTimeFormatInfo.GetInstance(new CultureInfo(EnglishUs));
        var week = dateTimeFormatInfo.Calendar.GetWeekOfYear(
            inDate,
            CalendarWeekRule.FirstFullWeek,
            DayOfWeek.Saturday);
        return new YearWeek(inDate.Year, week);
    }
}
