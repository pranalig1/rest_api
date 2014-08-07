using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace VisualStudioOnline.Api.Rest
{
    [DebuggerDisplay("{Name}")]
    public class Project
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    [DebuggerDisplay("{ReferenceName}")]
    public class SortOption
    {
        [JsonProperty(PropertyName = "refereceName")]
        public string ReferenceName { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class Query
    {
        [JsonProperty(PropertyName = "parentId")]
        public string ParentId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<Query> Children { get; set; }

        [JsonProperty(PropertyName = "project")]
        public Project Project { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "webUrl")]
        public string WebUrl { get; set; }

        [JsonProperty(PropertyName = "wiql")]
        public string QueryText { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public List<string> Columns { get; set; }

        [JsonProperty(PropertyName = "sortOptions")]
        public List<SortOption> SortOptions { get; set; }
    }

    public class QueryCollection
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<Query> Queries { get; set; }
    }
}
