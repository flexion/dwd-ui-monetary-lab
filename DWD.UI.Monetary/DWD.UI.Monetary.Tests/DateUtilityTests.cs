namespace DWD.UI.Monetary.Tests
{
    using System;
    using System.Collections.Generic;
    using DWD.UI.Monetary.Domain.Utilities;
    using Xunit;
    public class DateUtilTests
    {
        public static IEnumerable<object[]> DataForFirstDayOfCalendarQuarter =>
            new List<object[]>
            {
                new object[] {2020, 1, new DateTime(2020, 1, 1)  },
                new object[] {2020, 2, new DateTime(2020, 4, 1) },
                new object[] {2020, 3, new DateTime(2020, 7, 1) },
                new object[] {2020, 4, new DateTime(2020, 10, 1) },
            };
        [Theory]
        [MemberData(nameof(DataForFirstDayOfCalendarQuarter))]
        public void ShouldReturnFirstDayOfCalendarQuarter(int year, int quarterNumber, DateTime expected)
        {
            var actual = DateUtility.FirstDayOfCalendarQuarter(year, quarterNumber);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> DataForQuarterNumber =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 6), 1  },
                new object[] { new DateTime(2020, 4, 16), 2 },
                new object[] { new DateTime(2020, 8, 26), 3 },
                new object[] { new DateTime(2020, 10, 6), 4 },
            };
        [Theory]
        [MemberData(nameof(DataForQuarterNumber))]
        public void ShouldReturnQuarterNumber(DateTime date, int expected) =>
            Assert.Equal(expected, DateUtility.CalendarQuarterNumber(date));

        [Fact]
        public void TestFirstDayOfCalendarQuarterThrowsArgumentOutOfRangeException() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => DateUtility.FirstDayOfCalendarQuarter(2020, 5));
    }
}
