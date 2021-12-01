namespace DWD.UI.Monetary.Domain.UseCases
{
    using System;
    using BusinessEntities;

    public class CalculateBenefitPeriod
    {
        public BenefitPeriod CalculateBenefitPeriodFromDate(DateTime startDate) => new(startDate);
    }
}
