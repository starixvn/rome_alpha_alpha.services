using Alpha.Notifications.Configurations;
using Alpha.Notifications.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Notifications.Extensions;

public static class NotificationDIExtensions
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<NotificationAppSettings>(configuration.GetSection(nameof(NotificationAppSettings)))
            .AddScoped<IMessageService, MessageService>();
}
