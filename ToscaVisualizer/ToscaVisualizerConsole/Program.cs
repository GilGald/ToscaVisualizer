using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToscaVisualizer;

namespace ToscaVisualizerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\oren.c\Desktop\ToscaProj\Examples\tosca.zip";

            var toscaJson = Builder.GetToscaZipAsJson(path);

            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Tosca\jsonTry.json");
            file.WriteLine(toscaJson);
            file.Close();

        }
    }
}
