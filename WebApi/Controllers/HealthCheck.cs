using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/health")]
    [ApiController]
    public class HealthCheck : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Health()
        {
            return Ok("I'm fine =)");
        }
    }
}
