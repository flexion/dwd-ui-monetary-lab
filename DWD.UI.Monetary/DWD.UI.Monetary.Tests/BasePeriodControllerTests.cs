namespace DWD.UI.Monetary.Tests
{
    using System;
    using DWD.UI.Monetary.Domain.UseCases;
    using DWD.UI.Monetary.Service.Controllers;
    using MELT;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Xunit;

    public class BasePeriodControllerTests
    {
        private readonly ITestLoggerFactory loggerFactory;
        private readonly ILogger<BasePeriodController> logger;

        /// <summary>
        /// Instantiates the MonetaryController test class, injecting dependencies
        /// Serves as the setup before each test
        /// </summary>
        public BasePeriodControllerTests()
        {
            this.loggerFactory = TestLoggerFactory.Create();
            this.logger = this.loggerFactory.CreateLogger<BasePeriodController>();
        }

        [Fact]
        public void GetStandardBasePeriodFromInitialClaimDateShouldReturnSuccessWhenValidInitialClaimDate()
        {
            var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());
            var testDate = new DateTime(1981, 6, 28);

            var result = controller.GetStandardBasePeriodFromInitialClaimDate(testDate);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetStandardBasePeriodFromInitialClaimDateShouldLogAnErrorWhenInitialClaimDateInvalid()
        {
            var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());
            var testDate = new DateTime(1899, 1, 1);

            var result = controller.GetStandardBasePeriodFromInitialClaimDate(testDate);

            var log = Assert.Single(this.loggerFactory.Sink.LogEntries);
            Assert.IsType<ArgumentException>(log.Exception);
        }

        [Fact]
        public void GetAlternateBasePeriodFromInitialClaimDateShouldReturnSuccessWhenValidInitialClaimDate()
        {
            var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());
            var testDate = new DateTime(1981, 6, 28);

            var result = controller.GetAlternateBasePeriodFromInitialClaimDate(testDate);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetAlternateBasePeriodFromInitialClaimDateShouldLogAnErrorWhenInitialClaimDateInvalid()
        {
            var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());
            var testDate = new DateTime(1899, 1, 1);

            var result = controller.GetAlternateBasePeriodFromInitialClaimDate(testDate);

            var log = Assert.Single(this.loggerFactory.Sink.LogEntries);
            Assert.IsType<ArgumentException>(log.Exception);
        }
    }
}