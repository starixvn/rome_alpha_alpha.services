namespace Alpha.Reports.Models;

public record UMInfo
{
    public UMInfo()
    {
    }

    public UMInfo(string functionTypeName, DateTime beginTime, DateTime endTime)
    {
        FunctionTypeName = functionTypeName;
        BeginTime = beginTime;
        EndTime = endTime;
    }

    public string FunctionTypeName { get; }

    public DateTime BeginTime { get; }

    public DateTime EndTime { get; }
}
