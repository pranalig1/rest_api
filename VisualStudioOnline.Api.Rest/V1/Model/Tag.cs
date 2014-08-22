using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace VisualStudioOnline.Api.Rest
{
    public class TagCollection
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<Tag> Tags { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class Tag
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
