namespace DWD.UI.Monetary.Domain.BusinessEntities;

using System;
using System.Collections.Generic;
using System.Linq;
using DWD.UI.Calendar;
using DWD.UI.Monetary.Domain.Utilities;

/// <summary>
/// The base period used within our eligibility logic.
/// </summary>
/// <remarks> For unemployment purposes the quarter does not start until the first full week of that month.
/// Example: October of 2021. The first full week of October was Sunday-Saturday 10/3-10/9 so that is when the quarter 4
/// would start for unemployment purposes.  The week of 9/26-10/2 would be considered to be apart of Q3.
/// </remarks>
internal class BasePeriod : IBasePeriod
{
    // TODO: Ask Helen if this is needed, and if so what the correct minimum should be.

    /// <summary>
    /// The minimum valid initial base claim date that we will calculate from.
    /// </summary>

    private static readonly DateTime MinimumValidInitialBaseClaimDate = new(Constants.MinBenefitYear, 1, 1);

    /// <summary>
    /// Local storage for quarters.
    /// </summary>
    private UIQuarter[] standardQuarters = new UIQuarter[4];

    /// <summary>
    /// Local storage for quarters.
    /// </summary>
    private UIQuarter[] alternateQuarters = new UIQuarter[4];

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePeriod"/> class using initial claim date as input.
    /// </summary>
    /// <param name="initialClaimDate">The initial claim date.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throws a ArgumentException if the supplied initial claim date is not valid.</exception>
    public BasePeriod(DateTime initialClaimDate) => this.PopulateBasePeriods(initialClaimDate);

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePeriod"/> class given a year and week of year.
    /// </summary>
    /// <param name="uiWeek">The UIWeek object that represents the initial claim week.</param>
    public BasePeriod(UIWeek uiWeek) => this.PopulateBasePeriods(uiWeek.StartDate);

    /// <summary>
    /// Gets base period quarters as IEnumerable of IUIQuarter.
    /// </summary>
    public IEnumerable<IUIQuarter> BasePeriodQuarters => new List<IUIQuarter>(this.standardQuarters);

    /// <summary>
    /// Gets base period quarters as IEnumerable of IUIQuarter.
    /// </summary>
    public IEnumerable<IUIQuarter> AltBasePeriodQuarters => new List<IUIQuarter>(this.alternateQuarters);

    /// <summary>
    /// Populate standard and alternate base periods.
    /// </summary>
    /// <param name="initialClaimDate">The initial claim date.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throws a ArgumentException if the supplied initial claim date is not valid.</exception>
    private void PopulateBasePeriods(DateTime initialClaimDate)
    {
        // Check if claim date is invalid
        if (initialClaimDate.Date < MinimumValidInitialBaseClaimDate)
        {
            throw new ArgumentOutOfRangeException($"The supplied initial claim date is not valid: Dates before {MinimumValidInitialBaseClaimDate} are not supported (initialClaimDate={initialClaimDate.Date.ToShortDateString()}).");
        }

        // Populate basePeriod with last 5 complete quarters, skipping most recent complete quarter
        var previous5Quarters = new UIQuarter[5];
        var currentQuarter = new UIQuarter(initialClaimDate);

        for (var i = 4; i >= 0; i--)
        {
            currentQuarter = --currentQuarter;
            previous5Quarters[i] = new UIQuarter(currentQuarter.Year, currentQuarter.QuarterNumber);
        }

        this.standardQuarters = previous5Quarters.Take(4).ToArray();
        this.alternateQuarters = previous5Quarters.Skip(1).ToArray();
    }

    /// <summary>
    /// Constructor by year and week.
    /// </summary>
    /// <param name="year">year</param>
    /// <param name="weekOfYear">weak</param>
    public BasePeriod(int year, int weekOfYear)
    {
        var initialClaimDate = CalendarQuarter.GetDateTimeFromYearAndWeek(year, weekOfYear);
        this.PopulateBasePeriods(initialClaimDate);
    }

    /// <summary>
    /// Get base period quarters as IEnumerable of IUIQuarter.
    /// </summary>
    public IEnumerable<IUIQuarter> BasePeriodQuarters => new List<IUIQuarter>(this.standardQuarters);

    /// <summary>
    /// Get base period quarters as IEnumerable of IUIQuarter.
    /// </summary>
    public IEnumerable<IUIQuarter> AltBasePeriodQuarters => new List<IUIQuarter>(this.alternateQuarters);
}
