namespace DWD.UI.Calendar.Tests;

using System;
using Xunit;

public class QuarterTests
{
    [Theory]
    [InlineData(2022, 1, 2022, 1)]
    [InlineData(2022, 2, 2022, 2)]
    [InlineData(2022, 3, 2022, 3)]
    [InlineData(2022, 4, 2022, 4)]
    public void ShouldConstructFromYearAndQuarter(int year, int quarter, int expectedYear, int expectedQuarter)
    {
        var sut = new Quarter(year, quarter);

        Assert.Equal(expectedYear, sut.Year);
        Assert.Equal(expectedQuarter, sut.QuarterNumber);
    }

    [Theory]
    [InlineData(2021, 1, 2020, 4)]
    [InlineData(2021, 2, 2021, 1)]
    public void PreviousShouldProducePredecessorQuarter(int year, int quarter, int expectedYear, int expectedQuarter)
    {
        var a = new Quarter(year, quarter);

        var b = a.Previous();

        Assert.Equal(expectedYear, b.Year);
        Assert.Equal(expectedQuarter, b.QuarterNumber);
    }

    [Fact]
    public void QuarterShouldHaveValueEquality()
    {
        var a = new Quarter(2021, 1);
        var b = new Quarter(2021, 1);
        Assert.Equal(a, b);
    }

    [Fact]
    public void QuarterShouldBeComparableToAnotherQuarterWithBinaryOperators()
    {
        var a = new Quarter(2021, 4);
        var b = new Quarter(2022, 1);
        Assert.True(a < b);
        Assert.True(b > a);
        Assert.True(a <= b);
        Assert.True(b >= a);
    }

    [Fact]
    public void QuarterShouldBeComparableToNullWithBinaryLessThan()
    {
        var a = new Quarter(2021, 4);
        Assert.True(null < a);
        Assert.False(a < null);
    }

    [Fact]
    public void QuarterShouldBeComparableToNullWithBinaryLessThanOrEqual()
    {
        var a = new Quarter(2021, 4);
        Assert.True(null <= a);
        Assert.False(a <= null);
    }

    [Fact]
    public void QuarterShouldBeComparableToNullWithBinaryGreaterThan()
    {
        var a = new Quarter(2021, 4);
        Assert.True(a > null);
        Assert.False(null > a);
    }

    [Fact]
    public void QuarterShouldBeComparableToNullWithBinaryGreaterThanOrEqual()
    {
        var a = new Quarter(2021, 4);
        Assert.True(a >= null);
        Assert.False(null >= a);
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
