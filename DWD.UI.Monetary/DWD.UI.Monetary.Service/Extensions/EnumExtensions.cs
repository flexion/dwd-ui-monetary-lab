namespace DWD.UI.Monetary.Service.Extensions;

using System;
using System.ComponentModel;

/// <summary>
/// Methods to extend the functionality for type <see cref="Enum"/>.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    ///  Get description of an Enum from the annotation attribute
    /// </summary>
    /// <param name="genericEnum">enum to describe.</param>
    /// <returns>string</returns>
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

        dynamic attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return ((DescriptionAttribute)attributes[0]).Description;
    }
}
