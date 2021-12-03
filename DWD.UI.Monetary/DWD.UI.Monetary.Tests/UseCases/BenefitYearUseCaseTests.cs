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
    /// <summary>
    /// Created tests for calculating the benefit year.
    /// </summary>
    public class BenefitYearUseCaseTests
    {
        /// <summary>
        /// Base on the criteria matrix for user story 33.
        /// </summary>
        /// <param name="requestedStartDate">The request start day</param>
        /// <param name="expectedBeginDate">Expected benefit begin date (Sunday)</param>
        /// <param name="expectedEndDate">Expected benefit begin date (Saturday)</param>
        /// <param name="expectedBeginYearWeek">Expected benefit begin week</param>
        /// <param name="expectedEndYearWeek">Expected benefit end week</param>
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void CalculateBenefitPeriodFromDate(DateTime requestedStartDate,
                                                    DateTime expectedBeginDate,
                                                    DateTime expectedEndDate,
                                                    YearWeek expectedBeginYearWeek,
                                                    YearWeek expectedEndYearWeek)
        {
            var basePeriodUseCase = new CalculateBenefitYear(new CalculateBasePeriod());

            var result = basePeriodUseCase.CalculateBenefitYearFromDate(requestedStartDate);

            Assert.NotNull(result);
            Assert.Equal(expectedBeginDate, result.BeginDate);
            Assert.Equal(expectedBeginYearWeek, result.BeginYearWeek);
            Assert.Equal(expectedEndDate, result.EndDate);
            Assert.Equal(expectedEndYearWeek, result.EndYearWeek);
        }
    }

    /// <summary>
    /// PO acceptance criteria dates.
    /// </summary>
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
