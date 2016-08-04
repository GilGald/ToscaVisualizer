using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms.Search;
using Toscana;

namespace ToscaVisualizer
{
    public class ToscaCloudServiceArchiveWalker
    {
        private readonly Action<string, ToscaNodeType> action;
        private readonly AdjacencyGraph<string, ToscaGenericGraphEdge> graph;
        private readonly IReadOnlyDictionary<string, ToscaNodeType> nodeTypes;

        public ToscaCloudServiceArchiveWalker(ToscaCloudServiceArchive cloudServiceArchive, Action<string, ToscaNodeType> action)
        {
            nodeTypes = cloudServiceArchive.NodeTypes;
            graph = new AdjacencyGraph<string, ToscaGenericGraphEdge>();
            graph.AddVertexRange(cloudServiceArchive.NodeTypes.Select(_ => _.Key));
            foreach (var toscaNodeType in cloudServiceArchive.NodeTypes)
            {
                if (!toscaNodeType.Value.IsRoot())
                {
                    graph.AddEdge(new ToscaGenericGraphEdge(
                        "derived_from",
                        toscaNodeType.Value.DerivedFrom,
                        toscaNodeType.Key));
                }
                //foreach (var requirementKeyValue in toscaNodeType.Value.Requirements.SelectMany(r => r).ToArray())
                //{
                //    graph.AddEdge(new ToscaGenericGraphEdge(
                //        "requirement",
                //        toscaNodeType.Key,
                //        requirementKeyValue.Value.Node));
                //}
            }

            this.action = action;
        }

        public void Walk()
        {
            var breadthFirstSearchAlgorithm = new BreadthFirstSearchAlgorithm<string, ToscaGenericGraphEdge>(graph);
            breadthFirstSearchAlgorithm.DiscoverVertex += breadthFirstSearchAlgorithm_DiscoverVertex;
            breadthFirstSearchAlgorithm.Compute(ToscaDefaults.ToscaNodesRoot);
        }

        void breadthFirstSearchAlgorithm_DiscoverVertex(string vertex)
        {
            action(vertex, nodeTypes[vertex]);
        }

    }
}
