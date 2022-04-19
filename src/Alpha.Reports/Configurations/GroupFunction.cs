namespace Alpha.Reports.Configurations;

public sealed class GroupFunction
{
    public string Name { get; init; } = default!;

    public string[] Controllers { get; init; } = default!;

    public bool IsActive { get; init; }
}