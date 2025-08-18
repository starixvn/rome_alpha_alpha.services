using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Alpha.API.Sample.Models;
using Alpha.API.Client.Services;
using Alpha.API.Client.Exceptions;
using Alpha.API.Client.Models.Customers;
using System.Diagnostics.CodeAnalysis;

namespace Alpha.API.Sample.Controllers;

// Justification: This controller is excluded from code coverage as it only contains presentation logic and delegates all business logic to services.
[ExcludeFromCodeCoverage(Justification = "This controller is excluded from code coverage as it only contains presentation logic and delegates all business logic to services.")]
public class HomeController : Controller
{
    private readonly ICustomerService customerService;
    private readonly IOutstandingService outstandingService;
    private readonly IBalanceInfoService balanceInfoService;
    private readonly IWinLossService winLossService;

    public HomeController(
        ICustomerService customerService,
        IOutstandingService outstandingService,
        IBalanceInfoService balanceInfoService,
        IWinLossService winLossService)
    {
        this.customerService = customerService;
        this.outstandingService = outstandingService;
        this.balanceInfoService = balanceInfoService;
        this.winLossService = winLossService;
    }

    public async Task<IActionResult> Outstanding()
    {
        const int superId = 17_996_398;
        const byte superLevelId = 4;
        const int masterId = 18_004_021;
        const string language = "th-TH";
        try
        {
            ViewData["DownlineOutstanding"] = await outstandingService.GetDownlineOutstanding(superId, superLevelId, 1, masterId, language);
        }
        catch (MinimizeUMException ex)
        {
            Console.WriteLine(ex.ToString());
        }

        const int agentId = 23_003_322;
        const byte agentLevel = 2;
        const int top = 5;
        ViewData["TopOutstanding"] = await outstandingService.GetTopOutstanding(agentId, agentLevel, top);

        ViewData["TopOutstandingUsername"] = await outstandingService.GetTopUsername(agentId, agentLevel);

        return View();
    }

    public async Task<IActionResult> BalanceInfo()
    {
        const int superId = 1_176_070;
        const byte superLevel = 4;

        ViewData["MyBalance"] = await balanceInfoService.GetMyBalance(superId, superLevel);

        const int masterId = 23_722_864;
        const byte masterLevel = 3;
        ViewData["DownlineBalance"] = await balanceInfoService.GetDownlineBalance(masterId, masterLevel, "10.18.200.55");

        ViewData["TopBalance"] = await balanceInfoService.GetTopBalance(superId, superLevel);

        ViewData["TopUsername"] = await balanceInfoService.GetTopUsername(superId, superLevel);

        return View();
    }

    public async Task<IActionResult> TodayWinLoss()
    {
        const int superId = 17_996_398;
        const byte superLevel = 4;
        var today = DateTime.Today;

        ViewData["TodayWinLoss"] = await winLossService.GetWinLoss(superId, superLevel, today);

        const int masterId = 23_722_864;
        ViewData["TodayDownlineWinLoss"] = await winLossService.GetDownlineWinLoss(superId, superLevel, today, masterId);

        ViewData["TopUsername"] = await winLossService.GetTopUsername(superId, superLevel, today);

        return View();
    }

    public async Task<IActionResult> YesterdayWinLoss()
    {
        const int superId = 17_996_398;
        const byte superLevel = 4;
        var yesterday = DateTime.Today.AddDays(-1);

        ViewData["YesterdayWinLoss"] = await winLossService.GetWinLoss(superId, superLevel, yesterday);

        const int masterId = 23_722_864;
        ViewData["YesterdayDownlineWinLoss"] = await winLossService.GetDownlineWinLoss(superId, superLevel, yesterday, masterId);

        ViewData["TopUsername"] = await winLossService.GetTopUsername(superId, superLevel, yesterday);

        return View();
    }

    public async Task<IActionResult> Customer()
    {
        const int superId = 1_176_070;

        ViewData["customerStatus"] = await customerService.GetCustomerStatusByBatch(new[]
        {
            new CustomerStatusParam(23_003_140, 0),
            new CustomerStatusParam(1_176_070, 0),
            new CustomerStatusParam(1_176_070, 87_413),
            new CustomerStatusParam(1_176_083, 0),
            new CustomerStatusParam(33_278_659, 0),
            new CustomerStatusParam(33_278_689, 0),
            new CustomerStatusParam(33_278_717, 0),
            new CustomerStatusParam(1_176_086, 0)
        });

        const int superSubAccId = 98_583;
        ViewData["superStatus"] = await customerService.GetCustomerStatus(superId, 0);
        ViewData["superSubAccStatus"] = await customerService.GetCustomerStatus(superId, superSubAccId);

        const string master = "master1";
        ViewData["master"] = await customerService.GetDownlineInfo(superId, master);
        const string masterSubAcc = "master1sub01";
        ViewData["masterSubAcc"] = await customerService.GetDownlineInfo(superId, masterSubAcc);
        const string masterInvalid = "masterxxxxxxxxxx";
        ViewData["masterInvalid"] = await customerService.GetDownlineInfo(superId, masterInvalid);

        const string agent = "agent01";
        ViewData["agent"] = await customerService.GetDownlineInfo(superId, agent);
        const string agentSubAcc = "AGENT01Sub04";
        ViewData["agentSubAcc"] = await customerService.GetDownlineInfo(superId, agentSubAcc);
        const string agentInvalid = "agentxxxxxxxxxx";
        ViewData["agentInvalid"] = await customerService.GetDownlineInfo(superId, agentInvalid);

        const string member = "member01";
        ViewData["member"] = await customerService.GetDownlineInfo(superId, member);
        const string memberInvalid = "memberxxxxxxxxxx";
        ViewData["memberInvalid"] = await customerService.GetDownlineInfo(superId, memberInvalid);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
