namespace Alpha.API.Client.Models.Customers;

public record CustomerStatus(bool IsActive, byte Locked, bool IsProtectedMode)
{
    public bool IsLocked { get; } = Locked != 0;
}
