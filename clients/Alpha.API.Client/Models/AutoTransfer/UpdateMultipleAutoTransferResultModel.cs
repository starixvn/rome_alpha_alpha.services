namespace Alpha.API.Client.Models.AutoTransfer;

public class UpdateMultipleAutoTransferResultModel
{
        public UpdateMultipleAutoTransferResultModel() { }

        public UpdateMultipleAutoTransferResultModel(int code, string stopMessage, string language)
        {
                Code = code;
                StopMessage = stopMessage;
                Language = language;
        }

        public string Language { get; set; }

        public int? Code { get; set; }

        public List<string> WarningCusts { get; set; } = new List<string>();

        public string StopMessage { get; set; }

        public string WarningMessage { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public Dictionary<string, string> ErrMessages { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, List<string>> ErrCusts { get; set; } = new Dictionary<string, List<string>>();

        public bool HasError => !string.IsNullOrWhiteSpace(StopMessage) || !string.IsNullOrWhiteSpace(ErrorMessage);
        public bool HasWarning => !string.IsNullOrWhiteSpace(WarningMessage);
        public bool IsSuccess => !HasError && !HasWarning;
}