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
/// Unit tests for the CalculateBasePeriod use case.  Standard base period calculations.
/// </summary>
public class StandardBasePeriodUseCaseTests
{
    [Theory]
    [ClassData(typeof(StandardBasePeriodUseCaseTestData))]
    public void ShouldCalculateStandardBasePeriodFromInitialClaimDate(string testDescription,
        DateTime initialClaimDate, Collection<Quarter> expectedQuarters)
    {
        // Arrange
        var basePeriodUseCase = new CalculateBasePeriod();

        // Act
        var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(initialClaimDate);

        // Assert
        Assert.NotNull(result);
        var quarters = result.StandardQuarters;
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
    public void ShouldCalculateStandardBasePeriodFromYearAndWeek()
    {
        // Arrange
        var basePeriodUseCase = new CalculateBasePeriod();
        var expectedQuarters = new List<Quarter>
        {
            new Quarter(2020, 2),
            new Quarter(2020, 3),
            new Quarter(2020, 4),
            new Quarter(2021, 1)
        };

        // Act
        var result = basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(2021, 28);

        // Assert
        Assert.NotNull(result);
        var quarters = result.StandardQuarters;
        Assert.NotNull(quarters);
        Assert.Equal(expectedQuarters, quarters);
    }
}

public class StandardBasePeriodUseCaseTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "Valid claim date",
            new DateTime(2021, 10, 15),
            new Collection<Quarter>() { new(2020, 3), new(2020, 4), new(2021, 1), new(2021, 2) }
        };
        yield return new object[]
        {
            "Claim date prior to first Sunday of first quarter",
            new DateTime(2021, 1, 1),
            new Collection<Quarter>() { new(2019, 3), new(2019, 4), new(2020, 1), new(2020, 2) }
        };
        yield return new object[]
        {
            "Claim date prior to first Sunday of second quarter",
            new DateTime(2021, 4, 1),
            new Collection<Quarter>() { new(2019, 4), new(2020, 1), new(2020, 2), new(2020, 3) }
        };
        yield return new object[]
        {
            "Claim date on first Sunday of quarter",
            new DateTime(2021, 1, 3),
            new Collection<Quarter>() { new(2019, 4), new(2020, 1), new(2020, 2), new(2020, 3) }
        };
        yield return new object[]
        {
            "Claim date after first Sunday of first quarter",
            new DateTime(2021, 1, 15),
            new Collection<Quarter>() { new(2019, 4), new(2020, 1), new(2020, 2), new(2020, 3) }
        };
        yield return new object[]
        {
            "Claim date after first Sunday of second quarter",
            new DateTime(2021, 4, 6),
            new Collection<Quarter>() { new(2020, 1), new(2020, 2), new(2020, 3), new(2020, 4) }
        };
        yield return new object[]
        {
            "Claim date in fourth quarter of leap year",
            new DateTime(2020, 12, 6),
            new Collection<Quarter>() { new(2019, 3), new(2019, 4), new(2020, 1), new(2020, 2) }
        };
        yield return new object[]
        {
            "Claim date on holiday",
            new DateTime(2021, 11, 25),
            new Collection<Quarter>() { new(2020, 3), new(2020, 4), new(2021, 1), new(2021, 2) }
        };
        yield return new object[]
        {
            "Claim date on first Sunday of quarter and holiday",
            new DateTime(2021, 7, 4),
            new Collection<Quarter>() { new(2020, 2), new(2020, 3), new(2020, 4), new(2021, 1) }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
