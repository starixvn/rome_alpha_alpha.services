using System.Runtime.Serialization;

namespace Alpha.API.Client.Exceptions;

[Serializable]
public sealed class MinimizeUMException : Exception
{
    private MinimizeUMException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }

    public MinimizeUMException(string message, string startTime, string endTime)
        : base(message)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public string StartTime { get; }

    public string EndTime { get; }
}