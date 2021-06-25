using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API_Graphs.Objects
{
    /// <summary>
    /// La clase <c>Node</c> genera objetos de nodos.
    /// Contiene un id de nodo, el grado entrante y saliente del nodo y valor del nodo en formato JSON.
    /// </summary>
    /// Ver <see cref="NodeController"/> para la clase NodeController.
    public class Node
    {
        private int id;
        private int inDegree;
        private int outDegree;
        private JsonElement entity;
        public int vertexWeight = int.MaxValue;
        public Node previousVertex = null;
        
        /// <summary>
        /// Constructor de la clase <c>Node</c> que asigna el id y crea un resultado JSON con el valor de la entidad.
        /// </summary>
        public Node(int id, JsonElement entity)
        {
            this.id = id;
            this.entity = entity;
        }

        /// <summary>
        /// El metodo <c>Id</c> permite acceder al id del nodo.
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
        /// El metodo <c>OutDegree</c> permite acceder al grado saliente.
        /// </summary>
        /// <returns>
        /// El grado saliente del nodo.
        /// </returns>
        public int OutDegree
        {
            get { return this.outDegree; }
            set { this.outDegree = value; }
        }

        /// <summary>
        /// El metodo <c>InDegree</c> permite acceder al grado entrante.
        /// </summary>
        /// <returns>
        /// El grado entrante del nodo.
        /// </returns>
        public int InDegree
        {
            get { return this.inDegree; }
            set { this.inDegree = value; }
        }

        /// <summary>
        /// El metodo <c>Entity</c> permite acceder a la entidad almacenada en el nodo.
        /// </summary>
        /// <returns>
        /// El atributo entity en formato JSON.
        /// </returns>
        public JsonElement Entity
        {
            get { return this.entity; }
            set { this.entity = value; }
        }
    }
}