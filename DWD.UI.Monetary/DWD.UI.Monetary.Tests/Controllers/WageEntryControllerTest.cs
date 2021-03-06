namespace DWD.UI.Monetary.Tests.Controllers;

using System;
using System.Collections.ObjectModel;
using AutoMapper;
using DWD.UI.Calendar;
using DWD.UI.Monetary.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Controllers;
using Service.Frameworks;
using Service.Gateways;
using Service.Models.Stubs;
using Xunit;

/// <summary>
/// Some throw away tests once we get the real object mode.
/// Fluent tests would be preferable here.
/// </summary>
public sealed class WageEntryControllerTest : IDisposable
{

    private readonly ClaimantWageContext dbContextOptions;
    private readonly ClaimantWageDbRepository claimantWageDbRepository;
    private readonly WageEntryController controller;

    public WageEntryControllerTest()
    {
        var config = new MapperConfiguration(opts => opts.CreateMap<Quarter, CalendarQuarterDto>().ReverseMap());
        var mapper = config.CreateMapper();
        this.dbContextOptions =
            new ClaimantWageContext(new DbContextOptionsBuilder<ClaimantWageContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
        this.claimantWageDbRepository = new ClaimantWageDbRepository(this.dbContextOptions);
        this.controller = new WageEntryController(this.claimantWageDbRepository, mapper);
    }

    [Fact]
    public void CreateClaimantWageTest()
    {
        _ = this.controller.CreateClaimantWage("12", 2021, 3, (decimal)100.00);
        var actionResult = this.controller.GetAllClaimantWages();
        Assert.NotNull(actionResult);
        // cast it to the expected response type
        var okResult = actionResult as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);

        // verify the DB answer
        var wages = this.claimantWageDbRepository.GetClaimantWagesByClaimantId("12");
        Assert.True(wages != null && wages.Count.Equals(1));
        Assert.Equal(1, wages[0].Id);
        Assert.Equal("12", wages[0].ClaimantId);
    }

    [Fact]
    public void GetClaimantWageTest()
    {
        var inWage = new ClaimantWage
        {
            ClaimantId = "19",
            WageYear = 2021,
            WageQuarter = 3,
            TotalWages = (decimal)100.00
        };

        this.claimantWageDbRepository.AddClaimantWage(inWage);
        var wages = this.claimantWageDbRepository.GetClaimantWagesByClaimantId("19");
        Assert.NotNull(wages);
        var actionResult = this.controller.GetClaimantWage(wages[0].Id);

        Assert.NotNull(actionResult);
        // We cast it to the expected response type
        var okResult = actionResult as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        var wage = okResult.Value as ClaimantWage;
        Assert.True(wage != null);
        Assert.Equal("19", wage.ClaimantId);
        Assert.Equal(2021, wage.WageYear);
        Assert.Equal((short)3, wage.WageQuarter);
        Assert.Equal((decimal)100.00, wage.TotalWages);
        Assert.Equal(1, wage.Id);
    }

    [Fact]
    public void UpdateClaimantWageTest()
    {
        var inWage = new ClaimantWage
        {
            ClaimantId = "21",
            WageYear = 2021,
            WageQuarter = 3,
            TotalWages = (decimal)100.00
        };

        this.claimantWageDbRepository.AddClaimantWage(inWage);

        var wages = this.claimantWageDbRepository.GetClaimantWagesByClaimantId("21");
        Assert.NotNull(wages);
        _ = this.controller.UpdateClaimantWage(wages[0].Id, 2010, 4, 200);

        var wage = this.claimantWageDbRepository.GetClaimantWage(wages[0].Id);
        Assert.True(wage != null);
        Assert.Equal("21", wage.ClaimantId);
        Assert.Equal(2010, wage.WageYear);
    }

    [Fact]
    public void DeleteClaimantWageTest()
    {
        var inWage = new ClaimantWage
        {
            ClaimantId = "21",
            WageYear = 2021,
            WageQuarter = 3,
            TotalWages = (decimal)100.00
        };

        this.claimantWageDbRepository.AddClaimantWage(inWage);

        var wages = this.claimantWageDbRepository.GetClaimantWagesByClaimantId("21");
        Assert.NotNull(wages);

        var actionResultDel = this.controller.DeleteClaimantWage(wages[0].Id);
        Assert.NotNull(actionResultDel);

        var okResultDel = actionResultDel as OkObjectResult;
        Assert.NotNull(okResultDel);
        Assert.Equal(200, okResultDel.StatusCode);
        var wagesDel = okResultDel.Value as Collection<ClaimantWage>;
        Assert.NotNull(wagesDel);
        Assert.Empty(wagesDel);
    }

    [Fact]
    public void GetAllWagesForClaimantTest()
    {
        var inWage = new ClaimantWage
        {
            ClaimantId = "33",
            WageYear = 2021,
            WageQuarter = 3,
            TotalWages = (decimal)100.00
        };

        this.claimantWageDbRepository.AddClaimantWage(inWage);

        var actionResult = this.controller.GetAllClaimantWagesForClaimant("33");

        Assert.NotNull(actionResult);
        // We cast it to the expected response type
        var okResult = actionResult as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        var wages = okResult.Value as Collection<ClaimantWage>;
        Assert.True(wages != null && wages.Count.Equals(1));
        Assert.Equal(1, wages[0].Id);
        Assert.Equal("33", wages[0].ClaimantId);
    }

    [Fact]
    public void GetAllWagesForClaimantByQuartersTest()
    {
        var wage1 = new ClaimantWage
        {
            Id = 1,
            ClaimantId = "33",
            WageYear = 2021,
            WageQuarter = 1,
            TotalWages = (decimal)100.00
        };

        var wage2 = new ClaimantWage
        {
            Id = 2,
            ClaimantId = "33",
            WageYear = 2021,
            WageQuarter = 2,
            TotalWages = (decimal)75.00
        };

        var wage3 = new ClaimantWage
        {
            Id = 3,
            ClaimantId = "33",
            WageYear = 2021,
            WageQuarter = 3,
            TotalWages = (decimal)50.00
        };

        this.claimantWageDbRepository.AddClaimantWage(wage1);
        this.claimantWageDbRepository.AddClaimantWage(wage2);
        this.claimantWageDbRepository.AddClaimantWage(wage3);

        var calendarQuarters = new Collection<CalendarQuarterDto>()
        {
            new CalendarQuarterDto() {Year = 2021, QuarterNumber = 1},
            new CalendarQuarterDto() {Year = 2021, QuarterNumber = 2},
        };

        var actionResult = this.controller.GetAllWagesForClaimantByQuarters("33", calendarQuarters);

        Assert.NotNull(actionResult);
        // We cast it to the expected response type
        var okResult = actionResult as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        var wages = okResult.Value as Collection<ClaimantWage>;
        Assert.True(wages is { Count: 2 });
        Assert.Equal(1, wages[0].Id);
        Assert.Equal(2, wages[1].Id);
        Assert.Equal("33", wages[0].ClaimantId);
        Assert.Equal("33", wages[1].ClaimantId);
    }

    public void Dispose() => this.dbContextOptions?.Dispose();
}
