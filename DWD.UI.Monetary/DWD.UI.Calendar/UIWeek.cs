namespace DWD.UI.Calendar;

using System;
using System.Globalization;

/// <summary>
/// 
/// </summary>
public record UIWeek
{
    private readonly DayOfWeek firstDayOfWeek;

    /// <summary>
    /// Gets the first day of the week as a DateTime.
    /// </summary>
    public DateTime StartDate { get; }

    /// <summary>
    /// Gets the last day of the week as a DateTime.
    /// </summary>
    public DateTime EndDate { get; }

    /// <summary>
    /// Gets the number of the week in the year according to the UI definition.
    /// </summary>
    public int WeekNumber { get; }

    /// <summary>
    /// Initializes a new UIWeek, given a year and week number.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="weekNumber">The week nember.</param>
    /// <param name="firstDayOfWeek">Optional definition of the first day of the week.</param>
    /// <exception cref="ArgumentOutOfRangeException">The week number was out of the acceptible range for the given year</exception>
    public UIWeek(int year, int weekNumber, DayOfWeek firstDayOfWeek = DayOfWeek.Saturday)
    {
        var totalWeeksInYear = GetTotalWeeksInYear(year);

        if (weekNumber < 1 || weekNumber > totalWeeksInYear)
        {
            throw new ArgumentOutOfRangeException(nameof(weekNumber), $"The week number must be between 1 and {totalWeeksInYear} in {year}");
        }

        this.firstDayOfWeek = firstDayOfWeek;
        this.WeekNumber = weekNumber;
        this.StartDate = FirstDateOfWeek(year, weekNumber);
        this.EndDate = this.StartDate.AddDays(6);
    }

    /// <summary>
    /// Initializes a new UIWeek, given a date.
    /// </summary>
    /// <param name="date">A DateTime used as the basis of the week</param>
    public UIWeek(DateTime date)
    {
        date = date.Date;
        this.StartDate = date.AddDays(DayOfWeek.Sunday - date.DayOfWeek);
        this.EndDate = this.StartDate.AddDays(6);

        var dateTimeFormatInfo = DateTimeFormatInfo.GetInstance(new CultureInfo("en-US"));

        this.WeekNumber = dateTimeFormatInfo.Calendar.GetWeekOfYear(date,
            CalendarWeekRule.FirstFullWeek,
            this.firstDayOfWeek);
    }

    /// <summary>
    /// Get the number of weeks in the given year.
    /// </summary>
    /// <param name="year">The year.</param>
    private static int GetTotalWeeksInYear(int year)
    {
        var firstDayOfYear = new DateTime(year, 1, 1).DayOfWeek;
        var dtfi = DateTimeFormatInfo.GetInstance(new CultureInfo("en-US"));
        if (dtfi.Calendar.IsLeapYear(year))
        {
            return (firstDayOfYear is DayOfWeek.Friday or DayOfWeek.Saturday) ? 53 : 52;
        }
        else
        {
            return firstDayOfYear is DayOfWeek.Saturday ? 53 : 52;
        }
    }

    /// <summary>
    /// Find the date of the first day of the given year and week.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="weekOfYear">The week number (1-based).</param>
    /// <returns></returns>
    private static DateTime FirstDateOfWeek(int year, int weekOfYear)
    {
        var firstDayOfYear = new DateTime(year, 1, 1);

        // Set the firstDayOfYear to previous Sunday if it is not Sunday.
        if (firstDayOfYear.DayOfWeek != DayOfWeek.Sunday)
        {
            firstDayOfYear = firstDayOfYear.AddDays(DayOfWeek.Sunday - firstDayOfYear.DayOfWeek);
        }

        return firstDayOfYear.AddDays((weekOfYear - 1) * 7);
    }
}
