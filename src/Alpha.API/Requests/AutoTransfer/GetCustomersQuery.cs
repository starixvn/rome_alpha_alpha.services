using Alpha.Customers.Models.AutoTransfer;
using MediatR;

namespace Alpha.API.Requests.AutoTransfer;

public class  GetCustomersQuery: IRequest<AutoTransferCustomersPageModel> {
        public int CustId { get; set; }
        
        public string? Language { get; set; }

        public int CustLevel { get; set; }

        public string? Username { get; set; }

        public string? Status { get; set; }

        public string? AutoTransferStatus { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
}
