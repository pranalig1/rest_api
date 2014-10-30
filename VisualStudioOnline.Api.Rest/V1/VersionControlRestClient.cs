﻿using Newtonsoft.Json;
using System;
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
        public enum OrderBy
        {
            asc,
            desc
        }

        public struct SearchCriteria
        {
            public string ItemPath;
            public string Version;
            public string VersionType;
            public string VersionOption;
            public string Author;

            public int? FromId;
            public int? ToId;

            public DateTime? FromDate;
            public DateTime? ToDate;
        }

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
                new Dictionary<string, object>() { { "name", name }, { "owner", owner }, { "itemlabelFilter", itemLabelFilter }, { "$top", top }, { "$skip", skip } });
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
            string response = await GetResponse(string.Format("labels/{0}/items", labelId), new Dictionary<string, object>() { { "$top", top }, { "$skip", skip } });
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
                new Dictionary<string, object>() { { "owner", owner }, { "maxCommentLength", maxCommentLength }, { "$top", top }, { "$skip", skip } });
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
                new Dictionary<string, object>() { { "$top", top }, { "$skip", skip } });
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

        /// <summary>
        /// Get list of changesets
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <param name="order"></param>
        /// <param name="maxCommentLength"></param>
        /// <returns></returns>
        public async Task<JsonCollection<Changeset>> GetChangesets(SearchCriteria? criteria = null, int? top = null, int? skip = null, OrderBy? order = null, int? maxCommentLength = null)
        {
            var arguments = new Dictionary<string, object>() { { "maxCommentLength", maxCommentLength }, { "$top", top }, { "$skip", skip } };
            if(criteria.HasValue) 
            {
                arguments.Add("searchCriteria.itemPath", criteria.Value.ItemPath);
                arguments.Add("searchCriteria.version", criteria.Value.Version);
                arguments.Add("searchCriteria.versionType", criteria.Value.VersionType);
                arguments.Add("searchCriteria.versionOption", criteria.Value.VersionOption);
                arguments.Add("searchCriteria.author", criteria.Value.Author);

                arguments.Add("searchCriteria.fromId", criteria.Value.FromId);
                arguments.Add("searchCriteria.toId", criteria.Value.ToId);

                arguments.Add("searchCriteria.fromDate", criteria.Value.FromDate);
                arguments.Add("searchCriteria.toDate", criteria.Value.ToDate);
            }
            if(order.HasValue) { arguments.Add("$orderby", order.Value == OrderBy.asc ? "id asc" : "id desc"); }

            string response = await GetResponse("changesets", arguments);
            return JsonConvert.DeserializeObject<JsonCollection<Changeset>>(response);
        }

        /// <summary>
        /// Get list of changesets by a list of IDs
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="maxCommentLength"></param>
        /// <returns></returns>
        public async Task<JsonCollection<Changeset>> GetChangesets(int[] ids, int? maxCommentLength = null)
        {
            string response = await PostResponse("changesetsBatch", new { changesetIds = ids, commentLength = maxCommentLength });
            return JsonConvert.DeserializeObject<JsonCollection<Changeset>>(response);
        }

        /// <summary>
        /// Get a changeset
        /// </summary>
        /// <param name="changesetId"></param>
        /// <param name="includeDetails"></param>
        /// <param name="includeWorkItems"></param>
        /// <param name="maxChangeCount"></param>
        /// <param name="maxCommentLength"></param>
        /// <returns></returns>
        public async Task<Changeset> GetChangeset(int changesetId, bool? includeDetails = null, bool? includeWorkItems = null, int? maxChangeCount = null, int? maxCommentLength = null)
        {
            string response = await GetResponse(string.Format("changesets/{0}", changesetId), 
                new Dictionary<string, object>() 
                { 
                    { "includeDetails", includeDetails }, 
                    { "includeWorkItems", includeWorkItems }, 
                    { "maxChangeCount", maxChangeCount }, 
                    { "maxCommentLength", maxCommentLength } 
                });
            return JsonConvert.DeserializeObject<Changeset>(response);
        }

        /// <summary>
        /// Get list of changes in a changeset
        /// </summary>
        /// <param name="changesetId"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<VersionControlItemChange>> GetChangesetChanges(int changesetId, int? top = null, int? skip = null)
        {
            string response = await GetResponse(string.Format("changesets/{0}/changes", changesetId),
                new Dictionary<string, object>() { { "$top", top }, { "$skip", skip } });
            return JsonConvert.DeserializeObject<JsonCollection<VersionControlItemChange>>(response);
        }

        /// <summary>
        /// Get list of associated work items
        /// </summary>
        /// <param name="changesetId"></param>
        /// <returns></returns>
        public async Task<JsonCollection<WorkItemInfo>> GetChangesetWorkItems(int changesetId)
        {
            string response = await GetResponse(string.Format("changesets/{0}/workitems", changesetId));
            return JsonConvert.DeserializeObject<JsonCollection<WorkItemInfo>>(response);
        }
    }
}
