namespace DWD.UI.Monetary.Tests.Controllers;

using System;
using System.Net;
using Domain.BusinessEntities;
using Domain.UseCases;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Controllers;
using Xunit;

public class BenefitYearControllerTests
{
    [Fact]
    public void ShouldReturnValidBenefitYear()
    {
        // Arrange

        var requestedDate = new DateTime(2021, 2, 1);

        var mockCalculateBenefitYear = new Mock<ICalculateBenefitYear>();

        var benefitWeeks = 52;
        var beginDate = new DateTime(2021, 1, 3);
        var endDate = new DateTime(2022, 1, 1);
        var beginYearWeek = new YearWeek(2021, 2);
        var endYearWeek = new YearWeek(2022, 1);

        _ = mockCalculateBenefitYear.Setup(m =>
                m.CalculateBenefitYearFromDate(requestedDate))
                    .Returns(new BenefitYear(benefitWeeks,
                                                requestedDate,
                                                beginDate,
                                                endDate,
                                                beginYearWeek,
                                                endYearWeek));

        var controller = new BenefitYearController(mockCalculateBenefitYear.Object);

        // Act

        var actionResult = controller.GetBenefitYearForRequestedDate(requestedDate);

        // Assert
        Assert.NotNull(actionResult);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        Assert.NotNull(okResult);
        Assert.NotNull(okResult.StatusCode);

        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode.Value);

        var okResultValue = Assert.IsType<BenefitYear>(okResult.Value);
        Assert.NotNull(okResultValue);

        Assert.Equal(benefitWeeks, okResultValue.BenefitWeeks);
        Assert.Equal(beginDate, okResultValue.BeginDate);
        Assert.Equal(endDate, okResultValue.EndDate);
        Assert.Equal(beginYearWeek, okResultValue.BeginYearWeek);
        Assert.Equal(endYearWeek, okResultValue.EndYearWeek);
    }
}
