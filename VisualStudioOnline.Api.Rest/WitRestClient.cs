using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace VisualStudioOnline.Api.Rest
{
    /// <summary>
    /// WIT REST API client
    /// </summary>
    public class WitRestClient : RestClient
    {
        public enum QueryExpandOptions
        { 
            none, 
            all, 
            wiql, 
            columns, 
            sortOptions 
        }

        public enum WorkItemExpandOptions
        {
            all,
            links,
            resourceLinks,
            none
        }

        protected override string SubSystemName
        {
            get { return "wit"; }
        }

        public WitRestClient(string accountName, NetworkCredential userCredential) : base(accountName, userCredential)
        {
        }

        /// <summary>
        /// Create new work item
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public async Task<WorkItem> CreateWorkItem(WorkItem workItem)
        {
            string response = await PostResponse("workitems", new Dictionary<string, string>(), workItem);
            JsonConvert.PopulateObject(response, workItem);
            return workItem;
        }

        /// <summary>
        /// Get work item by id
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="includeLinks"></param>
        /// <returns></returns>
        public async Task<WorkItem> GetWorkItem(int workItemId, WorkItemExpandOptions options = WorkItemExpandOptions.none)
        {
            var arguments = new Dictionary<string, string>() { { "$expand", options.ToString() } };

            string response = await GetResponse(string.Format("workitems/{0}", workItemId), arguments);
            return JsonConvert.DeserializeObject<WorkItem>(response);
        }

        /// <summary>
        /// Get a list of work items by ids
        /// </summary>
        /// <param name="workItemIds"></param>
        /// <param name="includeLinks"></param>
        /// <param name="asOfDate"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<WorkItemCollection> GetWorkItems(int[] workItemIds, WorkItemExpandOptions options = WorkItemExpandOptions.none, DateTime? asOfDate = null, string[] fields = null)
        {
            var arguments = new Dictionary<string, string>() { { "ids", string.Join(",", workItemIds) }, { "$expand", options.ToString() } };
            if (asOfDate.HasValue) { arguments.Add("asof", asOfDate.Value.ToUniversalTime().ToString("u")); }
            if (fields != null) { arguments.Add("fields", string.Join(",", fields)); }

            string response = await GetResponse("workitems", arguments);
            return JsonConvert.DeserializeObject<WorkItemCollection>(response);
        }

        /// <summary>
        /// Update existing work item
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public async Task<WorkItem> UpdateWorkItem(WorkItem workItem)
        {
            workItem.Links.ForEach(l => 
                {
                    l.SourceId = l.Source.Id;
                    l.TargetId = l.Target.Id;
                    l.Source = null;
                    l.Target = null;
                });

            string response = await PatchResponse(string.Format("workitems/{0}", workItem.Id), workItem);
            
            workItem.Links.Clear();
            workItem.ResourceLinks.Clear();
            JsonConvert.PopulateObject(response, workItem);
            
            return workItem;
        }

        /// <summary>
        /// Get a list of queries and query folders
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="depth"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<QueryCollection> GetQueries(string projectName, int? depth = null, QueryExpandOptions options = QueryExpandOptions.none)
        {
            var arguments = new Dictionary<string, string>() { { "project", projectName }, { "$expand", options.ToString() } };
            if (depth.HasValue) { arguments.Add("$depth", depth.Value.ToString()); }

            string response = await GetResponse("queries", arguments);
            return JsonConvert.DeserializeObject<QueryCollection>(response);
        }

        /// <summary>
        /// Get query / query folder by id
        /// </summary>
        /// <param name="queryId"></param>
        /// <param name="depth"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<Query> GetQuery(string queryId, int? depth = null, QueryExpandOptions options = QueryExpandOptions.none)
        {
            var arguments = new Dictionary<string, string>() { { "$expand", options.ToString() } };
            if (depth.HasValue) { arguments.Add("$depth", depth.Value.ToString()); }

            string response = await GetResponse(string.Format("queries/{0}", queryId), arguments);
            return JsonConvert.DeserializeObject<Query>(response);
        }

        /// <summary>
        /// Create new query / query folder
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<Query> CreateQuery(Query query)
        {
            string response = await PostResponse("queries", new Dictionary<string, string>(), query);
            JsonConvert.PopulateObject(response, query);
            return query;
        }

        /// <summary>
        /// Update existing query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<Query> UpdateQuery(Query query)
        {
            string response = await PatchResponse(string.Format("queries/{0}", query.Id), query);
            JsonConvert.PopulateObject(response, query);
            return query;
        }

        /// <summary>
        /// Delete a query / query folder
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<string> DeleteQuery(Query query)
        {
            return await DeleteResponse(string.Format("queries/{0}", query.Id));
        }

        /// <summary>
        /// Download work item attachment
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<string> DownloadAttachment(string attachmentId)
        {
            return await GetResponse(string.Format("attachments/{0}", attachmentId), new Dictionary<string, string>());            
        }

        /// <summary>
        /// Upload binary attachment
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="area"></param>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<ResourceLink> UploadAttachment(string projectName, string area, string fileName, byte[] content)
        {
            return await UploadAttachment(projectName, area, fileName, Convert.ToBase64String(content));
        }

        /// <summary>
        /// Upload text attachment
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="area"></param>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<ResourceLink> UploadAttachment(string projectName, string area, string fileName, string content)
        {
            var arguments = new Dictionary<string, string>() 
            { 
                { "project", projectName },
                { "area", area },
                { "fileName", fileName }
            };

            string response = await PostResponse("attachments", arguments, content);
            return JsonConvert.DeserializeObject<ResourceLink>(response);
        }
    }
}
