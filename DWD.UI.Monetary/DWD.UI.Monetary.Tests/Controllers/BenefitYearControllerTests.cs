namespace DWD.UI.Monetary.Tests.Controllers
{
    using System;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Service.Controllers;
    using Xunit;

    public class BenefitYearControllerTests
    {
        [Fact]
        public void ShouldReturnValidBenefitYear()
        {

            var mock = new Mock<ILogger<BenefitYearContoller>>();

            var controller = new BenefitYearContoller(mock.Object, new CalculateBenefitYear(new CalculateBasePeriod()));

            // Act
            var actionResult = controller.GetBenefitYearForRequestedDate(new DateTime(2021, 2, 1));

            // Assert
            Assert.NotNull(actionResult);
            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var okResultValue = okResult.Value as BenefitYear;
            Assert.NotNull(okResultValue);
            Assert.Equal(52, okResultValue.BenefitWeeks);
        }

        [Fact]
        public void ShouldReturn400Error()
        {
            var mock = new Mock<ILogger<BenefitYearContoller>>();
            var controller = new BenefitYearContoller(mock.Object, new CalculateBenefitYear(new CalculateBasePeriod()));

            var actionResult = controller.GetBenefitYearForRequestedDate(new DateTime(1898, 2, 1));

            Assert.NotNull(actionResult);
            // We cast it to the expected response type
            var notOkResult = actionResult.Result as ObjectResult;
            Assert.NotNull(notOkResult);
            Assert.Equal(400, notOkResult.StatusCode);

        }
    }
}
