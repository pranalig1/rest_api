using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V2.Model;

namespace VisualStudioOnline.Api.Rest.V2
{
    public class WitRestClient : RestClient
    {
        public enum RevisionExpandOptions
        {
            all,
            relations,
            none
        }

        protected override string SubSystemName
        {
            get { return "wit"; }
        }

        public WitRestClient(string accountName, NetworkCredential userCredential)
            : base(string.Format(ACCOUNT_ROOT_URL, accountName), new BasicAuthenticationFilter(userCredential), "1.0-preview.2")
        {
        }

        /// <summary>
        /// Get work item history comments
        /// </summary>
        /// <param name="workitemId"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<HistoryComment>> GetWorkItemHistory(int workitemId, int? top = null, int? skip = null)
        {
            var arguments = new Dictionary<string, string>();
            if (top.HasValue) { arguments.Add("$top", top.Value.ToString()); }
            if (skip.HasValue) { arguments.Add("$skip", skip.Value.ToString()); }

            string response = await GetResponse(string.Format("workitems/{0}/history", workitemId), arguments);
            return JsonConvert.DeserializeObject<JsonCollection<HistoryComment>>(response);
        }

        /// <summary>
        /// Get work item revision history comment
        /// </summary>
        /// <param name="workitemId"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        public async Task<HistoryComment> GetWorkItemRevisionHistory(int workitemId, int revision)
        {
            string response = await GetResponse(string.Format("workitems/{0}/history/{1}", workitemId, revision));
            return JsonConvert.DeserializeObject<HistoryComment>(response);
        }

        /// <summary>
        /// Get work item relation types
        /// </summary>
        /// <returns></returns>
        public async Task<JsonCollection<WorkItemRelationType>> GetWorkItemRelationTypes()
        {
            string response = await GetResponse("workitemrelationtypes");
            return JsonConvert.DeserializeObject<JsonCollection<WorkItemRelationType>>(response);
        }

        /// <summary>
        /// Get specific work item relation type
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public async Task<WorkItemRelationType> GetWorkItemRelationType(string relationName)
        {
            string response = await GetResponse(string.Format("workitemrelationtypes/{0}", relationName));
            return JsonConvert.DeserializeObject<WorkItemRelationType>(response);
        }

        /// <summary>
        /// Get collection fields
        /// </summary>
        /// <returns></returns>
        public async Task<JsonCollection<Field>> GetFields()
        {
            string response = await GetResponse("fields");
            return JsonConvert.DeserializeObject<JsonCollection<Field>>(response);
        }

        /// <summary>
        /// Get specific field
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public async Task<Field> GetField(string fieldName)
        {
            string response = await GetResponse(string.Format("fields/{0}", fieldName));
            return JsonConvert.DeserializeObject<Field>(response);
        }

        /// <summary>
        /// Get work item type categories for a project
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public async Task<JsonCollection<WorkItemTypeCategory>> GetWorkItemTypeCategories(string projectName)
        {
            string response = await GetResponse("workitemtypecategories", projectName);
            return JsonConvert.DeserializeObject<JsonCollection<WorkItemTypeCategory>>(response);
        }

        /// <summary>
        /// Get work item type category for a project
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public async Task<WorkItemTypeCategory> GetWorkItemTypeCategory(string projectName, string categoryName)
        {
            string response = await GetResponse(string.Format("workitemtypecategories/{0}", categoryName), projectName);
            return JsonConvert.DeserializeObject<WorkItemTypeCategory>(response);
        }

        /// <summary>
        /// Get work item types for the project
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public async Task<JsonCollection<WorkItemType>> GetWorkItemTypes(string projectName)
        {
            string response = await GetResponse("workitemtypes", projectName);
            return JsonConvert.DeserializeObject<JsonCollection<WorkItemType>>(response);
        }

        /// <summary>
        /// Get specific work item type for the project
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public async Task<WorkItemType> GetWorkItemType(string projectName, string typeName)
        {
            string response = await GetResponse(string.Format("workitemtypes/{0}", typeName), projectName);
            return JsonConvert.DeserializeObject<WorkItemType>(response);
        }


        /// <summary>
        /// Get root classification nodes (areas and iterations)
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public async Task<JsonCollection<ClassificationNode>> GetClassificationNodes(string projectName, int? depth = null)
        {
            string response = await GetCssNode(projectName, string.Empty, depth);
            return JsonConvert.DeserializeObject<JsonCollection<ClassificationNode>>(response);
        }

        /// <summary>
        /// Get root area node
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public async Task<ClassificationNode> GetAreaNode(string projectName, int? depth = null)
        {
            return await GetAreaNode(projectName, string.Empty, depth);
        }

        /// <summary>
        /// Get area node by path
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="nodePath"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public async Task<ClassificationNode> GetAreaNode(string projectName, string nodePath, int? depth = null)
        {
            string path = "areas";
            if (!string.IsNullOrEmpty(nodePath)) { path = string.Format("{0}/{1}", path, nodePath); }

            string response = await GetCssNode(projectName, path, depth);
            return JsonConvert.DeserializeObject<ClassificationNode>(response);
        }

        /// <summary>
        /// Get root iteration node
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public async Task<ClassificationNode> GetIterationNode(string projectName, int? depth = null)
        {
            return await GetIterationNode(projectName, string.Empty, depth);
        }

        /// <summary>
        /// Get iteration path by path
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="nodePath"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public async Task<ClassificationNode> GetIterationNode(string projectName, string nodePath, int? depth = null)
        {
            string path = "iterations";
            if (!string.IsNullOrEmpty(nodePath)) { path = string.Format("{0}/{1}", path, nodePath); }

            string response = await GetCssNode(projectName, path, depth);
            return JsonConvert.DeserializeObject<ClassificationNode>(response);
        }

        /// <summary>
        /// Get work item revisions
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<JsonCollection<WorkItem>> GetWorkItemRevisions(int workItemId, int? top = null, int? skip = null, RevisionExpandOptions options = RevisionExpandOptions.none)
        {
            var arguments = new Dictionary<string, string>() { {"$expand", options.ToString()} };
            if (top.HasValue) { arguments.Add("$top", top.Value.ToString()); }
            if (skip.HasValue) { arguments.Add("$skip", skip.Value.ToString()); }

            string response = await GetResponse(string.Format("workitems/{0}/revisions", workItemId), arguments);
            return JsonConvert.DeserializeObject<JsonCollection<WorkItem>>(response);
        }

        /// <summary>
        /// Get work item revision
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="revision"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<WorkItem> GetWorkItemRevision(int workItemId, int revision, RevisionExpandOptions options = RevisionExpandOptions.none)
        {
            var arguments = new Dictionary<string, string>() { { "$expand", options.ToString() } };

            string response = await GetResponse(string.Format("workitems/{0}/revisions/{1}", workItemId, revision), arguments);
            return JsonConvert.DeserializeObject<WorkItem>(response);
        }
       
        private async Task<string> GetCssNode(string projectName, string nodePath, int? depth = null)
        {
            var arguments = new Dictionary<string, string>();
            if (depth.HasValue) { arguments.Add("$depth", depth.Value.ToString()); }

            string path = "classificationnodes";
            if (!string.IsNullOrEmpty(nodePath)) { path = string.Format("{0}/{1}", path, nodePath); }

            string response = await GetResponse(path, arguments, projectName);
            return response;
        }
    }
}
