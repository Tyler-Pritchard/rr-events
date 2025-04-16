using Microsoft.AspNetCore.Mvc;
using rr_events.Data;
using Microsoft.Extensions.Hosting;

namespace rr_events.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("reseed")]
        public IActionResult Reseed([FromHeader(Name = "x-admin-key")] string adminKey)
        {
            var expectedKey = Environment.GetEnvironmentVariable("ADMIN_SEED_KEY");

            if (string.IsNullOrEmpty(expectedKey) || adminKey != expectedKey)
                return Unauthorized("❌ Invalid or missing admin key.");

            try
            {
                DbInitializer.Seed(_context, _env);
                return Ok("✅ Seed executed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"🔥 Seed failed: {ex.Message}");
            }
        }
    }
}
