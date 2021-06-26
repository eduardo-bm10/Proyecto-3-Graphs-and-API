using Microsoft.AspNetCore.Mvc;

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
        private JsonResult entity;

        /// <summary>
        /// Constructor de la clase <c>Node</c> que asigna el id y crea un resultado JSON con el valor de la entidad.
        /// </summary>
        public Node(int id, int entity)
        {
            this.id = id;
            this.entity = new JsonResult(entity);
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
        /// El metodo <c>Entity</c> permite acceder a la entidad almacenada en el nodo.
        /// </summary>
        /// <returns>
        /// El atributo entity en formato JSON.
        /// </returns>
        public JsonResult Entity
        {
            get { return this.entity; }
            set { this.entity = value;}
        }
    }
}