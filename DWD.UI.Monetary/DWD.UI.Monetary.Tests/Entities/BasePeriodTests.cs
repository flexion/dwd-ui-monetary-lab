namespace DWD.UI.Monetary.Tests.Entities;

using System;
using System.Collections.Generic;
using DWD.UI.Calendar;
using DWD.UI.Monetary.Domain.BusinessEntities;
using Xunit;

public class BasePeriodTests
{
    private readonly List<Quarter> expectedStdQuarters;
    private readonly List<Quarter> expectedAltQuarters;

    public BasePeriodTests()
    {
        this.expectedStdQuarters = new List<Quarter>
        {
            new Quarter(2020, 3),
            new Quarter(2020, 4),
            new Quarter(2021, 1),
            new Quarter(2021, 2),
        };
        this.expectedAltQuarters = new List<Quarter>
        {
            new Quarter(2020, 4),
            new Quarter(2021, 1),
            new Quarter(2021, 2),
            new Quarter(2021, 3),
        };
    }

    [Fact]
    public void ShouldConstructStdBasePeriodFromClaimDate()
    {
        // Arrange
        var claimDate = new DateTime(2021, 12, 25);

        // Act
        var bp = new BasePeriod(claimDate);

        // Assert
        Assert.Equal(this.expectedStdQuarters, bp.StandardQuarters);
    }

    [Fact]
    public void ShouldConstructAltBasePeriodFromClaimDate()
    {
        // Arrange
        var claimDate = new DateTime(2021, 12, 25);

        // Act
        var bp = new BasePeriod(claimDate);

        // Assert
        Assert.Equal(this.expectedAltQuarters, bp.AlternateQuarters);
    }

    [Fact]
    public void ShouldConstructStdBasePeriodFromUIWeek()
    {
        // Arrange
        var claimDate = new DateTime(2021, 12, 25);
        var uiWeek = new UIWeek(claimDate);

        // Act
        var bp = new BasePeriod(uiWeek);

        // Assert
        Assert.Equal(this.expectedStdQuarters, bp.StandardQuarters);
    }

    [Fact]
    public void ShouldConstructAltBasePeriodFromUIWeek()
    {
        // Arrange
        var uiWeek = new UIWeek(new DateTime(2021, 12, 25));

        // Act
        var bp = new BasePeriod(uiWeek);

        // Assert
        Assert.Equal(this.expectedAltQuarters, bp.AlternateQuarters);
    }

}
