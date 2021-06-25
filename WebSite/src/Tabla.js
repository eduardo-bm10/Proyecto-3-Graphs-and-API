import './Tabla.css';
import React from 'react';
import Item from './Item';

function Tabla(props) {
  return (
    <div className="Tabla">
        {
            props.items.map(item =>
                <Item 
                key = {item.grafo}
                grafo = {item.grafo}
                nodo = {item.nodo}
                edge = {item.edge}
                title ={item.title}/>
            )
        }
    </div>
  );
}

export default Tabla;