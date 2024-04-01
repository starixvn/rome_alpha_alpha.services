using Alpha.Customers.Helpers;

namespace Alpha.Customers.Models.AutoTransfer;

public class AutoTransferCustomersPageModel
{
        public int Code { get; set; }
        public string Message { get; set; }

        public string UplineTransferOption { get; set; }
        public IEnumerable<AutoTransferCustomerModel> Customers { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        
        public int UplineRoleId { get; set; }
        public List<int> TransferrableDates => TransferConditionHelper.GetAutoTransferScheduleFromTransferOption(UplineTransferOption, UplineRoleId);
}