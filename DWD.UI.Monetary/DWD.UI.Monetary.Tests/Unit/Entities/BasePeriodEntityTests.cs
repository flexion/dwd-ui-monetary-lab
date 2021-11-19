namespace DWD.UI.Monetary.Tests.Unit.Entities
{
    using System;
    using System.Linq;
    using Domain.BusinessEntities;
    using Xunit;

    /// <summary>
    /// Unit tests for the BasePeriod entity.
    /// </summary>
    /// <remarks>Using the naming convention [Functionality to test]_[Input description]_[Expected output description].</remarks>
    public class BasePeriodEntityTests
    {
        /*
         * NOTES:
         *  > Methods and cases to test:
         *     - BasePeriod constructor [public BasePeriod(DateTime initialClaimDate)]
         *        # valid initial claim date
         *        # invalid initial claim date
         *  > Properties to test:
         *     - BasePeriodQuarters     [public IEnumerable<IUIQuarter>]
         *     - AltBasePeriodQuarters  [public IEnumerable<IUIQuarter>]
         *     - FirstQuarter   [public UIQuarter]
         *     - SecondQuarter  [public UIQuarter]
         *     - ThirdQuarter   [public UIQuarter]
         *     - FourthQuarter  [public UIQuarter]
         */

        [Fact]
        public void BasePeriodConstructor_ValidClaimDate_CorrectStandardQuartersForClaimDate()
        {
            var testClaimDate = new DateTime(2021, 10, 15);
            var testBasePeriod = new BasePeriod(testClaimDate);
            Assert.NotNull(testBasePeriod);
            Assert.NotNull(testBasePeriod.BasePeriodQuarters);

            var actualQuarters = testBasePeriod.BasePeriodQuarters.OrderBy(q => q.Year)
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
        public void BasePeriodConstructor_ValidClaimDate_CorrectAlternateQuartersForClaimDate()
        {
            var testClaimDate = new DateTime(2021, 10, 15);
            var testBasePeriod = new BasePeriod(testClaimDate);
            Assert.NotNull(testBasePeriod);
            Assert.NotNull(testBasePeriod.AltBasePeriodQuarters);

            var actualQuarters = testBasePeriod.AltBasePeriodQuarters.OrderBy(q => q.Year)
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
        public void BasePeriodConstructor_InValidClaimDate_ArgumentException()
        {
            var testClaimDate = new DateTime(1776, 7, 4);
            Assert.Throws<ArgumentException>(() => new BasePeriod(testClaimDate));
        }

        [Fact]
        public void BasePeriodFirstQuarterProperty_ValidClaimDate_CorrectStandardFirstQuarterForClaimDate()
        {
            var testClaimDate = new DateTime(2021, 10, 15);
            var testBasePeriod = new BasePeriod(testClaimDate);
            Assert.NotNull(testBasePeriod);
            var expectedFirstQuarter = new UIQuarter(2020, 3);
            Assert.Equal(expectedFirstQuarter, testBasePeriod.FirstQuarter);
        }

        [Fact]
        public void BasePeriodSecondQuarterProperty_ValidClaimDate_CorrectStandardSecondQuarterForClaimDate()
        {
            var testClaimDate = new DateTime(2021, 10, 15);
            var testBasePeriod = new BasePeriod(testClaimDate);
            Assert.NotNull(testBasePeriod);
            var expectedFirstQuarter = new UIQuarter(2020, 4);
            Assert.Equal(expectedFirstQuarter, testBasePeriod.SecondQuarter);
        }

        [Fact]
        public void BasePeriodSecondQuarterProperty_ValidClaimDate_CorrectStandardThirdQuarterForClaimDate()
        {
            var testClaimDate = new DateTime(2021, 10, 15);
            var testBasePeriod = new BasePeriod(testClaimDate);
            Assert.NotNull(testBasePeriod);
            var expectedFirstQuarter = new UIQuarter(2021, 1);
            Assert.Equal(expectedFirstQuarter, testBasePeriod.ThirdQuarter);
        }

        [Fact]
        public void BasePeriodSecondQuarterProperty_ValidClaimDate_CorrectStandardFourthQuarterForClaimDate()
        {
            var testClaimDate = new DateTime(2021, 10, 15);
            var testBasePeriod = new BasePeriod(testClaimDate);
            Assert.NotNull(testBasePeriod);
            var expectedFirstQuarter = new UIQuarter(2021, 2);
            Assert.Equal(expectedFirstQuarter, testBasePeriod.FourthQuarter);
        }
    }
}
