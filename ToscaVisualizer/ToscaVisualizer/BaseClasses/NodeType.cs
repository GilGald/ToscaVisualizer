using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToscaVisualizer.BaseClasses
{
    public class NodeType
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string SourceFullName { get; set; } // inherited from
        public string SourceName { get; set; } // inherited from
        public Artifact Artifact { get; set; }
        public List<Requirement> Requirements { get; set; }
        public List<Property> Properties { get; set; }
        public List<Capability> Capabilities { get; set; }
        public List<ToscaVisualizerAttribute> Attributes { get; set; }

        public NodeType()
        {
            Artifact = new Artifact();
            Requirements = new List<Requirement>();
            Properties = new List<Property>();
            Capabilities = new List<Capability>();
            Attributes = new List<ToscaVisualizerAttribute>();
        }
    }
}
