using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace VisualStudioOnline.Api.Rest.VPreview1.Model
{
    public enum LinkUpdateType
    {
        add,
        delete,
        update
    }

    [DebuggerDisplay("{Id}")]
    public class WorkItemReference : ObjectWithId<int>
    {
        [JsonProperty(PropertyName = "webUrl")]
        public string WebUrl { get; set; }
    }

    [DebuggerDisplay("{Location}")]
    public class ResourceLink : BaseObject
    {
        [JsonProperty(PropertyName = "resourceId")]
        public int ResourceId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get; set; }

        [JsonProperty(PropertyName = "lastModifiedDate")]
        public string LastModifiedDate { get; set; }

        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        [JsonProperty(PropertyName = "source")]
        public WorkItemReference Source { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "updateType")]
        public LinkUpdateType UpdateType { get; set; }

        public ResourceLink()
        {
            Type = "attachment";
        }
    }

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

        [JsonProperty(PropertyName = "targetWorkItemId")]
        public int TargetId { get; set; }

        [JsonProperty(PropertyName = "source")]
        public WorkItem Source { get; set; }

        [JsonProperty(PropertyName = "sourceWorkItemId")]
        public int SourceId { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "updateType")]
        public LinkUpdateType UpdateType { get; set; }

        public Link()
        {
            UpdateType = LinkUpdateType.add;
        }
    }

    [DebuggerDisplay("{ReferenceName}")]
    public class FieldMetadata
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

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
    public class WorkItem : ObjectWithId<int>
    {
        public WorkItem()
        {
            Fields = new List<Field>();
            Links = new List<Link>();
            ResourceLinks = new List<ResourceLink>();
        }

        [JsonProperty(PropertyName = "rev")]
        public int Rev { get; set; }

        [JsonProperty(PropertyName = "webUrl")]
        public string WebUrl { get; set; }

        [JsonProperty(PropertyName = "updatesUrl")]
        public string UpdatesUrl { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public List<Field> Fields { get; set; }

        [JsonProperty(PropertyName = "links")]
        public List<Link> Links { get; set; }

        [JsonProperty(PropertyName = "resourceLinks")]
        public List<ResourceLink> ResourceLinks { get; set; }

        [JsonIgnore]
        public string this[string refName]
        {
            get
            {
                return GetFieldValue(refName);
            }
            set
            {
                SetFieldValue(refName, value);
            }
        }

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

    [DebuggerDisplay("{Field.ReferenceName}: {OriginalValue} -> {UpdatedValue}")]
    public class FieldUpdate
    {
        [JsonProperty(PropertyName = "field")]
        public FieldMetadata Field { get; set; }

        [JsonProperty(PropertyName = "originalValue")]
        public object OriginalValue { get; set; }

        [JsonProperty(PropertyName = "updatedValue")]
        public object UpdatedValue { get; set; }
    }

    [DebuggerDisplay("{Id} Rev:{Rev}")]
    public class WorkItemUpdate : ObjectWithId<int>
    {
        [JsonProperty(PropertyName = "revisionUrl")]
        public string RevisionUrl { get; set; }

        [JsonProperty(PropertyName = "rev")]
        public int Rev { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public List<FieldUpdate> FieldUpdates { get; set; }

        [JsonProperty(PropertyName = "linkUpdates")]
        public List<Link> LinkUpdates { get; set; }

        [JsonProperty(PropertyName = "resourceLinkUpdates")]
        public List<ResourceLink> ResourceLinkUpdates { get; set; }
    }
}
