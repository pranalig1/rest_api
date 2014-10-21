using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V1;

namespace VisualStudioOnline.Api.Rest.Test.V1
{
    [TestClass]
    public class ProjectRestClientTest
    {
        private ProjectRestClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new ProjectRestClient(Settings.Default.AccountName,
                new NetworkCredential(Settings.Default.UserName, Settings.Default.Password));
        }

        [TestMethod]
        public void TestGetTeams()
        {
            var teams = _client.GetProjectTeams(Settings.Default.ProjectName).Result;

            var team = _client.GetProjectTeam(Settings.Default.ProjectName, teams[0].Id.ToString()).Result;
            var teamMembers = _client.GetTeamMembers(Settings.Default.ProjectName, team.Name).Result;
        }
    }
}
