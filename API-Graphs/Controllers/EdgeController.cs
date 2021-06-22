using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_Graphs.Objects;

namespace API_Graphs.Controllers
{
    [ApiController]
    [Route("graphs/{id}/edges")]
    /// <summary>
    /// La clase EdgeController permite crear, eliminar y modificar aristas en el grafo indicado.
    /// </summary>
    /// Ver <see cref="Edge"/> para la clase de Arista.
    public class EdgeController : ControllerBase
    {
        private ILogger<EdgeController> _logger;

        /// <summary>
        /// Constructor de la clase EdgeController.
        /// Asigna una instancia de tipo ILogger a la clase.
        /// </summary>
        public EdgeController(ILogger<EdgeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Verifica si existen ambos nodos, inicial y final, para crear la arista entre ellos.
        /// </summary>
        /// <returns>
        /// Verdadero si ambos nodos para los valores start y end existen.
        /// Falso si no existe al menos uno de los dos nodos. 
        /// </returns>
        public bool VerifyNodes(Graph g, int start, int end)
        {
            foreach (Node n1 in g.Nodes)
            {
                if (n1.Id == start)
                {
                    foreach (Node n2 in g.Nodes)
                    {
                        if (n2.Id == end)
                        {
                            return true;
                        }                        
                    }
                    return false; 
                }
            }
            return false;
        }

        [HttpGet]
        /// <summary>
        /// Obtiene todas las aristas en el grafo indicado.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK con la lista de aristas si el grafo existe.
        /// Codigo de estado 500 InternalServerError si el grafo no existe.
        /// </returns>
        public IActionResult GetAllEdges([FromRoute] int id)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                return Ok(g.Edges);
            }
            return StatusCode(500, new JsonResult("El grafo especificado en ruta no existe."));
        }

        [HttpDelete]
        /// <summary>
        /// Elimina todas las aristas en el grafo indicado.
        /// </summary>
        /// <returns>
        /// Codigo de estado 404 NotFound si existe ninguna arista en el grafo.
        /// Codigo de estado 200 OK si se lograron eliminar todas las aristas.
        /// Codigo de estado 500 InternalServerError si el grafo no existe.
        /// </returns>
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
            return StatusCode(500, new JsonResult("El grafo especificado en ruta no existe."));
        }

        [HttpPost]
        /// <summary>
        /// Crea una nueva arista con nodo inicial y final, y peso.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK con el id de la nueva arista si el valor de nodo inicial o final existe.
        /// Codigo de estado 500 InternalServerError si no se encontro ningun nodo inicial o final.
        /// Codigo de estado 500 InternalServerError si el grafo no existe.
        /// </returns>
        public IActionResult PostNewEdge([FromRoute] int id, [FromBody] JsonElement data)
        {
            Graph g = GraphController.GetGraph(id);
            if (!(this.VerifyNodes(g, data.GetProperty("startNode").GetInt32(), data.GetProperty("endNode").GetInt32())))
            {
                return StatusCode(500, new JsonResult("No existen los nodos inicial y final especificados."));
            }
            if (g != null)
            {
                Edge e = new Edge(g.counterIdEdge++, data.GetProperty("startNode").GetInt32(), data.GetProperty("endNode").GetInt32(), data.GetProperty("weight").GetInt32());
                g.Edges.Add(e);
                return Ok(e.Id);   
            }
            return StatusCode(500, new JsonResult("El grafo especificado en ruta no existe."));
        }

        [HttpPut("{id1}")]
        /// <summary>
        /// Actualiza los atributos de la arista identificada por el id1.
        /// </summary>
        /// <returns>
        /// Codigo de estado 500 InternalServerError si el grafo no existe.
        /// </returns>
        public IActionResult PutIdEdge([FromRoute] int id, int id1, [FromBody] JsonElement data)
        {
            Graph g = GraphController.GetGraph(id);
            if (!(this.VerifyNodes(g, data.GetProperty("startNode").GetInt32(), data.GetProperty("endNode").GetInt32())))
            {
                return StatusCode(500, new JsonResult("Los nodos indicados como inicial y final no existen"));
            }
            if (g != null)
            {
                foreach (Edge e in g.Edges)
                {
                    if (e.Id == id1)
                    {
                        e.Start = data.GetProperty("startNode").GetInt32();
                        e.End = data.GetProperty("endNode").GetInt32();
                        e.Weight = data.GetProperty("weight").GetInt32();
                        return Ok();
                    }
                }
                return NotFound();
            }
            return StatusCode(500, new JsonResult("El grafo especificado en ruta no existe."));
        }

        [HttpDelete("{id1}")]
        /// <summary>
        /// Elimina la arista identificada por el id1.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK si se logro eliminar la arista.
        /// Codigo de estado 404 NotFound si no se encontro la arista.
        /// Codigo de estado 500 InternalServerError si el grafo no existe. 
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
            return StatusCode(500, new JsonResult("El grafo especificado en ruta no existe."));
        }
    }
}