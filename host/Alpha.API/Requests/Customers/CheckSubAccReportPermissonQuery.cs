using Alpha.Customers.Models;
using MediatR;

namespace Alpha.API.Requests.Customers;

public record CheckSubAccReportPermissonQuery(int CustId, int SubAccId) : IRequest<bool>;
