using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V1.Model;

namespace VisualStudioOnline.Api.Rest.V1
{
    /// <summary>
    /// TFS Version Control REST API client
    /// </summary>
    public class VersionControlRestClient : RestClientV1
    {
        protected override string SubSystemName
        {
            get
            {
                return "tfvc";
            }
        }

        public VersionControlRestClient(string accountName, NetworkCredential userCredential, string collectionName = DEFAULT_COLLECTION)
            : base(string.Format(ACCOUNT_ROOT_URL, accountName, collectionName), new BasicAuthenticationFilter(userCredential))
        {
        }


        /// <summary>
        /// Get a list of root branches
        /// </summary>
        /// <param name="includeChildren"></param>
        /// <param name="includeParent"></param>
        /// <param name="includeDeleted"></param>
        /// <returns></returns>
        public async Task<JsonCollection<Branch>> GetRootBranches(bool? includeChildren = null, bool? includeDeleted = null)
        {
            string response = await GetResponse("branches", new Dictionary<string, object>() { { "includeChildren", includeChildren }, { "includeDeleted", includeDeleted } });
            return JsonConvert.DeserializeObject<JsonCollection<Branch>>(response);
        }

        /// <summary>
        /// Get a branch
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeChildren"></param>
        /// <param name="includeParent"></param>
        /// <param name="includeDeleted"></param>
        /// <returns></returns>
        public async Task<Branch> GetBranch(string path, bool? includeChildren = null, bool? includeParent = null, bool? includeDeleted = null)
        {
            string response = await GetResponse(string.Format("branches/{0}", path), 
                new Dictionary<string, object>() { { "includeChildren", includeChildren }, { "includeParent", includeParent }, { "includeDeleted", includeDeleted } });
            return JsonConvert.DeserializeObject<Branch>(response);
        }

        /// <summary>
        /// Get list of labels
        /// </summary>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        /// <param name="itemLabelFilter"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<Label>> GetLabels(string name = null, string owner = null, string itemLabelFilter = null, int? top = null, int? skip = null)
        {
            string response = await GetResponse("labels",
                new Dictionary<string, object>() { { "name", name }, { "owner", owner }, { "itemlabelFilter", itemLabelFilter }, { "top", top }, { "skip", skip } });
            return JsonConvert.DeserializeObject<JsonCollection<Label>>(response);
        }

        /// <summary>
        /// Get label by id
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="maxItemCount"></param>
        /// <returns></returns>
        public async Task<Label> GetLabel(string labelId, int? maxItemCount = null)
        {
            string response = await GetResponse(string.Format("labels/{0}", labelId), new Dictionary<string, object>() { { "maxItemCount", maxItemCount } });
            return JsonConvert.DeserializeObject<Label>(response);
        }

        /// <summary>
        /// Get list of labelled items
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<VersionControlItem>> GetLabelledItems(string labelId, int? top = null, int? skip = null)
        {
            string response = await GetResponse(string.Format("labels/{0}/items", labelId), new Dictionary<string, object>() { { "top", top }, { "skip", skip } });
            return JsonConvert.DeserializeObject<JsonCollection<VersionControlItem>>(response);
        }


        /// <summary>
        /// Get list of shelvesets
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="maxCommentLength"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<Shelveset>> GetShelvesets(string owner = null, string maxCommentLength = null, int? top = null, int? skip = null)
        {
            string response = await GetResponse("shelvesets",
                new Dictionary<string, object>() { { "owner", owner }, { "maxCommentLength", maxCommentLength }, { "top", top }, { "skip", skip } });
            return JsonConvert.DeserializeObject<JsonCollection<Shelveset>>(response);
        }

        /// <summary>
        /// Get a shelveset by id
        /// </summary>
        /// <param name="shelvesetId"></param>
        /// <param name="includeDetails"></param>
        /// <param name="includeWorkItems"></param>
        /// <param name="maxChangeCount"></param>
        /// <param name="maxCommentLength"></param>
        /// <returns></returns>
        public async Task<Shelveset> GetShelveset(string shelvesetId, bool? includeDetails = null, bool? includeWorkItems = null, int? maxChangeCount = null, string maxCommentLength = null)
        {
            string response = await GetResponse(string.Format("shelvesets/{0}", shelvesetId),
                new Dictionary<string, object>() { { "includeDetails", includeDetails }, { "includeWorkItems", includeWorkItems }, { "maxChangeCount", maxChangeCount }, { "maxCommentLength", maxCommentLength } });
            return JsonConvert.DeserializeObject<Shelveset>(response);
        }

        /// <summary>
        /// Get shelveset changes
        /// </summary>
        /// <param name="shelvesetId"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<VersionControlItemChange>> GetShelvesetChanges(string shelvesetId, int? top = null, int? skip = null)
        {
            string response = await GetResponse(string.Format("shelvesets/{0}/changes", shelvesetId),
                new Dictionary<string, object>() { { "top", top }, { "skip", skip } });
            return JsonConvert.DeserializeObject<JsonCollection<VersionControlItemChange>>(response);
        }

        /// <summary>
        /// Get shelveset work items
        /// </summary>
        /// <param name="shelvesetId"></param>
        /// <returns></returns>
        public async Task<JsonCollection<WorkItemInfo>> GetShelvesetWorkItems(string shelvesetId)
        {
            string response = await GetResponse(string.Format("shelvesets/{0}/workitems", shelvesetId));
            return JsonConvert.DeserializeObject<JsonCollection<WorkItemInfo>>(response);
        }
    }
}
