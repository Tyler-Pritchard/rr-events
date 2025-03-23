using Microsoft.AspNetCore.Mvc;
using rr_events.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;

namespace rr_events.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DbTestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DbTestController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                var connection = (NpgsqlConnection)_context.Database.GetDbConnection();
                var dbName = connection.Database;
                var connectionString = connection.ConnectionString;

                return Ok(new
                {
                    message = "Connection successful.",
                    database = dbName,
                    connectionString
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Connection failed.", error = ex.Message });
            }
        }
    }
}
