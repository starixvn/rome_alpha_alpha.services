using Alpha.Customers.Models;
using MediatR;

namespace Alpha.API.Requests.Customers;

public record GetDownlineInfoQuery(int CustId, string DownlineUsername) : IRequest<DownlineInfo>;
