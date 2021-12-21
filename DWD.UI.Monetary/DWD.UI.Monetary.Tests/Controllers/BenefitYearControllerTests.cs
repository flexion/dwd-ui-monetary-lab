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
        var mockCalculateBenefitYear = new Mock<CalculateBenefitYear>().Object;
        var controller = new BenefitYearController(mockCalculateBenefitYear);

        // Act
        var actionResult = controller.GetBenefitYearForRequestedDate(new DateTime(2021, 2, 1));

        // Assert
        Assert.NotNull(actionResult);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

        Assert.NotNull(okResult);
        Assert.NotNull(okResult.StatusCode);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode.Value);

        var okResultValue = Assert.IsType<BenefitYear>(okResult.Value);
        Assert.NotNull(okResultValue);
        Assert.Equal(52, okResultValue.BenefitWeeks);
        Assert.Equal(200, okResult.StatusCode);
    }
}
