using Microsoft.AspNetCore.Mvc;

namespace OzzyBank_Demo.Api.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("api/user")]
        public IActionResult Get()
        {
            return Ok(new {name = "Nick"});
        }
    }
}
