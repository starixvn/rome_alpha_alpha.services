using Alpha.Notifications.Models;
using MediatR;

namespace Alpha.API.Requests.Notifications;

public record GetMessageQuery(DateTime FromTime, DateTime ToTime, byte MessageType)
    : IRequest<IEnumerable<Message>>;
