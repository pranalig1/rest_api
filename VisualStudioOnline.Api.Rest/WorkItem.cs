using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace VisualStudioOnline.Api.Rest
{
    [DebuggerDisplay("{Source.Id} -> {Target.Id}")]
    public class Link
    {
        [JsonProperty(PropertyName = "linkType")]
        public string LinkType { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "locked")]
        public bool IsLocked { get; set; }

        [JsonProperty(PropertyName = "target")]
        public WorkItem Target { get; set; }

        [JsonProperty(PropertyName = "source")]
        public WorkItem Source { get; set; }
    }

    [DebuggerDisplay("{ReferenceName}")]
    public class FieldMetadata
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "refName")]
        public string ReferenceName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    [DebuggerDisplay("{Metadata.ReferenceName}={Value}")]
    public class Field
    {
        [JsonProperty(PropertyName = "field")]
        public FieldMetadata Metadata { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }

    [DebuggerDisplay("{Id}")]
    public class WorkItem
    {
        public WorkItem()
        {
            Fields = new List<Field>();
            Links = new List<Link>();
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "rev")]
        public int Rev { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "webUrl")]
        public string WebUrl { get; set; }

        [JsonProperty(PropertyName = "updatesUrl")]
        public string UpdatesUrl { get; set; }
        
        public List<Field> Fields { get; set; }

        public List<Link> Links { get; set; }

        public Field GetField(string refName)
        {
            return Fields.FirstOrDefault(f => f.Metadata.ReferenceName == refName);
        }

        public string GetFieldValue(string refName)
        {
            return GetField(refName) != null ? GetField(refName).Value : null;
        }

        public Field SetFieldValue(string refName, string value)
        {
            Field field = GetField(refName);

            if (field == null)
            {
                field = new Field() { Metadata = new FieldMetadata() { ReferenceName = refName } };
                Fields.Add(field);
            }

            field.Value = value;

            return field;
        }
    }

    public class WorkItemCollection
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<WorkItem> WorkItems { get; set; }
    }
}
