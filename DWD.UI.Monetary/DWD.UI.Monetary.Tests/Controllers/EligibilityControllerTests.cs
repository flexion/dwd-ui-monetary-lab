namespace DWD.UI.Monetary.Tests.Controllers
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Domain.UseCases;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using DWD.UI.Monetary.Service.Controllers;
    using DWD.UI.Monetary.Service.Models;
    using MELT;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    /// <summary>
    /// Some throw away tests once we get the real object mode.
    /// Fluent tests would be preferable here.
    /// </summary>
    public sealed class EligibilityControllerTest
    {
        private readonly ILogger<BasePeriodController> logger;

        public EligibilityControllerTest()
        {
            using var loggerFactory = TestLoggerFactory.Create();
            this.logger = loggerFactory.CreateLogger<BasePeriodController>();
        }

        [Fact]
        public async Task ShouldReturnEligibleResultDtoWhenVerifiedEligible()
        {
            // Arrange
            var data = new EligibilityRequestDto()
            {
                InitialClaimDate = new DateTime(2021, 11, 26),
                ClaimantId = "abc123",
                WagesOfQuarters = new Collection<decimal>() { 1500M, 6500M, 250M, 2500M }
            };
            const decimal expectedWeeklyBenefitRate = 260M;
            var mock = new Mock<ICheckEligibilityOfMonetaryRequirements>();
            _ = mock.Setup(m => m.VerifyAsync(It.IsAny<EligibilityVerificationRequest>()))
                                 .ReturnsAsync(new EligibleResult(expectedWeeklyBenefitRate));
            var controller = new EligibilityController(mock.Object, this.logger);

            // Act
            var resp = await controller.VerifyEligibilityAsync(data).ConfigureAwait(true);

            // Assert
            Assert.NotNull(resp);
            var okObjectResult = Assert.IsType<OkObjectResult>(resp);
            var eligibilityResult = Assert.IsType<EligibleResultDto>(okObjectResult.Value);
            Assert.True(eligibilityResult.IsEligible);
            Assert.Equal(expectedWeeklyBenefitRate, eligibilityResult.WeeklyBenefitRate);
        }

        [Fact]
        public async Task ShouldReturnIneligibleResultDtoWhenVerifiedIneligible()
        {
            // Arrange
            var data = new EligibilityRequestDto()
            {
                InitialClaimDate = new DateTime(2021, 11, 26),
                ClaimantId = "abc123",
                WagesOfQuarters = new Collection<decimal>() { 42M, 250M, 1000M, 325M }
            };

            var mock = new Mock<ICheckEligibilityOfMonetaryRequirements>();
            _ = mock.Setup(m => m.VerifyAsync(It.IsAny<EligibilityVerificationRequest>()))
                    .ReturnsAsync(new IneligibleResult(new Collection<IneligibilityReason>()
                    {
                        IneligibilityReason.InsufficientHighQuarterWage
                    }));
            var controller = new EligibilityController(mock.Object, this.logger);

            // Act
            var resp = await controller.VerifyEligibilityAsync(data).ConfigureAwait(true);

            // Assert
            Assert.NotNull(resp);
            var okObjectResult = Assert.IsType<OkObjectResult>(resp);
            var ineligibleResult = Assert.IsType<IneligibleResultDto>(okObjectResult.Value);
            Assert.False(ineligibleResult.IsEligible);
            Assert.NotEmpty(ineligibleResult.IneligibleReasons);
        }
    }
}
