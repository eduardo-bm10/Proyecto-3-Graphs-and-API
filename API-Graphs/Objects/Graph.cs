using System.Collections.Generic;

namespace API_Graphs.Objects
{
    public class Graph
    {
        private static int currentId = 0; 
        public int currentNodeId = 0;
        private int id;
        private List<Node> nodes = new List<Node>();
        private List<Edge> edges = new List<Edge>();
        
        public Graph()
        {
            this.id = currentId++;
        }
        
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public List<Node> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }

        public List<Edge> Edges
        {
            get { return this.edges; }
            set {this.edges = value; }
        }
    }
}