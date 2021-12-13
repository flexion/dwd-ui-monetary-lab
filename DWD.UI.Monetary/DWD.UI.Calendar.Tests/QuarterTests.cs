namespace DWD.UI.Calendar.Tests;

using System;
using Xunit;

public class QuarterTests
{
    [Fact]
    public void QuarterShouldHaveValueEquality()
    {
        var a = new Quarter(2021, 1);
        var b = new Quarter(2021, 1);
        Assert.Equal(a, b);
    }

    [Fact]
    public void QuarterShouldBeComparableWithBinaryOperators()
    {
        var a = new Quarter(2021, 4);
        var b = new Quarter(2022, 1);
        Assert.True(a < b);
        Assert.True(b > a);
        Assert.True(a <= b);
        Assert.True(b >= a);
    }

    [Fact]
    public void QuarterShouldBeComparableWithAnotherQuarter()
    {
        var a = new Quarter(2021, 4);
        var b = new Quarter(2022, 1);
        var c = new Quarter(2022, 1);
        Assert.Equal(-1, a.CompareTo(b));
        Assert.Equal(1, b.CompareTo(a));
        Assert.Equal(0, b.CompareTo(c));
        Assert.Equal(1, b.CompareTo(null));
    }

    [Theory]
    [InlineData(2020, 0)]
    [InlineData(2020, -1)]
    [InlineData(2020, 5)]
    public void QuarterShouldRejectInvalidConstruction(int year, int quarterNumber) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new Quarter(year, quarterNumber));
}
