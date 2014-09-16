using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V2.Model;

namespace VisualStudioOnline.Api.Rest.V2
{
    public class WitRestClient : RestClient
    {
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
        public async Task<HistoryCommentCollection> GetWorkItemHistory(int workitemId, int? top = null, int? skip = null)
        {
            var arguments = new Dictionary<string, string>();
            if (top.HasValue) { arguments.Add("$top", top.Value.ToString()); }
            if (skip.HasValue) { arguments.Add("$skip", skip.Value.ToString()); }

            string response = await GetResponse(string.Format("workitems/{0}/history", workitemId), arguments);
            return JsonConvert.DeserializeObject<HistoryCommentCollection>(response);
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
        public async Task<WorkItemRelationTypeCollection> GetWorkItemRelationTypes()
        {
            string response = await GetResponse("workitemrelationtypes");
            return JsonConvert.DeserializeObject<WorkItemRelationTypeCollection>(response);
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
        public async Task<FieldCollection> GetFields()
        {
            string response = await GetResponse("fields");
            return JsonConvert.DeserializeObject<FieldCollection>(response);
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
        public async Task<WorkItemTypeCategoryCollection> GetWorkItemTypeCategories(string projectName)
        {
            string response = await GetResponse("workitemtypecategories", projectName);
            return JsonConvert.DeserializeObject<WorkItemTypeCategoryCollection>(response);
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
        public async Task<WorkItemTypeCollection> GetWorkItemTypes(string projectName)
        {
            string response = await GetResponse("workitemtypes", projectName);
            return JsonConvert.DeserializeObject<WorkItemTypeCollection>(response);
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
        public async Task<ClassificationNodeCollection> GetClassificationNodes(string projectName, int? depth = null)
        {
            string response = await GetCssNode(projectName, string.Empty, depth);
            return JsonConvert.DeserializeObject<ClassificationNodeCollection>(response);
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
