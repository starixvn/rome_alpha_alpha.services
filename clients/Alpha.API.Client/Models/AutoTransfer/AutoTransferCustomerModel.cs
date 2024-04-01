namespace Alpha.API.Client.Models.AutoTransfer;

public class AutoTransferCustomerModel
{
        public decimal Balance { get; set; }
        public int RowNumber { get; set; }
        public string Username { get; set; }
        public decimal YesterdayBalance { get; set; }
        public int CustId { get; set; }

        public bool Enabled { get; set; }
        public bool Closed { get; set; }
        public bool Suspended { get; set; }
        public byte ClosedReason { get; set; }
        public byte Locked { get; set; }
        public bool EnableAutoTransfer { get; set; }
        public string AutoTransferSchedule { get; set; }

        public IEnumerable<string> AutoTransferDates { get; set; }

        public int Status { get; set; }
}