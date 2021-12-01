namespace DWD.UI.Monetary.Service.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Domain.BusinessEntities;

    public static class ExtensionMethods
    {
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
        public static string GetDescription(this Enum genericEnum)
        {
            if (genericEnum is null)
            {
                return string.Empty;
            }

            var genericEnumType = genericEnum.GetType();
            var memberInfo = genericEnumType.GetMember(genericEnum.ToString());

            if (memberInfo.Length <= 0)
            {
                return genericEnum.ToString();
            }

            dynamic attributes = memberInfo[0].GetCustomAttributes
                (typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
