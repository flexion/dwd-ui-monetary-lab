namespace DWD.UI.Monetary.Tests.Entities;

using System;
using DWD.UI.Monetary.Domain.BusinessEntities;
using Xunit;

public class BenefitYearTests
{
    [Fact]
    public void ShouldConstructUsingPositionalParameters()
    {
        const int benefitWeeks = 2;
        var claimRequestedDate = new DateTime(1990, 5, 4);
        var beginDate = new DateTime(1981, 6, 28);
        var endDate = new DateTime(1984, 8, 25);
        var beginYearWeek = new YearWeek(1981, 24);
        var endYearWeek = new YearWeek(1984, 36);

        var sut = new BenefitYear(benefitWeeks, claimRequestedDate, beginDate, endDate, beginYearWeek, endYearWeek);

        Assert.Equal(benefitWeeks, sut.BenefitWeeks);
        Assert.Equal(claimRequestedDate, sut.ClaimRequestedDate);
        Assert.Equal(beginDate, sut.BeginDate);
        Assert.Equal(endDate, sut.EndDate);
        Assert.Equal(beginYearWeek, sut.BeginYearWeek);
        Assert.Equal(endYearWeek, sut.EndYearWeek);
    }

    [Fact]
    public void ShouldConstructUsingPositionalParametersAndDeconstruct()
    {
        const int benefitWeeks = 2;
        var claimRequestedDate = new DateTime(1990, 5, 4);
        var beginDate = new DateTime(1981, 6, 28);
        var endDate = new DateTime(1984, 8, 25);
        var beginYearWeek = new YearWeek(1981, 24);
        var endYearWeek = new YearWeek(1984, 36);

        int actualBenefitWeeks;
        DateTime actualBeginDate;
        DateTime actualEndDate;
        DateTime actualClaimRequestedDate;
        YearWeek actualBeginYearWeek;
        YearWeek actualEndYearWeek;

        (actualBenefitWeeks, actualClaimRequestedDate, actualBeginDate, actualEndDate, actualBeginYearWeek, actualEndYearWeek) =
            new BenefitYear(benefitWeeks, claimRequestedDate, beginDate, endDate, beginYearWeek, endYearWeek);

        Assert.Equal(benefitWeeks, actualBenefitWeeks);
        Assert.Equal(claimRequestedDate, actualClaimRequestedDate);
        Assert.Equal(beginDate, actualBeginDate);
        Assert.Equal(endDate, actualEndDate);
        Assert.Equal(beginYearWeek, actualBeginYearWeek);
        Assert.Equal(endYearWeek, actualEndYearWeek);
    }

}
