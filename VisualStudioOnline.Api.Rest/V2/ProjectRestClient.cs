﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VisualStudioOnline.Api.Rest.V2.Model;

namespace VisualStudioOnline.Api.Rest.V2
{
    public class ProjectRestClient : RestClient
    {
        protected override string SubSystemName
        {
            get { return "projects"; }
        }

        public ProjectRestClient(string accountName, NetworkCredential userCredential)
            : base(string.Format(ACCOUNT_ROOT_URL, accountName), new BasicAuthenticationFilter(userCredential), "1.0-preview.2")
        {
        }

        /// <summary>
        /// Get team project list
        /// </summary>
        /// <param name="stateFilter"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<TeamProject>> GetTeamProjects(ProjectState? stateFilter = null, int? top = null, int? skip = null)
        {
            string response = await GetResponse(string.Empty,
                new Dictionary<string, object>() { { "$stateFilter", stateFilter }, { "$top", top }, { "$skip", skip } });
            return JsonConvert.DeserializeObject<JsonCollection<TeamProject>>(response);
        }

        /// <summary>
        /// Get team project by name or id
        /// </summary>
        /// <param name="projectNameOrId"></param>
        /// <param name="includecapabilities"></param>
        /// <returns></returns>
        public async Task<TeamProject> GetTeamProject(string projectNameOrId, bool? includecapabilities = null)
        {
            string response = await GetResponse(projectNameOrId, new Dictionary<string, object>() { { "includecapabilities", includecapabilities } });
            return JsonConvert.DeserializeObject<TeamProject>(response);
        }

        /// <summary>
        /// Update team project description
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<TeamProject> UpdateTeamProject(TeamProject project)
        {
            string response = await PatchResponse(project.Id.ToString(), new { description = project.Description }, null, JSON_MEDIA_TYPE);
            JsonConvert.PopulateObject(response, project);
            return project;
        }
    }
}
