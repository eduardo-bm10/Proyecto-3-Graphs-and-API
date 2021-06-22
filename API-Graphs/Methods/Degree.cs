using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace API_Graphs.Methods
{
    [ApiController]
    [Route("graphs/{id}/degree")]
    public class Degree : ControllerBase
    {
        private ILogger<Degree> _logger;
        public Degree(ILogger<Degree> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetDegree([FromRoute] int id)
        {
            return Ok();
        }
    }
}