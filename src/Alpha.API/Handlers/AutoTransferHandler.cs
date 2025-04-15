using Alpha.API.Requests.AutoTransfer;
using Alpha.Customers.Consts;
using Alpha.Customers.Models;
using Alpha.Customers.Models.AutoTransfer;
using Alpha.Customers.Resources;
using Alpha.Customers.Services;
using MediatR;

namespace Alpha.API.Handlers;

public class AutoTransferHandler :
    IRequestHandler<GetIsAllDayBalanceTransferQuery, bool>,
    IRequestHandler<GetAutoTransferMonitorQuery, AutoTransferMonitoringPageModel>,
    IRequestHandler<GetCustomersQuery, AutoTransferCustomersPageModel>,
    IRequestHandler<UpdateStatusQuery, Result>,
    IRequestHandler<UpdateScheduleQuery, Result>,
    IRequestHandler<UpdateMultipleQuery, UpdateMultipleAutoTransferResultModel>
{
    private readonly IAutoTransferService autoTransferService;

    public AutoTransferHandler(IAutoTransferService autoTransferService)
        => this.autoTransferService = autoTransferService;

    public Task<bool> Handle(GetIsAllDayBalanceTransferQuery query, CancellationToken cancellationToken)
        => autoTransferService.IsAllDayBalanceTransfer(query.CustId);

    public Task<AutoTransferMonitoringPageModel> Handle(GetAutoTransferMonitorQuery query, CancellationToken cancellationToken)
        => autoTransferService.Monitor(
            query.CustId,
            query.Username,
            query.Status,
            query.SortedBy,
            query.Direction,
            query.LevelType,
            query.Language);

    public Task<AutoTransferCustomersPageModel> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
        => autoTransferService.Customers(
            query.CustId,
            query.CustLevel,
            query.Language,
            query.Username,
            query.Status,
            query.AutoTransferStatus,
            query.PageIndex,
            query.PageSize);

    public async Task<Result> Handle(UpdateStatusQuery query, CancellationToken cancellationToken)
    {
        if (query == null || !query.IsValid())
        {
            return new Result(ErrorCodes.BadRequest, ResourceHelper.Get(nameof(SharedResource.BadRequest), query.Language));
        }

        if (await autoTransferService.IsAllDayBalanceTransfer(query.UplineId))
        {
            return new Result(ErrorCodes.ADBTChanged, ResourceHelper.Get(nameof(SharedResource.ADBTChangeError), query.Language));
        }

        return await autoTransferService.UpdateStatus(
            query.UplineId, query.SubAccId, query.UplineRoleId, query.Language,
            query.CustId, query.Username, query.State, query.Schedule, query.OldTransferOption);
    }

    public async Task<Result> Handle(UpdateScheduleQuery query, CancellationToken cancellationToken)
    {
        if (query == null || !query.IsValid())
        {
            return new Result(ErrorCodes.BadRequest, ResourceHelper.Get(nameof(SharedResource.BadRequest), query.Language));
        }

        if (await autoTransferService.IsAllDayBalanceTransfer(query.UplineId))
        {
            return new Result(ErrorCodes.ADBTChanged, ResourceHelper.Get(nameof(SharedResource.ADBTChangeError), query.Language));
        }

        return await autoTransferService.UpdateSchedule(
            query.UplineId, query.SubAccId, query.UplineRoleId, query.Language,
            query.CustId, query.Username, query.Schedule, query.OldTransferOption);
    }

    public async Task<UpdateMultipleAutoTransferResultModel> Handle(UpdateMultipleQuery query, CancellationToken cancellationToken)
    {
        if (query == null)
        {
            return new UpdateMultipleAutoTransferResultModel(ErrorCodes.BadRequest, ResourceHelper.Get(nameof(SharedResource.BadRequest), query.Language), query.Language);
        }

        if (await autoTransferService.IsAllDayBalanceTransfer(query.UplineId))
        {
            return new UpdateMultipleAutoTransferResultModel(ErrorCodes.ADBTChanged, ResourceHelper.Get(nameof(SharedResource.ADBTChangeError), query.Language), query.Language);
        }

        return await autoTransferService.UpdateMultiple(
            query.UplineId, query.SubAccId, query.UplineRoleId, query.Language,
            query.OnIds, query.OffIds, query.OnNames, query.OffNames,
            query.OnSchedule, query.OffSchedule, query.OnStatus, query.OffStatus, query.OldTransferOption);
    }
}
