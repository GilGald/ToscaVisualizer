$(function(){ // on dom ready


var urlOfJson="https://raw.githubusercontent.com/GilGald/ToscaVisualizer/master/Client/jsonTry.json"

 function loadJSON(callback) {   

    var xobj = new XMLHttpRequest();
        xobj.overrideMimeType("application/json");
    xobj.open('GET', urlOfJson, true); // Replace 'my_data' with the path to your file
    xobj.onreadystatechange = function () {
          if (xobj.readyState == 4 && xobj.status == "200") {
            // Required use of an anonymous callback as .open will NOT return a value but simply returns undefined in asynchronous mode
            callback(xobj.responseText);
          }
    };
    xobj.send(null);  
 }
 
 





var cy = cytoscape({
  container: document.getElementById('cy'),  
  boxSelectionEnabled: true,
  autounselectify: false,  
  
  style: [
    {
      selector: 'node',
      css: {
        'content': 'data(id)',
        'text-valign': 'center',
        'text-halign': 'center',
		'background-color': 'black',
		'color': 'white',
		'font-size': '6px'
      }
    },
    {
      selector: '$node > node',
      css: {
        'padding-top': '5px',
        'padding-left': '5px',
        'padding-bottom': '5px',
        'padding-right': '5px',
        'text-valign': 'top',
        'text-halign': 'center',
        'background-color': 'blue'
      }
    },
    {
      selector: 'edge',
      css: {
        'target-arrow-shape': 'triangle',
        'curve-style': 'bezier',
		'line-color': 'pink'
      }
    },
    {
      selector: ':selected',
      css: {
        'background-color': 'red',
        'line-color': 'red',
        'target-arrow-color': 'red',
        'source-arrow-color': 'red'
      }
    }
  ],
  
 
  
  layout: {
    name: 'preset',
    padding: 450
  }
});



init();
function addElements(nodes,edges) {	
	cy.add(nodes);
	cy.add(edges);	
}		

function createNodes(json){
	var nodes=[];
	for (var i=0;i<json.NodesList.length;i++){
		nodes.push(
		{
		group: "nodes",
		data: { id: json.NodesList[i].Name}, 
		position: { x: 25*i, y: 25*i }
		});
	}

	return nodes;
}

function createEdges(json){

	var edges=[];
	
	for (var i=0;i<json.NodesList.length;i++){
		edges.push(
		{
		group: "edges",
		data: { id: 'e'+i, source: json.NodesList[i].Name, target: json.NodesList[i].SourceName}
		});
	}
	
	return edges;		
}


 function init() { 
	 json=loadJSON(function(response) {
	  // Parse JSON string into object
		var actual_JSON = JSON.parse(response);
		addElements(createNodes(actual_JSON),createEdges(actual_JSON));		
	 });
	 
}
 
 

	
}); // on dom ready
