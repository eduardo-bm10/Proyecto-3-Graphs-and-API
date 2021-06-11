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
        private int currentSize = 0;
        public GraphController(ILogger<GraphController> logger)
        {
            _logger = logger; 
        }

        [HttpPost]
        public JsonResult CreateNewGraph()
        {
            Graph newGraph = new Graph();
            this.currentSize++;
            graphs.Add(newGraph);
            if (GraphController.graphs.Count == this.currentSize)
            {
                return new JsonResult(newGraph.Id);
            }
            else
            {
                return new JsonResult(StatusCode(500));
            }
        }

        [HttpGet]
        public List<Graph> GetAllGraphs()
        {
            if (graphs.Count == 0)
            {
                Ok();
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
            if (GraphController.graphs.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}