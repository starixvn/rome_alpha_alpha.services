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
        : base($"We're updating this function for a better experience. Thus, this function is turned off temporarily ({startTime} - {endTime} (GMT+8)). Please try again later and sorry for the inconvenience.",
        ErrorCodeConst,
        new Dictionary<string, object>
        {
            [nameof(startTime)] = startTime,
            [nameof(endTime)] = endTime
        })
    {
    }
}
