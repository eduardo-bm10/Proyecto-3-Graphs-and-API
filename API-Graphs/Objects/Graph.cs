using System.Collections.Generic;

namespace API_Graphs.Objects
{
    public class Graph
    {
        private int currentId = 0;
        private int id;
        private List<Node> nodes;
        private List<Edge> edges;
        
        public Graph()
        {
            this.id = currentId++;
        }

        public Graph(int id)
        {
            this.id = id;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}