namespace API_Graphs.Objects
{
    public class Node
    {
        private int currentId = 0;
        private int id;
        private int inDegree;
        private int outDegree;
        private string entity;

        public Node()
        {
            this.id = currentId++;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}