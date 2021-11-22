namespace DWD.UI.Monetary.Tests.Unit.Entities
{
    using System;
    using System.Linq;
    using Domain.BusinessEntities;
    using Xunit;

    /// <summary>
    /// Unit tests for the BasePeriod entity.
    /// </summary>
    /// <remarks>
    /// Using the naming convention:
    ///     [Class][functionality to test]_ShouldReturn[output description]_When[input description].
    /// </remarks>
    public class BasePeriodEntityTests
    {
        [Fact]
        public void BasePeriodConstructor_ShouldReturnCorrectStandardQuartersForClaimDate_WhenValidClaimDate()
        {
            var testClaimDate = new DateTime(2021, 10, 15);
            var testBasePeriod = new BasePeriod(testClaimDate);
            Assert.NotNull(testBasePeriod);
            Assert.NotNull(testBasePeriod.BasePeriodQuarters);

            var actualQuarters = testBasePeriod.BasePeriodQuarters
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
        public void BasePeriodConstructor_ShouldReturnPriorStandardBaseQuarters_WhenClaimDatePriorToFirstSundayOfQuarter()
        {
            var priorToFirstSundayOfQuarter = new DateTime(2021, 1, 1);
            var testBasePeriod = new BasePeriod(priorToFirstSundayOfQuarter);
            Assert.NotNull(testBasePeriod);
            Assert.NotNull(testBasePeriod.BasePeriodQuarters);

            var actualQuarters = testBasePeriod.BasePeriodQuarters
                .OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();

            var testQuarters = new UIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 3);
            testQuarters[1] = new UIQuarter(2019, 4);
            testQuarters[2] = new UIQuarter(2020, 1);
            testQuarters[3] = new UIQuarter(2020, 2);

            Assert.Equal(testQuarters[0], actualQuarters[0]);
            Assert.Equal(testQuarters[1], actualQuarters[1]);
            Assert.Equal(testQuarters[2], actualQuarters[2]);
            Assert.Equal(testQuarters[3], actualQuarters[3]);
        }

        [Fact]
        public void BasePeriodConstructor_ShouldReturnCurrentStandardBaseQuarters_WhenClaimDateOnFirstSundayOfQuarter()
        {
            var firstSundayOfQuarter = new DateTime(2021, 1, 3);
            var testBasePeriod = new BasePeriod(firstSundayOfQuarter);
            Assert.NotNull(testBasePeriod);
            Assert.NotNull(testBasePeriod.BasePeriodQuarters);

            var actualQuarters = testBasePeriod.BasePeriodQuarters
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
        public void BasePeriodConstructor_ShouldReturnCurrentStandardBaseQuarters_WhenClaimDateAfterFirstSundayOfQuarter()
        {
            var afterFirstSundayOfQuarter = new DateTime(2021, 1, 15);
            var testBasePeriod = new BasePeriod(afterFirstSundayOfQuarter);
            Assert.NotNull(testBasePeriod);
            Assert.NotNull(testBasePeriod.BasePeriodQuarters);

            var actualQuarters = testBasePeriod.BasePeriodQuarters
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
        public void BasePeriodConstructor_ShouldReturnCorrectAlternateQuartersForClaimDate_WhenValidClaimDate()
        {
            var testClaimDate = new DateTime(2021, 10, 15);
            var testBasePeriod = new BasePeriod(testClaimDate);
            Assert.NotNull(testBasePeriod);
            Assert.NotNull(testBasePeriod.AltBasePeriodQuarters);

            var actualQuarters = testBasePeriod.AltBasePeriodQuarters
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
        public void BasePeriodConstructor_ShouldReturnPriorAltBaseQuarters_WhenClaimDatePriorToFirstSundayOfQuarter()
        {
            var priorToFirstSundayOfQuarter = new DateTime(2021, 1, 1);
            var testBasePeriod = new BasePeriod(priorToFirstSundayOfQuarter);
            Assert.NotNull(testBasePeriod);
            Assert.NotNull(testBasePeriod.AltBasePeriodQuarters);

            var actualQuarters = testBasePeriod.AltBasePeriodQuarters
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
        public void BasePeriodConstructor_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateOnFirstSundayOfQuarter()
        {
            var firstSundayOfQuarter = new DateTime(2021, 1, 3);
            var testBasePeriod = new BasePeriod(firstSundayOfQuarter);
            Assert.NotNull(testBasePeriod);
            Assert.NotNull(testBasePeriod.AltBasePeriodQuarters);

            var actualQuarters = testBasePeriod.AltBasePeriodQuarters
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
        public void BasePeriodConstructor_ShouldReturnCurrentAltBaseQuarters_WhenClaimDateAfterFirstSundayOfQuarter()
        {
            var afterFirstSundayOfQuarter = new DateTime(2021, 1, 15);
            var testBasePeriod = new BasePeriod(afterFirstSundayOfQuarter);
            Assert.NotNull(testBasePeriod);
            Assert.NotNull(testBasePeriod.AltBasePeriodQuarters);

            var actualQuarters = testBasePeriod.AltBasePeriodQuarters
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
        public void BasePeriodConstructor_ShouldReturnArgumentException_WhenInvalidClaimDate()
        {
            var testClaimDate = new DateTime(1776, 7, 4);
            Assert.Throws<ArgumentException>(() => new BasePeriod(testClaimDate));
        }
    }
}
