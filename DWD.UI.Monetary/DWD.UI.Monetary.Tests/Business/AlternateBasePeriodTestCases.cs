namespace DWD.UI.Monetary.Tests.Business;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Domain.BusinessEntities;
using Domain.UseCases;
using Xunit;

/// <summary>
/// These are alternate base period business test cases provided by the SME as part of the user story.
/// </summary>
public class AlternateBasePeriodTestCases
{
    [Theory]
    [ClassData(typeof(AlternateBasePeriodTestData))]
    public void BusinessTestCases_TestAlternateBasePeriodCalculations_ClassData(string testDescription, DateTime initialClaimDate, Collection<Quarter> expectedQuarters)
    {
        // Arrange
        var basePeriodUseCase = new CalculateBasePeriod();

        // Act
        var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(initialClaimDate);

        // Assert
        Assert.NotNull(result);
        var quarters = result.AltBasePeriodQuarters;
        Assert.NotNull(quarters);

        var actualQuarters = quarters
            .OrderBy(q => q.Year)
            .ThenBy(q => q.QuarterNumber)
            .ToArray();

        var expectedUIQuarters = expectedQuarters
            .Select(x => new UIQuarter(x.Year, x.QuarterNumber))
            .ToArray();

        Assert.True(expectedUIQuarters[0].Equals(actualQuarters[0]), testDescription);
        Assert.True(expectedUIQuarters[1].Equals(actualQuarters[1]), testDescription);
        Assert.True(expectedUIQuarters[2].Equals(actualQuarters[2]), testDescription);
        Assert.True(expectedUIQuarters[3].Equals(actualQuarters[3]), testDescription);
    }

    [Fact]
    public void BusinessTestCase_ShouldReturnAlternateBasePeriods_WhenYearAndWeekValid()
    {
        // Arrange
        var basePeriodUseCase = new CalculateBasePeriod();

        // Act
        var result = basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(2021, 28);

        // Assert
        Assert.NotNull(result);
        var quarters = result.AltBasePeriodQuarters;
        Assert.NotNull(quarters);

        var actualQuarters = quarters
            .OrderBy(q => q.Year)
            .ThenBy(q => q.QuarterNumber)
            .ToArray();

        var expectedUIQuarters = new IUIQuarter[4];
        expectedUIQuarters[0] = new UIQuarter(2020, 3);
        expectedUIQuarters[1] = new UIQuarter(2020, 4);
        expectedUIQuarters[2] = new UIQuarter(2021, 1);
        expectedUIQuarters[3] = new UIQuarter(2021, 2);

        Assert.Equal(expectedUIQuarters, actualQuarters);
    }

    [Fact]
    public void BusinessTestCase_ShouldReturnArgumentException_WhenInvalidClaimDate()
    {
        var invalidClaimDate = new DateTime(1887, 7, 4);
        var basePeriodUseCase = new CalculateBasePeriod();

        _ = Assert.Throws<ArgumentException>(() =>
            basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(invalidClaimDate));
    }

    [Fact]
    public void BusinessTestCase_ShouldThrowArgumentException_WhenYearIsLessThanOne()
    {
        var basePeriodUseCase = new CalculateBasePeriod();
        // Expect ArgumentException
        _ = Assert.Throws<ArgumentException>(() =>
            basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(0, 45));
    }

    [Fact]
    public void BusinessTestCase_ShouldThrowArgumentException_WhenWeekIsLessThanOne()
    {
        var basePeriodUseCase = new CalculateBasePeriod();
        // Expect ArgumentException
        _ = Assert.Throws<ArgumentException>(() =>
            basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(1989, 0));
    }

    [Fact]
    public void BusinessTestCase_ShouldThrowArgumentException_WhenWeekIsGreaterThan53()
    {
        var basePeriodUseCase = new CalculateBasePeriod();
        // Expect ArgumentException
        _ = Assert.Throws<ArgumentException>(() =>
            basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(2022, 54));
    }
}

public class AlternateBasePeriodTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnCorrectAltBaseQuarters_WhenValidClaimDate",
            new DateTime(2021, 10, 31),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 4}, new() {Year = 2021, QuarterNumber = 1}, new() {Year=2021, QuarterNumber = 2}, new() {Year=2021, QuarterNumber = 3}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateOnFirstSundayOfQuarter",
            new DateTime(2021, 1, 3),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 1}, new() {Year = 2020, QuarterNumber = 2}, new(){Year=2020, QuarterNumber = 3}, new(){Year=2020, QuarterNumber = 4}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnThirdQuarterAltBaseQuarters_WhenClaimDateInFourthQuarterOfLeapYear",
            new DateTime(2020, 12, 6),
            new Collection<Quarter>()
            {
                new() {Year = 2019, QuarterNumber = 4}, new() {Year = 2020, QuarterNumber = 1}, new(){Year=2020, QuarterNumber = 2}, new(){Year=2020, QuarterNumber = 3}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnPriorQuarterAltBaseQuarters_WhenClaimDatePriorToFirstSundayOfQuarter",
            new DateTime(2021, 4, 1),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 1}, new() {Year = 2020, QuarterNumber = 2}, new(){Year=2020, QuarterNumber = 3}, new(){Year=2020, QuarterNumber = 4}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateAfterFirstSundayOfQuarter",
            new DateTime(2021, 4, 6),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 2}, new() {Year = 2020, QuarterNumber = 3}, new(){Year=2020, QuarterNumber = 4}, new(){Year=2021, QuarterNumber = 1}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnPriorQuarterAltBaseQuarters_WhenClaimDatePriorToFirstSundayOfYear",
            new DateTime(2022, 1, 1),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 4}, new() {Year = 2021, QuarterNumber = 1}, new(){Year=2021, QuarterNumber = 2}, new(){Year=2021, QuarterNumber = 3}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateOnFirstSundayOfQuarterAndHoliday",
            new DateTime(2021, 7, 4),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 3}, new() {Year = 2020, QuarterNumber = 4}, new(){Year=2021, QuarterNumber = 1}, new(){Year=2021, QuarterNumber = 2}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateOnHoliday",
            new DateTime(2021, 11, 25),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 4}, new() {Year = 2021, QuarterNumber = 1}, new(){Year=2021, QuarterNumber = 2}, new(){Year=2021, QuarterNumber = 3}
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}