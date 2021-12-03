namespace DWD.UI.Monetary.Tests.UseCases
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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
        /// <param name="expectedWeeks">Expected weeks in the period</param>
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void CalculateBenefitPeriodFromDate(DateTime requestedStartDate,
                                                    DateTime expectedBeginDate,
                                                    DateTime expectedEndDate,
                                                    YearWeek expectedBeginYearWeek,
                                                    YearWeek expectedEndYearWeek,
                                                    int expectedWeeks)
        {
            var basePeriodUseCase = new CalculateBenefitYear();

            var result = basePeriodUseCase.CalculateBenefitYearFromDate(requestedStartDate);

            Assert.NotNull(result);
            Assert.Equal(expectedBeginDate, result.BeginDate);
            Assert.Equal(expectedBeginYearWeek, result.BeginYearWeek);
            Assert.Equal(expectedEndDate, result.EndDate);
            Assert.Equal(expectedEndYearWeek, result.EndYearWeek);
            Assert.Equal(expectedWeeks, result.BenefitWeeks);
            Assert.Equal(requestedStartDate, result.ClaimRequestedDate);
        }
    }

    /// <summary>
    /// PO acceptance criteria dates.
    /// </summary>
    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> data = new()
        {
            new object[] {  new DateTime(2021, 11, 9),
                            new DateTime(2021, 11, 7),
                            new DateTime(2022, 11, 5),
                            new YearWeek(2021, 46),
                            new YearWeek(2022, 45),
                            52},

            new object[] {  new DateTime(2021, 7, 1),
                            new DateTime(2021, 6, 27),
                            new DateTime(2022, 6, 25),
                            new YearWeek(2021, 27),
                            new YearWeek(2022, 26),
                            52},

            new object[] {  new DateTime(2021, 12, 19),
                            new DateTime(2021, 12, 19),
                            new DateTime(2022, 12, 17),
                            new YearWeek(2021, 52),
                            new YearWeek(2022, 51),
                            52},

            new object[] {  new DateTime(2021, 1, 2),
                            new DateTime(2020, 12, 27),
                            new DateTime(2021, 12, 25),
                            new YearWeek(2021, 1),
                            new YearWeek(2021, 52),
                            52},

            new object[] {  new DateTime(2021, 1, 4),
                            new DateTime(2021, 1, 3),
                            new DateTime(2022, 1, 1),
                            new YearWeek(2021, 2),
                            new YearWeek(2022, 1),
                            52},

            new object[] {  new DateTime(2020, 12, 28),
                            new DateTime(2020, 12, 27),
                            new DateTime(2021, 12, 25),
                            new YearWeek(2021, 1),
                            new YearWeek(2021, 52),
                            52},
        };

        public IEnumerator<object[]> GetEnumerator() => this.data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

}
