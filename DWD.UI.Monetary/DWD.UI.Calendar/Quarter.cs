[assembly: CLSCompliant(true)]

namespace DWD.UI.Calendar;

/// <summary>
/// An object representing a quarter of a year.
/// </summary>
public record Quarter : IComparable<Quarter>
{
    /// <summary>
    /// The quarter's year.
    /// </summary>
    public short Year { get; }

    /// <summary>
    /// The quarter number.
    /// </summary>
    public short QuarterNumber { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Quarter"/> from year and quarter number.
    /// </summary>
    /// <param name="year">The quarter's year.</param>
    /// <param name="quarterNumber">The quarter number.</param>
    /// <exception cref="ArgumentOutOfRangeException">The quarter number was not in the range [1,4]</exception>"
    public Quarter(short year, short quarterNumber)
    {
        if (quarterNumber is < 1 or > 4)
        {
            throw new ArgumentOutOfRangeException(nameof(quarterNumber), "Quarter number must be between 1 and 4");
        }
        this.Year = year;
        this.QuarterNumber = quarterNumber;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Quarter"/> from year and quarter number.
    /// </summary>
    /// <remarks>UI Quarters begin on Sunday of the first full week of months 1, 4, 7, &amp; 10.</remarks>
    /// <param name="date">A date on which to base the quarter.</param>
    public Quarter(DateTime date)
    {
        const int monthsPerQuarter = 3;

        var firstDayOfWeek = date.AddDays(DayOfWeek.Sunday - date.DayOfWeek);

        this.Year = Convert.ToInt16(firstDayOfWeek.Year);
        //this.Year = short.Parse(firstDayOfWeek.Year.ToString());
        this.QuarterNumber = Convert.ToInt16(Math.Ceiling((double)firstDayOfWeek.Month / monthsPerQuarter));
    }

    /// <summary>
    /// Creates a new Quarter that is the chronological predecessor of this instance.
    /// </summary>
    /// <returns>A new instance of Quarter that is the predecessor of the given Quarter.</returns>
    public Quarter Previous()
    {
        var newYear = this.Year;
        var newQuarterNumber = Convert.ToInt16(this.QuarterNumber - 1);
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
    /// <param name="other">The other quarter.</param>
    /// <returns>-1, 0, or 1, depending on whether the this instance is
    /// earlier, equal to, or later than the other quarter, respectively.</returns>
    public int CompareTo(Quarter? other)
    {
        if (other is null)
        {
            return 1;
        }
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
