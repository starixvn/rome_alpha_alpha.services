using Alpha.Shared.Enumerations;

namespace Alpha.Customers.Models;

public record CustomerInfo
{
    // private readonly IDictionary<int, string> systems = new Dictionary<int, string>
    // {
    //     [1] = "IBCBet",
    //     [7] = "789Y",
    //     [8] = "EU88",
    //     [9] = "Y987",
    //     [11] = "IBCIndo"
    // };

    public CustomerInfo()
    {
    }

    public CustomerInfo(
        int custId,
        string userName,
        byte roleId,
        string currencyName,
        int siteCode,
        bool enabled,
        bool isProtectedMode)
    {
        CustId = custId;
        UserName = userName;
        RoleId = roleId;
        CurrencyName = currencyName;
        SiteCode = siteCode;
        Enabled = enabled;
        IsProtectedMode = isProtectedMode;
    }

    public int CustId { get; }

    public string UserName { get; } = default!;

    public byte RoleId { get; }

    public UserLevel CustLevel => Enumeration.FromValue<UserLevel>(RoleId);

    public string CurrencyName { get; } = default!;

    public int SiteCode { get; }

    //public string System => systems[SiteCode];

    public bool Enabled { get; }

    public bool IsProtectedMode { get; }
}
