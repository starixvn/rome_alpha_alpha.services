using Alpha.Shared.Enumerations;

namespace Alpha.API.Requests.Reports;

public abstract record ReportBaseQuery(int CustId, byte CustLevelId)
{
    internal UserLevel CustLevel => Enumeration.FromValue<UserLevel>(CustLevelId);
}
