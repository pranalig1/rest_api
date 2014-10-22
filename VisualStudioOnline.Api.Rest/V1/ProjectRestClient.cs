using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V1.Model;

namespace VisualStudioOnline.Api.Rest.V1
{
    public class ProjectRestClient : RestClientV1
    {
        protected override string SubSystemName
        {
            get { return "projects"; }
        }

        public ProjectRestClient(string accountName, NetworkCredential userCredential, string collectionName = DEFAULT_COLLECTION)
            : base(string.Format(ACCOUNT_ROOT_URL, accountName, collectionName), new BasicAuthenticationFilter(userCredential))
        {
        }

        /// <summary>
        /// Get all teams within the project that the authenticated user has access to.
        /// </summary>
        /// <param name="projectNameOrId"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<ProjectTeam>> GetProjectTeams(string projectNameOrId, int? top = null, int? skip = null)
        {
            string response = await GetResponse(string.Format("{0}/teams", projectNameOrId), new Dictionary<string, object>() { { "$top", top }, { "$skip", skip } });
            return JsonConvert.DeserializeObject<JsonCollection<ProjectTeam>>(response);
        }

        /// <summary>
        /// Get team by name or id
        /// </summary>
        /// <param name="projectNameOrId"></param>
        /// <param name="teamNameOrId"></param>
        /// <returns></returns>
        public async Task<ProjectTeam> GetProjectTeam(string projectNameOrId, string teamNameOrId)
        {
            string response = await GetResponse(string.Format("{0}/teams/{1}", projectNameOrId, teamNameOrId));
            return JsonConvert.DeserializeObject<ProjectTeam>(response);
        }

        /// <summary>
        /// Get a list of identity references for the team's members.
        /// </summary>
        /// <param name="projectNameOrId"></param>
        /// <param name="teamNameOrId"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<UserIdentity>> GetTeamMembers(string projectNameOrId, string teamNameOrId, int? top = null, int? skip = null)
        {
            string response = await GetResponse(string.Format("{0}/teams/{1}/members", projectNameOrId, teamNameOrId),
                new Dictionary<string, object>() { { "$top", top }, { "$skip", skip } });
            return JsonConvert.DeserializeObject<JsonCollection<UserIdentity>>(response);
        }
    }
}
