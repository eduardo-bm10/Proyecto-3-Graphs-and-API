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
        public IActionResult PostNewNode(int entity)
        {
            Node n = new Node(entity);
            Graph.nodes.Add(n);
            return Ok(n.Id);
        }

        [HttpPut("{id1}/{entity}")]
        public IActionResult PutIdNode(int id1, int entity)
        {
            foreach (Node n in Graph.nodes)
            {
                if (n.Id == id1)
                {
                    n.Entity = new JsonResult(entity);
                    return Ok();
                }
            }
            return StatusCode(500);
        }

        [HttpGet]
        public IActionResult GetAllNodes()
        {
            return Ok(Graph.nodes);
        }

        [HttpDelete("{id1}")]
        public IActionResult DeleteIdNode(int id1)
        {
            foreach (Node n in Graph.nodes)
            {
                if (n.Id == id1)
                {
                    Graph.nodes.Remove(n);
                    return Ok();
                }
            }
            return StatusCode(500);
        }

        [HttpDelete]
        public IActionResult DeleteAllNodes()
        {
            Graph.nodes.Clear();
            if (Graph.nodes.Count == 0)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}