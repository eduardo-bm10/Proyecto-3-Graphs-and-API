using Microsoft.AspNetCore.Mvc;

namespace API_Graphs.Objects
{
    public class Node
    {
        private int currentId = 0;
        private int id;
        private int inDegree;
        private int outDegree;
        private JsonResult entity;

        public Node(int entity)
        {
            this.id = currentId++;
            this.entity = new JsonResult(entity);
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public JsonResult Entity
        {
            get { return this.entity; }
            set { this.entity = value;}
        }
    }
}