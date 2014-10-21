using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V2.Model;

namespace VisualStudioOnline.Api.Rest.V2
{
    public class ProjectCollectionRestClient : RestClient
    {
        protected override string SubSystemName
        {
            get { return "projectcollections"; }
        }

        public ProjectCollectionRestClient(string accountName, NetworkCredential userCredential)
            : base(string.Format(ACCOUNT_ROOT_URL, accountName), new BasicAuthenticationFilter(userCredential), "1.0-preview.2")
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
