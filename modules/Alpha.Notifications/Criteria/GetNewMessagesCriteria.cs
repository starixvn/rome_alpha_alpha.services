using Fanex.Data;
using Fanex.Data.Repository;

namespace Alpha.Notifications.Criteria;

public class GetNewMessagesCriteria : CriteriaBase
{
    public GetNewMessagesCriteria(
        DateTime fromTime,
        DateTime toTime,
        byte messageType,
        bool isTest)
    {
        FromTime = fromTime;
        ToTime = toTime;
        MessageType = messageType;
        IsTest = isTest;
    }

    public DateTime FromTime { get; }

    public DateTime ToTime { get; }

    [SpParam(SqlParamName = "Type")]
    public byte MessageType { get; }

    public bool IsTest { get; }

    public override string GetSettingKey() => "Age_GetNewMessage";

    public override bool IsValid() => FromTime <= ToTime;
}
