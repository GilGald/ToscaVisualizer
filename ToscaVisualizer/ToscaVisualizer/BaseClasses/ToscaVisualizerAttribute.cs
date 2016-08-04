using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToscaVisualizer.BaseClasses
{
    public class ToscaVisualizerAttribute
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public object Default { get; set; }
        public string Status { get; set; }
        public string EntrySchema { get; set; }
    }
}
