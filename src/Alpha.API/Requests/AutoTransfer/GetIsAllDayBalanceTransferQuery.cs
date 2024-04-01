using MediatR;

namespace Alpha.API.Requests.AutoTransfer;

public record GetIsAllDayBalanceTransferQuery(int CustId) : IRequest<bool>;
