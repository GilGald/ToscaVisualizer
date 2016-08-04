using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToscaVisualizer.BaseClasses
{
    public class Requirement
    {
        public string NodeName { get; set; }
        public string NodeFullName { get; set; }
        public string Capability { get; set; }
        public string Relationaship { get; set; }
        public object[] Occurences { get; set; }
    }
}
