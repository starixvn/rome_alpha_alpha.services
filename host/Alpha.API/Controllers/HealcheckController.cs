using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

public class Controller
{
    [Route("/")]
    [ApiController]
    public class HealcheckController : ControllerBase
    {
        [HttpGet]
        public string Index() => "Healthy";
    }
}
