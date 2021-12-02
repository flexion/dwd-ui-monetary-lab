namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;

    public class BenefitYear
    {
        public BenefitYear(int benefitWeeks, DateTime claimRequestedDate, DateTime benefitBeginDate, DateTime benefitEndDate, YearWeek benefitBeginYearWeek, YearWeek benefitEndYearWeek)
        {
            this.BenefitWeeks = benefitWeeks;
            this.ClaimRequestedDate = claimRequestedDate;
            this.BeginDate = benefitBeginDate;
            this.EndDate = benefitEndDate;
            this.BeginYearWeek = benefitBeginYearWeek;
            this.EndYearWeek = benefitEndYearWeek;
        }

        public int BenefitWeeks { get; }

        public DateTime ClaimRequestedDate { get; }

        public DateTime BeginDate { get; }

        public DateTime EndDate { get; }

        public YearWeek BeginYearWeek { get; }

        public YearWeek EndYearWeek { get; }

    }
}
