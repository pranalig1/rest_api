using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V1.Model;

namespace VisualStudioOnline.Api.Rest.V1.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class BuildRestClient : RestClientVersion1
    {
        protected override string SubSystemName
        {
            get { return "build"; }
        }

        public BuildRestClient(string accountName, NetworkCredential userCredential, string collectionName = DEFAULT_COLLECTION)
            : base(string.Format(ACCOUNT_ROOT_URL, accountName, collectionName), new BasicAuthenticationFilter(userCredential))
        {
        }

        /// <summary>
        /// Get a list of build definitions
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public async Task<JsonCollection<BuildDefinition>> GetBuildDefinitions(string projectName)
        {
            string response = await GetResponse("definitions", projectName);
            return JsonConvert.DeserializeObject<JsonCollection<BuildDefinition>>(response);
        }

        /// <summary>
        /// Get a build definition
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="definitionId"></param>
        /// <returns></returns>
        public async Task<BuildDefinition> GetBuildDefinition(string projectName, int definitionId)
        {
            string response = await GetResponse(string.Format("definitions/{0}", definitionId), projectName);
            return JsonConvert.DeserializeObject<BuildDefinition>(response);
        }

        /// <summary>
        /// Get a list of qualities
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public async Task<JsonCollection<string>> GetBuildQualities(string projectName)
        {
            string response = await GetResponse("qualities", projectName);
            return JsonConvert.DeserializeObject<JsonCollection<string>>(response);
        }

        /// <summary>
        /// Add a quality
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public async Task<string> AddBuildQuality(string projectName, string quality)
        {
            string response = await PutResponse(string.Format("qualities/{0}", quality), content: null, projectName: projectName);
            return response;
        }

        /// <summary>
        /// Remove a quality
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public async Task<string> DeleteBuildQuality(string projectName, string quality)
        {
            string response = await DeleteResponse(string.Format("qualities/{0}", quality), projectName);
            return response;
        }
    }
}
