using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace VisualStudioOnline.Api.Rest
{
    //public class WorkItem_1
    //{
    //    private Dictionary<string, Field_1> _fields = new Dictionary<string, Field_1>();
    //    private List<Link> _links = new List<Link>();

    //    [JsonProperty(PropertyName = "fields")]
    //    public IEnumerable<Field_1> Fields
    //    {
    //        get
    //        {
    //            return _fields.Select(f => f.Value);
    //        }
    //        set
    //        { }
    //    }

    //    [JsonProperty(PropertyName = "links")]
    //    public List<Link> Links
    //    {
    //        get
    //        {
    //            return _links;
    //        }
    //        set
    //        { }
    //    }

    //    [JsonIgnore]
    //    public string Title
    //    {
    //        get
    //        {
    //            return GetFieldValue("System.Title");
    //        }
    //        set
    //        {
    //            SetField("System.Title", value);
    //        }
    //    }

    //    [JsonIgnore]
    //    public string WorkItemType
    //    {
    //        get
    //        {
    //            return GetFieldValue("System.WorkItemType");
    //        }
    //        set
    //        {
    //            SetField("System.WorkItemType", value);
    //        }
    //    }

    //    [JsonIgnore]
    //    public string State
    //    {
    //        get
    //        {
    //            return GetFieldValue("System.State");
    //        }
    //        set
    //        {
    //            SetField("System.State", value);
    //        }
    //    }

    //    [JsonIgnore]
    //    public string Reason
    //    {
    //        get
    //        {
    //            return GetFieldValue("System.Reason");
    //        }
    //        set
    //        {
    //            SetField("System.Reason", value);
    //        }
    //    }

    //    [JsonIgnore]
    //    public string AreaPath
    //    {
    //        get
    //        {
    //            return GetFieldValue("System.AreaPath");
    //        }
    //        set
    //        {
    //            SetField("System.AreaPath", value);
    //        }
    //    }

    //    [JsonIgnore]
    //    public string IterationPath
    //    {
    //        get
    //        {
    //            return GetFieldValue("System.IterationPath");
    //        }
    //        set
    //        {
    //            SetField("System.IterationPath", value);
    //        }
    //    }

    //    public WorkItem_1()
    //    {
    //    }

    //    public WorkItem_1(string type, string title)
    //    {
    //        WorkItemType = type;
    //        Title = title;
    //    }


    //    public string GetFieldValue(string name)
    //    {
    //        return GetField(name) != null ? GetField(name).Value : null;
    //    }
    //}

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

    public class FieldMetadata
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "refName")]
        public string ReferenceName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class Field
    {
        [JsonProperty(PropertyName = "field")]
        public FieldMetadata Metadata { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }

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
