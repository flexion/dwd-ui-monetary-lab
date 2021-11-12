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
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, false);

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
