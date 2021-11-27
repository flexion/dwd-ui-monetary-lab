namespace DWD.UI.Monetary.Tests.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using DWD.UI.Monetary.Service.Controllers;
    using DWD.UI.Monetary.Service.Models;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    /// <summary>
    /// Some throw away tests once we get the real object mode.
    /// Fluent tests would be preferable here.
    /// </summary>
    public sealed class ElegibilityControllerTest
    {
        private readonly ElegibilityDto data;
        private readonly EligibilityController controller;

        public ElegibilityControllerTest()
        {
            this.data = new ElegibilityDto()
            {
                InitialClaimDate = new DateTime(2021, 11, 26),
                ClaimantId = "abc123",
                MinHighQuarterEarnings = 1350,
                PercentWeeklyBenefitRate = 4,
                MinQuarters = 2,
                WagesOutsideOfHighQuarterFactor = 4,
                BasePeriodWagesFactor = 35,
                NumberOfWeeks = 26,
                PercentOfBasePeriodWages = 40
            };
            this.controller = new EligibilityController();
        }

        [Theory]
        [ClassData(typeof(WageData))]
        public void TestElegibilityClassData(Collection<decimal> wages, bool expectedElegibility)
        {
            // Arrange
            this.data.WagesOfQuarters = wages;

            // Act
            var resp = this.controller.VerifyElegibility(this.data);

            // Assert
            Assert.NotNull(resp);
            var okObjectResult = Assert.IsType<OkObjectResult>(resp);
            var elegibilityResult = Assert.IsType<EligibilityResult>(okObjectResult.Value);
            Assert.Equal(expectedElegibility, elegibilityResult.IsEligible);
        }
    }

    public class WageData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Collection<decimal>() { 1500M, 6500M, 250M, 2500M }, true };
            yield return new object[] { new Collection<decimal>() { 42M, 250M, 1000M, 325M }, false };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
