namespace Alpha.Customers.Models;

public record DomainInfo
{
    public string Domain { get; init; }

    public bool IsAgency { get; init; }
}
