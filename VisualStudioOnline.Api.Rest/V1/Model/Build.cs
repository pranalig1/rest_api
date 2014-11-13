using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace VisualStudioOnline.Api.Rest.V1.Model
{
    [DebuggerDisplay("{Name}")]
    public class Queue : ObjectWithId<int>
    {
        [JsonProperty(PropertyName = "queueType")]
        public string QueueType { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class LastBuild
    {
        public int id { get; set; }
        public string url { get; set; }
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
        public LastBuild LastBuild { get; set; }

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
}
