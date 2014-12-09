using Newtonsoft.Json;
using System.Diagnostics;

namespace VisualStudioOnline.Api.Rest.V1.Model
{
    [DebuggerDisplay("{Name}")]
    public class Repository : ObjectWithId<string>
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("project")]
        public TeamProject Project { get; set; }

        [JsonProperty("defaultBranch")]
        public string DefaultBranch { get; set; }

        [JsonProperty("remoteUrl")]
        public string RemoteUrl { get; set; }
    }
}
