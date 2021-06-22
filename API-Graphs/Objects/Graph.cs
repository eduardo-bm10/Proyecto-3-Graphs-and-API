using System.Collections.Generic;

namespace API_Graphs.Objects
{
    /// <summary>
    /// La clase <c>Graph</c> genera objetos de grafos.
    /// Contiene un id, una lista de nodos y de aristas.
    /// </summary>
    /// Ver <see cref="GraphController"/> para la clase GraphController.
    public class Graph
    {
        private static int currentId = 0; 
        public int counterIdNode = 0;
        public int counterIdEdge = 0;
        private int id;
        private List<Node> nodes = new List<Node>();
        private List<Edge> edges = new List<Edge>();
        
        /// <summary>
        /// Constructor de la clase <c>Graph</c> que suma el id cada vez que se crea un nuevo grafo.
        /// </summary>
        public Graph()
        {
            this.id = currentId++;
        }
        
        /// <summary>
        /// El metodo <c>Id</c> permite acceder al atributo id del grafo.
        /// </summary>
        /// <returns>
        /// El valor del id.
        /// </returns>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// El metodo <c>Nodes</c> permite acceder a la lista de nodos del grafo.
        /// </summary>
        /// <returns>
        /// La lista de nodos.
        /// </returns>
        public List<Node> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }

        /// <summary>
        /// El metodo <c>Nodes</c> permite acceder a la lista de aristas del grafo.
        /// </summary>
        /// <returns>
        /// La lista de aristas.
        /// </returns>
        public List<Edge> Edges
        {
            get { return this.edges; }
            set {this.edges = value; }
        }
    }
}