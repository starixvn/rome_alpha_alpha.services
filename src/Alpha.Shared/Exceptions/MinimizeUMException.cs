using System.Runtime.Serialization;

namespace Alpha.Shared.Exceptions;

[Serializable]
public sealed class MinimizeUMException : DomainException
{
    private const int ErrorCodeConst = 1001;

    private MinimizeUMException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }

    public MinimizeUMException(string startTime, string endTime)
        : base($"Sorry, the system is under maintenance from {startTime} to {endTime} GMT+8. Please come back later",
        ErrorCodeConst,
        new Dictionary<string, object>
        {
            [nameof(startTime)] = startTime,
            [nameof(endTime)] = endTime
        })
    {
    }
}
