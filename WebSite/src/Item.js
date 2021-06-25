import React from 'react';

function Item(props) {
//Añadir una html para la parte grafica
  return (
    <div className = "item"> 
        <div className="title">{props.title}</div>
        <div className="grafo">{props.grafo}</div>
        <div className="nodo">{props.nodo}</div>
        <div className="edge">{props.edge}</div>

        <div className="actions">
            <button>Delete</button>
        </div>


    
    </div>
  );
}

export default Item;