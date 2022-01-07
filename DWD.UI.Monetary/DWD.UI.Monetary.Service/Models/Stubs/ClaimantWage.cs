#nullable disable

namespace DWD.UI.Monetary.Service.Models.Stubs;

/// <summary>
/// ClaimantWage stub.
/// </summary>
public partial class ClaimantWage
{
    /// <summary>
    /// Gets or sets iD.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets claimant ID.
    /// </summary>
    public string ClaimantId { get; set; }

    /// <summary>
    /// Gets or sets wage Year.
    /// </summary>
    public int? WageYear { get; set; }

    /// <summary>
    /// Gets or sets wage Quarter.
    /// </summary>
    public short? WageQuarter { get; set; }

    /// <summary>
    /// Gets or sets total Wages.
    /// </summary>
    public decimal? TotalWages { get; set; }
}
