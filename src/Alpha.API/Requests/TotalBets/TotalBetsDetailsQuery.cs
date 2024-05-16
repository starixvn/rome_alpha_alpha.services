using Alpha.Reports.Models.TotalBets;
using MediatR;

namespace Alpha.API.Requests.TotalBets;

public class TotalBetsDetailsQuery : IRequest<TotalBetsDetailsModel> 
{
    public int CustId { get; set; }
    public int SubAccId { get; set; }   
    public int CustomerLevel { get; set; }   
    public string? Language { get; set; }
    public int MatchId { get; set; }  
    public bool IsApplyPT { get; set; }  
}