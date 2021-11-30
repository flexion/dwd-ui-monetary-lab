#pragma warning disable IDE0055
#pragma warning disable CS3016

namespace DWD.UI.Monetary.Tests.Business
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Xunit;

    public class CheckEligibilityOfMonetaryRequirementsTests
    {
        private readonly ICheckEligibilityOfMonetaryRequirements objectUnderTest = new CheckEligibilityOfMonetaryRequirements();

        public static IEnumerable<object[]> DataForAtLeastTwoQuartersEligibilityVerification =>
         new List<object[]>
        {
            new object[] {new Collection<decimal>(){1500, 6500, 250, 2500}, true},
            new object[] {new Collection<decimal>(){0, 0, 0, 2500}, false},
            new object[] {new Collection<decimal>(){0, 6500, 0, 2600}, true},
        };

        [Theory]
        [MemberData(nameof(DataForAtLeastTwoQuartersEligibilityVerification))]
        public void ShouldHaveAtLeastTwoQuarters(Collection<decimal> wagesOfQuarters, bool expected)
        {
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020,5,7), "1234567890");
            var eligibilityBasis = new EligibilityBasis(54, 4, 2, 4, 35, 26, 40);
            var result = this.objectUnderTest.Verify(eligibilityVerificationRequest, eligibilityBasis);
            Assert.NotNull(result);
            Assert.Equal(expected, result.IsEligible);
            if (!result.IsEligible)
            {
                Assert.Contains(IneligibilityReason.InsufficientQuartersWithWages, result.IneligibilityReasons);
            }
            else
            {
                Assert.Empty(result.IneligibilityReasons);
            }
        }

        [Fact]
        public void ShouldThrowInvalidOperationExceptionWhenQuarterWagesIsEmpty(){
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(new Collection<decimal>(), new DateTime(2020,5,7), "1234567890");
            var eligibilityBasis = new EligibilityBasis(1350, 4, 2, 4, 35, 26, 40);
            Assert.Throws<InvalidOperationException>(() => this.objectUnderTest.Verify(eligibilityVerificationRequest, eligibilityBasis));
        }

        public static IEnumerable<object[]> DataForHighQuarterEligibilityVerification =>
            new List<object[]>
            {
                new object[] {new Collection<decimal>(){1500, 6500,250,2500}, true},
                new object[] {new Collection<decimal>(){450, 1300, 100, 1000}, false},
            };

        [Theory]
        [MemberData(nameof(DataForHighQuarterEligibilityVerification))]
        public void ShouldHaveWagesOfHighQuarterAtLeastMinHighQuarterEarnings(Collection<decimal> wagesOfQuarters, bool expected)
        {
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020,5,7), "1234567890");
            var eligibilityBasis = new EligibilityBasis(1350, 4, 2, 4, 35, 26, 40);
            var result = this.objectUnderTest.Verify(eligibilityVerificationRequest, eligibilityBasis);
            Assert.NotNull(result);
            Assert.Equal(expected, result.IsEligible);
            if (!result.IsEligible)
            {
                Assert.Contains(IneligibilityReason.InsufficientHighQuarterWage, result.IneligibilityReasons);
            }
            else
            {
                Assert.Empty(result.IneligibilityReasons);
            }
        }

        public static IEnumerable<object[]> DataForPercentHighQuarterEligibilityVerification =>
            new List<object[]>
            {
                new object[] {new Collection<decimal>(){1500, 6500, 250, 2500}, true, 260m},
            };
        [Theory]
        [MemberData(nameof(DataForPercentHighQuarterEligibilityVerification))]
        public void ShouldCalculateWeeklyBenifitRateCorrectly(Collection<decimal> wagesOfQuarters, bool expected, decimal? expectedWeeklyBenefitRate)
        {
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020,5,7), "1234567890");
            var eligibilityBasis = new EligibilityBasis(1350, 4, 2, 4, 35, 26, 40);
            var result = this.objectUnderTest.Verify(eligibilityVerificationRequest, eligibilityBasis);
            Assert.NotNull(result);
            Assert.Equal(expected, result.IsEligible);
            Assert.Equal(expectedWeeklyBenefitRate, result.WeeklyBenefitRate);
        }

        public static IEnumerable<object[]> DataForWagesOutsideHighQuarterEligibilityVerification =>
            new List<object[]>
            {
                new object[] {new Collection<decimal>(){1500, 6500,250,2500}, true, 260m},
                new object[] {new Collection<decimal>(){150, 2500, 100, 100}, false, null},
            };
        [Theory]
        [MemberData(nameof(DataForWagesOutsideHighQuarterEligibilityVerification))]
        public void ShouldHaveWagesOutsideHighQuarter(Collection<decimal> wagesOfQuarters, bool expected, decimal? expectedWeeklyBenefitRate)
        {
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020,5,7), "1234567890");
            var eligibilityBasis = new EligibilityBasis(1350, 4, 2, 4, 35, 26, 40);
            var result = this.objectUnderTest.Verify(eligibilityVerificationRequest, eligibilityBasis);
            Assert.NotNull(result);
            Assert.Equal(expected, result.IsEligible);
            Assert.Equal(expectedWeeklyBenefitRate, result.WeeklyBenefitRate);
            if (!result.IsEligible)
            {
                Assert.Contains(IneligibilityReason.InsufficientNonHighQuarterWages, result.IneligibilityReasons);
            }
            else
            {
                Assert.Empty(result.IneligibilityReasons);
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
        public void ShouldHaveTotalBasePeriodWagesAtLeastBasePeriodWagesFactorTimesWeeklyBenefitRate(Collection<decimal> wagesOfQuarters, bool expected, decimal? expectedWeeklyBenefitRate)
        {
            var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020,5,7), "1234567890");
            var eligibilityBasis = new EligibilityBasis(1350, 4, 2, 4, 35, 26, 40);
            var result = this.objectUnderTest.Verify(eligibilityVerificationRequest, eligibilityBasis);
            Assert.NotNull(result);
            Assert.Equal(expected, result.IsEligible);
            Assert.Equal(expectedWeeklyBenefitRate, result.WeeklyBenefitRate);
            if (!result.IsEligible)
            {
                Assert.Contains(IneligibilityReason.InsufficientTotalBasePeriodWages, result.IneligibilityReasons);
            }
            else
            {
                Assert.Empty(result.IneligibilityReasons);
            }
        }
    }
}
