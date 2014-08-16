﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace VisualStudioOnline.Api.Rest
{
    public enum LinkUpdateType
    {
        add,
        delete,
        update
    }

    [DebuggerDisplay("{Id}")]
    public class WorkItemReference
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "webUrl")]
        public string WebUrl { get; set; }
    }

    [DebuggerDisplay("{Type}")]
    public class ResourceLink
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

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "linkType")]
        public WorkItemReference source { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
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

        [JsonProperty(PropertyName = "source")]
        public WorkItem Source { get; set; }

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
            ResourceLinks = new List<ResourceLink>();
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

    public class WorkItemCollection
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public List<WorkItem> WorkItems { get; set; }
    }
}
