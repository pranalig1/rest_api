using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Diagnostics;

namespace VisualStudioOnline.Api.Rest.V2.Model
{
    public enum FieldType
    {
        boolean,
        dateTime,
        @double,
        history,
        html,
        integer,
        plainText,
        @string,
        treePath                        
    }

    [DebuggerDisplay("{ReferenceName}")]
    public class Field
    {
        [JsonProperty(PropertyName = "referenceName")]
        public string ReferenceName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "type")]
        public FieldType Type { get; set; }

        [JsonProperty(PropertyName = "readOnly")]
        public bool ReadOnly { get; set; }
    }

    public class FieldCollection
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<Field> Fields { get; set; }
    }

    [DebuggerDisplay("{Field.ReferenceName}")]
    public class FieldInstance
    {
        [JsonProperty(PropertyName = "field")]
        public Field Field { get; set; }

        [JsonProperty(PropertyName = "helpText")]
        public string HelpText { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class WorkItemType
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "xmlForm")]
        public string Form { get; set; }

        [JsonProperty(PropertyName = "fieldInstances")]
        public List<FieldInstance> Fields { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class WorkItemTypeCollection
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<WorkItemType> Types { get; set; }
    }

    [DebuggerDisplay("{Name}")]
    public class WorkItemTypeCategory
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "referenceName")]
        public string ReferenceName { get; set; }

        [JsonProperty(PropertyName = "defaultWorkItemType")]
        public WorkItemType DefaultWorkItemType { get; set; }

        [JsonProperty(PropertyName = "workItemTypes")]
        public List<WorkItemType> workItemTypes { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class WorkItemTypeCategoryCollection
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<WorkItemTypeCategory> Categories { get; set; }
    }
}
