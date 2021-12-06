namespace DWD.UI.Monetary.Tests.Controllers
{
    using System;
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

            var mockCalculateBenefitYear = new Mock<CalculateBenefitYear>().Object;

            var controller = new BenefitYearContoller(mockCalculateBenefitYear);

            // Act
            var actionResult = controller.GetBenefitYearForRequestedDate(new DateTime(2021, 2, 1));

            // Assert
            Assert.NotNull(actionResult);
            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var okResultValue = okResult.Value as BenefitYear;
            Assert.NotNull(okResultValue);
            Assert.Equal(52, okResultValue.BenefitWeeks);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
