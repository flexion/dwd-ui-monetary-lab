namespace DWD.UI.Monetary.Tests
{
    using System;
    using System.Linq;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Xunit;

    public class AltBasePeriodUseCaseTests
    {

        [Fact]
        public void CalculateAltBasePeriodFromInitialClaimDate()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2020, 11, 22);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate);

            // Check result
            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                                                                .ThenBy(q => q.QuarterNumber)
                                                                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 4);
            testQuarters[1] = new UIQuarter(2020, 1);
            testQuarters[2] = new UIQuarter(2020, 2);
            testQuarters[3] = new UIQuarter(2020, 3);

            Assert.Equal(testQuarters, actualQuarters);

        }

        [Fact]
        public void CalculateAltBasePeriodFromInitialClaimDateFail()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 1, 02);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate);

            // Check result
            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 4);
            testQuarters[1] = new UIQuarter(2020, 1);
            testQuarters[2] = new UIQuarter(2020, 2);
            testQuarters[3] = new UIQuarter(2020, 3);

            Assert.Equal(testQuarters, actualQuarters);

        }

        [Fact]
        public void CalculateAltBasePeriodFromYearAndWeek()
        {
            // Get base period from year and week
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(2020, 48);

            // Check result
            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 4);
            testQuarters[1] = new UIQuarter(2020, 1);
            testQuarters[2] = new UIQuarter(2020, 2);
            testQuarters[3] = new UIQuarter(2020, 3);

            Assert.Equal(testQuarters, actualQuarters);

        }
    }
}
