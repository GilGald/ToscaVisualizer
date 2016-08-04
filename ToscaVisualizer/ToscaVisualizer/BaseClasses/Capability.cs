using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toscana;

namespace ToscaVisualizer.BaseClasses
{
    public class Capability
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        //public Dictionary<string, Property> Properties { get; set; }
        //public  Dictionary<string, ToscaVisualizerAttribute> Attributes { get; set; }
        public List<Property> Properties { get; set; }
        public List<ToscaVisualizerAttribute> Attributes { get; set; }

        public Capability()
        {
            Properties = new List<Property>();//Dictionary<string, Property>();
            Attributes = new List<ToscaVisualizerAttribute>();//Dictionary<string, ToscaVisualizerAttribute>();
        }


        public static List<ToscaVisualizerAttribute> ConvertToLocalAttribute(Dictionary<string, ToscaAttributeDefinition> outerAttributes)
        {
            if (outerAttributes == null) return null;

            var returnDic = new List<ToscaVisualizerAttribute>();

            foreach (var outerAttribute in outerAttributes)
            {
                var attribute = new ToscaVisualizerAttribute
                {
                    Name = outerAttribute.Key,
                    Type = outerAttribute.Value.Type,
                    EntrySchema = outerAttribute.Value.EntrySchema,
                    Default = outerAttribute.Value.Default.ToString(),
                    Status = outerAttribute.Value.Status.ToString(),
                    Description = outerAttribute.Value.Description
                };

                returnDic.Add(attribute);
            }

            return returnDic;
        }
    }
    
}
