namespace DWD.UI.Monetary.Tests.UseCases
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Xunit;

    public class BenefitPeriodUseCaseTests
    {

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void CalculateBenefitPeriodFromDate(DateTime requestedStartDate,
                                                    DateTime expectedBeginDate,
                                                    DateTime expectedEndDate,
                                                    YearWeek expectedBeginYearWeek,
                                                    YearWeek expectedEndYearWeek)
        {
            var basePeriodUseCase = new CalculateBenefitPeriod(new CalculateBasePeriod());

            var result = basePeriodUseCase.CalculateBenefitPeriodFromDate(requestedStartDate, null, null, null);

            Assert.NotNull(result);
            Assert.Equal(expectedBeginDate, result.BeginDate);
            Assert.Equal(expectedBeginYearWeek, result.BeginYearWeek);
            Assert.Equal(expectedEndDate, result.EndDate);
            Assert.Equal(expectedEndYearWeek, result.EndYearWeek);

            var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault };
            var serialize = JsonSerializer.Serialize(result, options);
            Console.Out.WriteLine(serialize);

        }

    }

    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> data = new()
        {
            new object[] {  new DateTime(2021, 12, 25),
                            new DateTime(2021, 12, 19),
                            new DateTime(2022, 12, 17),
                            new YearWeek(2021, 52),
                            new YearWeek(2022, 51)},

            new object[] {  new DateTime(2021, 12, 25),
                            new DateTime(2021, 12, 19),
                            new DateTime(2022, 12, 17),
                            new YearWeek(2021, 52),
                            new YearWeek(2022, 51)}
        };

        public IEnumerator<object[]> GetEnumerator() => this.data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

}
