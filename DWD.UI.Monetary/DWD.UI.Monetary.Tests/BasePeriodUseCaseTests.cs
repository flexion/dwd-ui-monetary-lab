namespace DWD.UI.Monetary.Tests
{
    using System;
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
        public void CalculateBasePeriodFromYearAndWeek()
        {
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromYearAndWeek(2021, 8);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);
            foreach (var basePeriod in quarters)
            {
                Assert.NotNull(basePeriod);
            }
        }

    }
}
