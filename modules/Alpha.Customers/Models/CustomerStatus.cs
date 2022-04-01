namespace Alpha.Customers.Models;

public record CustomerStatus(bool IsActive, byte Locked, bool IsProtectedMode = false);