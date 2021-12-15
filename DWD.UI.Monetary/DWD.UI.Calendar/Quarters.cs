namespace DWD.UI.Calendar;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a sorted collection of calendar year quarters.
/// </summary>
public class Quarters : IEquatable<Quarters>
{
    private readonly SortedSet<Quarter> quarters;

    /// <summary>
    /// Initializes a new instance of Quarters.
    /// </summary>
    public Quarters() => this.quarters = new SortedSet<Quarter>();

    /// <summary>
    /// Add a Quarter to the collection.
    /// </summary>
    /// <param name="quarter">The quarter to be added.</param>
    public void Add(Quarter quarter) => this.quarters.Add(quarter);

    /// <summary>
    /// Gets the quarters in the collection as an array, sorted chronologically ascending (oldest first).
    /// </summary>
    public Quarter[] ToArray() => this.quarters.ToArray();

    /// <summary>
    /// Gets the quarters in the collection as a list, sorted chronologically ascending (oldest first).
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<Quarter> ToList() => this.quarters.ToList();

    /// <summary>
    /// Indicates if this instance is equal to another instance in value and order.
    /// </summary>
    /// <param name="other">The other Quarters collection to compare to this instance.</param>
    public bool Equals(Quarters? other)
    {
        if (other is null || this.quarters.Count != other.quarters.Count)
        {
            return false;
        }

        return ReferenceEquals(this, other) || SortedSet<Quarter>.CreateSetComparer().Equals(this.quarters, other.quarters);
    }

    /// <summary>
    /// Indicates if this instance is equal to another instance in value and order.
    /// </summary>
    /// <param name="obj">The other Quarters collection to compare to this instance.</param>
    public override bool Equals(object? obj) => obj is not null && this.Equals(obj as Quarters);

    /// <summary>
    /// Gets a hash code unique to this instance.
    /// </summary>
    public override int GetHashCode() => this.quarters.GetHashCode();
}
