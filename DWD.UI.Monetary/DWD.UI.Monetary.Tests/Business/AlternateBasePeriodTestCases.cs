namespace DWD.UI.Monetary.Tests.Business
{
    using System;
    using System.Linq;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Xunit;

    /// <summary>
    /// These are alternate base period business test cases provided by the SME as part of the user story.
    /// </summary>
    /// <remarks>
    /// Using the naming convention:
    ///     BusinessTestCase_ShouldReturn[output description]_When[input description].
    /// </remarks>
    public class AlternateBasePeriodTestCases
    {
        [Fact]
        public void BusinessTestCase_ShouldReturnCorrectAltBaseQuarters_WhenValidClaimDate()
        {
            var validClaimDate = new DateTime(2021, 10, 31);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(validClaimDate);

            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 4);
            testQuarters[1] = new UIQuarter(2021, 1);
            testQuarters[2] = new UIQuarter(2021, 2);
            testQuarters[3] = new UIQuarter(2021, 3);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void BusinessTestCase_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateOnFirstSundayOfQuarter()
        {
            var firstSundayOfQuarter = new DateTime(2021, 1, 3);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(firstSundayOfQuarter);

            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 1);
            testQuarters[1] = new UIQuarter(2020, 2);
            testQuarters[2] = new UIQuarter(2020, 3);
            testQuarters[3] = new UIQuarter(2020, 4);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void BusinessTestCase_ShouldReturnThirdQuarterAltBaseQuarters_WhenClaimDateInFourthQuarterOfLeapYear()
        {
            // 2020 was a leap year
            var claimDateInFourthQuarterOfLeapYear = new DateTime(2020, 12, 6);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(claimDateInFourthQuarterOfLeapYear);

            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 4);
            testQuarters[1] = new UIQuarter(2020, 1);
            testQuarters[2] = new UIQuarter(2020, 2);
            testQuarters[3] = new UIQuarter(2020, 3);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void BusinessTestCase_ShouldReturnPriorQuarterAltBaseQuarters_WhenClaimDatePriorToFirstSundayOfQuarter()
        {
            // 04/01/2021 is technically part of calendar Q2 but in UI it is still considered part of Q1.
            var priorToFirstSundayOfQuarter = new DateTime(2021, 4, 1);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(priorToFirstSundayOfQuarter);

            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 1);
            testQuarters[1] = new UIQuarter(2020, 2);
            testQuarters[2] = new UIQuarter(2020, 3);
            testQuarters[3] = new UIQuarter(2020, 4);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void BusinessTestCase_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateAfterFirstSundayOfQuarter()
        {
            var afterFirstSundayOfQuarter = new DateTime(2021, 4, 6);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(afterFirstSundayOfQuarter);

            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 2);
            testQuarters[1] = new UIQuarter(2020, 3);
            testQuarters[2] = new UIQuarter(2020, 4);
            testQuarters[3] = new UIQuarter(2021, 1);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void BusinessTestCase_ShouldReturnPriorQuarterAltBaseQuarters_WhenClaimDatePriorToFirstSundayOfYear()
        {
            var priorToFirstSundayOfYear = new DateTime(2022, 1, 1);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(priorToFirstSundayOfYear);

            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 4);
            testQuarters[1] = new UIQuarter(2021, 1);
            testQuarters[2] = new UIQuarter(2021, 2);
            testQuarters[3] = new UIQuarter(2021, 3);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void BusinessTestCase_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateOnFirstSundayOfQuarterAndHoliday()
        {
            // 7/4/2021 is the first Sunday of the quarter and a holiday and we still take claims.
            var firstSundayOfQuarterAndHoliday = new DateTime(2021, 7, 4);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(firstSundayOfQuarterAndHoliday);

            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 3);
            testQuarters[1] = new UIQuarter(2020, 4);
            testQuarters[2] = new UIQuarter(2021, 1);
            testQuarters[3] = new UIQuarter(2021, 2);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void BusinessTestCase_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateOnHoliday()
        {
            // 11/25/2021 is a holiday and we still take claims.
            var holiday = new DateTime(2021, 11, 25);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(holiday);

            Assert.NotNull(result);
            var quarters = result.AltBasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 4);
            testQuarters[1] = new UIQuarter(2021, 1);
            testQuarters[2] = new UIQuarter(2021, 2);
            testQuarters[3] = new UIQuarter(2021, 3);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void BusinessTestCase_ShouldReturnArgumentException_WhenInvalidClaimDate()
        {
            var invalidClaimDate = new DateTime(1887, 7, 4);
            var basePeriodUseCase = new CalculateBasePeriod();

            Assert.Throws<ArgumentException>(() =>
                basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(invalidClaimDate));
        }
    }
}
