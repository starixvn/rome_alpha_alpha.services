using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Alpha.API.Sample.Models;
using Alpha.API.Client.Services;
using Alpha.API.Client.Exceptions;
using Alpha.API.Client.Models.Customers;

namespace Alpha.API.Sample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly ICustomerService customerService;
    private readonly IOutstandingService outstandingService;
    private readonly IBalanceInfoService balanceInfoService;
    private readonly IWinLossService winLossService;

    public HomeController(
        ILogger<HomeController> logger,
        ICustomerService customerService,
        IOutstandingService outstandingService,
        IBalanceInfoService balanceInfoService,
        IWinLossService winLossService)
    {
        this.logger = logger;
        this.customerService = customerService;
        this.outstandingService = outstandingService;
        this.balanceInfoService = balanceInfoService;
        this.winLossService = winLossService;
    }

    public async Task<IActionResult> Outstanding()
    {
        var superId = 23003140;
        var superName = "SupertestTE01";
        byte superLevel = 4;
        // var superSubAccId = 920715;
        // var superSubAccName = "SUPERTESTTE01Sub99";
        // ViewData["MyOutstandingSubAcc"] = await reportService.GetMyOutstandingAsync(superId, superSubAccId, custLevel);
        
        var agentId = 23003322;
        var agentName = "SupertestTE010101";
        byte agentLevel = 2;
        var memberId = 23003440;
        var memberName = "SupertestTE010101001";
        try
        {
            ViewData["DownlineOutstanding"] = await outstandingService.GetDownlineOutstanding(17996398, 4, 18004021, "en-US");
        }
        catch (MinimizeUMException ex)
        {
            Console.WriteLine(ex.ToString());
        }
        
        ViewData["TopOutstanding"] = await outstandingService.GetTopOutstanding(agentId, agentLevel, 5);

        ViewData["TopOutstandingUsername"] = await outstandingService.GetTopUsername(agentId, agentLevel);

        return View();
    }

    public async Task<IActionResult> BalanceInfo()
    {
        //var superId = 23003140;
        var superId = 1176070;
        var superName = "SupertestTE01";
        byte superLevel = 4;

        ViewData["MyBalance"] = await balanceInfoService.GetMyBalance(superId, superLevel);
        
        var masterId = 23722864;
        ViewData["DownlineBalance"] = await balanceInfoService.GetDownlineBalance(superId, superLevel, masterId);
        
        ViewData["TopBalance"] = await balanceInfoService.GetTopBalance(superId, superLevel);
        
        ViewData["TopUsername"] = await balanceInfoService.GetTopUsername(superId, superLevel);

        return View();
    }

    public async Task<IActionResult> TodayWinLoss()
    {
        var superId = 17996398;
        var superName = "SupertestTE01";
        byte superLevel = 4;
        var today = DateTime.Today;

        ViewData["TodayWinLoss"] = await winLossService.GetWinLoss(superId, superLevel, today);
        
        var masterId = 23722864;
        ViewData["TodayDownlineWinLoss"] = await winLossService.GetDownlineWinLoss(superId, superLevel, today, masterId);
                
        ViewData["TopUsername"] = await winLossService.GetTopUsername(superId, superLevel, today);

        return View();
    }

    public async Task<IActionResult> YesterdayWinLoss()
    {
        var superId = 17996398;
        var superName = "SupertestTE01";
        byte superLevel = 4;
        var yesterday = DateTime.Today.AddDays(-1);

        ViewData["YesterdayWinLoss"] = await winLossService.GetWinLoss(superId, superLevel, yesterday);
        
        var masterId = 23722864;
        ViewData["YesterdayDownlineWinLoss"] = await winLossService.GetDownlineWinLoss(superId, superLevel, yesterday, masterId);
                
        ViewData["TopUsername"] = await winLossService.GetTopUsername(superId, superLevel, yesterday);

        return View();
    }

    public async Task<IActionResult> Customer()
    {
        var superId = 1176070;
        var superName = "supertest1";

        ViewData["customerStatus"] = await customerService.GetCustomerStatusByBatch(new[]
        {
            new CustomerStatusParam(23003140, 0),
            new CustomerStatusParam(1176070, 0),
            new CustomerStatusParam(1176070, 87413),
            new CustomerStatusParam(1176083, 0),
            new CustomerStatusParam(33278659, 0),
            new CustomerStatusParam(33278689, 0),
            new CustomerStatusParam(33278717, 0),
            new CustomerStatusParam(1176086, 0)
        });

        ViewData["superStatus"] = await customerService.GetCustomerStatus(1176070, 0);
        ViewData["superSubAccStatus"] = await customerService.GetCustomerStatus(1176070, 98583);

        var master = "master1";
        ViewData["master"] = await customerService.GetDownlineInfo(superId, master);
        var masterSubAcc = "master1sub01";
        ViewData["masterSubAcc"] = await customerService.GetDownlineInfo(superId, masterSubAcc);
        var masterInvalid = "masterxxxxxxxxxx";
        ViewData["masterInvalid"] = await customerService.GetDownlineInfo(superId, masterInvalid);

        var agent = "agent01";
        ViewData["agent"] = await customerService.GetDownlineInfo(superId, agent);
        var agentSubAcc = "AGENT01Sub04";
        ViewData["agentSubAcc"] = await customerService.GetDownlineInfo(superId, agentSubAcc);
        var agentInvalid = "agentxxxxxxxxxx";
        ViewData["agentInvalid"] = await customerService.GetDownlineInfo(superId, agentInvalid);

        var member = "member01";
        ViewData["member"] = await customerService.GetDownlineInfo(superId, member);
        var memberInvalid = "memberxxxxxxxxxx";
        ViewData["memberInvalid"] = await customerService.GetDownlineInfo(superId, memberInvalid);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
