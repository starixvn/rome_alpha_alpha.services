using Alpha.Customers.Models.AutoTransfer;
using MediatR;

namespace Alpha.API.Requests.AutoTransfer;

public class GetAutoTransferMonitorQuery : IRequest<AutoTransferMonitoringPageModel>
{
    public int CustId { get; set; }

    public string? Language { get; set; }

    public string? Username { get; set; }

    public string? Status { get; set; }

    public int SortedBy { get; set; }

    public int Direction { get; set; }

    public byte LevelType { get; set; }
}
