$(function () { // on dom ready


    $("#uploadBtn")
        .click(function () {
            var formdata = new FormData();
            var file = $("#uploadfile")[0].files[0];
            formdata.append("Uploaded", file);

            $.ajax({
                url: '../api/Tosca/Upload',
                type: 'POST',
                data: formdata,
                success: function (data) {
                    addElements(createNodes(data), createEdges(data));
                },
                cache: false,
                contentType: false,
                processData: false
            });
        });


    // We can attach the `fileselect` event to all file inputs on the page
    $(document).on('change', ':file', function () {
        var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [numFiles, label]);
    });

    // We can watch for our custom `fileselect` event like this
    $(document).ready(function () {
        $(':file').on('fileselect', function (event, numFiles, label) {

            var input = $(this).parents('.input-group').find(':text'),
                log = numFiles > 1 ? numFiles + ' files selected' : label;

            if (input.length) {
                input.val(log);
            } else {
                if (log) alert(log);
            }

        });
    });






    var cy = cytoscape({
        container: document.getElementById('cy'),
        boxSelectionEnabled: true,
        autounselectify: false,
        zoom: 3,
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
                  'line-color': 'black'
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


    function addElements(nodes, edges) {
        cy.add(nodes);
        cy.add(edges);
    }

    function createNodes(json) {
        var nodes = [];
        for (var i = 0; i < json.NodesList.length; i++) {
            nodes.push(
            {
                group: "nodes",
                data: { id: json.NodesList[i].Name },
                position: { x: 25 * (i + 1), y: 25 * (i + 1) }
            });
        }

        return nodes;
    }

    function createEdges(json) {

        var edges = [];

        for (var i = 0; i < json.NodesList.length; i++) {
            edges.push(
            {
                group: "edges",
                data: { id: 'e' + i, source: json.NodesList[i].Name, target: json.NodesList[i].SourceName }
            });
        }

        return edges;
    }


}); // on dom ready
