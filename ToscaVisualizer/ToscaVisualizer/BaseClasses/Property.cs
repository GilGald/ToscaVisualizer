using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toscana;

namespace ToscaVisualizer.BaseClasses
{
    public class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public object Default { get; set; }
        public string Status { get; set; }
        public List<Dictionary<string, object>> Constraints { get; set; }
        public string EntrySchema { get; set; }
        public List<string> Tags { get; set; }


        public static List<Property> ConvertToLocalProperty(Dictionary<string, ToscaPropertyDefinition> outerProperties)
        {
            if (outerProperties == null) return null;

            var returnDic = new List<Property>();//new Dictionary<string, Property>();

            foreach (var outerProperty in outerProperties)
            {
                var property = new Property
                {
                    Name = outerProperty.Key,
                    Type = outerProperty.Value.Type,
                    Description = outerProperty.Value.Description,
                    Required = outerProperty.Value.Required,
                    Default = outerProperty.Value.Type,
                    Constraints = outerProperty.Value.Constraints,
                    EntrySchema = outerProperty.Value.EntrySchema,
                    Status = outerProperty.Value.Status.ToString(),
                    Tags = outerProperty.Value.Tags
                };


                returnDic.Add(property);
            }

            return returnDic;
        }
    }
}
