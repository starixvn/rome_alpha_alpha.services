using Alpha.Reports.Models.WinLoss;
using MediatR;

namespace Alpha.API.Requests.Reports.WinLoss;

public record GetEnteredResultQuery : IRequest<EnteredResult>;