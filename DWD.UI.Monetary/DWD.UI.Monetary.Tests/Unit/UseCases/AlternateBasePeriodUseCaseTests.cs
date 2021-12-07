namespace DWD.UI.Monetary.Tests.Unit.UseCases
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Xunit;

    /// <summary>
    /// Unit tests for the CalculateBasePeriod use case.  Alternate base period calculations.
    /// </summary>
    public class AlternateBasePeriodUseCaseTests
    {
        [Theory]
        [ClassData(typeof(AlternateBasePeriodUseCaseTestData))]
        public void TestAlternateBasePeriodUseCase_ClassData(string testDescription, DateTime initialClaimDate, Collection<Quarter> expectedQuarters)
        {
            // Arrange
            var basePeriodUseCase = new CalculateBasePeriod();

            // Act
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(initialClaimDate);

            // Assert
            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var expectedUIQuarters = expectedQuarters
                .Select(x => new UIQuarter(x.Year, x.QuarterNumber))
                .ToArray();

            Assert.True(expectedUIQuarters[0].Equals(actualQuarters[0]), testDescription);
            Assert.True(expectedUIQuarters[1].Equals(actualQuarters[1]), testDescription);
            Assert.True(expectedUIQuarters[2].Equals(actualQuarters[2]), testDescription);
            Assert.True(expectedUIQuarters[3].Equals(actualQuarters[3]), testDescription);
        }

        [Fact]
        public void CalculateBasePeriodFromInitialClaimDate_ShouldReturnArgumentException_WhenInvalidClaimDate()
        {
            var testClaimDate = new DateTime(1776, 7, 4);
            var basePeriodUseCase = new CalculateBasePeriod();
            Assert.Throws<ArgumentException>(() => basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(testClaimDate));
        }
    }

    public class AlternateBasePeriodUseCaseTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                "CalculateBasePeriodFromInitialClaimDate_ShouldReturnCorrectAlternateQuartersForClaimDate_WhenValidClaimDate",
                new DateTime(2021, 10, 15),
                new Collection<Quarter>()
                {
                    new() {Year = 2020, QuarterNumber = 4}, new() {Year = 2021, QuarterNumber = 1}, new() {Year=2021, QuarterNumber = 2}, new() {Year=2021, QuarterNumber = 3}
                }
            };
            yield return new object[]
            {
                "CalculateBasePeriodFromInitialClaimDate_ShouldReturnPriorAltBaseQuarters_WhenClaimDatePriorToFirstSundayOfQuarter",
                new DateTime(2021, 1, 1),
                new Collection<Quarter>()
                {
                    new() {Year = 2019, QuarterNumber = 4}, new() {Year = 2020, QuarterNumber = 1}, new(){Year=2020, QuarterNumber = 2}, new(){Year=2020, QuarterNumber = 3}
                }
            };
            yield return new object[]
            {
                "CalculateBasePeriodFromInitialClaimDate_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateOnFirstSundayOfQuarter",
                new DateTime(2021, 1, 3),
                new Collection<Quarter>()
                {
                    new() {Year = 2020, QuarterNumber = 1}, new() {Year = 2020, QuarterNumber = 2}, new(){Year=2020, QuarterNumber = 3}, new(){Year=2020, QuarterNumber = 4}
                }
            };
            yield return new object[]
            {
                "CalculateBasePeriodFromInitialClaimDate_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateAfterFirstSundayOfQuarter",
                new DateTime(2021, 1, 15),
                new Collection<Quarter>()
                {
                    new() {Year = 2020, QuarterNumber = 1}, new() {Year = 2020, QuarterNumber = 2}, new(){Year=2020, QuarterNumber = 3}, new(){Year=2020, QuarterNumber = 4}
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
