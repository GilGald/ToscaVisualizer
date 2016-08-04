using System;
using System.Linq;
using Toscana;
using QuickGraph;
using QuickGraph.Algorithms.Search;

namespace ToscaVisualizer
{

    public class CloudServiceArchiveWalker
    {
        private readonly Action<string> action;
        public AdjacencyGraph<string, ToscaGraphEdge> Graph { get; set; }

        public CloudServiceArchiveWalker(ToscaCloudServiceArchive toscaCloudServiceArchive, Action<string> action)
        {
            Graph = new AdjacencyGraph<string, ToscaGraphEdge>();
            Graph.AddVertexRange(toscaCloudServiceArchive.NodeTypes.Select(_ => _.Key));
            foreach (var toscaNodeType in toscaCloudServiceArchive.NodeTypes)
            {
                if (!toscaNodeType.Value.IsRoot())
                {
                    //Graph.AddEdge(new ToscaGraphEdge(toscaNodeType.Value.DerivedFrom, toscaNodeType.Key));
                    Graph.AddEdge(new ToscaGraphEdge(toscaNodeType.Key, toscaNodeType.Value.DerivedFrom));
                }
            }

            this.action = action;
        }

        public void Walk(string rootNodeType)
        {
            var breadthFirstSearchAlgorithm = new BreadthFirstSearchAlgorithm<string, ToscaGraphEdge>(Graph);
            breadthFirstSearchAlgorithm.DiscoverVertex += breadthFirstSearchAlgorithm_DiscoverVertex;
            breadthFirstSearchAlgorithm.Compute(rootNodeType);
        }

        void breadthFirstSearchAlgorithm_DiscoverVertex(string vertex)
        {
            action(vertex);
        }
    }

    //This is came from BFS (in toscana), originally it was internal, maybe should be changed in the Nuget
    public class ToscaGraphEdge : IEdge<string>
    {
        private readonly string source;
        private readonly string target;

        public ToscaGraphEdge(string source, string target)
        {
            this.source = source;
            this.target = target;
        }

        public string Source
        {
            get { return source; }
        }

        public string Target
        {
            get { return target; }
        }
    }
}
