using Alpha.API.Requests.Notifications;
using Alpha.Notifications.Models;
using Alpha.Notifications.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class MessageHandler : IRequestHandler<GetMessageQuery, IEnumerable<Message>>
{
    private readonly IMessageService messageService;

    public MessageHandler(IMessageService messageService)
        => this.messageService = messageService;
    
    public Task<IEnumerable<Message>> Handle(GetMessageQuery query, CancellationToken cancellationToken)
        => messageService.GetMessagesAsync(query.FromTime, query.ToTime, query.MessageType);
}
