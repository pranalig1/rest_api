﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V1.Model;

namespace VisualStudioOnline.Api.Rest.V1
{
    /// <summary>
    /// Tagging REST API client
    /// </summary>
    public class TagRestClient : RestClient
    {
        protected override string SubSystemName
        {
            get 
            {
                return "tagging";
            }
        }

        public TagRestClient(string accountName, NetworkCredential userCredential, string collectionName = DEFAULT_COLLECTION)
            : base(string.Format(ACCOUNT_ROOT_URL, accountName, collectionName), new BasicAuthenticationFilter(userCredential), "1.0-preview.1")
        {
        }

        /// <summary>
        /// Get tag list
        /// </summary>
        /// <param name="scopeId">e.g. project id</param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public async Task<TagCollection> GetTagList(string scopeId, bool includeInactive = false)
        {
            string response = await GetResponse(string.Format("scopes/{0}/tags", scopeId), new Dictionary<string, object>() { { "includeInactive", includeInactive } });
            return JsonConvert.DeserializeObject<TagCollection>(response);
        }

        /// <summary>
        /// Get tag by name or id
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Tag> GetTag(string scopeId, string nameOrId)
        {
            string response = await GetResponse(string.Format("scopes/{0}/tags/{1}", scopeId, nameOrId));
            return JsonConvert.DeserializeObject<Tag>(response);
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Tag> CreateTag(string scopeId, string name)
        {
            string response = await PostResponse(string.Format("scopes/{0}/tags", scopeId), new Dictionary<string, object>(), new Tag() { Name = name });
            return JsonConvert.DeserializeObject<Tag>(response);
        }

        /// <summary>
        /// Update existing tag
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<Tag> UpdateTag(string scopeId, Tag tag)
        {
            string response = await PatchResponse(string.Format("scopes/{0}/tags/{1}", scopeId, tag.Id), tag, null, JSON_MEDIA_TYPE);
            JsonConvert.PopulateObject(response, tag);
            return tag;
        }

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<string> DeleteTag(string scopeId, Tag tag)
        {
            return await DeleteResponse(string.Format("scopes/{0}/tags/{1}", scopeId, tag.Id));
        }

    }
}
