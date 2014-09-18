using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
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

    public class RelationTypeAttributes
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
    public class WorkItemRelationType : BaseObject
    {
        [JsonProperty(PropertyName = "referenceName")]
        public string ReferenceName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "attributes")]
        public RelationTypeAttributes Attributes { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class User : BaseObject
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Value}")]
    public class HistoryComment : BaseObject
    {
        [JsonProperty(PropertyName = "rev")]
        public int Rev { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "revisedBy")]
        public User RevisedBy { get; set; }

        [JsonProperty(PropertyName = "revisedDate")]
        public DateTime RevisedDate { get; set; }
    }

    [DebuggerDisplay("{Id:Name}")]
    public class RelationAttributes
    {
        [JsonProperty(PropertyName = "authorizedDate")]
        public DateTime AuthorizedDate { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "resourceCreatedDate")]
        public DateTime ResourceCreatedDate { get; set; }

        [JsonProperty(PropertyName = "resourceModifiedDate")]
        public DateTime ResourceModifiedDate { get; set; }

        [JsonProperty(PropertyName = "revisedDate")]
        public DateTime RevisedDate { get; set; }

        [JsonProperty(PropertyName = "resourceSize")]
        public int ResourceSize { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "isLocked")]
        public bool? IsLocked { get; set; }
    }

    [DebuggerDisplay("{Rel}")]
    public class WorkItemRelation : BaseObject
    {
        [JsonProperty(PropertyName = "rel")]
        public string Rel { get; set; }

        [JsonProperty(PropertyName = "attributes")]
        public RelationAttributes Attributes { get; set; }
    }

    [DebuggerDisplay("{Id:Rev}")]
    public abstract class WorkItemCore : BaseObject
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "rev")]
        public int Rev { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public dynamic Fields { get; set; }
    }
    
    public class WorkItem : WorkItemCore
    {
        [JsonProperty(PropertyName = "relations")]
        public List<WorkItemRelation> Relations { get; set; }
    }

    public class RelationChanges
    {
        [JsonProperty(PropertyName = "added")]
        public List<WorkItemRelation> AddedRelations { get; set; }

        [JsonProperty(PropertyName = "removed")]
        public List<WorkItemRelation> RemovedRelations { get; set; }
    }

    public class WorkItemUpdate : WorkItemCore
    {
        [JsonProperty(PropertyName = "revisedBy")]
        public User RevisedBy { get; set; }

        [JsonProperty(PropertyName = "revisedDate")]
        public DateTime RevisedDate { get; set; }

        [JsonProperty(PropertyName = "relations")]
        public RelationChanges Changes { get; set; }
    }

    [DebuggerDisplay("{Id}")]
    public class FileReference : BaseObject
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
