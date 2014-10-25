using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;

namespace VisualStudioOnline.Api.Rest.V2.Model
{
    public enum ProjectState
    {
        WellFormed,
        CreatePending,
        Deleting,
        New,
        All
    }

    [DebuggerDisplay("{Name}")]
    public class Team : ObjectWithId<string>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class Versioncontrol
    {
        [JsonProperty(PropertyName = "sourceControlType")]
        public string Type { get; set; }
    }

    public class ProcessTemplate
    {
        [JsonProperty(PropertyName = "templateName")]
        public string Name { get; set; }
    }

    public class Capabilities
    {
        [JsonProperty(PropertyName = "versioncontrol")]
        public Versioncontrol VersionControl { get; set; }

        [JsonProperty(PropertyName = "processTemplate")]
        public ProcessTemplate ProcessTemplate { get; set; }
    }

    public class TeamProjectReference
    {
        [JsonProperty(PropertyName = "self")]
        public ObjectReference Self { get; set; }

        [JsonProperty(PropertyName = "collection")]
        public ObjectReference Collection { get; set; }

        [JsonProperty(PropertyName = "web")]
        public ObjectReference Web { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class TeamProject : ObjectWithId<Guid, TeamProjectReference>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectState State { get; set; }

        [JsonProperty(PropertyName = "capabilities")]
        public Capabilities Capabilities { get; set; }

        [JsonProperty(PropertyName = "defaultTeam")]
        public Team DefaultTeam { get; set; }
    }

    public class TeamProjectCollectionReference
    {
        [JsonProperty(PropertyName = "self")]
        public ObjectReference Self { get; set; }

        [JsonProperty(PropertyName = "web")]
        public ObjectReference Web { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class TeamProjectCollection : ObjectWithId<string, TeamProjectCollectionReference>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}
