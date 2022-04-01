using Alpha.Customers.Models;
using MediatR;

namespace Alpha.API.Requests.Customers;

public record GetCustomerInfoQuery(int CustId) : IRequest<CustomerInfo>;
