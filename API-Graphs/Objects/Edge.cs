namespace API_Graphs.Objects
{
    public class Edge
    {
        private int id;
        private int start;
        private int end;
        private int weight;

        public Edge(int id, int start, int end, int weight)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.weight = weight; 
        }
    }
}