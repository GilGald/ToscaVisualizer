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

        public NodeType()
        {
            Artifact = new Artifact();
            Requirements = new List<Requirement>();
        }
    }
}
