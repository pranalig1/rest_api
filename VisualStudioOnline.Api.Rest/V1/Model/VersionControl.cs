using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace VisualStudioOnline.Api.Rest.V1.Model
{
    [DebuggerDisplay("{Path}")]
    public class Branch : BaseObject
    {
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public UserIdentity Owner { get; set; }

        [JsonProperty(PropertyName = "createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty(PropertyName = "relatedBranches")]
        public List<Branch> RelatedBranches { get; set; }

        [JsonProperty(PropertyName = "children")]
        public List<Branch> Children { get; set; }

        [JsonProperty(PropertyName = "mappings")]
        public List<object> Mappings { get; set; }

        [JsonProperty(PropertyName = "isDeleted")]
        public bool IsDeleted { get; set; }
    }

    public class LabelReference
    {
        [JsonProperty(PropertyName = "self")]
        public ObjectReference Self { get; set; }

        [JsonProperty(PropertyName = "items")]
        public ObjectReference Items { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public ObjectReference Owner { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class Label : ObjectWithId<int>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "labelScope")]
        public string LabelScope { get; set; }

        [JsonProperty(PropertyName = "modifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public UserIdentity Owner { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public LabelReference References { get; set; }
    }

    [DebuggerDisplay("{Path}")]
    public class VersionControlItem : BaseObject
    {
        [JsonProperty(PropertyName = "version")]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "isFolder")]
        public bool IsFolder { get; set; }
    }
}
