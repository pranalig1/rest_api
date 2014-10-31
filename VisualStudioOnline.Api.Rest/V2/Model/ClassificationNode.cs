using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Diagnostics;

namespace VisualStudioOnline.Api.Rest.V2.Model
{
    public enum NodeType
    {
        area,
        iteration,
    }

    [DebuggerDisplay("{Name}")]
    public class ClassificationNode : BaseObject<SelfLink>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "structureType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NodeType StructureType { get; set; }

        [JsonProperty(PropertyName = "hasChildren")]
        public bool HasChildren { get; set; }

        [JsonProperty(PropertyName = "children")]
        public List<ClassificationNode> Children { get; set; }
    }
}
