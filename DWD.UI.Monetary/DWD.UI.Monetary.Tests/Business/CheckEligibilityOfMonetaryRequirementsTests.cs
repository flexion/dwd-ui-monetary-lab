namespace DWD.UI.Monetary.Tests.Business;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Domain.BusinessEntities;
using Domain.Interfaces;
using Domain.UseCases;
using Moq;
using Xunit;

public class CheckEligibilityOfMonetaryRequirementsTests
{
    private readonly ICheckEligibilityOfMonetaryRequirements objectUnderTest;

    public CheckEligibilityOfMonetaryRequirementsTests()
    {
        var mockEligibilityBasisGateway = new Mock<IEligibilityBasisGateway>();
        _ = mockEligibilityBasisGateway.Setup(m => m.GetEligibilityBasisAsync())
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
    [CLSCompliant(false)]
    public async Task ShouldHaveAtLeastTwoQuarters(Collection<decimal> wagesOfQuarters, bool expectedIsEligible)
    {
        var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020, 5, 7), "1234567890");
        var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest).ConfigureAwait(true);
        Assert.NotNull(result);
        Assert.Equal(expectedIsEligible, result.IsEligible);
        if (expectedIsEligible)
        {
            _ = Assert.IsType<EligibleResult>(result);
        }
        else
        {
            var ineligibilityResult = Assert.IsType<IneligibleResult>(result);
            Assert.Contains(IneligibilityReason.InsufficientQuartersWithWages, ineligibilityResult.IneligibilityReasons);
        }
    }

    [Fact]
    public void ShouldThrowInvalidOperationExceptionWhenQuarterWagesIsEmpty()
    {
        var eligibilityVerificationRequest = new EligibilityVerificationRequest(new Collection<decimal>(), new DateTime(2020, 5, 7), "1234567890");
        _ = Assert.ThrowsAsync<InvalidOperationException>(() => this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest));
    }

    public static IEnumerable<object[]> DataForHighQuarterEligibilityVerification =>
        new List<object[]>
        {
            new object[] {new Collection<decimal>(){1500, 6500,250,2500}, true},
            new object[] {new Collection<decimal>(){450, 1300, 100, 1000}, false},
        };

    [Theory]
    [MemberData(nameof(DataForHighQuarterEligibilityVerification))]
    [CLSCompliant(false)]
    public async Task ShouldHaveWagesOfHighQuarterAtLeastMinHighQuarterEarnings(Collection<decimal> wagesOfQuarters, bool expectedIsEligible)
    {
        var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020, 5, 7), "1234567890");
        var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest).ConfigureAwait(true);
        Assert.NotNull(result);
        Assert.Equal(expectedIsEligible, result.IsEligible);
        if (expectedIsEligible)
        {
            _ = Assert.IsType<EligibleResult>(result);
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
    [CLSCompliant(false)]
    public async Task ShouldCalculateWeeklyBenefitRateCorrectly(Collection<decimal> wagesOfQuarters, bool expectedIsEligible, decimal? expectedWeeklyBenefitRate)
    {
        var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020, 5, 7), "1234567890");
        var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest).ConfigureAwait(true);
        Assert.NotNull(result);
        Assert.Equal(expectedIsEligible, result.IsEligible);
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
    [CLSCompliant(false)]
    public async Task ShouldHaveWagesOutsideHighQuarter(Collection<decimal> wagesOfQuarters, bool expectedIsEligible, decimal? expectedWeeklyBenefitRate)
    {
        var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020, 5, 7), "1234567890");
        var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest).ConfigureAwait(true);
        Assert.NotNull(result);
        Assert.Equal(expectedIsEligible, result.IsEligible);
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
    [CLSCompliant(false)]
    public async Task ShouldHaveTotalBasePeriodWagesAtLeastBasePeriodWagesFactorTimesWeeklyBenefitRate(Collection<decimal> wagesOfQuarters, bool expectedIsEligible, decimal? expectedWeeklyBenefitRate)
    {
        var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020, 5, 7), "1234567890");
        var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest).ConfigureAwait(true);
        Assert.NotNull(result);
        Assert.Equal(expectedIsEligible, result.IsEligible);

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

    public static IEnumerable<object[]> DataForMBACalculation =>
        new List<object[]>
        {
            new object[] {new Collection<decimal>(){1500, 6500,  250, 2500}, 4300M },  // Case 1: 40% of total base period wages is the lesser amount
            new object[] {new Collection<decimal>(){1500, 2500, 2500, 2500}, 2600M},   // Case 2: 26 times WBR is the lesser amount
        };

    /// <summary>
    /// Tests the selection of the lesser of X times WBR or Y% of total base period wages. Test cases are based on story examples where X=26 and Y=40.
    /// </summary>
    /// <param name="wagesOfQuarters">4 quarters of wages</param>
    /// <param name="expectedMBA">Expected Max Benefit Amount</param>
    [Theory]
    [MemberData(nameof(DataForMBACalculation))]
    [CLSCompliant(false)]
    public async Task ShouldCalculateMBA(Collection<decimal> wagesOfQuarters, decimal expectedMBA)
    {
        // Arrange
        var eligibilityVerificationRequest = new EligibilityVerificationRequest(wagesOfQuarters, new DateTime(2020, 5, 7), "1234567890");

        // Act
        var result = await this.objectUnderTest.VerifyAsync(eligibilityVerificationRequest).ConfigureAwait(true);

        // Assert
        Assert.NotNull(result);
        var eligibleResult = Assert.IsType<EligibleResult>(result);
        Assert.Equal(expectedMBA, eligibleResult.MaximumBenefitAmount);
    }
}