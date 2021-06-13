using System.Collections.Generic;

namespace API_Graphs.Objects
{
    public class Graph
    {
        private int id;
        private List<Node> nodes = new List<Node>();
        private List<Edge> edges = new List<Edge>();
        
        public Graph(int id)
        {
            this.id = id;
        }
        
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public List<Node> Nodes
        {
            get { return this.nodes; }
            set { this.nodes = value; }
        }
    }
}