namespace DWD.UI.Monetary.Tests
{
    using System;
    using DWD.UI.Monetary.Domain.UseCases;
    using DWD.UI.Monetary.Service.Controllers;
    using MELT;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Xunit;

    public class MonetaryControllerTests
    {
        private readonly ITestLoggerFactory loggerFactory;
        private readonly ILogger<MonetaryController> logger;

        /// <summary>
        /// Instantiates the MonetaryController test class, injecting dependencies
        /// Serves as the setup before each test
        /// </summary>
        public MonetaryControllerTests()
        {
            this.loggerFactory = TestLoggerFactory.Create();
            this.logger = this.loggerFactory.CreateLogger<MonetaryController>();
        }

        [Fact]
        public void GetBasePeriodFromInitialClaimDateShouldReturnSuccessWhenValidInitialClaimDate()
        {
            var controller = new MonetaryController(this.logger, new CalculateBasePeriod());
            var testDate = new DateTime(1981, 6, 28);

            var result = controller.GetBasePeriodFromInitialClaimDate(testDate, false);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetBasePeriodFromInitialClaimDateShouldLogAnErrorWhenInitialClaimDateInvalid()
        {
            var controller = new MonetaryController(this.logger, new CalculateBasePeriod());
            var testDate = new DateTime(1899, 1, 1);

            var result = controller.GetBasePeriodFromInitialClaimDate(testDate, false);

            var log = Assert.Single(this.loggerFactory.Sink.LogEntries);
            Assert.IsType<ArgumentException>(log.Exception);
        }
    }
}