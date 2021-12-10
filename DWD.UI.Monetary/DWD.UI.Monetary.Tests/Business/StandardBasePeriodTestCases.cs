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
/// These are standard base period business test cases provided by the SME as part of the user story.
/// </summary>
public class StandardBasePeriodTestCases
{
    [Theory]
    [ClassData(typeof(StandardBasePeriodTestData))]
    public void BusinessTestCases_TestStandardBasePeriodCalculations_ClassData(string testDescription,
        DateTime initialClaimDate, Collection<Quarter> expectedQuarters)
    {
        // Arrange
        var basePeriodUseCase = new CalculateBasePeriod();

        // Act
        var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(initialClaimDate);

        // Assert
        Assert.NotNull(result);
        var quarters = result.BasePeriodQuarters;
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
    public void BusinessTestCase_ShouldReturnStandardBasePeriods_WhenYearAndWeekValid()
    {
        // Arrange
        var basePeriodUseCase = new CalculateBasePeriod();

        // Act
        var result = basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(2021, 28);

        // Assert
        Assert.NotNull(result);
        var quarters = result.BasePeriodQuarters;
        Assert.NotNull(quarters);

        var actualQuarters = quarters
            .OrderBy(q => q.Year)
            .ThenBy(q => q.QuarterNumber)
            .ToArray();

        var testQuarters = new IUIQuarter[4];
        testQuarters[0] = new UIQuarter(2020, 2);
        testQuarters[1] = new UIQuarter(2020, 3);
        testQuarters[2] = new UIQuarter(2020, 4);
        testQuarters[3] = new UIQuarter(2021, 1);

        Assert.Equal(testQuarters, actualQuarters);
    }
}

public class StandardBasePeriodTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnCorrectStandardBaseQuarters_WhenValidClaimDate",
            new DateTime(2021, 10, 31),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 3}, new() {Year = 2020, QuarterNumber = 4}, new() {Year=2021, QuarterNumber = 1}, new() {Year=2021, QuarterNumber = 2}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnCurrentStandardBaseQuarters_WhenClaimDateOnFirstSundayOfQuarter",
            new DateTime(2021, 1, 3),
            new Collection<Quarter>()
            {
                new() {Year = 2019, QuarterNumber = 4}, new() {Year = 2020, QuarterNumber = 1}, new(){Year=2020, QuarterNumber = 2}, new(){Year=2020, QuarterNumber = 3}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnThirdQuarterStandardBaseQuarters_WhenClaimDateInFourthQuarterOfLeapYear",
            new DateTime(2020, 12, 6),
            new Collection<Quarter>()
            {
                new() {Year = 2019, QuarterNumber = 3}, new() {Year = 2019, QuarterNumber = 4}, new(){Year=2020, QuarterNumber = 1}, new(){Year=2020, QuarterNumber = 2}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnPriorQuarterStandardBaseQuarters_WhenClaimDatePriorToFirstSundayOfQuarter",
            new DateTime(2021, 4, 1),
            new Collection<Quarter>()
            {
                new() {Year = 2019, QuarterNumber = 4}, new() {Year = 2020, QuarterNumber = 1}, new(){Year=2020, QuarterNumber = 2}, new(){Year=2020, QuarterNumber = 3}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnCurrentStandardBaseQuarters_WhenClaimDateAfterFirstSundayOfQuarter",
            new DateTime(2021, 4, 6),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 1}, new() {Year = 2020, QuarterNumber = 2}, new(){Year=2020, QuarterNumber = 3}, new(){Year=2020, QuarterNumber = 4}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnPriorQuarterStandardBaseQuarters_WhenClaimDatePriorToFirstSundayOfYear",
            new DateTime(2022, 1, 1),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 3}, new() {Year = 2020, QuarterNumber = 4}, new(){Year=2021, QuarterNumber = 1}, new(){Year=2021, QuarterNumber = 2}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnCurrentStandardBaseQuarters_WhenClaimDateOnHoliday",
            new DateTime(2021, 11, 25),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 3}, new() {Year = 2020, QuarterNumber = 4}, new(){Year=2021, QuarterNumber = 1}, new(){Year=2021, QuarterNumber = 2}
            }
        };
        yield return new object[]
        {
            "BusinessTestCase_ShouldReturnCurrentStandardBaseQuarters_WhenClaimDateOnFirstSundayOfQuarterAndHoliday",
            new DateTime(2021, 7, 4),
            new Collection<Quarter>()
            {
                new() {Year = 2020, QuarterNumber = 2}, new() {Year = 2020, QuarterNumber = 3}, new(){Year=2020, QuarterNumber = 4}, new(){Year=2021, QuarterNumber = 1}
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}