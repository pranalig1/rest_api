using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V1.Model;

namespace VisualStudioOnline.Api.Rest.V1.Client
{
    public interface IVsoGit
    {
        Task<JsonCollection<Repository>> GetRepositories();

        Task<Repository> GetRepository(string id);

        Task<Repository> CreateRepository(string name, string projectId);

        Task<Repository> RenameRepository(string id, string newName);

        Task<string> DeleteRepository(string id);

        Task<JsonCollection<GitReference>> GetRefs(string repoId, string filter = null);
    }

    public class GitRestClient : RestClientVersion1, IVsoGit
    {
        protected override string SubSystemName
        {
            get { return "git"; }
        }

        public GitRestClient(string url, NetworkCredential userCredential)
            : base(url, new BasicAuthenticationFilter(userCredential))
        {
        }

        /// <summary>
        /// Get a list of repositories
        /// </summary>
        /// <returns></returns>
        public async Task<JsonCollection<Repository>> GetRepositories()
        {
            string response = await GetResponse("repositories");
            return JsonConvert.DeserializeObject<JsonCollection<Repository>>(response);
        }

        /// <summary>
        /// Get a repository
        /// </summary>
        /// <param name="repoId"></param>
        /// <returns></returns>
        public async Task<Repository> GetRepository(string repoId)
        {
            string response = await GetResponse(string.Format("repositories/{0}", repoId));
            return JsonConvert.DeserializeObject<Repository>(response);
        }

        /// <summary>
        /// Create a repository
        /// </summary>
        /// <param name="name"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<Repository> CreateRepository(string name, string projectId)
        {
            string response = await PostResponse("repositories", new { name = name, project = new { id = projectId } });
            return JsonConvert.DeserializeObject<Repository>(response);
        }

        /// <summary>
        /// Rename a repository
        /// </summary>
        /// <param name="repoId"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public async Task<Repository> RenameRepository(string repoId, string newName)
        {
            string response = await PatchResponse(string.Format("repositories/{0}", repoId), new { name = newName }, null, JSON_MEDIA_TYPE);
            return JsonConvert.DeserializeObject<Repository>(response);
        }

        /// <summary>
        /// Delete a repository
        /// </summary>
        /// <param name="repoId"></param>
        /// <returns></returns>
        public async Task<string> DeleteRepository(string repoId)
        {
            string response = await DeleteResponse(string.Format("repositories/{0}", repoId));
            return response;
        }

        /// <summary>
        /// Get a list of references
        /// </summary>
        /// <param name="repoId"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<JsonCollection<GitReference>> GetRefs(string repoId, string filter = null)
        {
            string response = await GetResponse(string.IsNullOrEmpty(filter) ? 
                string.Format("repositories/{0}/refs", repoId) : string.Format("repositories/{0}/refs/{1}", repoId, filter));
            return JsonConvert.DeserializeObject<JsonCollection<GitReference>>(response);
        }
    }
}
