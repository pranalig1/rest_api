using Newtonsoft.Json;
using System.Collections.Generic;

namespace VisualStudioOnline.Api.Rest.V2.Model
{
    public class JsonCollection<T> where T : class
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<T> Items { get; set; }

        public T this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }
    }

    public abstract class BaseObject
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
