namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    public class BenefitPeriod
    {
        public DateTime ClaimEntryDate { get; }
        public DateTime ClaimBeginDate { get; }

        public DateTime ClaimEndDate { get; }

        public YearWeek ClaimBeginYearWeek { get; }

        public YearWeek ClaimEndYearWeek { get; }
        public BenefitPeriod(DateTime theStartDate)
        {
            this.ClaimEntryDate = theStartDate;
            this.ClaimBeginDate = this.ClaimEntryDate.AddDays(-1 * (this.ClaimEntryDate.DayOfWeek - DayOfWeek.Sunday));
            this.ClaimEndDate = this.ClaimBeginDate.AddDays((7 * 52) - 1);
            this.ClaimBeginYearWeek = GetYearWeekFromDate(this.ClaimBeginDate);
            this.ClaimEndYearWeek = GetYearWeekFromDate(this.ClaimEndDate);
        }

        private static YearWeek GetYearWeekFromDate(DateTime inDate)
        {
            var dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
            Debug.Assert(dateTimeFormatInfo != null, nameof(dateTimeFormatInfo) + " != null");
            var week = dateTimeFormatInfo.Calendar.GetWeekOfYear(inDate,
                                            dateTimeFormatInfo.CalendarWeekRule,
                                            dateTimeFormatInfo.FirstDayOfWeek);
            return new YearWeek(inDate.Year, week);
        }

        public class YearWeek
        {
            public YearWeek(int theYear, int theWeek)
            {
                this.Year = theYear;
                this.Week = theWeek;
            }

            public int Week { get; }
            public int Year { get; }
        }
    }
}
