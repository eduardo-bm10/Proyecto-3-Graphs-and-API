using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Graphs.Objects;
using Microsoft.Extensions.Logging;

namespace API_Graphs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {
        private ILogger<GraphController> _logger;
        private static List<Graph> graphs = new List<Graph>();
        public GraphController(ILogger<GraphController> logger)
        {
            _logger = logger; 
        }

        [HttpPost]
        public JsonResult CreateNewGraph()
        {
            Graph newGraph = new Graph();
            JsonResult info = new JsonResult(newGraph.Id);
            graphs.Add(newGraph);

            return info;
        }

        [HttpGet]
        public List<Graph> GetAllGraphs()
        {
            if (graphs.Count == 0)
            {
                Ok(graphs);
            }
            return graphs;
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
            return NoContent();
        }
    }
}