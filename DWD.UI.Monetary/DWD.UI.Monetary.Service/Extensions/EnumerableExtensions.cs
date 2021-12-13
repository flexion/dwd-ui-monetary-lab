namespace DWD.UI.Monetary.Service.Extensions;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Extends the IEnumerable class to convert Lists to Collections.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Convert a list to a collection.
    /// </summary>
    /// <param name="items">The List to be converted.</param>
    /// <typeparam name="T">The Type of the items in the list.</typeparam>
    /// <returns>Collection of items of type T.</returns>
    /// <exception cref="ArgumentNullException">The items list was null.</exception>
    public static Collection<T> ToCollection<T>(this IEnumerable<T> items)
    {
        if (items is null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        var collection = new Collection<T>();

        foreach (var item in items)
        {
            collection.Add(item);
        }

        return collection;
    }
}
