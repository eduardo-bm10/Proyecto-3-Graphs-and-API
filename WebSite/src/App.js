import React from 'react';
import './App.css';
import Menu from './Menu';
import Tabla from './Tabla';


class App extends React.Component{

  constructor(props){
    super(props);
    this.state = {
      books:[
        {grafo: 0, nodo: 1, edge:3, title:"Primer Grafo"},
        {grafo: 0, nodo: 1, edge:3, title:"Segundo Grafo"}
      ]
    };
  }

  render(){
    return (
    <div className="App">
        <Menu title="Rest API"/>
        <Tabla items= {this.state.books}/>
      </div>
    );
  }
}

export default App;
