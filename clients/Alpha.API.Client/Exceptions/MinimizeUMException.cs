namespace Alpha.API.Client.Exceptions;

public sealed class MinimizeUMException : Exception
{
    public MinimizeUMException(string message, string startTime, string endTime)
        : base(message)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public string StartTime { get; }

    public string EndTime { get; }
}