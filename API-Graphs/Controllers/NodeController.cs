using Microsoft.AspNetCore.Mvc;
using API_Graphs.Objects;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace API_Graphs.Controllers
{
    [ApiController]
    [Route("graphs/{id}/nodes")]
    /// <summary>
    /// La clase NodeController permite crear, eliminar, y modificar nodos contenidos en un determinado grafo.
    /// </summary>
    /// Ver <see cref="Node"/> para ver la clase Nodo.
    public class NodeController : ControllerBase
    {
        private ILogger<NodeController> _logger;
        
        /// <summary>
        /// El constructor de la clase NodeController.
        /// Asigna una instancia de tipo ILogger.
        /// </summary>
        public NodeController(ILogger<NodeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        /// <summary>
        /// Crea un nuevo nodo con la entidad brindada en el grafo indicado por el id en ruta.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK con el id del nodo si el grafo indicado existe y se logro crear el nodo.
        /// Codigo de estado 500 InternalServerError si el grafo no fue encontrado.
        /// </returns>
        public IActionResult PostNewNode([FromRoute] int id, [FromBody] JsonElement data)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            { 
                Node n = new Node(g.counterIdNode++, data.GetProperty("entity"));
                g.Nodes.Add(n);
                return Ok(n.Id);
            }
            return StatusCode(500, new JsonResult("El grafo especificado en ruta no existe."));
        }

        [HttpPut("{id1}")]
        /// <summary>
        /// Actualiza el nodo indicado por id1 y le asigna una nueva entidad.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK si tanto el grafo como el nodo fueron encontrados.
        /// Codigo de estado 404 NotFound si el nodo buscado no existe en dicho grafo.
        /// Codigo de estado 500 InternalServerError si el grafo no existe. 
        /// </returns>
        public IActionResult PutIdNode([FromRoute] int id, int id1, [FromBody] JsonElement data)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                foreach (Node n in g.Nodes)
                {
                    if (n.Id == id1)
                    {
                        n.Entity = data.GetProperty("entity");
                        return Ok();
                    }
                }
                return NotFound();
            }
            return StatusCode(500, new JsonResult("El grafo especificado en ruta no existe."));
        }

        [HttpGet]
        /// <summary>
        /// Obtiene todos los nodos en el grafo indicado por id.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK con la lista de nodos si el grafo existe.
        /// Codigo de estado 500 InternalServerError si el grafo no existe.
        /// </returns>
        public IActionResult GetAllNodes([FromRoute] int id)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                return Ok(g.Nodes);
            }
            return StatusCode(500, new JsonResult("El grafo especificado en ruta no existe."));
        }

        [HttpDelete("{id1}")]
        /// <summary>
        /// Elimina el nodo indicado por el id1 en el grafo indicado por el id.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK si se encontro el grafo y el nodo indicados, y se logro eliminar el nodo.
        /// Codigo de estado 404 NotFound si el nodo no existe.
        /// Codigo de estado 500 InternalServerError si el grafo no fue existe.
        /// </returns>
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
                return NotFound();
            }
            return StatusCode(500, new JsonResult("El grafo especificado en ruta no existe."));
        }

        [HttpDelete]
        /// <summary>
        /// Elimina todos los nodos en el grafo indicado por id.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK si se logro eliminar todos los nodos del grafo.
        /// Codigo de estado 500 InternalServerError si los nodos no se eliminaron o si el grafo no existe.
        /// </returns>
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
                    return StatusCode(500, new JsonResult("Los nodos no se eliminaron correctamente."));
                }
            }
            return StatusCode(500, new JsonResult("El grafo especificado en ruta no existe."));
        }
    }
}