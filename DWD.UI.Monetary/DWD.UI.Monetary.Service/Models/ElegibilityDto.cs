namespace DWD.UI.Monetary.Service.Models

{
    using System;
    using System.Collections.Generic;

    public class ElegibilityDto
    {
        public IEnumerable<decimal> WagesOfQuarters { get; } = new List<decimal>();
        public DateTime InitialClaimDate { get; set; }
        public string ClaimantId { get; set; }
        public int MinHighQuarterEarnings { get; set; }
        public int PercentWeeklyBenefitRate { get; set; }
        public int MinQuarters { get; set; }
        public int WagesOutsideOfHighQuarterFactor { get; set; }
        public int BasePeriodWagesFactor { get; set; }
        public int NumberOfWeeks { get; set; }
        public int PercentOfBasePeriodWages { get; set; }
    }
}