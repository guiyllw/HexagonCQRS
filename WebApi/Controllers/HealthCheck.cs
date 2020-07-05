using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/health")]
    [ApiController]
    public class HealthCheck : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Health()
        {
            return Ok("I'm fine =)");
        }
    }
}
