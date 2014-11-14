using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace VisualStudioOnline.Api.Rest.V1.Model
{
    public enum BuildStatus
    {
        All,
        Cancelled,
        Completed,
        InProgress,
        None,
        Postponed,
        Queued,
        Retry
    }

    public enum BuildReason
    {
        BatchedCI,
        CheckInShelveset,
        IndividualCI,
        Manual,
        None,
        Schedule,
        ScheduleForced,
        Triggered,
        UserCreated,
        ValidateShelveset
    }

    public enum BuildPriority
    {
        Normal,
        AboveNormal,
        BelowNormal,
        High,
        Low
    }

    [DebuggerDisplay("{Name}")]
    public class Queue : ObjectWithId<int>
    {
        [JsonProperty(PropertyName = "queueType")]
        public string QueueType { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class Build : ObjectWithId<int>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class BuildDefinition : ObjectWithId<int>
    {
        [JsonProperty(PropertyName = "batchSize")]
        public int BatchSize { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "queue")]
        public Queue Queue { get; set; }

        [JsonProperty(PropertyName = "triggerType")]
        public string TriggerType { get; set; }

        [JsonProperty(PropertyName = "defaultDropLocation")]
        public string DefaultDropLocation { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "buildArgs")]
        public string BuildArgs { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty(PropertyName = "supportedReasons")]
        public string SupportedReasons { get; set; }

        [JsonProperty(PropertyName = "lastBuild")]
        public Build LastBuild { get; set; }

        [JsonProperty(PropertyName = "definitionType")]
        public string DefinitionType { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "continuousIntegrationQuietPeriod")]
        public int? ContinuousIntegrationQuietPeriod { get; set; }

        [JsonProperty(PropertyName = "queueStatus")]
        public string QueueStatus { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class BuildQueue : ObjectWithId<int>
    {
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        [JsonProperty(PropertyName = "createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty(PropertyName = "updatedDate")]
        public DateTime UpdatedDate { get; set; }

        [JsonProperty(PropertyName = "queueType")]
        public string QueueType { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Uri}")]
    public class BuildRequest : ObjectWithId<int>
    {
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "queue")]
        public Queue Queue { get; set; }

        [JsonProperty(PropertyName = "definition")]
        public BuildDefinition Definition { get; set; }

        [JsonProperty(PropertyName = "builds")]
        public List<Build> Builds { get; set; }

        [JsonProperty(PropertyName = "customGetVersion")]
        public string CustomGetVersion { get; set; }

        [JsonProperty(PropertyName = "priority")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BuildPriority Priority { get; set; }

        [JsonProperty(PropertyName = "queuePosition")]
        public int QueuePosition { get; set; }

        [JsonProperty(PropertyName = "queueTime")]
        public DateTime QueueTime { get; set; }

        [JsonProperty(PropertyName = "reason")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BuildReason Reason { get; set; }

        [JsonProperty(PropertyName = "requestedBy")]
        public UserIdentity RequestedBy { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BuildStatus Status { get; set; }

        [JsonProperty(PropertyName = "project")]
        public TeamProject Project { get; set; }

        [JsonProperty(PropertyName = "requestedFor")]
        public UserIdentity RequestedFor { get; set; }

        [JsonProperty(PropertyName = "shelvesetName")]
        public string ShelvesetName { get; set; }

        [JsonProperty(PropertyName = "requestDropLocation")]
        public string RequestDropLocation { get; set; }
    }
}
