namespace DWD.UI.Monetary.Tests
{
    using System;
    using System.Linq;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Xunit;

    public class BasePeriodUseCaseTests
    {

        [Fact]
        public void CalculateBasePeriodFromInitialClaimDate()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 10, 31);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);
            foreach (var basePeriod in quarters)
            {
                Assert.NotNull(basePeriod);
            }
        }

        [Fact]
        public void CalculateBasePeriodFromInitialClaimDateFail()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 1, 02);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate);
            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);
            var actualQuarters = quarters.OrderBy(q => q.Year)
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

    }
}
