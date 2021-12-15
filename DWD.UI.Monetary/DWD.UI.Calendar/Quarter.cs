[assembly: CLSCompliant(true)]

namespace DWD.UI.Calendar;

/// <summary>
/// An object representing a quarter of a year.
/// </summary>
public record Quarter : IComparable
{
    /// <summary>
    /// The quarter's year.
    /// </summary>
    public int Year { get; private set; }

    /// <summary>
    /// The quarter number.
    /// </summary>
    public int QuarterNumber { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Quarter"/> from year and quarter number.
    /// </summary>
    /// <param name="year">The quarter's year.</param>
    /// <param name="quarterNumber">The quarter number.</param>
    /// <exception cref="ArgumentOutOfRangeException">The quarter number was not in the range [1,4]</exception>"
    public Quarter(int year, int quarterNumber)
    {
        if (quarterNumber is < 1 or > 4)
        {
            throw new ArgumentOutOfRangeException(nameof(quarterNumber), "Quarter number must be between 1 and 4");
        }
        this.Year = year;
        this.QuarterNumber = quarterNumber;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Quarter"/> from a date.
    /// </summary>
    /// <remarks>
    /// For unemployment purposes the quarter starts on Sunday of the 1st, 14th, 27th, and 40th UI weeks of the year.
    /// </remarks>
    /// <param name="date">The date from which to construct the new Quarter.</param>
    public Quarter(DateTime date)
    {
        var uiWeek = new UIWeek(date);
        this.Year = uiWeek.Year;
        this.QuarterNumber = uiWeek.WeekNumber switch
        {
            < 14 => 1,
            < 27 => 2,
            < 40 => 3,
            _ => 4,
        };
    }

    /// <summary>
    /// Creates a new Quarter that is the chronological predecessor of this instance
    /// </summary>
    /// <returns></returns>
    public Quarter Previous()
    {
        var newYear = this.Year;
        var newQuarterNumber = this.QuarterNumber - 1;
        if (newQuarterNumber < 1)
        {
            newYear--;
            newQuarterNumber = 4;
        }
        return new Quarter(newYear, newQuarterNumber);
    }

    /// <summary>
    /// Compares this instance to a specified Quarter and returns an indication of their relative values.
    /// </summary>
    /// <param name="obj">The other quarter.</param>
    /// <returns>-1, 0, or 1, depending on whether the this instance is
    /// earlier, equal to, or later than the other quarter, respectively.</returns>
    public int CompareTo(object? obj)
    {
        if (obj == null || !this.GetType().IsAssignableFrom(obj.GetType()))
        {
            return 1;
        }
        var other = obj as Quarter;
        return this.Year == other!.Year ? this.QuarterNumber.CompareTo(other.QuarterNumber) : this.Year.CompareTo(other.Year);
    }

    /// <summary>
    /// Indicates whether a quarter precedes another quarter.
    /// </summary>
    /// <param name="left">Left-hand quarter.</param>
    /// <param name="right">Right-hand quarter.</param>
    public static bool operator <(Quarter left, Quarter right) => left is null ? right is not null : left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether a quarter equals or precedes another quarter.
    /// </summary>
    /// <param name="left">Left-hand quarter.</param>
    /// <param name="right">Right-hand quarter.</param>
    public static bool operator <=(Quarter left, Quarter right) => left is null || left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether a quarter follows another quarter.
    /// </summary>
    /// <param name="left">Left-hand quarter.</param>
    /// <param name="right">Right-hand quarter.</param>
    public static bool operator >(Quarter left, Quarter right) => left is not null && left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether a quarter equals or follows another quarter.
    /// </summary>
    /// <param name="left">Left-hand quarter.</param>
    /// <param name="right">Right-hand quarter.</param>
    public static bool operator >=(Quarter left, Quarter right) => left is null ? right is null : left.CompareTo(right) >= 0;
}
