using System.Diagnostics.CodeAnalysis;

namespace Alpha.API.Sample.Models;

[ExcludeFromCodeCoverage(Justification = "Model used only for error display in sample API.")]
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
