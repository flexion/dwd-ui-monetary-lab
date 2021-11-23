namespace DWD.UI.Monetary.Tests.Controllers
{
    using System;
    using System.Linq;
    using Domain.UseCases;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging.Abstractions;
    using Service.Controllers;
    using Service.Models;
    using Xunit;

    /// <summary>
    /// Test endpoints in the base period controller.
    /// </summary>
    public class BasePeriodControllerTests
    {
        [Fact]
        public void BasePeriodControllerGetStandardBasePeriod_ShouldReturnStandardBasePeriod_WhenValidClaimDate()
        {
            var validClaimDate = new DateTime(2021, 10, 15);
            var basePeriodController =
                new BasePeriodController(new NullLogger<BasePeriodController>(), new CalculateBasePeriod());

            var actionResult = basePeriodController.GetStandardBasePeriodFromInitialClaimDate(validClaimDate);
            Assert.NotNull(actionResult);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            Assert.NotNull(okResult.Value);
            var basePeriod = Assert.IsAssignableFrom<IBasePeriodDto>(okResult.Value);
            Assert.NotNull(basePeriod);
            var quarters = basePeriod.Quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            Assert.Equal(2020, quarters[0].Year);
            Assert.Equal(3, quarters[0].QuarterNumber);
            Assert.Equal(2020, quarters[1].Year);
            Assert.Equal(4, quarters[1].QuarterNumber);
            Assert.Equal(2021, quarters[2].Year);
            Assert.Equal(1, quarters[2].QuarterNumber);
            Assert.Equal(2021, quarters[3].Year);
            Assert.Equal(2, quarters[3].QuarterNumber);
        }

        [Fact]
        public void BasePeriodControllerGetAlternateBasePeriod_ShouldReturnAltBasePeriod_WhenValidClaimDate()
        {
            var validClaimDate = new DateTime(2021, 10, 15);
            var basePeriodController =
                new BasePeriodController(new NullLogger<BasePeriodController>(), new CalculateBasePeriod());

            var actionResult = basePeriodController.GetAlternateBasePeriodFromInitialClaimDate(validClaimDate);
            Assert.NotNull(actionResult);
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            Assert.NotNull(okResult.Value);
            var basePeriod = Assert.IsAssignableFrom<IBasePeriodDto>(okResult.Value);
            Assert.NotNull(basePeriod);
            var quarters = basePeriod.Quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            Assert.Equal(2020, quarters[0].Year);
            Assert.Equal(4, quarters[0].QuarterNumber);
            Assert.Equal(2021, quarters[1].Year);
            Assert.Equal(1, quarters[1].QuarterNumber);
            Assert.Equal(2021, quarters[2].Year);
            Assert.Equal(2, quarters[2].QuarterNumber);
            Assert.Equal(2021, quarters[3].Year);
            Assert.Equal(3, quarters[3].QuarterNumber);
        }

        [Fact]
        public void BasePeriodControllerGetStandardBasePeriod_ShouldReturnBadRequest_WhenInvalidClaimDate()
        {
            var invalidClaimDate = new DateTime(1887, 1, 1);
            var basePeriodController =
                new BasePeriodController(new NullLogger<BasePeriodController>(), new CalculateBasePeriod());

            var actionResult = basePeriodController.GetStandardBasePeriodFromInitialClaimDate(invalidClaimDate);
            Assert.NotNull(actionResult);
            var badRequestResult = Assert.IsAssignableFrom<ObjectResult>(actionResult);
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void BasePeriodControllerGetAlternateBasePeriod_ShouldReturnBadRequest_WhenInvalidClaimDate()
        {
            var invalidClaimDate = new DateTime(1887, 1, 1);
            var basePeriodController =
                new BasePeriodController(new NullLogger<BasePeriodController>(), new CalculateBasePeriod());

            var actionResult = basePeriodController.GetAlternateBasePeriodFromInitialClaimDate(invalidClaimDate);
            Assert.NotNull(actionResult);
            var badRequestResult = Assert.IsAssignableFrom<ObjectResult>(actionResult);
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }
    }
}
