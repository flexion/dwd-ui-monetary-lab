namespace DWD.UI.Monetary.Tests.Unit.UseCases
{
    using System;
    using System.Linq;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Xunit;

    /// <summary>
    /// Unit tests for the CalculateBasePeriod use case.  Standard base period calculations.
    /// </summary>
    public class StandardBasePeriodUseCaseTests
    {
        [Fact]
        public void CalculateBasePeriodFromInitialClaimDate_ShouldReturnCorrectStandardQuartersForClaimDate_WhenValidClaimDate()
        {
            var testClaimDate = new DateTime(2021, 10, 15);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(testClaimDate);

            Assert.NotNull(result);
            Assert.NotNull(result.BasePeriodQuarters);

            var actualQuarters = result.BasePeriodQuarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 3);
            testQuarters[1] = new UIQuarter(2020, 4);
            testQuarters[2] = new UIQuarter(2021, 1);
            testQuarters[3] = new UIQuarter(2021, 2);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void CalculateBasePeriodFromInitialClaimDate_ShouldReturnPriorStandardBaseQuarters_WhenClaimDatePriorToFirstSundayOfQuarter()
        {
            var priorToFirstSundayOfQuarter = new DateTime(2021, 1, 1);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(priorToFirstSundayOfQuarter);

            Assert.NotNull(result);
            Assert.NotNull(result.BasePeriodQuarters);

            var actualQuarters = result.BasePeriodQuarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 3);
            testQuarters[1] = new UIQuarter(2019, 4);
            testQuarters[2] = new UIQuarter(2020, 1);
            testQuarters[3] = new UIQuarter(2020, 2);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void CalculateBasePeriodFromInitialClaimDate_ShouldReturnCurrentStandardBaseQuarters_WhenClaimDateOnFirstSundayOfQuarter()
        {
            var firstSundayOfQuarter = new DateTime(2021, 1, 3);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(firstSundayOfQuarter);

            Assert.NotNull(result);
            Assert.NotNull(result.BasePeriodQuarters);

            var actualQuarters = result.BasePeriodQuarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 4);
            testQuarters[1] = new UIQuarter(2020, 1);
            testQuarters[2] = new UIQuarter(2020, 2);
            testQuarters[3] = new UIQuarter(2020, 3);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void CalculateBasePeriodFromInitialClaimDate_ShouldReturnCurrentStandardBaseQuarters_WhenClaimDateAfterFirstSundayOfQuarter()
        {
            var afterFirstSundayOfQuarter = new DateTime(2021, 1, 15);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(afterFirstSundayOfQuarter);

            Assert.NotNull(result);
            Assert.NotNull(result.BasePeriodQuarters);

            var actualQuarters = result.BasePeriodQuarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 4);
            testQuarters[1] = new UIQuarter(2020, 1);
            testQuarters[2] = new UIQuarter(2020, 2);
            testQuarters[3] = new UIQuarter(2020, 3);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void CalculateBasePeriodFromInitialClaimDate_ShouldReturnArgumentException_WhenInvalidClaimDate()
        {
            var testClaimDate = new DateTime(1776, 7, 4);
            var basePeriodUseCase = new CalculateBasePeriod();
            Assert.Throws<ArgumentException>(() => basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(testClaimDate));
        }
    }
}
