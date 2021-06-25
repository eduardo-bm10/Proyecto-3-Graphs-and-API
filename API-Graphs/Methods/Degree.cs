using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using API_Graphs.Objects;
using System.Collections.Generic;
using API_Graphs.Controllers;

namespace API_Graphs.Methods
{
    [ApiController]
    [Route("graphs/{id}/degree")]
    /// <summary>
    /// La clase Degree se encarga de brindar un endpoint para obtener una lista ordenada de nodos dependiendo del
    /// parametro sort brindado en el metodo GetDegree.
    /// </summary>
    /// Ver <see cref="Node"/> para ver la clase Nodo.
    public class Degree : ControllerBase
    {
        private ILogger<Degree> _logger;
        public Degree(ILogger<Degree> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Ordena la lista de nodos proporcionada de mayor a menor grado promedio.
        /// </summary>
        /// <returns>
        /// La lista ordenada.
        /// </returns>
        private List<Node> DescBubbleSort(List<Node> list)
        {
            int sum1;
            int sum2;
            Node tmp;
            int size = list.Count;
            while (size > 0)
            {
                for (int i = 0; i < size - 1; i++)
                {
                    sum1 = list[i].InDegree + list[i].OutDegree;
                    sum2 = list[i+1].InDegree + list[i+1].OutDegree;
                    if (sum1 < sum2)
                    {
                        tmp = list[i];
                        list[i] = list[i+1];
                        list[i+1] = tmp;
                    }
                }
                size--;
            }
            return list;
        }

        /// <summary>
        /// Ordena la lista de nodos proporcionada de menor a mayor grado promedio.
        /// </summary>
        /// <returns>
        /// La lista ordenada.
        /// </returns>
        private List<Node> AscBubbleSort(List<Node> list)
        {
            int sum1;
            int sum2;
            Node tmp;
            int size = list.Count;
            while (size > 0)
            {
                for (int i = 0; i < size - 1; i++)
                {
                    sum1 = list[i].InDegree + list[i].OutDegree;
                    sum2 = list[i+1].InDegree + list[i+1].OutDegree;
                    if (sum1 > sum2)
                    {
                        tmp = list[i];
                        list[i] = list[i+1];
                        list[i+1] = tmp;
                    }
                }
                size--;
            }
            return list;
        }

        [HttpGet]
        /// <summary>
        /// Obtiene la lista de nodos ordenada por grado promedio.
        /// </summary>
        /// <returns>
        /// Codigo de estado 200 OK con la lista de nodos ordenada.
        /// Codigo de estado 500 InternalServerError si el grafo especificado no existe.
        /// Codigo de estado 500 InternalServerError si el parametro sort no es valido.
        /// </returns>
        public IActionResult GetDegree([FromRoute] int id, [FromQuery] string sort)
        {
            Graph g = GraphController.GetGraph(id);
            if (g != null)
            {
                if (g.Nodes.Count == 0)
                {
                    return StatusCode(500, new JsonResult("No hay nodos en el grafo"));
                }
                else
                {
                    List<Node> orderedList;
                    if (sort.Equals("DESC"))
                    {
                        orderedList = this.DescBubbleSort(g.Nodes);
                    }
                    else if (sort.Equals("ASC"))
                    {
                        orderedList = this.AscBubbleSort(g.Nodes);
                    }
                    else
                    {
                        return StatusCode(500, new JsonResult("El parametro sort ingresado no es valido."));
                    }
                    return Ok(orderedList);
                }
            }
            return StatusCode(500, new JsonResult("El grafo indicado no existe."));
        }
    }
}