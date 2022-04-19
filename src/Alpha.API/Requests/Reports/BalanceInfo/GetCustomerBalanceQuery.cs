using Alpha.Reports.Models.BalanceInfo;
using MediatR;

namespace Alpha.API.Requests.Reports.BalanceInfo;

public record GetCustomerBalanceQuery(int CustId, byte CustLevelId) :
    ReportBaseQuery(CustId, CustLevelId),
    IRequest<CustomerBalance>;
