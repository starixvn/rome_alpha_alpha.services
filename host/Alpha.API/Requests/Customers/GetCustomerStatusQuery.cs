using Alpha.Customers.Models;
using MediatR;

namespace Alpha.API.Requests.Customers;

public record GetCustomerStatusQuery(int CustId, int SubAccId) : IRequest<CustomerStatus>;
