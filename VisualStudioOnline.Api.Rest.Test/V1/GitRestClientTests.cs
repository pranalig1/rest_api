using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisualStudioOnline.Api.Rest.Test.Properties;
using VisualStudioOnline.Api.Rest.V1.Client;

namespace VisualStudioOnline.Api.Rest.Test.V1
{
    [TestClass]
    public class GitRestClientTests : VsoTestBase
    {
        private IVsoGit _client;

        protected override void OnInitialize(VsoClient vsoClient)
        {
            _client = vsoClient.GetService<IVsoGit>();
        }

        [TestMethod]
        public void TestRepositories()
        {
            var repos = _client.GetRepositories().Result;
            var repo = _client.GetRepository(repos.Items[0].Id).Result;

            var newRepo = _client.CreateRepository("MyRepo", Settings.Default.ProjectId).Result;
            newRepo = _client.RenameRepository(newRepo.Id, "MyRepoRenamed").Result;
            string result = _client.DeleteRepository(newRepo.Id).Result;
        }
    }
}
