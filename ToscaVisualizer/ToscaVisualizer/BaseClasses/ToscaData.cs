using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToscaVisualizer.BaseClasses
{
    public class ToscaData
    {
        public List<NodeType> NodesList { get; set; }

        public ToscaData()
        {
            NodesList = new List<NodeType>();
        }
    }
}
