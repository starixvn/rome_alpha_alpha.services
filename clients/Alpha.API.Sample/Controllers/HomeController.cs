using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Alpha.API.Sample.Models;
using Alpha.API.Client.Services;

namespace Alpha.API.Sample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly ICustomerService customerService;
    private readonly IReportService reportService;

    public HomeController(
        ILogger<HomeController> logger,
        ICustomerService customerService,
        IReportService reportService)
    {
        this.logger = logger;
        this.customerService = customerService;
        this.reportService = reportService;
    }

    public async Task<IActionResult> Outstanding()
    {
        var superId = 23003140;
        var superName = "SupertestTE01";
        byte superLevel = 4;
        ViewData["MyOutstanding"] = await reportService.GetMyOutstandingAsync(superId, 0, superLevel);

        // var superSubAccId = 920715;
        // var superSubAccName = "SUPERTESTTE01Sub99";
        // ViewData["MyOutstandingSubAcc"] = await reportService.GetMyOutstandingAsync(superId, superSubAccId, custLevel);
        
        var agentId = 23003322;
        var agentName = "SupertestTE010101";
        byte agentLevel = 2;
        var memberId = 23003440;
        var memberName = "SupertestTE010101001";
        ViewData["DownlineOutstanding"] = await reportService.GetDownlineOutstandingAsync(agentId, 0, agentLevel, memberId, "en-US");
        
        ViewData["TopOutstanding"] = await reportService.GetTopOutstandingAsync(agentId, 0, agentLevel, 5);

        ViewData["TopOutstandingUsername"] = await reportService.GetTopOutstandingUsernameAsync(agentId, 0, agentLevel);

        return View();
    }

    public IActionResult BalanceInfo()
    {
        return View();
    }

    public IActionResult TodayWinLoss()
    {
        return View();
    }

    public IActionResult YesterdayWinLoss()
    {
        return View();
    }

    public async Task<IActionResult> Customer()
    {
        var superId = 1176070;
        var superName = "supertest1";

        ViewData["superStatus"] = await customerService.GetCustomerStatusAsync(1176070, 0);
        ViewData["superSubAccStatus"] = await customerService.GetCustomerStatusAsync(1176070, 98583);

        var master = "master1";
        ViewData["master"] = await customerService.GetDownlineInfoAsync(superId, master);
        var masterSubAcc = "master1sub01";
        ViewData["masterSubAcc"] = await customerService.GetDownlineInfoAsync(superId, masterSubAcc);
        var masterInvalid = "masterxxxxxxxxxx";
        ViewData["masterInvalid"] = await customerService.GetDownlineInfoAsync(superId, masterInvalid);

        var agent = "agent01";
        ViewData["agent"] = await customerService.GetDownlineInfoAsync(superId, agent);
        var agentSubAcc = "AGENT01Sub04";
        ViewData["agentSubAcc"] = await customerService.GetDownlineInfoAsync(superId, agentSubAcc);
        var agentInvalid = "agentxxxxxxxxxx";
        ViewData["agentInvalid"] = await customerService.GetDownlineInfoAsync(superId, agentInvalid);

        var member = "member01";
        ViewData["member"] = await customerService.GetDownlineInfoAsync(superId, member);
        var memberInvalid = "memberxxxxxxxxxx";
        ViewData["memberInvalid"] = await customerService.GetDownlineInfoAsync(superId, memberInvalid);

        var subAccHasReportPermission = 520850;
        ViewData["subAccHasReportPermission"] = await customerService.IsSubAccHasReportPermissionAsync(1176083, subAccHasReportPermission);
        var subAccNoReportPermission = 91726;
        ViewData["subAccNoReportPermission"] = await customerService.IsSubAccHasReportPermissionAsync(1176083, subAccNoReportPermission);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
