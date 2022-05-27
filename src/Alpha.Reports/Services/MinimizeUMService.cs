using Alpha.Reports.Configurations;
using Alpha.Shared.Exceptions;

namespace Alpha.Reports.Services;

public interface IMinimizeUMService
{
    void ThrowIfUM(string controllerName);
}

public class MinimizeUMService : IMinimizeUMService
{
    private readonly MinimizeUM minimizeUM;

    public MinimizeUMService(MinimizeUM minimizeUM)
        => this.minimizeUM = minimizeUM;

    public void ThrowIfUM(string controllerName)
    {
        if (minimizeUM.ControllerNames.ContainsKey(controllerName))
        {
            throw new MinimizeUMException(minimizeUM.StartTime, minimizeUM.EndTime);
        }
    }
}