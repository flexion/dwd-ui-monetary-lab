#pragma warning disable IDE0055
#pragma warning disable CS3016

namespace DWD.UI.Monetary.Tests.Business
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Moq;
    using Service.Gateways;
    using Xunit;

    public class CheckEligibilityOfMonetaryRequirementsTests
    {

        private readonly ICheckEligibilityOfMonetaryRequirements objectUnderTest;

        public CheckEligibilityOfMonetaryRequirementsTests()
        {
            var mockEligibilityBasisGateway = new Mock<IEligibilityBasisGateway>();
            mockEligibilityBasisGateway.Setup(m => m.GetEligibilityBasisAsync())
                .ReturnsAsync(new EligibilityBasis(1350, 4, 2,
                    4, 35, 26, 40));
            this.objectUnderTest = new CheckEligibilityOfMonetaryRequirements(mockEligibilityBasisGateway.Object);
        }
        public static IEnumerable<object[]> DataForAtLeastTwoQuartersEligibilityVerification =>
         new List<object[]>
        {
            new object[] {new Collection<decimal>(){1500, 6500, 250, 2500}, true},
            new object[] {new Collection<decimal>(){0, 0, 0, 2500}, false},
            new object[] {new Collection<decimal>(){0, 6500, 0, 2600}, true},
        };

        [Theory]
        [MemberData(nameof(DataForAtLeastTwoQuartersEligibilityVerification))]
        public async Task ShouldHaveAtLeastTwoQuarters(Collection<decimal> wagesOfQuarters, bool expected)
        {
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020,5,7), "1234567890");
            var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest);
            Assert.NotNull(result);
            Assert.Equal(expected, result.IsEligible);
            if (expected)
            {
                Assert.IsType<EligibleResult>(result);
            }
            else
            {
                var ineligibilityResult = Assert.IsType<IneligibleResult>(result);
                Assert.Contains(IneligibilityReason.InsufficientQuartersWithWages, ineligibilityResult.IneligibilityReasons);
            }
        }

        [Fact]
        public void ShouldThrowInvalidOperationExceptionWhenQuarterWagesIsEmpty(){
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(new Collection<decimal>(), new DateTime(2020,5,7), "1234567890");
            Assert.ThrowsAsync<InvalidOperationException>(() => this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest));
        }

        public static IEnumerable<object[]> DataForHighQuarterEligibilityVerification =>
            new List<object[]>
            {
                new object[] {new Collection<decimal>(){1500, 6500,250,2500}, true},
                new object[] {new Collection<decimal>(){450, 1300, 100, 1000}, false},
            };

        [Theory]
        [MemberData(nameof(DataForHighQuarterEligibilityVerification))]
        public async Task ShouldHaveWagesOfHighQuarterAtLeastMinHighQuarterEarnings(Collection<decimal> wagesOfQuarters, bool expected)
        {
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020,5,7), "1234567890");
            var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest);
            Assert.NotNull(result);
            Assert.Equal(expected, result.IsEligible);
            if (expected)
            {
                Assert.IsType<EligibleResult>(result);
            }
            else
            {
                var ineligibilityResult = Assert.IsType<IneligibleResult>(result);
                Assert.Contains(IneligibilityReason.InsufficientHighQuarterWage, ineligibilityResult.IneligibilityReasons);
            }
        }

        public static IEnumerable<object[]> DataForPercentHighQuarterEligibilityVerification =>
            new List<object[]>
            {
                new object[] {new Collection<decimal>(){1500, 6500, 250, 2500}, true, 260m},
            };
        [Theory]
        [MemberData(nameof(DataForPercentHighQuarterEligibilityVerification))]
        public async Task ShouldCalculateWeeklyBenifitRateCorrectly(Collection<decimal> wagesOfQuarters, bool expected, decimal? expectedWeeklyBenefitRate)
        {
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020,5,7), "1234567890");
            var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest);
            Assert.NotNull(result);
            Assert.Equal(expected, result.IsEligible);
            var eligibleResult = Assert.IsType<EligibleResult>(result);
            Assert.Equal(expectedWeeklyBenefitRate, eligibleResult.WeeklyBenefitRate);
        }

        public static IEnumerable<object[]> DataForWagesOutsideHighQuarterEligibilityVerification =>
            new List<object[]>
            {
                new object[] {new Collection<decimal>(){1500, 6500,250,2500}, true, 260m},
                new object[] {new Collection<decimal>(){150, 2500, 100, 100}, false, null},
            };
        [Theory]
        [MemberData(nameof(DataForWagesOutsideHighQuarterEligibilityVerification))]
        public async Task ShouldHaveWagesOutsideHighQuarter(Collection<decimal> wagesOfQuarters, bool expected, decimal? expectedWeeklyBenefitRate)
        {
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020,5,7), "1234567890");
            var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest);
            Assert.NotNull(result);
            Assert.Equal(expected, result.IsEligible);
            if (!result.IsEligible)
            {
                var ineligibleResult = Assert.IsType<IneligibleResult>(result);
                Assert.Contains(IneligibilityReason.InsufficientNonHighQuarterWages, ineligibleResult.IneligibilityReasons);
            }
            else
            {
                var eligibleResult = Assert.IsType<EligibleResult>(result);
                Assert.Equal(expectedWeeklyBenefitRate, eligibleResult.WeeklyBenefitRate);
            }
        }

        public static IEnumerable<object[]> DataForTotalBasePeriodEligibilityVerification =>
            new List<object[]>
            {
                new object[] {new Collection<decimal>(){1500, 6500,250,2500}, true, 260m},
                new object[] {new Collection<decimal>(){150, 1500, 100, 100}, false, null},
            };
        [Theory]
        [MemberData(nameof(DataForTotalBasePeriodEligibilityVerification))]
        public async Task ShouldHaveTotalBasePeriodWagesAtLeastBasePeriodWagesFactorTimesWeeklyBenefitRate(Collection<decimal> wagesOfQuarters, bool expected, decimal? expectedWeeklyBenefitRate)
        {
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020,5,7), "1234567890");
            var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest);
            Assert.NotNull(result);
            Assert.Equal(expected, result.IsEligible);

            if (!result.IsEligible)
            {
                var ineligibleResult = Assert.IsType<IneligibleResult>(result);
                Assert.Contains(IneligibilityReason.InsufficientTotalBasePeriodWages, ineligibleResult.IneligibilityReasons);
            }
            else
            {
                var eligibleResult = Assert.IsType<EligibleResult>(result);
                Assert.Equal(expectedWeeklyBenefitRate, eligibleResult.WeeklyBenefitRate);
            }
        }
    }
}
