using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using API_Graphs.Objects;
using Microsoft.Extensions.Logging;

namespace API_Graphs.Controllers
{
    [ApiController]
    [Route("graphs/{id}/nodes")]
    public class NodeController : ControllerBase
    {
        private ILogger<NodeController> _logger;
        
        public NodeController(ILogger<NodeController> logger)
        {
            _logger = logger;
        }

        [HttpPost("{entity}")]
        public IActionResult PostNewNode(int entity, [FromRoute] int id)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            { 
                Node n = new Node(g.counterIdNode++, entity);
                g.Nodes.Add(n);
                return Ok(n.Id);
            }
            return NotFound();
        }

        [HttpPut("{id1}/{entity}")]
        public IActionResult PutIdNode([FromRoute] int id, int id1, int entity)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                foreach (Node n in g.Nodes)
                {
                    if (n.Id == id1)
                    {
                        n.Entity = new JsonResult(entity);
                        return Ok();
                    }
                }
                return StatusCode(500);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult GetAllNodes([FromRoute] int id)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                return Ok(g.Nodes);
            }
            return NotFound();
        }

        [HttpDelete("{id1}")]
        public IActionResult DeleteIdNode([FromRoute] int id, int id1)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                foreach (Node n in g.Nodes)
                {
                    if (n.Id == id1)
                    {
                        g.Nodes.Remove(n);
                        return Ok();
                    }
                }
                return StatusCode(500);
            }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteAllNodes([FromRoute] int id)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                g.Nodes.Clear();
                if (g.Nodes.Count == 0)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500);
                }
            }
            return NotFound();
        }
    }
}