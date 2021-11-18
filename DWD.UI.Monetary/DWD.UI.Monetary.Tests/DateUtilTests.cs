namespace DWD.UI.Monetary.Tests
{
    using System;
    using DWD.UI.Monetary.Domain.Utilities;
    using Xunit;
    public class DateUtilTests
    {
        [Fact]
        public void TestQuarterNumberForFirstQuarterDate()
        {
            var date = new DateTime(2020, 2, 6);
            Assert.Equal(1, DateUtil.CalendarQuarterNumber(date));
        }

        [Fact]
        public void TestQuarterNumberForSecondQuarterDate()
        {
            var date = new DateTime(2020, 4, 16);
            Assert.Equal(2, DateUtil.CalendarQuarterNumber(date));
        }

        [Fact]
        public void TestQuarterNumberForThirdQuarterDate()
        {
            var date = new DateTime(2020, 8, 26);
            Assert.Equal(3, DateUtil.CalendarQuarterNumber(date));
        }

        [Fact]
        public void TestQuarterNumberForFourthQuarterDate()
        {
            var date = new DateTime(2020, 10, 6);
            Assert.Equal(4, DateUtil.CalendarQuarterNumber(date));
        }

        [Fact]
        public void TestFirstDayOfCalendarQuarter1()
        {
            var actual = DateUtil.FirstDayOfCalendarQuarter(2020, 1);
            var expected = new DateTime(2020, 1, 1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFirstDayOfCalendarQuarter2()
        {
            var actual = DateUtil.FirstDayOfCalendarQuarter(2020, 2);
            var expected = new DateTime(2020, 4, 1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFirstDayOfCalendarQuarter3()
        {
            var actual = DateUtil.FirstDayOfCalendarQuarter(2020, 3);
            var expected = new DateTime(2020, 7, 1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFirstDayOfCalendarQuarter4()
        {
            var actual = DateUtil.FirstDayOfCalendarQuarter(2020, 4);
            var expected = new DateTime(2020, 10, 1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFirstDayOfCalendarQuarterThrowsArgumentOutOfRangeException() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => DateUtil.FirstDayOfCalendarQuarter(2020, 5));
    }
}
