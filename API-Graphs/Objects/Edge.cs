namespace API_Graphs.Objects
{
    /// <summary>
    /// La clase <c>Edge</c> genera objetos aristas.
    /// Contiene un id de arista, un valor para el nodo inicial y final, y su peso.
    /// </summary>
    /// Ver <see cref="EdgeController"/> para la clase EdgeController.
    public class Edge
    {
        private int id;
        private int start;
        private int end;
        private int weight;

        /// <summary>
        /// Constructor de la clase <c>Edge</c> que asigna sus respectivos atributos.
        /// </summary>
        public Edge(int id, int start, int end, int weight)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.weight = weight; 
        }
        
        /// <summary>
        /// El metodo <c>Id</c> permite acceder al id de la arista.
        /// </summary>
        /// <returns>
        /// El valor del id.
        /// </returns>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}