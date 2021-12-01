namespace DWD.UI.Monetary.Tests.UseCases
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Domain.UseCases;
    using Xunit;

    public class BenefitPeriodUseCaseTests
    {

        [Fact]
        public void CalculateBenefitPeriodFromDate()
        {
            // Get base period from date
            var startDate = new DateTime(2021, 12, 21);
            var basePeriodUseCase = new CalculateBenefitPeriod();
            var result = basePeriodUseCase.CalculateBenefitPeriodFromDate(startDate);

            Assert.NotNull(result);
            Assert.Equal(new DateTime(2021, 12, 19), result.ClaimBeginDate);
            Assert.Equal(52, result.ClaimBeginYearWeek.Week);
            Assert.Equal(2021, result.ClaimBeginYearWeek.Year);

            Assert.Equal(new DateTime(2022, 12, 17), result.ClaimEndDate);
            Assert.Equal(51, result.ClaimEndYearWeek.Week);
            Assert.Equal(2022, result.ClaimEndYearWeek.Year);

             var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault };
            var serialize = JsonSerializer.Serialize(result, options);
            Console.Out.WriteLine(serialize);

        }

    }

}
