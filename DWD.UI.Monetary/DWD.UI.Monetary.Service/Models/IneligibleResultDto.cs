namespace DWD.UI.Monetary.Service.Models
{
    using System.Collections.ObjectModel;
    using Domain.BusinessEntities;

    public class IneligibleResultDto
    {
        public bool IsEligible { get; }
        public Collection<IneligibilityDisplayReason> IneligibilityReasons { get; }

        public IneligibleResultDto(bool isEligible, Collection<IneligibilityDisplayReason> ineligibilityReasons)
        {
            this.IsEligible = isEligible;
            this.IneligibilityReasons = ineligibilityReasons;
        }
    }

    public class IneligibilityDisplayReason
    {
        public IneligibilityReason Code { get; }
        public string Description { get; }

        public IneligibilityDisplayReason(IneligibilityReason code, string description)
        {
            this.Code = code;
            this.Description = description;
        }
    }
}
