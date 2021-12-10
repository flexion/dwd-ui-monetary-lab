namespace DWD.UI.Calendar.Tests
{
    using System;
    using System.Collections.Generic;
    using DWD.UI.Calendar;
    using Xunit;

    public class UIWeekShould
    {
        /// <summary>
        /// Test cases from https://dwd.wisconsin.gov/uiben/calendars.htm
        /// </summary>
        public static IEnumerable<object[]> TestData => new List<object[]>
            {
                new object[] {2023, 1, new DateTime(2023, 1, 1) },    // 2023 starts on Sunday
                new object[] {2023, 52, new DateTime(2023, 12, 24) },
                new object[] {2018, 1, new DateTime(2017, 12, 31) },  // 2018 starts on Monday
                new object[] {2018, 52, new DateTime(2018, 12, 23) },
                new object[] {2019, 1, new DateTime(2018, 12, 30) },  // 2019 starts on Tuesday
                new object[] {2019, 52, new DateTime(2019, 12, 22) },
                new object[] {2014, 1, new DateTime(2013, 12, 29) },  // 2014 starts on Wednesday
                new object[] {2014, 52, new DateTime(2014, 12, 21) },
                new object[] {2015, 1, new DateTime(2014, 12, 28) },  // 2015 starts on Thursday
                new object[] {2015, 52, new DateTime(2015, 12, 20) },
                new object[] {2021, 1, new DateTime(2020, 12, 27) },  // 2021 starts on Friday
                new object[] {2021, 52, new DateTime(2021, 12, 19) },
                new object[] {2016, 1, new DateTime(2015, 12, 27) },  // 2016 is a leap year that starts on Friday
                new object[] {2016, 53, new DateTime(2016, 12, 25) },
                new object[] {2022, 1, new DateTime(2021, 12, 26) },  // 2022 starts on Saturday
                new object[] {2022, 53, new DateTime(2022, 12, 25) },
                new object[] {2028, 1, new DateTime(2027, 12, 26) },  // 2028 is a leap year that starts on Saturday
                new object[] {2028, 53, new DateTime(2028, 12, 24) },
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateStartDateOfWeekFromYearAndWeek(int year, int weekOfYear, DateTime expectedStartDate)
        {
            var uiWeek = new UIWeek(year, weekOfYear);
            Assert.Equal(expectedStartDate, uiWeek.StartDate);
            Assert.Equal(expectedStartDate.AddDays(6), uiWeek.EndDate);
        }


        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateStartDateOfWeekFromYearAndWeekWhenFirstDayOfWeekIsMonday(int year, int weekOfYear, DateTime expectedStartDate)
        {
            var uiWeek = new UIWeek(year, weekOfYear, DayOfWeek.Monday);
            Assert.Equal(expectedStartDate, uiWeek.StartDate);
            Assert.Equal(expectedStartDate.AddDays(6), uiWeek.EndDate);
        }

        [Theory]
        [InlineData(2020, 0)]
        [InlineData(2020, -1)]
        [InlineData(2028, 54)]
        [InlineData(2023, 53)] // Non-leap year starting on Sunday has 52 weeks
        [InlineData(2018, 53)] // Non-leap year starting on Monday has 52 weeks
        [InlineData(2019, 53)] // Non-leap year starting on Tuesday has 52 weeks
        [InlineData(2014, 53)] // Non-leap year starting on Wednesday has 52 weeks
        [InlineData(2015, 53)] // Non-leap year starting on Thursday has 52 weeks
        [InlineData(2021, 53)] // Non-leap year starting on Friday has 52 weeks
        [InlineData(2016, 54)] // Leap year starting on Friday has 53 weeks
        [InlineData(-18, 5)]
        public void ThrowArgumentOutOfRangeExceptionWhenWeekNumberIsInvalid(int year, int weekOfYear) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new UIWeek(year, weekOfYear));

        [Fact]
        public void HaveValueEquality()
        {
            var aDate = new DateTime(2021, 12, 10);
            var a = new UIWeek(aDate);
            var b = new UIWeek(aDate);
            Assert.Equal(a, b);
        }
    }
}
