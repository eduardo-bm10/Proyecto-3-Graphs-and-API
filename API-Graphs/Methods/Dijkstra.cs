using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_Graphs
{
    [ApiController]
    [Route("graphs/{id}/dijkstra")]
    public class Dijkstra : ControllerBase
    {
        public ILogger<Dijkstra> _logger;

        public Dijkstra(ILogger<Dijkstra> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetDijkstraPath([FromRoute] int id)
        {
            return StatusCode(500);
        }
    }
}