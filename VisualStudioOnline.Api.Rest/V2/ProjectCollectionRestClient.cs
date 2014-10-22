using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V2.Model;

namespace VisualStudioOnline.Api.Rest.V2
{
    public class ProjectCollectionRestClient : RestClientV2
    {
        protected override string SubSystemName
        {
            get { return "projectcollections"; }
        }

        public ProjectCollectionRestClient(string accountName, NetworkCredential userCredential, string collectionName = DEFAULT_COLLECTION)
            : base(string.Format(ACCOUNT_ROOT_URL, accountName, collectionName), new BasicAuthenticationFilter(userCredential))
        {
        }

        /// <summary>
        /// Get team project collection list
        /// </summary>
        /// <param name="stateFilter"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<TeamProjectCollection>> GetProjectCollections(int? top = null, int? skip = null)
        {
            string response = await GetResponse(string.Empty,
                new Dictionary<string, object>() { { "$top", top }, { "$skip", skip } });
            return JsonConvert.DeserializeObject<JsonCollection<TeamProjectCollection>>(response);
        }

        /// <summary>
        /// Get team project collection by name
        /// </summary>
        /// <param name="projectNameOrId"></param>
        /// <param name="includecapabilities"></param>
        /// <returns></returns>
        public async Task<TeamProjectCollection> GetProjectCollection(string projectCollectionName)
        {
            string response = await GetResponse(projectCollectionName);
            return JsonConvert.DeserializeObject<TeamProjectCollection>(response);
        }
     }

}
