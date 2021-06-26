using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Graphs.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace API_Graphs.Controllers
{
    [ApiController]
    [Route("graphs")]
    /// <summary>
    /// La clase GraphController permite crear, eliminar y modificar los grafos.
    /// </summary>
    /// Ver <see cref="Graph"/> para ver la clase Grafo.
    public class GraphController : ControllerBase
    {
        private ILogger<GraphController> _logger;
        public static List<Graph> graphs = new List<Graph>();

        /// <summary>
        /// Constructor de la clase GraphController.
        /// Asigna una instancia de tipo ILogger.
        /// </summary>
        public GraphController(ILogger<GraphController> logger)
        {
            _logger = logger; 
        }

        /// <summary>
        /// Metodo para obtener el grafo asociado al id de la ruta.
        /// </summary>
        /// <returns>
        /// El grafo identificado por el id dado.
        /// </returns>
        public static Graph GetGraph(int id)
        {
            foreach (Graph g in graphs)
            {
                if (g.Id == id)
                {
                    return g;
                }
            }
            return null;
        }

        [HttpPost]
        /// <summary>
        /// Crea un nuevo grafo.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK con el id del nuevo grafo.
        /// </returns>
        public IActionResult PostNewGraph()
        {
            Graph g = new Graph();
            GraphController.graphs.Add(g);
            return Ok(g.Id);
        }

        [HttpGet]
        /// <summary>
        /// Obtiene todos los grafos existentes.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK con la lista de grafos en formato JSON.
        /// </returns>
        public IActionResult GetAllGraphs()
        {
            return Ok(graphs);
        }

        [HttpGet("{id}")]
        /// <summary>
        /// Obtiene el grafo identificado por el id dado.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK con el grafo si se encontro.
        /// Codigo de estado 404 NotFound si el grafo indicado no existe.
        /// </returns>
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
        /// <summary>
        /// Elimina todos los grafos existentes.
        /// </summary>
        /// <returns>
        /// Codigo de estado 204 NoContent si todos los grafos fueron eliminados.
        /// Codigo de estado 500 InternalServerError si no lograron eliminarse los grafos.
        /// </returns>
        public IActionResult DeleteAllGraphs()
        {
            GraphController.graphs.Clear();
            if (GraphController.graphs.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, new JsonResult("Los grafos no lograron eliminarse correctamente"));
            }
        }

        [HttpDelete("{id}")]
        /// <summary>
        /// Elimina el grafo asociado al id.
        /// </summary>
        /// <returns>
        /// Codigo de estado 204 NoContent si se elimino el grafo con ese id.
        /// Codigo de estado 404 NotFound si el grafo buscado no existe.
        /// </returns>
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