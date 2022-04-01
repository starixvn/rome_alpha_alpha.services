using Alpha.Notifications.Configurations;
using Alpha.Notifications.Criteria;
using Alpha.Notifications.Models;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;

namespace Alpha.Notifications.Services;

public interface IMessageService
{
    Task<IEnumerable<Message>> GetMessagesAsync(DateTime fromTime, DateTime toTime, byte messageType);
}

public class MessageService : IMessageService
{
    private readonly NotificationAppSettings appSettings;
    private readonly IDynamicRepository dynamicRepository;

    public MessageService(IOptions<NotificationAppSettings> options, IDynamicRepository dynamicRepository)
    {
        appSettings = options.Value;
        this.dynamicRepository = dynamicRepository;
    }

    public Task<IEnumerable<Message>> GetMessagesAsync(DateTime fromTime, DateTime toTime, byte messageType)
        => dynamicRepository.FetchAsync<Message>(
            new GetNewMessagesCriteria(fromTime, toTime, messageType, appSettings.IsLocalTest));
}
