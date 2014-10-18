using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V2;

namespace VisualStudioOnline.Api.Rest.Test.V2
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
        public void TestGetProjects()
        {
            var projects = _client.GetTeamProjects().Result;
            
            var project = _client.GetTeamProject(projects[0].Id.ToString(), true).Result;
            project.Description = DateTime.Now.Ticks.ToString();

            project = _client.UpdateTeamProject(project).Result;
        }
    }
}
