#pragma warning disable CS3016
namespace DWD.UI.Monetary.Tests
{
    using System;
    using System.Collections.Generic;
    using DWD.UI.Monetary.Domain.Utilities;
    using Xunit;
    public class CalendarQuarterTests
    {
        private readonly CalendarQuarter calendarQuarter = new();
        public static IEnumerable<object[]> DataForFirstDayOfCalendarQuarter
        {
            get
            {
                var startDateOfFirstCalendarQuarter = new DateTime(2020, 1, 1);
                var startDateOfSecondCalendarQuarter = new DateTime(2020, 4, 1);
                var startDateOfThirdCalendarQuarter = new DateTime(2020, 7, 1);
                var startDateOfFourthCalendarQuarter = new DateTime(2020, 10, 1);
                return new List<object[]>
                {
                    new object[] {2020, 1, startDateOfFirstCalendarQuarter},
                    new object[] {2020, 2, startDateOfSecondCalendarQuarter},
                    new object[] {2020, 3, startDateOfThirdCalendarQuarter},
                    new object[] {2020, 4, startDateOfFourthCalendarQuarter},
                };
            }
        }
        [Theory]
        [MemberData(nameof(DataForFirstDayOfCalendarQuarter))]
        public void ShouldReturnFirstDayOfCalendarQuarter(int year, int quarterNumber, DateTime expected)
        {
            var actual = this.calendarQuarter.FirstDayOfCalendarQuarter(year, quarterNumber);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> DataForQuarterNumber
        {
            get
            {
                var aDateInFirstCalendarQuarter = new DateTime(2020, 2, 6);
                var aDateInSecondCalendarQuarter = new DateTime(2020, 4, 16);
                var aDateInThirdCalendarQuarter = new DateTime(2020, 8, 26);
                var aDateInFourthCalendarQuarter = new DateTime(2020, 10, 6);
                return new List<object[]>
                {
                    new object[] {aDateInFirstCalendarQuarter, 1},
                    new object[] {aDateInSecondCalendarQuarter, 2},
                    new object[] {aDateInThirdCalendarQuarter, 3},
                    new object[] {aDateInFourthCalendarQuarter, 4},
                };
            }
        }
        [Theory]
        [MemberData(nameof(DataForQuarterNumber))]
        public void ShouldReturnQuarterNumber(DateTime input, int expected) =>
            Assert.Equal(expected, this.calendarQuarter.CalendarQuarterNumber(input));

        [Fact]
        public void TestInvalidQuarterNumberThrowsArgumentOutOfRangeException() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => this.calendarQuarter.FirstDayOfCalendarQuarter(2020, 5));
    }
}
