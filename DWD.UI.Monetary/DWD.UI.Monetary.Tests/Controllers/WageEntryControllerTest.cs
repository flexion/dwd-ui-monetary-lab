#pragma warning disable IDE0060
#pragma warning disable CA1801

namespace DWD.UI.Monetary.Tests.Controllers
{
    using System;
    using System.Collections.ObjectModel;
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


        public WageEntryControllerTest()
        {
            this.dbContextOptions =
                new ClaimantWageContext(new DbContextOptionsBuilder<ClaimantWageContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options);

            this.claimantWageDbRepository = new ClaimantWageDbRepository(this.dbContextOptions);
        }

        [Fact]
        public void CreateClaimantWageTest()
        {
            var controller = this.GetWageEntryController();
            _ = controller.CreateClaimantWage("12", 2021, 3, (decimal)100.00);
            var actionResult = controller.GetAllClaimantWages();
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
            var controller = this.GetWageEntryController();
            var actionResult = controller.GetClaimantWage(wages[0].Id);

            Assert.NotNull(actionResult);
            // We cast it to the expected response type
            var okResult = actionResult as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var wage = okResult.Value as ClaimantWage;
            Assert.True(wage != null);
            Assert.Equal("19", wage.ClaimantId);
            Assert.Equal((short)2021, wage.WageYear);
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
            var controller = this.GetWageEntryController();
            _ = controller.UpdateClaimantWage(wages[0].Id, 2010, 4, 200);

            var wage = this.claimantWageDbRepository.GetClaimantWage(wages[0].Id);
            Assert.True(wage != null);
            Assert.Equal("21", wage.ClaimantId);
            Assert.Equal((short)2010, wage.WageYear);
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

            var controller = this.GetWageEntryController();
            var actionResultDel = controller.DeleteClaimantWage(wages[0].Id);
            Assert.NotNull(actionResultDel);

            var okResultDel = actionResultDel as OkObjectResult;
            Assert.NotNull(okResultDel);
            Assert.Equal(200, okResultDel.StatusCode);
            var wagesDel = okResultDel.Value as Collection<ClaimantWage>;
            Assert.NotNull(wagesDel);
            Assert.Empty(wagesDel);
        }

        [Fact]
        public void GetAllClaimantWagesForClaimantTest()
        {
            var inWage = new ClaimantWage
            {
                ClaimantId = "33",
                WageYear = 2021,
                WageQuarter = 3,
                TotalWages = (decimal)100.00
            };

            this.claimantWageDbRepository.AddClaimantWage(inWage);

            var controller = this.GetWageEntryController();
            var actionResult = controller.GetAllClaimantWagesForClaimant("33");

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

        private WageEntryController GetWageEntryController() => new(this.claimantWageDbRepository);

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool all) => this.dbContextOptions.Dispose();

    }
}
