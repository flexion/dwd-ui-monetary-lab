namespace DWD.UI.Monetary.Tests;

using System;
using System.Collections.Generic;
using DWD.UI.Monetary.Domain.Utilities;
using Xunit;
public class CalendarQuarterTests
{
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

    [CLSCompliant(false)]
    [Theory]
    [MemberData(nameof(DataForFirstDayOfCalendarQuarter))]
    public void ShouldReturnFirstDayOfCalendarQuarter(int year, int quarterNumber, DateTime expected)
    {
        var actual = CalendarQuarter.FirstDayOfCalendarQuarter(year, quarterNumber);
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

    [CLSCompliant(false)]
    [Theory]
    [MemberData(nameof(DataForQuarterNumber))]
    public void ShouldReturnQuarterNumber(DateTime input, int expected) =>
        Assert.Equal(expected, CalendarQuarter.CalendarQuarterNumber(input));

    [Fact]
    public void TestInvalidQuarterNumberThrowsArgumentOutOfRangeException() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => CalendarQuarter.FirstDayOfCalendarQuarter(2020, 5));

    public static IEnumerable<object[]> DataForGetDateFromYearAndWeekNumber
    {
        get
        {
            var startDateOfWeek1In2022 = new DateTime(2021, 12, 26);
            var startDateOfWeek52In2021 = new DateTime(2021, 12, 19);
            var startDateOfWeek2In2021 = new DateTime(2021, 1, 3);
            var startDateOfWeek50In2020 = new DateTime(2020, 12, 6);
            var startDateOfWeek14In2021 = new DateTime(2021, 3, 28);
            var startDateOfWeek15In2021 = new DateTime(2021, 4, 4);
            var startDateOfWeek28In2021 = new DateTime(2021, 7, 4);
            var startDateOfWeek45In2021 = new DateTime(2021, 10, 31);
            var startDateOfWeek48In2021 = new DateTime(2021, 11, 21);
            var startDateOfWeek54In2020 = new DateTime(2000, 12, 24);
            return new List<object[]>
            {
                new object[] {2022, 1, startDateOfWeek1In2022},
                new object[] {2021, 52, startDateOfWeek52In2021},
                new object[] {2021, 2, startDateOfWeek2In2021},
                new object[] {2020, 50, startDateOfWeek50In2020},
                new object[] {2021, 14, startDateOfWeek14In2021},
                new object[] {2021, 15, startDateOfWeek15In2021},
                new object[] {2021, 28, startDateOfWeek28In2021},
                new object[] {2021, 45, startDateOfWeek45In2021},
                new object[] {2021, 48, startDateOfWeek48In2021},
                new object[] {2000, 53, startDateOfWeek54In2020},
            };
        }
    }

    [CLSCompliant(false)]
    [Theory]
    [MemberData(nameof(DataForGetDateFromYearAndWeekNumber))]
    public void ShouldReturnDateTimeFromYearAndWeekNumber(int year, int weekOfYear, DateTime expectedStartDate) =>
        Assert.Equal(expectedStartDate, CalendarQuarter.GetDateTimeFromYearAndWeek(year, weekOfYear));

    [CLSCompliant(false)]
    [Theory]
    [InlineData(2020, 0)]
    [InlineData(2020, -1)]
    [InlineData(2028, 54)]
    [InlineData(-18, 5)]
    public void ShouldReturnArgumentOutOfRangeExceptionWhenGetDateFromYearAndWeekNumber(int year, int weekOfYear) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => CalendarQuarter.GetDateTimeFromYearAndWeek(year, weekOfYear));
}
