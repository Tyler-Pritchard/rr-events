using Microsoft.AspNetCore.Mvc;

namespace rr_events.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return Ok(new { message = "Hello from rr-events!" });
        }
    }
}
