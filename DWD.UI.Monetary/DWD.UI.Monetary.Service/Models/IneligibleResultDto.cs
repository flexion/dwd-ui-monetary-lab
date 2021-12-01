namespace DWD.UI.Monetary.Service.Models
{
    using System.Collections.ObjectModel;
    using Domain.BusinessEntities;

    public class IneligibleResultDto
    {
        public bool IsEligible { get; }
        public Collection<IneligibleDisplayReason> IneligibleReasons { get; }

        public IneligibleResultDto(bool isEligible, Collection<IneligibleDisplayReason> ineligibleReasons)
        {
            this.IsEligible = isEligible;
            this.IneligibleReasons = ineligibleReasons;
        }
    }

    public class IneligibleDisplayReason
    {
        public IneligibilityReason Code { get; }
        public string Description { get; }

        public IneligibleDisplayReason(IneligibilityReason code, string description)
        {
            this.Code = code;
            this.Description = description;
        }
    }
}
