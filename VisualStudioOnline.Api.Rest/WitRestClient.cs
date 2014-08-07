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
            string response = await PostResponse("workitems", workItem);
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
            string response = await PatchResponse(string.Format("workitems/{0}", workItem.Id), workItem);
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
            string response = await PostResponse("queries", query);
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
    }
}
