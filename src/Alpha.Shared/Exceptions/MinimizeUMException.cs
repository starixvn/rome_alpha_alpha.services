namespace Alpha.Shared.Exceptions;

public sealed class MinimizeUMException : DomainException
{
    public MinimizeUMException(string startTime, string endTime)
        : base($"Our server is too busy at the moment so this function is turned off temporarily {startTime} - {endTime} (GMT+8). Please try again later and sorry for the inconvenience.",
        1001,
        new Dictionary<string, object>
        {
            [nameof(startTime)] = startTime,
            [nameof(endTime)] = endTime
        })
    {
    }
}
