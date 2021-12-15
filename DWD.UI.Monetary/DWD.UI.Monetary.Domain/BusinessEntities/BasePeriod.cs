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
    private List<Quarter> standardQuarters = new();

    /// <summary>
    /// Local storage for alternate quarters.
    /// </summary>
    private List<Quarter> alternateQuarters = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePeriod"/> class using initial claim date as input.
    /// </summary>
    /// <param name="initialClaimDate">The initial claim date.</param>
    public BasePeriod(DateTime initialClaimDate) => this.PopulateBasePeriods(initialClaimDate);

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePeriod"/> class given a UIWeek object.
    /// </summary>
    /// <param name="uiWeek">The UIWeek object that represents the initial claim week.</param>
    public BasePeriod(UIWeek uiWeek) => this.PopulateBasePeriods(uiWeek.StartDate);

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
    /// Build a list of the (five) quarters completed before the given initial claim date.
    /// </summary>
    /// <param name="initialClaimDate">Initial claim date.</param>
    /// <returns>List of <see cref="Quarter"/>.</returns>
    private static List<Quarter> QuartersCompletedBefore(DateTime initialClaimDate)
    {
        Quarters quarters = new();
        var currentQuarter = new Quarter(initialClaimDate);
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
    /// <param name="initialClaimDate">The initial claim date.</param>
    /// <exception cref="ArgumentOutOfRangeException">The supplied initial claim date is too old.</exception>
    private void PopulateBasePeriods(DateTime initialClaimDate)
    {
        var completedQuarters = QuartersCompletedBefore(initialClaimDate);

        // First four of the last (five)
        this.standardQuarters = completedQuarters.Take(4).ToList();

        // Four most recent
        this.alternateQuarters = completedQuarters.Skip(1).ToList();
    }
}
