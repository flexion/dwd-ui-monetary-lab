namespace DWD.UI.Monetary.Service.Models;

using DWD.UI.Monetary.Domain.BusinessEntities;

/// <summary>
/// Presentation of the Ineligibility reason.
/// </summary>
public class IneligibilityDisplayReason
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="code">reason code</param>
    /// <param name="description">reason description</param>
    public IneligibilityDisplayReason(IneligibilityReason code, string description)
    {
        this.Code = code;
        this.Description = description;
    }

    /// <summary>
    /// Get the reason code.
    /// </summary>
    /// <returns>the code.</returns>
    public IneligibilityReason Code { get; }

    /// <summary>
    /// Get the reason description.
    /// </summary>
    /// <returns>the reason description.</returns>
    public string Description { get; }
}
