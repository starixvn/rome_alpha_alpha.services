using Alpha.Shared.Enumerations;

namespace Alpha.API.Requests.Reports;

public abstract record ReportBaseQuery(int CustId, int SubAccId, byte CustLevelId)
{
    internal UserLevel CustLevel => Enumeration.FromValue<UserLevel>(CustLevelId);
}
