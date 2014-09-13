﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Diagnostics;

namespace VisualStudioOnline.Api.Rest.V2.Model
{
    public enum TopologyType
    {
        dependency,
        network,
        tree
    }

    public class Attributes
    {
        [JsonProperty(PropertyName = "usage")]
        public string Usage { get; set; }

        [JsonProperty(PropertyName = "editable")]
        public bool Editable { get; set; }

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        [JsonProperty(PropertyName = "acyclic")]
        public bool Acyclic { get; set; }

        [JsonProperty(PropertyName = "directional")]
        public bool Directional { get; set; }

        [JsonProperty(PropertyName = "singleTarget")]
        public bool SingleTarget { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "topology")]
        public TopologyType Topology { get; set; }
    }

    [DebuggerDisplay("{ReferenceName}")]
    public class WorkItemRelationType
    {
        [JsonProperty(PropertyName = "referenceName")]
        public string ReferenceName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class WorkItemRelationTypeCollection
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
        
        [JsonProperty(PropertyName = "value")]
        public List<WorkItemRelationType> Relations { get; set; }
    }
}