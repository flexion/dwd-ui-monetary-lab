namespace DWD.UI.Monetary.Tests.Controllers;

using Microsoft.Extensions.Logging.Abstractions;
using Service.Models;

using System;
using System.Linq;
using DWD.UI.Monetary.Domain.UseCases;
using DWD.UI.Monetary.Service.Controllers;
using MELT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

/// <summary>
/// Test endpoints in the base period controller.
/// </summary>
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
        _ = Assert.IsType<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.Equal(200, okResult.StatusCode);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public void GetStandardBasePeriodFromInitialClaimDateShouldLogAnErrorWhenInitialClaimDateInvalid()
    {
        var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());
        var testDate = new DateTime(1899, 1, 1);

        _ = controller.GetStandardBasePeriodFromInitialClaimDate(testDate);

        var log = Assert.Single(this.loggerFactory.Sink.LogEntries);
        _ = Assert.IsType<ArgumentOutOfRangeException>(log.Exception);
    }

    [Fact]
    public void GetStandardBasePeriodFromYearAndWeekShouldLogAnErrorForInvalidYear()
    {
        var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());

        _ = controller.GetStandardBasePeriodFromYearAndWeek(1899, 8);

        var log = Assert.Single(this.loggerFactory.Sink.LogEntries);
        _ = Assert.IsType<ArgumentOutOfRangeException>(log.Exception);
    }

    [Fact]
    public void GetStandardBasePeriodFromYearAndWeekShouldLogAnErrorForInvalidWeek()
    {
        var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());

        _ = controller.GetStandardBasePeriodFromYearAndWeek(1981, 53);

        var log = Assert.Single(this.loggerFactory.Sink.LogEntries);
        _ = Assert.IsType<ArgumentOutOfRangeException>(log.Exception);
    }

    [Fact]
    public void GetStandardBasePeriodFromYearAndWeekShouldReturnSuccessWhenYearAndWeekValid()
    {
        var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());

        var result = controller.GetStandardBasePeriodFromYearAndWeek(2021, 15);

        Assert.NotNull(result);
        _ = Assert.IsType<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.Equal(200, okResult.StatusCode);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public void GetAlternateBasePeriodFromInitialClaimDateShouldReturnSuccessWhenValidInitialClaimDate()
    {
        var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());
        var testDate = new DateTime(1981, 6, 28);

        var result = controller.GetAlternateBasePeriodFromInitialClaimDate(testDate);

        Assert.NotNull(result);
        _ = Assert.IsType<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.Equal(200, okResult.StatusCode);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public void GetAlternateBasePeriodFromInitialClaimDateShouldLogAnErrorWhenInitialClaimDateInvalid()
    {
        var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());
        var testDate = new DateTime(1899, 1, 1);

        _ = controller.GetAlternateBasePeriodFromInitialClaimDate(testDate);

        var log = Assert.Single(this.loggerFactory.Sink.LogEntries);
        _ = Assert.IsType<ArgumentOutOfRangeException>(log.Exception);
    }

    [Fact]
    public void GetAlternateBasePeriodFromYearAndWeekShouldLogAnErrorForInvalidYear()
    {
        var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());

        _ = controller.GetAlternateBasePeriodFromYearAndWeek(1899, 8);

        var log = Assert.Single(this.loggerFactory.Sink.LogEntries);
        _ = Assert.IsType<ArgumentOutOfRangeException>(log.Exception);
    }

    [Fact]
    public void GetAlternateBasePeriodFromYearAndWeekShouldLogAnErrorForInvalidWeek()
    {
        var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());

        _ = controller.GetAlternateBasePeriodFromYearAndWeek(1981, 53);

        var log = Assert.Single(this.loggerFactory.Sink.LogEntries);
        _ = Assert.IsType<ArgumentOutOfRangeException>(log.Exception);
    }

    [Fact]
    public void GetAlternateBasePeriodFromYearAndWeekShouldReturnSuccessWhenYearAndWeekValid()
    {
        var controller = new BasePeriodController(this.logger, new CalculateBasePeriod());

        var result = controller.GetAlternateBasePeriodFromYearAndWeek(2021, 15);

        Assert.NotNull(result);
        _ = Assert.IsType<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.Equal(200, okResult.StatusCode);
        Assert.NotNull(okResult.Value);
    }

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
