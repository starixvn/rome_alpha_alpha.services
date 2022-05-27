namespace Alpha.Shared.Exceptions;

public sealed class MinimizeUMException : DomainException
{
    public MinimizeUMException(string startTime, string endTime)
        : base($"Sorry, the system is under maintenance from {startTime} to {endTime} GMT+8. Please come back later",
        1001,
        new Dictionary<string, object>
        {
            [nameof(startTime)] = startTime,
            [nameof(endTime)] = endTime
        })
    {
    }
}
