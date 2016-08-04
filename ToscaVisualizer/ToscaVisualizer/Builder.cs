using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Toscana;
using ToscaVisualizer.BaseClasses;

namespace ToscaVisualizer
{
    public class Builder
    {
        public static NodeType BuildNodeTypeData(ToscaNodeType toscanaNodeType, string nodeTypeName)
        {
            var nodeTypeToReturn = new NodeType();
            nodeTypeToReturn.Name = nodeTypeName.Substring(nodeTypeName.LastIndexOf('.') + 1);
            nodeTypeToReturn.FullName = nodeTypeName;
            nodeTypeToReturn.SourceName = toscanaNodeType.DerivedFrom.Substring(toscanaNodeType.DerivedFrom.LastIndexOf('.') + 1); ;
            nodeTypeToReturn.SourceFullName = toscanaNodeType.DerivedFrom;
            if (toscanaNodeType.Artifacts.ContainsKey("driver"))
            {
                nodeTypeToReturn.Artifact.Driver.Name = toscanaNodeType.Artifacts["driver"].File;
            }
            if (toscanaNodeType.Artifacts.ContainsKey("icon"))
            {
                nodeTypeToReturn.Artifact.Icon.Image = toscanaNodeType.Artifacts["icon"].File;
            }

            for (var curNodeType = toscanaNodeType; curNodeType != null; curNodeType = curNodeType.Base)
            {
                if (curNodeType.Requirements.Any())
                    nodeTypeToReturn.Requirements.AddRange(curNodeType.Requirements.SelectMany(r => r)
                        .Select(rq => new Requirement
                        {
                            Name = rq.Key,
                            Capability = rq.Value.Capability,
                            NodeFullName = rq.Value.Node,
                            Relationaship = rq.Value.Relationship,
                            Occurences = rq.Value.Occurrences
                        }));

                if (curNodeType.Properties.Any())
                    nodeTypeToReturn.Properties.AddRange(curNodeType.Properties.Select(r => r)
                        .Select(pr => new Property
                        {
                            Name = pr.Key,
                            Description = pr.Value.Description,
                            Constraints = pr.Value.Constraints,
                            Default = pr.Value.Default,
                            EntrySchema = pr.Value.EntrySchema,
                            Status = pr.Value.Status.ToString(),
                            Tags = pr.Value.Tags,
                            Required = pr.Value.Required,
                            Type = pr.Value.Type,
                        }));

                if (curNodeType.Capabilities.Any())
                {
                    nodeTypeToReturn.Capabilities.AddRange(curNodeType.Capabilities.Select(r => r)
                        .Select(cb => new Capability
                        {
                            Name = cb.Key,
                            Description = cb.Value.Description,
                            Type = cb.Value.Type,
                            Properties = Property.ConvertToLocalProperty(cb.Value.Properties),
                            Attributes = Capability.ConvertToLocalAttribute(cb.Value.Attributes)
                        }));
                }

                if (curNodeType.Attributes.Any())
                    nodeTypeToReturn.Attributes.AddRange(curNodeType.Attributes.Select(r => r)
                        .Select(attr => new ToscaVisualizerAttribute
                        {
                            Name = attr.Key,
                            Type = attr.Value.Type,
                            Description = attr.Value.Description,
                            EntrySchema = attr.Value.EntrySchema,
                            Default = attr.Value.Default,
                            Status = attr.Value.Status.ToString(),
                        }));
            }
            //MergeRequirements(toscanaNodeType.Requirements.Select(s => s.Values), nodeTypeToReturn);

            //MergeCapabilities(toscanaNodeType);

            return nodeTypeToReturn;
        }

        private static string ObjectToJson(object toConvert)
        {
            return JsonConvert.SerializeObject(toConvert);

        }

        public static string GetToscaZipAsJson(string path)
        {
            ToscaCloudServiceArchive toscaCloudServiceArchive = ToscaCloudServiceArchive.Load(path);

            return ReturnToscaJson(toscaCloudServiceArchive);
        }

        public static string GetToscaStreamAsJson(Stream stream)
        {
            ToscaCloudServiceArchive toscaCloudServiceArchive = ToscaCloudServiceArchive.Load(stream);

            return ReturnToscaJson(toscaCloudServiceArchive);
        }

        private static string ReturnToscaJson(ToscaCloudServiceArchive toscaCloudServiceArchive)
        {
            var toscaData = new ToscaData();
            var requiremnetList = new List<Requirement>();

            foreach (
                var keyValuePair in
                    toscaCloudServiceArchive.NodeTypes.Where(n => n.Value.IsRoot() && n.Key != ToscaDefaults.ToscaNodesRoot))
            {
                keyValuePair.Value.DerivedFrom = ToscaDefaults.ToscaNodesRoot;
            }

            var nodeTypeWalker = new ToscaCloudServiceArchiveWalker(toscaCloudServiceArchive, (nodeTypeName, nodeType) =>
            {
                if (!nodeType.IsRoot())
                {
                    var parentNode = toscaCloudServiceArchive.NodeTypes[nodeType.DerivedFrom];
                    //MergeCapabilities(parentNode, toscanaNodeType);
                    //MergeProperties(parentNode, toscanaNodeType);
                    //MergeInterfaces(parentNode, toscanaNodeType);
                    toscaData.NodesList.Add(BuildNodeTypeData(nodeType, nodeTypeName));
                    //MergeAttributes(parentNode, toscanaNodeType);
                }
            });
            nodeTypeWalker.Walk();

            return ObjectToJson(toscaData);
        }
    }
}
