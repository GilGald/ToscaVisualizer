$(function(){ // on dom ready


 function loadJSON(callback) {   

    var xobj = new XMLHttpRequest();
        xobj.overrideMimeType("application/json");
    xobj.open('GET', 'https://gist.githubusercontent.com/mbostock/4063269/raw/e87e5bbd6ba610d3f693dd42c00c7fc583fda5aa/flare.json', true); // Replace 'my_data' with the path to your file
    xobj.onreadystatechange = function () {
          if (xobj.readyState == 4 && xobj.status == "200") {
            // Required use of an anonymous callback as .open will NOT return a value but simply returns undefined in asynchronous mode
            callback(xobj.responseText);
          }
    };
    xobj.send(null);  
 }
 
 
 
 function init() {
 loadJSON(function(response) {
  // Parse JSON string into object
    var actual_JSON = JSON.parse(response);
	alert(actual_JSON);
 });
}
 
 
 
init();



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
  
  elements: {
    nodes: [
		{ data: { id: 'Root'}, position: { x: 0, y: 85 } },
		{ data: { id: 'Network'}, position: { x: 100, y: 85 } },
		{ data: { id: 'Switch'}, position: { x: 200, y: 85 } },
		{ data: { id: 'NXOS'}, position: { x: 300, y: 85 } },	  		
		{ data: { id: 'Port'}, position: { x: 400, y: 85 } },	  		
    ],
    edges: [
		{ data: { id: 'NX->sw', source: 'NXOS', target: 'Switch' } } ,     
		{ data: { id: 'sw->NT', source: 'Switch', target: 'Network' } } ,     
		/*{ data: { id: 'nt->sw', source: 'Network', target: 'Switch' } } ,     */
		{ data: { id: 'nt->rt', source: 'Network', target: 'Root' } } ,     		
		{ data: { id: 'NX->sw', source: 'NXOS', target: 'Switch' } } ,     
		{ data: { id: 'NX->req1', source: 'NXOS', target: 'Port' } } ,     
    ]
  },
  
  layout: {
    name: 'preset',
    padding: 155
  }
});



addElements();


function addElements() {
cy.add({
    group: "nodes",
    data: { id: 'TTT'}, 
    position: { x: 250, y: 150 }
});

cy.add({
    group: "edges",
    data: { id: 'NX->TT', source: 'NXOS', target: 'TTT'},     
});
	
}	
	
	
}); // on dom ready