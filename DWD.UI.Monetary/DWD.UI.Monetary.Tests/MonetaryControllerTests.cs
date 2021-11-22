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
        ITestLoggerFactory loggerFactory;
        ILogger<MonetaryController> logger;

        public MonetaryControllerTests()
        {
            loggerFactory = TestLoggerFactory.Create();
            logger = loggerFactory.CreateLogger<MonetaryController>();
        }

        public void Dispose()
        {
            loggerFactory.Dispose();
        }

        [Fact]
        public void GetBasePeriodFromInitialClaimDate_Should_ReturnSuccess_When_ValidInitialClaimDate()
        {
            var controller = new MonetaryController(logger, new CalculateBasePeriod());
            var testDate = new DateTime(1981, 6, 28);

            var result = controller.GetBasePeriodFromInitialClaimDate(testDate, false);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetBasePeriodFromInitialClaimDate_Should_LogAnError_WhenInitialClaimDateInvalid()
        {
            var controller = new MonetaryController(logger, new CalculateBasePeriod());
            var testDate = new DateTime(1899, 1, 1);

            var result = controller.GetBasePeriodFromInitialClaimDate(testDate, false);

            var log = Assert.Single(loggerFactory.Sink.LogEntries);
            Assert.IsType<ArgumentException>(log.Exception);
        }
    }
}