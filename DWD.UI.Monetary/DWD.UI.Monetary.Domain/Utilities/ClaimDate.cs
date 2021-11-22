namespace DWD.UI.Monetary.Domain.Utilities
{
    using System;
    /// <summary>
    /// Additional methods to be used on any DateTime.
    /// </summary>
    public class ClaimDate
    {
        public DateTime FirstDayOfCalendarQuarter { get; private set; }

        public int QuarterNumber { get; private set; }

        public ClaimDate(DateTime claimDate)
        {
            this.SetQuarterNumber(claimDate);
            this.SetFirstDayOfCalendarQuarter(claimDate);
        }

        /// <summary>
        /// Set the calendar quarter number for the supplied date.
        /// </summary>
        /// <param name="date">date.</param>
        private void SetQuarterNumber(DateTime date) =>
            this.QuarterNumber = date.Month switch
            {
                < 4 => 1,
                < 7 => 2,
                < 10 => 3,
                _ => 4
            };

        /// <summary>
        /// Set the first day of the calendar quarter.
        /// </summary>
        /// <param name="date">date.</param>
        private void SetFirstDayOfCalendarQuarter(DateTime date)=>
            this.FirstDayOfCalendarQuarter = this.QuarterNumber switch
            {
                1 => new DateTime(date.Year, 1, 1),
                2 => new DateTime(date.Year, 4, 1),
                3 => new DateTime(date.Year, 7, 1),
                _ => new DateTime(date.Year, 10, 1),
            };
    }
}
