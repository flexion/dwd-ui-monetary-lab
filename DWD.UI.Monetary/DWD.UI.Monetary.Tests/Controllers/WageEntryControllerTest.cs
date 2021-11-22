namespace DWD.UI.Monetary.Tests.Controllers
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
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
    public class WageEntryControllerTest
    {
        [Fact]
        public void CreateClaimantWageTest()
        {
            var controller = GetWageEntryController();
            _ = controller.CreateClaimantWage("12", 2021, 3, (decimal)100.00);
            var actionResult = controller.GetAllClaimantWages();
            Assert.NotNull(actionResult);
            // We cast it to the expected response type
            var okResult = actionResult as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var wages = okResult.Value as Collection<ClaimantWage>;
            Assert.True(wages != null && wages.Count.Equals(1));
            Assert.Equal(1, wages[0].Id);
            Assert.Equal("12", wages[0].ClaimantId);
        }

        [Fact]
        public void GetClaimantWageTest()
        {
            var controller = GetWageEntryController();
            var actionResultFirst = controller.CreateClaimantWage("19", 2021, 3, (decimal)100.00);
            Assert.NotNull(actionResultFirst);

            var okResultFirst = actionResultFirst as OkObjectResult;
            Assert.NotNull(okResultFirst);
            var wagesFirst = okResultFirst.Value as Collection<ClaimantWage>;
            Assert.NotNull(wagesFirst);

            var actionResult = controller.GetClaimantWage(wagesFirst.Last().Id);

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
            var controller = GetWageEntryController();
            _ = controller.CreateClaimantWage("21", 2021, 3, (decimal)100.00);

            var actionResultFirst = controller.GetAllClaimantWagesForClaimant("21");
            Assert.NotNull(actionResultFirst);

            var okResultFirst = actionResultFirst as OkObjectResult;
            Assert.NotNull(okResultFirst);
            var wagesFirst = okResultFirst.Value as Collection<ClaimantWage>;
            Assert.NotNull(wagesFirst);

            _ = controller.UpdateClaimantWage(wagesFirst.Last().Id, 2010, 4, 200);

            var actionResult = controller.GetClaimantWage(wagesFirst.Last().Id);

            Assert.NotNull(actionResult);
            // We cast it to the expected response type
            var okResult = actionResult as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var wage = okResult.Value as ClaimantWage;
            Assert.True(wage != null);
            Assert.Equal("21", wage.ClaimantId);
            Assert.Equal((short)2010, wage.WageYear);
        }

        [Fact]
        public void DeleteClaimantWageTest()
        {
            var controller = GetWageEntryController();
            var actionResult = controller.CreateClaimantWage("12", 2021, 3, (decimal)100.00);
            Assert.NotNull(actionResult);

            var okResult = actionResult as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var wages = okResult.Value as Collection<ClaimantWage>;

            Assert.NotNull(wages);
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
            var controller = GetWageEntryController();
            _ = controller.CreateClaimantWage("13", 2021, 3, (decimal)100.00);

            var actionResult = controller.GetAllClaimantWagesForClaimant("13");

            Assert.NotNull(actionResult);
            // We cast it to the expected response type
            var okResult = actionResult as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var wages = okResult.Value as Collection<ClaimantWage>;
            Assert.True(wages != null && wages.Count.Equals(1));
            Assert.Equal(1, wages[0].Id);
            Assert.Equal("13", wages[0].ClaimantId);
        }

        private static WageEntryController GetWageEntryController()
        {
            var dbContextOptions =
                new DbContextOptionsBuilder<ClaimantWageContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
            var controller =
                new WageEntryController(new ClaimantWageDbRepository(
                                            new ClaimantWageContext(dbContextOptions)));
            return controller;
        }
    }
}
