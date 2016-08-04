using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToscaVisualizer.BaseClasses
{
    public class Artifact
    {
        public Driver Driver { get; set; }
        public Icon Icon { get; set; }

        public Artifact()
        {
            Driver = new Driver();
            Icon = new Icon();
        }
    }
}
