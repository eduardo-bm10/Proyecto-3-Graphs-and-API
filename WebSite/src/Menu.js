import './Menu.css';
import React from 'react';
import Search from './Search';

function Menu(props) {
  return (
      <div className ="container">
          <div className="subcontainer">
              <div className ="logo">
                  {props.title}
              </div>
              <div className = "Search">
              <Search />

              </div>
              <div className = "actions">
                   <button className="button btn-blue">
                   +Add Graph
                   </button>
              </div>
              <div className = "Search">
              <Search />

              </div>
              <div className = "actions">
                  
                   <button className="button btn-red">
                   +Add  Nodo
                   </button>
              </div>
              <div className = "Search">
              <Search />

              </div>
              <div className = "actions">
                  
                   <button className="button btn-yellow">
                   +Add Edge
                   </button>
              </div>


          </div>


      </div>
  );
}

export default Menu;