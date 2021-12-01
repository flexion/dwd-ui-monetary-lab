namespace DWD.UI.Monetary.Tests.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using DWD.UI.Monetary.Service.Controllers;
    using DWD.UI.Monetary.Service.Models;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Service.Gateways;
    using Xunit;

    /// <summary>
    /// Some throw away tests once we get the real object mode.
    /// Fluent tests would be preferable here.
    /// </summary>
    public sealed class EligibilityControllerTest
    {
        [Theory]
        [ClassData(typeof(WageData))]
        public async Task TestEligibilityClassData(Collection<decimal> wages, bool expectedEligibility, decimal? expectedWeeklyBenefitRate)
        {
            // Arrange
            var data = new EligibilityRequestDto()
            {
                InitialClaimDate = new DateTime(2021, 11, 26),
                ClaimantId = "abc123",
                WagesOfQuarters = wages
            };
            var mockEligibilityBasisGateway = new Mock<IEligibilityBasisGateway>();
            mockEligibilityBasisGateway.Setup(m => m.GetEligibilityBasisAsync())
                .ReturnsAsync(new EligibilityBasis(1350, 4, 2,
                    4, 35, 26, 40));
            var controller = new EligibilityController(mockEligibilityBasisGateway.Object);

            // Act
            var resp = await controller.VerifyEligibilityAsync(data);

            // Assert
            Assert.NotNull(resp);
            var okObjectResult = Assert.IsType<OkObjectResult>(resp);
            if (expectedEligibility)
            {
                var eligibilityResult = Assert.IsType<EligibleResult>(okObjectResult.Value);
                Assert.Equal(expectedEligibility, eligibilityResult.IsEligible);
                Assert.Equal(expectedWeeklyBenefitRate, eligibilityResult.WeeklyBenefitRate);
            }
            else
            {
                var eligibilityResult = Assert.IsType<IneligibleResult>(okObjectResult.Value);
                Assert.NotEmpty(eligibilityResult.IneligibilityReasons);
            }
        }
    }

    public class WageData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new Collection<decimal>() { 1500M, 6500M, 250M, 2500M }, true, 260M };
            yield return new object[] { new Collection<decimal>() { 42M, 250M, 1000M, 325M }, false, null };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
