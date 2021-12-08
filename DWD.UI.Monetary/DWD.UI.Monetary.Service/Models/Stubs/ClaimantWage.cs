#nullable disable

namespace DWD.UI.Monetary.Service.Models.Stubs;

public partial class ClaimantWage
{
    public long Id { get; set; }
    public string ClaimantId { get; set; }
    public short? WageYear { get; set; }
    public short? WageQuarter { get; set; }
    public decimal? TotalWages { get; set; }
}