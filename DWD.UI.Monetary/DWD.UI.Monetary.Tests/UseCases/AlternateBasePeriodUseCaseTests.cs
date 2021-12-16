namespace DWD.UI.Monetary.Tests.UseCases;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.UseCases;
using DWD.UI.Calendar;
using Xunit;
using Xunit.Sdk;

/// <summary>
/// Unit tests for the CalculateBasePeriod use case.  Alternate base period calculations.
/// </summary>
public class AlternateBasePeriodUseCaseTests
{
    [Theory]
    [ClassData(typeof(AlternateBasePeriodUseCaseTestData))]
    public void ShouldCalculateAlternateBasePeriodFromInitialClaimDate(string testDescription, DateTime initialClaimDate, Collection<Quarter> expectedQuarters)
    {
        // Arrange
        var basePeriodUseCase = new CalculateBasePeriod();

        // Act
        var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(initialClaimDate);

        // Assert
        Assert.NotNull(result);
        var quarters = result.AlternateQuarters;
        Assert.NotNull(quarters);
        try
        {
            Assert.Equal(expectedQuarters, quarters);
        }
        catch (EqualException ex)
        {
            throw new XunitException($"{testDescription}\n{ex}");
        }
    }

    [Fact]
    public void ShouldCalculateAlternateBasePeriodFromYearAndWeek()
    {
        // Arrange
        var basePeriodUseCase = new CalculateBasePeriod();
        var expectedQuarters = new List<Quarter>
        {
            new Quarter(2019, 4),
            new Quarter(2020, 1),
            new Quarter(2020, 2),
            new Quarter(2020, 3),
        };

        // Act
        var result = basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(2020, 48);

        // Assert
        Assert.NotNull(result);
        var quarters = result.AlternateQuarters;
        Assert.NotNull(quarters);
        Assert.Equal(expectedQuarters, quarters);
    }

    [Fact]
    public void CalculateBasePeriodFromInitialClaimDate_ShouldReturnArgumentException_WhenInvalidClaimDate()
    {
        var testClaimDate = new DateTime(1776, 7, 4);
        var basePeriodUseCase = new CalculateBasePeriod();
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(testClaimDate));
    }

    [Fact]
    public void BusinessTestCase_ShouldThrowArgumentException_WhenYearIsLessThanOne()
    {
        var basePeriodUseCase = new CalculateBasePeriod();
        // Expect ArgumentException
        _ = Assert.Throws<ArgumentOutOfRangeException>(() =>
            basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(0, 45));
    }

    [Fact]
    public void BusinessTestCase_ShouldThrowArgumentException_WhenWeekIsLessThanOne()
    {
        var basePeriodUseCase = new CalculateBasePeriod();
        // Expect ArgumentException
        _ = Assert.Throws<ArgumentOutOfRangeException>(() =>
            basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(1989, 0));
    }

    [Fact]
    public void BusinessTestCase_ShouldThrowArgumentException_WhenWeekIsGreaterThan53()
    {
        var basePeriodUseCase = new CalculateBasePeriod();
        // Expect ArgumentException
        _ = Assert.Throws<ArgumentOutOfRangeException>(() =>
            basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(2022, 54));
    }
}

public class AlternateBasePeriodUseCaseTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "Valid claim date",
            new DateTime(2021, 10, 15),
            new Collection<Quarter>() { new(2020, 4), new(2021, 1), new(2021, 2), new(2021, 3) }
        };
        yield return new object[]
        {
            "Claim date prior to first Sunday of first quarter",
            new DateTime(2021, 1, 1),
            new Collection<Quarter>() { new(2019, 4), new(2020, 1), new(2020, 2), new(2020, 3) }
        };
        yield return new object[]
        {
            "Claim date prior to first Sunday of second quarter",
            new DateTime(2021, 4, 1),
            new Collection<Quarter>() { new(2020, 1), new(2020, 2), new(2020, 3), new(2020, 4) }
        };
        yield return new object[]
        {
            "Claim date on first Sunday of quarter",
            new DateTime(2021, 1, 3),
            new Collection<Quarter>() { new(2020, 1), new(2020, 2), new(2020, 3), new(2020, 4) }
        };
        yield return new object[]
        {
            "Claim date after first Sunday of first quarter",
            new DateTime(2021, 1, 15),
            new Collection<Quarter>() { new(2020, 1), new(2020, 2), new(2020, 3), new(2020, 4) }
        };
        yield return new object[]
        {
            "Claim date after first Sunday of second quarter",
            new DateTime(2021, 4, 6),
            new Collection<Quarter>() { new(2020, 2), new(2020, 3), new(2020, 4), new(2021, 1) }
        };
        yield return new object[]
        {
            "Claim date in fourth quarter of leap year",
            new DateTime(2020, 12, 6),
            new Collection<Quarter>() { new(2019, 4), new(2020, 1), new(2020, 2), new(2020, 3) }
        };
        yield return new object[]
        {
            "Claim date on holiday",
            new DateTime(2021, 11, 25),
            new Collection<Quarter>() { new(2020, 4), new(2021, 1), new(2021, 2), new(2021, 3) }
        };
        yield return new object[]
        {
            "Claim date on first Sunday of quarter and holiday",
            new DateTime(2021, 7, 4),
            new Collection<Quarter>() { new(2020, 3), new(2020, 4), new(2021, 1), new(2021, 2) }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
