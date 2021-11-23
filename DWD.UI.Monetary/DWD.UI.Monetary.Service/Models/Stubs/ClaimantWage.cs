#nullable disable

namespace DWD.UI.Monetary.Service.Models.Stubs
{
    /// <summary>
    /// ClaimantWage stub
    /// </summary>
    public partial class ClaimantWage
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Claimant ID
        /// </summary>
        public string ClaimantId { get; set; }

        /// <summary>
        /// Wage Year
        /// </summary>
        public short? WageYear { get; set; }

        /// <summary>
        /// Wage Quarter
        /// </summary>
        public short? WageQuarter { get; set; }

        /// <summary>
        /// Total Wages
        /// </summary>
        public decimal? TotalWages { get; set; }
    }
}
