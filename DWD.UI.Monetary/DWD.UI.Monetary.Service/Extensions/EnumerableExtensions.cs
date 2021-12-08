namespace DWD.UI.Monetary.Service.Extensions;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public static class ExtensionMethods
{
    /// <summary>
    /// Convert a list to a collection
    /// </summary>
    /// <param name="items"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>Collection</returns>
    /// <exception cref="ArgumentNullException"></exception>
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