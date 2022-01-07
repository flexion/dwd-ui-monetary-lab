namespace DWD.UI.Monetary.Domain.UseCases;

using System;
using DWD.UI.Calendar;
using DWD.UI.Monetary.Domain.BusinessEntities;

/// <summary>
/// Benefit year calculations.
/// </summary>
public class CalculateBenefitYear : ICalculateBenefitYear
{
    /// <summary>
    /// The usual number of weeks in a benefit period.
    /// </summary>
    public const int StandardBenefitWeeks = 52;

    /// <summary>
    /// Calculate the benefit period based on requested start date.
    /// </summary>
    /// <param name="requestedClaimStartDate">The requested start date for the UI claim.</param>
    /// <returns>benefit year.</returns>
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
        var claimBeginWeek = new UIWeek(claimBeginDate.AddDays(DayOfWeek.Saturday - claimBeginDate.DayOfWeek));
        var claimEndWeek = new UIWeek(claimEndDate);

        return new BenefitYear(
            calculatedBenefitWeeks,
            claimRequestedDate,
            claimBeginDate,
            claimEndDate,
            new YearWeek(claimBeginWeek.Year, claimBeginWeek.WeekNumber),
            new YearWeek(claimEndWeek.Year, claimEndWeek.WeekNumber));
    }
}
