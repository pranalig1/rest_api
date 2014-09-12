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
