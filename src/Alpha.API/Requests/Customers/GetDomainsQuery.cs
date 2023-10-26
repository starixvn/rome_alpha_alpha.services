using Alpha.Customers.Models;
using MediatR;

namespace Alpha.API.Requests.Customers;

public record GetDomainsQuery(string Site) : IRequest<IEnumerable<DomainInfo>>;
