using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_Graphs.Objects;
using API_Graphs.Controllers;
using System.Collections.Generic;

namespace API_Graphs
{
    [ApiController]
    [Route("graphs/{id}/dijkstra")]
    /// <summary>
    /// La clase Dijkstra permite encontrar el camino mas corto entre dos nodos del grafo.
    /// </summary>
    /// Ver <see cref="Node"/> para la clase Nodo.
    /// Ver <see cref="Edge"/> para la clase Arista.
    public class Dijkstra : ControllerBase
    {
        private ILogger<Dijkstra> _logger;

        public Dijkstra(ILogger<Dijkstra> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Compara los nodos adyancentes al nodo actual para ver cual tiene menor peso.
        /// </summary>
        /// <returns>
        /// El nodo con menor peso.
        /// </returns>
        private Node GetShortestDistance(List<Node> list)
        {
            Node result = list[0];
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].vertexWeight < list[i+1].vertexWeight)
                {
                    result = list[i];
                }
                else
                {
                    result = list[i+1];
                }
            }
            return result;
        }

        /// <summary>
        /// Encuentra el camino mas corto entre dos nodos.
        /// </summary>
        /// <returns>
        /// Codigo 200 Ok con la distancia total y el camino de nodos recorrido si encuentra la ruta.
        /// Codigo 404 NotFound si no se encuentran aristas que conecten los nodos.
        /// Codigo 500 InternalServerError si el nodo inicial dado no existe.
        /// </returns>
        private IActionResult DijkstraMethod(Graph g, int start, int end)
        {
            DijkstraResult result = new DijkstraResult();
            List<Node> vertex = g.Nodes;
            List<Node> ady = new List<Node>();
            Node current = null;
            foreach (Node i in vertex)
            {
                if (i.Id == start)
                {
                    result.Path.Add(i);
                    current = i;
                    break;
                }
            }
            if (current != null)
            {
                foreach (Edge e in g.Edges)
                {
                    if (e.Start == current.Id)
                    {
                        foreach (Node next in vertex)
                        {
                            if (e.End == next.Id)
                            {
                                ady.Add(next);
                                if (e.Weight < next.vertexWeight)
                                {
                                    next.vertexWeight = e.Weight;
                                    next.previousVertex = current;
                                }
                                if (next.Id == end)
                                {
                                    result.Path.Add(next);
                                    result.Distance += next.vertexWeight;
                                    return Ok(result);
                                }
                            }
                        }
                        current = this.GetShortestDistance(ady);
                        ady.Clear();
                        result.Distance += current.vertexWeight;
                        result.Path.Add(current);
                    }
                }
                return NotFound();
            }
            return StatusCode(500, new JsonResult("No existe el nodo inicial indicado"));
        }

        [HttpGet]
        /// <summary>
        /// Verifica si los parametros indicados existen en el grafo y retorna el metodo Dijkstra en dado caso.
        /// </summary>
        /// <returns>
        /// El metodo Dijkstra.
        /// Codigo 500 InternalServerError si no existen los nodos indicados o no existe el grafo.
        /// </returns>
        public IActionResult GetDijkstraPath([FromRoute] int id, [FromQuery] int startNode, [FromQuery] int endNode)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                if (EdgeController.VerifyNodes(g, startNode, endNode))
                {
                    return this.DijkstraMethod(g, startNode, endNode);
                }
                else
                {
                    return StatusCode(500, new JsonResult("No existen los nodos dados."));
                }
            }
            return StatusCode(500, new JsonResult("No existe el grafo indicado."));
        }
    }
}