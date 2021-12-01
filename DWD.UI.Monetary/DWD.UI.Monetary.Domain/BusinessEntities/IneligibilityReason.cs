namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System.ComponentModel;

    public enum IneligibilityReason
    {
        [Description("Claimant does not have at least two quarters' wages")]
        InsufficientQuartersWithWages,

        [Description("Claimant's high quarter wage is not at least minimum High Quarter Earnings")]
        InsufficientHighQuarterWage,

        [Description("Claimant's outside High Quarter wages are too low relative to Weekly Benefit Rate")]
        InsufficientNonHighQuarterWages,

        [Description("Claimant's Total Base Period Wages are too low relative to Weekly Benefit Rate")]
        InsufficientTotalBasePeriodWages,
    }

    public class IneligibilityReason2
    {
        private IneligibilityReason2(string value) { Code = value; }
        public string Code { get; private set; }
        public string Description { get; private set; }


        public static IneligibilityReason2 Trace { get { return new IneligibilityReason2("Trace"); } }
        public static IneligibilityReason2 Debug { get { return new IneligibilityReason2("Debug"); } }
        public static IneligibilityReason2 Info { get { return new IneligibilityReason2("Info"); } }
        public static IneligibilityReason2 Warning { get { return new IneligibilityReason2("Warning"); } }
        public static IneligibilityReason2 Error { get { return new IneligibilityReason2("Error"); } }
    }
}
