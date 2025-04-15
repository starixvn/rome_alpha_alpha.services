using Alpha.API.Client.Models;
using Alpha.API.Client.Models.AutoTransfer;

namespace Alpha.API.Client.Services;

public interface IAutoTransferService
{
    Task<bool> IsAllDayBalanceTransfer(int custId);

    Task<AutoTransferMonitoringPageModel> Monitor(int custId, string username, string status, int sortedBy, int direction, byte levelType, string language);

    Task<AutoTransferCustomersPageModel> Customers(int custId, int custLevel, string language, string username, string status, string autoTransferStatus, int pageIndex, int pageSize);

    Task<Result> UpdateStatus(
        int uplineId, int subAccId, int uplineRoleId, string language,
        int custId, string username, bool state, string schedule, string oldTransferOption);

    Task<Result> UpdateSchedule(
        int uplineId, int subAccId, int uplineRoleId, string language,
        int custId, string username, string schedule, string oldTransferOption);

    Task<UpdateMultipleAutoTransferResultModel> UpdateMultiple(
        int uplineId, int subAccId, int uplineRoleId, string language,
        string onIds, string offIds, string onNames, string offNames,
        string onSchedule, string offSchedule, bool onStatus, bool offStatus, string oldTransferOption);
}

public class AutoTransferService : ServiceBase, IAutoTransferService
{
    public AutoTransferService(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<bool> IsAllDayBalanceTransfer(int custId)
        => Post<bool>($"/api/AutoTransfer/IsAllDayBalanceTransfer", new { CustId = custId });

    public Task<AutoTransferMonitoringPageModel> Monitor(int custId, string username, string status, int sortedBy, int direction, byte levelType, string language)
        => Post<AutoTransferMonitoringPageModel>("/api/AutoTransfer/Monitor", new
        {
            CustId = custId,
            Username = username,
            Status = status,
            SortedBy = sortedBy,
            Direction = direction,
            LevelType = levelType,
            Language = language,
        });

    public Task<AutoTransferCustomersPageModel> Customers(int custId, int custLevel, string language, string username, string status, string autoTransferStatus, int pageIndex, int pageSize)
        => Post<AutoTransferCustomersPageModel>("/api/AutoTransfer/Customers", new
        {
            CustId = custId,
            CustLevel = custLevel,
            Language = language,
            Username = username,
            Status = status,
            AutoTransferStatus = autoTransferStatus,
            PageIndex = pageIndex,
            PageSize = pageSize,
        });

    public Task<Result> UpdateStatus(
        int uplineId, int subAccId, int uplineRoleId, string language,
        int custId, string username, bool state, string schedule, string oldTransferOption)
        => Post<Result>("/api/AutoTransfer/UpdateStatus", new
        {
            UplineId = uplineId,
            SubAccId = subAccId,
            UplineRoleId = uplineRoleId,
            Language = language,
            CustId = custId,
            Username = username,
            State = state,
            Schedule = schedule,
            OldTransferOption = oldTransferOption,
        });

    public Task<Result> UpdateSchedule(
        int uplineId, int subAccId, int uplineRoleId, string language,
        int custId, string username, string schedule, string oldTransferOption)
        => Post<Result>("/api/AutoTransfer/UpdateSchedule", new
        {
            UplineId = uplineId,
            SubAccId = subAccId,
            UplineRoleId = uplineRoleId,
            Language = language,
            CustId = custId,
            Username = username,
            Schedule = schedule,
            OldTransferOption = oldTransferOption,
        });

    public Task<UpdateMultipleAutoTransferResultModel> UpdateMultiple(
        int uplineId, int subAccId, int uplineRoleId, string language,
        string onIds, string offIds, string onNames, string offNames,
        string onSchedule, string offSchedule, bool onStatus, bool offStatus, string oldTransferOption)
        => Post<UpdateMultipleAutoTransferResultModel>("/api/AutoTransfer/UpdateMultiple", new
        {
            UplineId = uplineId,
            SubAccId = subAccId,
            UplineRoleId = uplineRoleId,
            Language = language,
            OnIds = onIds,
            OffIds = offIds,
            OnNames = onNames,
            OffNames = offNames,
            OnSchedule = onSchedule,
            OffSchedule = offSchedule,
            OnStatus = onStatus,
            OffStatus = offStatus,
            OldTransferOption = oldTransferOption,
        });
}