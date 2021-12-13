namespace DWD.UI.Calendar;

using System;

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
    /// Construct from year and quarter number.
    /// </summary>
    /// <param name="year">The quarter's year.</param>
    /// <param name="quarterNumber">The quarter number.</param>
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
    /// <param name="a">Left-hand quarter.</param>
    /// <param name="b">Right-hand quarter.</param>
    public static bool operator <(Quarter a, Quarter b) =>
        a.Year < b.Year || (a.Year == b.Year && a.QuarterNumber < b.QuarterNumber);

    /// <summary>
    /// Indicates whether a quarter follows another quarter.
    /// </summary>
    /// <param name="a">Left-hand quarter.</param>
    /// <param name="b">Right-hand quarter.</param>
    public static bool operator >(Quarter a, Quarter b) =>
        a.Year > b.Year || (a.Year == b.Year && a.QuarterNumber > b.QuarterNumber);

    /// <summary>
    /// Indicates whether a quarter equals or precedes another quarter.
    /// </summary>
    /// <param name="a">Left-hand quarter.</param>
    /// <param name="b">Right-hand quarter.</param>
    public static bool operator <=(Quarter a, Quarter b) =>
        a.Year < b.Year || (a.Year == b.Year && a.QuarterNumber <= b.QuarterNumber);

    /// <summary>
    /// Indicates whether a quarter equals or follows another quarter.
    /// </summary>
    /// <param name="a">Left-hand quarter.</param>
    /// <param name="b">Right-hand quarter.</param>
    public static bool operator >=(Quarter a, Quarter b) =>
        a.Year > b.Year || (a.Year == b.Year && a.QuarterNumber >= b.QuarterNumber);
}
