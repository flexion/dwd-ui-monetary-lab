namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;

    /// <summary>
    ///
    /// </summary>
    /// <param name="BenefitWeeks">
    /// The number of weeks in a benefit year.
    ///
    /// The value can be 52 or 53. 53 is an exceptional case when for
    /// when a subsequent year claim's base period overlaps with the prior.
    /// </param>
    /// <param name="ClaimRequestedDate">
    /// The benefit date requested by the claim.
    ///
    /// Usually the claim initiated date but claimant can request benefits begin
    /// in a week after that date.
    /// </param>
    /// <param name="BeginDate">
    /// The calculated benefit begin date.
    ///
    /// Benefits always begin on a Sunday regardless of the requested date.
    /// </param>
    /// <param name="EndDate">
    /// The calculated benefit end date.
    ///
    /// Benefits always end on a Sunday 52 weeks after the begin date.
    /// In exceptional cases to avoid an overlap in base periods for
    /// subsequent claims, this value may be 53 weeks.
    /// </param>
    /// <param name="BeginYearWeek">
    /// The benefit begin year and week.
    ///
    /// First week of the year is the week that contains January 1st
    /// even if the beginning of the week is in the prior year.
    /// The week number and year is calculated based on the Saturday
    /// (week ending date) following the BeginDate.
    /// </param>
    /// <param name="EndYearWeek">
    /// The benefit end year and week.
    ///
    /// First week of the year is the week that contains January 1st
    /// even if the beginning of the week is in the prior year.
    /// The week number and year is calculated based EndDate
    /// which is always a Saturday.
    /// </param>
    public record BenefitYear(int BenefitWeeks,
        DateTime ClaimRequestedDate,
        DateTime BeginDate,
        DateTime EndDate,
        YearWeek BeginYearWeek,
        YearWeek EndYearWeek);
}
