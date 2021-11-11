namespace DWD.UI.Monetary.Tests
{
    using System;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using Xunit;

    // TODO: Add more tests

    public class BasePeriodEntityTests
    {
        [Fact]
        public void OctoberThirtyFirst2021()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 10, 31);
            var myBasePeriod = new BasePeriod(myClaimDate);

            // Check quarter properties
            Assert.Equal(2020, myBasePeriod.FirstQuarter.Year);
            Assert.Equal(3, myBasePeriod.FirstQuarter.QuarterNumber);

            Assert.Equal(2020, myBasePeriod.SecondQuarter.Year);
            Assert.Equal(4, myBasePeriod.SecondQuarter.QuarterNumber);

            Assert.Equal(2021, myBasePeriod.ThirdQuarter.Year);
            Assert.Equal(1, myBasePeriod.ThirdQuarter.QuarterNumber);

            Assert.Equal(2021, myBasePeriod.FourthQuarter.Year);
            Assert.Equal(2, myBasePeriod.FourthQuarter.QuarterNumber);

            // Check base period is enumerable
            foreach (var quarter in myBasePeriod)
            {
                Assert.NotNull(quarter);
            }

            // Check base period indexer
            for (var i = 0; i < 4; i++)
            {
                Assert.NotNull(myBasePeriod[i]);
            }

            // Check IBasePeriod interface
            var basePeriods = ((IBasePeriod)myBasePeriod).BasePeriodQuarters;
            foreach (var basePeriod in basePeriods)
            {
                Assert.NotNull(basePeriod);
            }
        }

        [Fact]
        public void JanuaryThird2021()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 1, 3);
            var myBasePeriod = new BasePeriod(myClaimDate);

            // Check quarter properties
            Assert.Equal(2019, myBasePeriod.FirstQuarter.Year);
            Assert.Equal(4, myBasePeriod.FirstQuarter.QuarterNumber);

            Assert.Equal(2020, myBasePeriod.SecondQuarter.Year);
            Assert.Equal(1, myBasePeriod.SecondQuarter.QuarterNumber);

            Assert.Equal(2020, myBasePeriod.ThirdQuarter.Year);
            Assert.Equal(2, myBasePeriod.ThirdQuarter.QuarterNumber);

            Assert.Equal(2020, myBasePeriod.FourthQuarter.Year);
            Assert.Equal(3, myBasePeriod.FourthQuarter.QuarterNumber);

            // Check base period is enumerable
            foreach (var quarter in myBasePeriod)
            {
                Assert.NotNull(quarter);
            }

            // Check base period indexer
            for (var i = 0; i < 4; i++)
            {
                Assert.NotNull(myBasePeriod[i]);
            }
        }

        [Fact]
        public void DecemberSix2020()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2020, 12, 6);
            var myBasePeriod = new BasePeriod(myClaimDate);

            // Check quarter properties
            Assert.Equal(2019, myBasePeriod.FirstQuarter.Year);
            Assert.Equal(3, myBasePeriod.FirstQuarter.QuarterNumber);

            Assert.Equal(2019, myBasePeriod.SecondQuarter.Year);
            Assert.Equal(4, myBasePeriod.SecondQuarter.QuarterNumber);

            Assert.Equal(2020, myBasePeriod.ThirdQuarter.Year);
            Assert.Equal(1, myBasePeriod.ThirdQuarter.QuarterNumber);

            Assert.Equal(2020, myBasePeriod.FourthQuarter.Year);
            Assert.Equal(2, myBasePeriod.FourthQuarter.QuarterNumber);

            // Check base period is enumerable
            foreach (var quarter in myBasePeriod)
            {
                Assert.NotNull(quarter);
            }

            // Check base period indexer
            for (var i = 0; i < 4; i++)
            {
                Assert.NotNull(myBasePeriod[i]);
            }
        }

        [Fact]
        public void FutureClaimDate()
        {
            var myClaimDate = new DateTime(2999, 12, 1);
            Assert.Throws<ArgumentException>(() => new BasePeriod(myClaimDate));
        }

        [Fact]
        public void MinValidClaimDate()
        {
            var myClaimDate = new DateTime(1899, 1, 1);
            Assert.Throws<ArgumentException>(() => new BasePeriod(myClaimDate));
        }

        [Fact]
        public void CheckLeapYear()
        {
            var myClaimDate = new DateTime(2020, 2, 29);
            var myBasePeriod = new BasePeriod(myClaimDate);

            Assert.Equal(2018, myBasePeriod.FirstQuarter.Year);
            Assert.Equal(4, myBasePeriod.FirstQuarter.QuarterNumber);

            Assert.Equal(2019, myBasePeriod.SecondQuarter.Year);
            Assert.Equal(1, myBasePeriod.SecondQuarter.QuarterNumber);

            Assert.Equal(2019, myBasePeriod.ThirdQuarter.Year);
            Assert.Equal(2, myBasePeriod.ThirdQuarter.QuarterNumber);

            Assert.Equal(2019, myBasePeriod.FourthQuarter.Year);
            Assert.Equal(3, myBasePeriod.FourthQuarter.QuarterNumber);
        }
    }
}
