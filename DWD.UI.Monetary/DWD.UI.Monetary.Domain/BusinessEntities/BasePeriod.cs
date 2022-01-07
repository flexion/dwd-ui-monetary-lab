namespace DWD.UI.Monetary.Domain.BusinessEntities;

using System;
using System.Collections.Generic;
using System.Linq;
using DWD.UI.Calendar;

/// <summary>
/// The base period used within our eligibility logic.
/// </summary>
/// <remarks> For unemployment purposes the quarter does not start until the first full week of that month.
/// Example: October of 2021. The first full week of October was Sunday-Saturday 10/3-10/9 so that is when the quarter 4
/// would start for unemployment purposes.  The week of 9/26-10/2 would be considered to be apart of Q3.
/// </remarks>
internal class BasePeriod : IBasePeriod
{
    private const int NumberOfQuartersRequiredToCalculateBasePeriod = 5;

    /// <summary>
    /// Local storage for standard quarters.
    /// </summary>
    private readonly Quarters standardQuarters = new();

    /// <summary>
    /// Local storage for alternate quarters.
    /// </summary>
    private readonly Quarters alternateQuarters = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePeriod"/> class using initial claim date as input.
    /// </summary>
    /// <param name="initialClaimDate">The initial claim date.</param>
    public BasePeriod(DateTime initialClaimDate) => this.PopulateBasePeriods(initialClaimDate);

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePeriod"/> class given the year and week number of the initial claim.
    /// </summary>
    /// <param name="year">Year to calculate the base periods.</param>
    /// <param name="weekNumber">Week number of the year. Must be between 1 and 52 or 53(If first day of the year lands on saturday).</param>
    public BasePeriod(int year, int weekNumber)
    {
        var uiWeek = new UIWeek(year, weekNumber);
        this.PopulateBasePeriods(uiWeek.StartDate);
    }

    /// <summary>
    /// Gets the first four of the last five completed calendar quarters before the week a claimant
    /// files an initial claim application for a new benefit year.
    /// </summary>
    public IReadOnlyList<Quarter> StandardQuarters => this.standardQuarters.ToList();

    /// <summary>
    /// Gets the four most recently completed calendar quarters before the
    /// week a claimant filed an initial claim application for a new benefit year.
    /// </summary>
    public IReadOnlyList<Quarter> AlternateQuarters => this.alternateQuarters.ToList();

    /// <summary>
    /// Build a list of the (five) quarters completed before the given the UI week of the initial claim date.
    /// </summary>
    /// <param name="currentQuarterStartDate">The start date of the quarter containing the initial claim date.</param>
    /// <returns>List of <see cref="Quarter"/>.</returns>
    private static List<Quarter> QuartersCompletedBefore(DateTime currentQuarterStartDate)
    {
        Quarters quarters = new();
        var currentQuarter = new Quarter(currentQuarterStartDate);
        var quarter = currentQuarter.Previous();

        for (var i = 0; i < NumberOfQuartersRequiredToCalculateBasePeriod; i++)
        {
            quarters.Add(quarter);
            quarter = quarter.Previous();
        }

        return (List<Quarter>)quarters.ToList();
    }

    /// <summary>
    /// Populate standard and alternate base periods.
    /// </summary>
    /// <param name="currentQuarterStartDate">Sunday of the first full week of the quarter.</param>
    private void PopulateBasePeriods(DateTime currentQuarterStartDate)
    {
        var completedQuarters = QuartersCompletedBefore(currentQuarterStartDate);

        // First four of the last (five)
        this.standardQuarters.AddRange(completedQuarters.Take(4));

        // Four most recent
        this.alternateQuarters.AddRange(completedQuarters.Skip(1));
    }
}
