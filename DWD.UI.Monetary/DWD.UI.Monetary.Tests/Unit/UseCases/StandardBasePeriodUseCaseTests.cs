namespace DWD.UI.Monetary.Tests.Unit.UseCases
{
    using System;
    using Domain.UseCases;
    using Xunit;

    /// <summary>
    /// Unit tests for the CalculateBasePeriod use case.  Standard base period calculations.
    /// </summary>
    public class StandardBasePeriodUseCaseTests
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

    }
}
