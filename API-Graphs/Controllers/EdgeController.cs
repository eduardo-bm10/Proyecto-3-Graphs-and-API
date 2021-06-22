using System;
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
            return StatusCode(500);
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
            return StatusCode(500);
        }

        [HttpPost("{data}")]
        /// <summary>
        /// Crea una nueva arista con nodo inicial y final, y peso.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK con el id de la nueva arista si el valor de nodo inicial o final existe.
        /// Codigo de estado 404 NotFound si no se encontro ningun nodo inicial o final.
        /// Codigo de estado 500 InternalServerError si el grafo no existe.
        /// </returns>
        public IActionResult PostNewEdge([FromRoute] int id, string data)
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
        /// <summary>
        /// Actualiza los atributos de la arista identificada por el id1.
        /// </summary>
        /// <returns>
        /// Codigo de estado 500 InternalServerError si el grafo no existe.
        /// </returns>
        public IActionResult PutIdEdge([FromRoute] int id, int id1)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {

            }
            return StatusCode(500);
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
            return StatusCode(500);
        }
    }
}