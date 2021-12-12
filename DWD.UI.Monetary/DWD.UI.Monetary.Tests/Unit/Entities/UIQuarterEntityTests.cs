namespace DWD.UI.Monetary.Tests.Unit.Entities;

using System;
using Domain.BusinessEntities;
using Xunit;

/// <summary>
/// Unit tests for the UIQuarter entity.
/// </summary>
/// <remarks>
/// Using the naming convention:
///     [Class][functionality to test]_ShouldReturn[output description]_When[input description].
/// </remarks>
public class UIQuarterEntityTests
{
    [Fact]
    public void UIQuarterConstructor_ShouldReturnValidUIQuarter_WhenValidYearAndQuarter()
    {
        const int validYear = 2021;
        const int validQuarter = 1;

        var quarter = new UIQuarter(validYear, validQuarter);

        Assert.Equal(2021, quarter.Year);
        Assert.Equal(1, quarter.QuarterNumber);
    }

    [Fact]
    public void UIQuarterConstructor_ShouldReturnArgumentException_WhenInvalidQuarter()
    {
        const int validYear = 2021;
        const int invalidQuarter = 5;

        Assert.Throws<ArgumentException>(() => new UIQuarter(validYear, invalidQuarter));
    }

    [Fact]
    public void UIQuarterConstructor_ShouldReturnArgumentException_WhenInvalidYear()
    {
        const int invalidYear = -10;
        const int validQuarter = 4;

        Assert.Throws<ArgumentException>(() => new UIQuarter(invalidYear, validQuarter));
    }


    [Fact]
    public void UIQuarterConstructor_ShouldReturnArgumentException_WhenInvalidYearAndInvalidQuarter()
    {
        const int invalidYear = -10;
        const int invalidQuarter = -4;

        Assert.Throws<ArgumentException>(() => new UIQuarter(invalidYear, invalidQuarter));
    }

    [Fact]
    public void UIQuarterConstructor_ShouldReturnValidUIQuarter_WhenValidDate()
    {
        var validDate = new DateTime(2021, 4, 15);

        var quarter = new UIQuarter(validDate);

        Assert.Equal(2021, quarter.Year);
        Assert.Equal(2, quarter.QuarterNumber);
    }

    [Fact]
    public void UIQuarterConstructor_ShouldReturnPriorCalendarQuarter_WhenDateBeforeFirstSundayOfCalendarQuarter()
    {
        var priorToFirstSundayOfCalendarQuarter = new DateTime(2021, 1, 1);

        var quarter = new UIQuarter(priorToFirstSundayOfCalendarQuarter);

        Assert.Equal(2020, quarter.Year);
        Assert.Equal(4, quarter.QuarterNumber);
    }

    [Fact]
    public void UIQuarterConstructor_ShouldReturnCurrentCalendarQuarter_WhenDateOnFirstSundayOfCalendarQuarter()
    {
        var firstSundayOfCalendarQuarter = new DateTime(2021, 1, 3);

        var quarter = new UIQuarter(firstSundayOfCalendarQuarter);

        Assert.Equal(2021, quarter.Year);
        Assert.Equal(1, quarter.QuarterNumber);
    }

    [Fact]
    public void UIQuarterConstructor_ShouldReturnCurrentCalendarQuarter_WhenDateAfterLastSundayOfCalendarQuarter()
    {
        var lastDayOfCalendarQuarter = new DateTime(2021, 1, 15);

        var quarter = new UIQuarter(lastDayOfCalendarQuarter);

        Assert.Equal(2021, quarter.Year);
        Assert.Equal(1, quarter.QuarterNumber);
    }

    [Fact]
    public void UIQuarterConstructor_ShouldReturnCurrentCalendarQuarter_WhenDateOnLastDayOfCalendarQuarter()
    {
        var lastDayOfCalendarQuarter = new DateTime(2021, 12, 31);

        var quarter = new UIQuarter(lastDayOfCalendarQuarter);

        Assert.Equal(2021, quarter.Year);
        Assert.Equal(4, quarter.QuarterNumber);
    }

    [Fact]
    public void UIQuarterConstructor_ShouldReturnCurrentCalendarQuarter_WhenDateOnLeapDay()
    {
        var leapDay = new DateTime(2020, 2, 29);

        var quarter = new UIQuarter(leapDay);

        Assert.Equal(2020, quarter.Year);
        Assert.Equal(1, quarter.QuarterNumber);
    }

    [Fact]
    public void UIQuarterConstructor_ShouldReturnArgumentException_WhenInvalidDate()
    {
        var invalidDate = new DateTime(1800, 1, 1);
        _ = Assert.Throws<ArgumentException>(() => new UIQuarter(invalidDate));
    }

    [Fact]
    public void UIQuarterDecrement_ShouldReturnPreviousQuarter_WhenDecrementedByOperator()
    {
        var lastDayOfCalendarQuarter = new DateTime(2021, 1, 15);

        var quarter = new UIQuarter(lastDayOfCalendarQuarter);

        Assert.Equal(2021, quarter.Year);
        Assert.Equal(1, quarter.QuarterNumber);

        var previousQuarter = --quarter;

        Assert.Equal(2020, previousQuarter.Year);
        Assert.Equal(4, previousQuarter.QuarterNumber);
    }

    [Fact]
    public void UIQuarterDecrement_ShouldReturnPreviousQuarter_WhenDecrementedByMethod()
    {
        var lastDayOfCalendarQuarter = new DateTime(2021, 12, 15);

        var quarter = new UIQuarter(lastDayOfCalendarQuarter);

        Assert.Equal(2021, quarter.Year);
        Assert.Equal(4, quarter.QuarterNumber);

        var previousQuarter = UIQuarter.Decrement(quarter);

        Assert.Equal(2021, previousQuarter.Year);
        Assert.Equal(3, previousQuarter.QuarterNumber);
    }

    [Fact]
    public void UIQuarterEquals_ShouldReturnTrue_WhenQuarterValuesAreEqual()
    {
        var myFirstQuarter = new UIQuarter(2021, 1);
        var myEqualQuarter = new UIQuarter(new DateTime(2021, 1, 15));
        Assert.True(myFirstQuarter.Equals(myEqualQuarter));
    }

    [Fact]
    public void UIQuarterEquals_ShouldReturnFalse_WhenQuarterValuesAreNotEqual()
    {
        var myFirstQuarter = new UIQuarter(2021, 1);
        var notEqualQuarter = new UIQuarter(new DateTime(2021, 4, 15));
        Assert.False(myFirstQuarter.Equals(notEqualQuarter));
    }

    [Fact]
    public void UIQuarterEquals_ShouldReturnFalse_WhenIncorrectObjectType()
    {
        var myFirstQuarter = new UIQuarter(2021, 1);
        var incorrectObject = new BasePeriod(new DateTime(2021, 1, 15));
        Assert.False(myFirstQuarter.Equals(incorrectObject));
    }

    [Fact]
    public void UIQuarterGetHashCode_ShouldReturnInteger()
    {
        var myFirstQuarter = new UIQuarter(2021, 1);
        var hashCode = myFirstQuarter.GetHashCode();

        // We're in trouble if these fail :-)
        Assert.True(hashCode >= int.MinValue);
        Assert.True(hashCode <= int.MaxValue);
    }
}
