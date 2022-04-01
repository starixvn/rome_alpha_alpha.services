namespace Alpha.Customers.Models;

public record SubAccInfo
{
    public SubAccInfo()
    { 
    }

    public SubAccInfo(string pageAccess)
    {
        PageAccess = pageAccess;
    }

    public string PageAccess { get; } = default!;
}
