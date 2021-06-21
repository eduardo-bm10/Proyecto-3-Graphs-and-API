using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_Graphs.Objects;

namespace API_Graphs.Controllers
{
    [ApiController]
    [Route("graphs/{id}/edges")]
    public class EdgeController : ControllerBase
    {
        private ILogger<EdgeController> _logger;

        public EdgeController(ILogger<EdgeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllEdges([FromRoute] int id)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                return Ok(g.Edges);
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteAllEdges([FromRoute] int id)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                if (g.Edges.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    g.Edges.Clear();
                    return Ok();
                } 
            }
            return StatusCode(500);
        }

        [HttpPost]
        public IActionResult PostNewEdge([FromRoute] int id)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                Edge e = new Edge(g.counterIdEdge++, new Random().Next(int.MaxValue), new Random().Next(int.MaxValue), new Random().Next(100));
                foreach (Node n in g.Nodes)
                {
                    if (n.Id == e.Start || n.Id == e.End)
                    {
                        g.Edges.Add(e);
                        return Ok(e.Id);
                    }
                }
                return NotFound();
            }
            return StatusCode(500);
        }

        [HttpPut("{id1}")]
        public IActionResult PutIdEdge([FromRoute] int id, int id1)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {

            }
            return StatusCode(500);
        }

        [HttpDelete("{id1}")]
        public IActionResult DeleteIdEdges([FromRoute] int id, int id1)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                foreach (Edge e in g.Edges)
                {
                    if (e.Id == id1)
                    {
                        g.Edges.Remove(e);
                        return Ok();
                    }
                }
                return NotFound();
            }
            return StatusCode(500);
        }
    }
}