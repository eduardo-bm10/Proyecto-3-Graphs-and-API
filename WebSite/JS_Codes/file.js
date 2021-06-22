// Code goes here
sigma.utils.pkg('sigma.canvas.edges');
sigma.canvas.edges.t = function(edge, source, target, context, settings) {
  if(edge.relationship !== 'COUPLE') {
    // console.log(edge, source, target, context, settings)
    var color = edge.color,
        prefix = settings('prefix') || '',
        edgeColor = settings('edgeColor'),
        defaultNodeColor = settings('defaultNodeColor'),
        defaultEdgeColor = settings('defaultEdgeColor');
  
    if (!color)
      switch (edgeColor) {
        case 'source':
          color = source.color || defaultNodeColor;
          break;
        case 'target':
          color = target.color || defaultNodeColor;
          break;
        default:
          color = defaultEdgeColor;
          break;
      }
  
    context.strokeStyle = color;
    context.lineWidth = edge[prefix + 'size'] || 1;
    context.beginPath();
    context.moveTo(
      source[prefix + 'x'],
      source[prefix + 'y']
    );
    context.lineTo(
      source[prefix + 'x'],
      target[prefix + 'y']
    );
    context.lineTo(
      target[prefix + 'x'],
      target[prefix + 'y']
    );
    context.stroke();
  }
  else {
    return 'def'
  }
  
};

// s.canvas.labels.t = function (node, context, settings) {
  
// }

// Add a method to the graph model that returns an
// object with every neighbors of a node inside:
sigma.classes.graph.addMethod('neighbors', function(nodeId) {
  var k,
    neighbors = {},
    index = this.allNeighborsIndex[nodeId] || {};

  for (k in index)
    neighbors[k] = this.nodesIndex[k];

  return neighbors;
});

sigma.parsers.json(
  'data.json',
  { // Here is the ID of the DOM element that
    // will contain the graph:
    renderer: {
      container: document.getElementById('container'),
      type: 'canvas',
    },
    settings: {
      edgeColor: 'default',
      defaultNodeColor: '#ec5148',
      defaultEdgeColor: '#dbc09b',
      font: "calibri",
      labelSize: "proportional",
      boderSize: 1,
      sideMargin: 20,
      zoomMax: 1
    }
  },
  function(s) {
    s.graph.nodes().forEach(function(n){
      n.type = 'square';
    });
    // We first need to save the original colors of our
    // nodes and edges, like this:
    s.graph.nodes().forEach(function(n) {
      n.originalColor = n.color;
    });
    s.graph.edges().forEach(function(e) {
      e.originalColor = e.color;
    });

    // When a node is clicked, we check for each node
    // if it is a neighbor of the clicked one. If not,
    // we set its color as grey, and else, it takes its
    // original color.
    // We do the same for the edges, and we only keep
    // edges that have both extremities colored.
    s.bind('overNode', function(e) {
      var nodeId = e.data.node.id,
          toKeep = s.graph.neighbors(nodeId);
      toKeep[nodeId] = e.data.node;

      s.graph.nodes().forEach(function(n) {
        if (toKeep[n.id])
          n.color = n.originalColor;
        else
          n.color = '#eee';
      });

      s.graph.edges().forEach(function(e) {
        if (toKeep[e.source] && toKeep[e.target])
          e.color = e.originalColor;
        else
          e.color = '#eee';
      });
      s.refresh()
    });
    // When the stage is clicked, we just color each
    // node and edge with its original color.
    s.bind('outNode', function(e) {
      s.graph.nodes().forEach(function(n) {
        n.color = n.originalColor;
      });

      s.graph.edges().forEach(function(e) {
        e.color = e.originalColor;
      });

      // Same as in the previous event:
      s.refresh();
    });

    s.startForceAtlas2();
    
    setTimeout(function () {
      console.log('stopping')
      s.stopForceAtlas2()
      }, 10000)
    
    // setInterval(addNode, 3000)
  }
);

function addNode () {
  
}