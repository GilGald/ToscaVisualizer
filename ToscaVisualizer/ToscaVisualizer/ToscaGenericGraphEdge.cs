using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace ToscaVisualizer
{
    public class ToscaGenericGraphEdge : IEdge<string>
    {
        private readonly string source;
        private readonly string target;
        private readonly string edgeType;

        public string Source
        {
            get { return source; }
        }

        public string Target
        {
            get { return target; }
        }

        public string EdgeType
        {
            get { return edgeType; }
        }

        public ToscaGenericGraphEdge(string edgeType, string source, string target)
        {
            this.edgeType = edgeType;
            this.source = source;
            this.target = target;
        }

    }
}
