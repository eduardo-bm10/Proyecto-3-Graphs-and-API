using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Graphs.Objects;
using Microsoft.Extensions.Logging;

namespace API_Graphs.Controllers
{
    [ApiController]
    [Route("graphs")]
    public class GraphController : ControllerBase
    {
        private ILogger<GraphController> _logger;
        public static List<Graph> graphs = new List<Graph>();

        public GraphController(ILogger<GraphController> logger)
        {
            _logger = logger; 
        }

        [HttpPost]
        public IActionResult PostNewGraph()
        {
            Graph g = new Graph();
            GraphController.graphs.Add(g);
            return Ok(g.Id);
        }

        [HttpGet]
        public IActionResult GetAllGraphs()
        {
            return Ok(graphs);
        }

        [HttpGet("{id}")]
        public IActionResult GetIdGraph(int id)
        {
            foreach (Graph g in GraphController.graphs)
            {
                if (id == g.Id)
                {
                    return Ok(g);
                }
            }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteAllGraphs()
        {
            GraphController.graphs.Clear();
            if (GraphController.graphs.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIdGraph(int id)
        {
            foreach (Graph g in graphs)
            {
                if (g.Id == id)
                {
                    graphs.Remove(g);
                    return NoContent();
                }
            }
            return NotFound();
        }
    }
}