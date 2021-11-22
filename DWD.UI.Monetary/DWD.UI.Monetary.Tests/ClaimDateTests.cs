namespace DWD.UI.Monetary.Tests
{
    using System;
    using System.Collections.Generic;
    using DWD.UI.Monetary.Domain.Utilities;
    using Xunit;
    public class ClaimDateTests
    {
        public static IEnumerable<object[]> DataForFirstDayOfCalendarQuarter
        {
            get
            {
                var claimDate1 = new DateTime(2020, 2, 15);
                var claimDate2 = new DateTime(2020, 4, 15);
                var claimDate3 = new DateTime(2020, 7, 15);
                var claimDate4 = new DateTime(2020, 10, 15);
                var firstDayOfCalendarQuarter1 = new DateTime(2020, 1, 1);
                var firstDayOfCalendarQuarter2 = new DateTime(2020, 4, 1);
                var firstDayOfCalendarQuarter3 = new DateTime(2020, 7, 1);
                var firstDayOfCalendarQuarter4 = new DateTime(2020, 10, 1);
                return new List<object[]>
                {
                    new object[] {claimDate1, 1, firstDayOfCalendarQuarter1},
                    new object[] {claimDate2, 2, firstDayOfCalendarQuarter2},
                    new object[] {claimDate3, 3, firstDayOfCalendarQuarter3},
                    new object[] {claimDate4, 4, firstDayOfCalendarQuarter4},
                };
            }
        }

        [Theory]
        [MemberData(nameof(DataForFirstDayOfCalendarQuarter))]
        public void ShouldHaveFirstDayOfCalendarQuarterAndQuarterNumber(DateTime date, int expectedQuarterNumber, DateTime expectedStartDateOfCalendarQuarter)
        {
            var claimDate = new ClaimDate(date);
            Assert.Equal(expectedStartDateOfCalendarQuarter, claimDate.FirstDayOfCalendarQuarter);
            Assert.Equal(expectedQuarterNumber, claimDate.QuarterNumber);
        }
    }
}
